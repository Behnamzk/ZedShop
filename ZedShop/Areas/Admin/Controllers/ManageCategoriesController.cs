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

    }
}
