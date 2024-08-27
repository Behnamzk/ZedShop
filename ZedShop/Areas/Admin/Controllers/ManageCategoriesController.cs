using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ZedShop.Core.Convertors;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.CategoryViewModel;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteOwner")] //Only Admin and SiteOwner
    public class ManageCategoriesController : Controller
    {
        private readonly IProductService _productService;

        private int pageCount, currentPage, allCategoryCount, numberPerPage, filterId;

        private readonly List<FilterBaseViewModel> filtersBase = new List<FilterBaseViewModel>();

        private readonly MapperConfiguration productAEMapperConfig;

        private IMapper productAEMapper;

        public ManageCategoriesController(IProductService productService)
        {
            _productService = productService;

            filtersBase.Add(new FilterBaseViewModel(-1, "همه دسته‌بندی‌ها"));
            filtersBase.Add(new FilterBaseViewModel(0, "دسته‌های والد"));


            productAEMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapping>());
            productAEMapper = productAEMapperConfig.CreateMapper();

            // paging initialization
            numberPerPage = 5;
            currentPage = 1;
            filterId = -1;
            allCategoryCount = _productService.GetAllCategoriesCount(filterId);
            pageCount = (int)Math.Ceiling((double)allCategoryCount / numberPerPage);
        }

        public IActionResult Index()
        {
            filterId = -1;
            List<CategoryViewModelAdmin> categoriesViews = GetCategories(filterId);


            currentPage = 1;

            ViewBag.Filters = filtersBase;
            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return View(categoriesViews);
        }

        private List<CategoryViewModelAdmin> GetCategories(int _filterId)
        {
            this.filterId = _filterId;
            var Categories = _productService.GetAllCategoriesPaged(currentPage, numberPerPage, filterId);

            List<CategoryViewModelAdmin> categoriesViews = new List<CategoryViewModelAdmin>();

            foreach (var category in Categories)
            {

                CategoryViewModelAdmin categoryVM = new CategoryViewModelAdmin()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsRoot = category.IsRoot,
                    Parent = category.Parent
                };
                categoriesViews.Add(categoryVM);
            }

            allCategoryCount = _productService.GetAllCategoriesCount(filterId);
            pageCount = (int)Math.Ceiling((double)allCategoryCount / numberPerPage);

            return categoriesViews;
        }

        [HttpGet]
        public IActionResult AllCategoriesOfFilter(int _filterId)
        {
            this.filterId = _filterId;
            List<CategoryViewModelAdmin> categoriesViews = GetCategories(filterId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_CategoriesTable", categoriesViews);
        }

        [HttpGet]
        public IActionResult ChangePage(int _filterId, int pageNumber = 1)
        {
            this.filterId = _filterId;
            currentPage = pageNumber;
            List<CategoryViewModelAdmin> categoriesViews = GetCategories(filterId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_CategoriesTable", categoriesViews);
        }

        [CheckAccess("DeleteCategory")]
        [Route("/Admin/ManageCategories/DeleteCategory")]
        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            bool resault = _productService.DeleteCategory(categoryId);

            return Json(new { success = resault });
        }

        [CheckAccess("AddCategory")]
        [Route("/Admin/ManageCategories/AddCategory")]
        [HttpGet]
        public ActionResult AddCategory()
        {
            CategoryAddEditViewModel categoryAddEdit = new CategoryAddEditViewModel();

            var categories = _productService.GetAllCategory();

            foreach (var category in categories)
            {
                CategoryViewModelTemp categoryTemp = new CategoryViewModelTemp()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsActive = false
                };
                categoryAddEdit.ParentCategory.Add(categoryTemp);
            }

            ViewBag.EditMode = false;
            ViewBag.ActionName = "AddCategory";

            return View("AddEditCategory", categoryAddEdit);
        }

        [CheckAccess("AddCategory")]
        [Route("/Admin/ManageCategories/AddCategory")]
        [HttpPost]
        public ActionResult AddCategory(CategoryAddEditViewModel categoryView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = false;
                ViewBag.ActionName = "AddCategory";
                return View("AddEditCategory", categoryView);
            }

            try
            {
                if (_productService.IsCategoryNameExist(categoryView.Name, categoryView.Id))
                {
                    ModelState.AddModelError("Name", "دسته دیگری با این نام وجود دارد!");

                    ViewBag.EditMode = false;
                    ViewBag.ActionName = "AddCategory";
                    return View("AddEditCategory", categoryView);
                }

                Category category = new Category()
                {
                    Name = categoryView.Name,
                    IsRoot = categoryView.IsRoot
                };

                foreach (var item in categoryView.ParentCategory)
                {
                    if (item.IsActive)
                    {
                        category.ParentId = item.Id;
                        break;
                    }
                }

                _productService.AddCategory(category);

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "اطلاعات به درستی وارد نشده است!");
            }


            ViewBag.EditMode = false;
            ViewBag.ActionName = "AddCategory";
            return View("AddEditCategory", categoryView);
        }

        [CheckAccess("EditCategory")]
        [Route("/Admin/ManageCategories/EditCategory/{categoryId}")]
        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            CategoryAddEditViewModel categoryAddEdit = new CategoryAddEditViewModel();

            var categories = _productService.GetAllCategory();

            var category = _productService.GetCategory(categoryId);

            if (category != null)
            {
                categoryAddEdit.Name = category.Name;
                categoryAddEdit.IsRoot = category.IsRoot;
                categoryAddEdit.Id = category.Id;

                if(category.ParentId == null)
                {
                    category.ParentId = -1;
                }

                foreach (var item in categories)
                {
                    if(item.Id != category.Id)
                    {
                        CategoryViewModelTemp categoryTemp = new CategoryViewModelTemp()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            IsActive = false
                        };

                        if (item.Id == category.ParentId)
                        {
                            categoryTemp.IsActive = true;
                        }

                        categoryAddEdit.ParentCategory.Add(categoryTemp);
                    }
                  
                }

                ViewBag.EditMode = true;
                ViewBag.ActionName = "EditCategory";

                return View("AddEditCategory", categoryAddEdit);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [CheckAccess("EditCategory")]
        [Route("/Admin/ManageCategories/EditCategory/{categoryId}")]
        [HttpPost]
        public ActionResult EditCategory(CategoryAddEditViewModel categoryView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.ActionName = "EditCategory";
                return View("AddEditCategory", categoryView);
            }

            try
            {
                if(_productService.IsCategoryNameExist(categoryView.Name, categoryView.Id))
                {
                    ModelState.AddModelError("Name", "دسته دیگری با این نام وجود دارد!");

                    ViewBag.EditMode = true;
                    ViewBag.ActionName = "EditCategory";
                    return View("AddEditCategory", categoryView);
                }

                Category category = new Category()
                {
                    Id = categoryView.Id,
                    Name = categoryView.Name,
                    IsRoot = categoryView.IsRoot
                };

                foreach (var item in categoryView.ParentCategory)
                {
                    if (item.IsActive)
                    {
                        category.ParentId = item.Id;
                        break;
                    }
                }

                _productService.UpdateCategory(category);

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "اطلاعات به درستی وارد نشده است!");
            }


            ViewBag.EditMode = true;
            ViewBag.ActionName = "EditCategory";
            return View("AddEditCategory", categoryView);
        }


    }
}
