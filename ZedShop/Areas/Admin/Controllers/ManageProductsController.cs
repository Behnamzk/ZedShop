using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Globalization;
using ZedShop.Core.Convertors;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.ProductViewModel;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteOwner")] //Only admin and SiteOwner

    public class ManageProductsController : Controller
    {
        private readonly IProductService _productService;

        private int pageCount, currentPage, allProductCount, numberPerPage, filterId;

        private readonly List<FilterBaseViewModel> filtersBase = new List<FilterBaseViewModel>();

        private readonly PersianCalendar pc;

        private readonly MapperConfiguration productAEMapperConfig;

        private IMapper productAEMapper;
        public ManageProductsController(IProductService productService)
        {
            _productService = productService;

            filtersBase.Add(new FilterBaseViewModel(-1, "همه کالاها"));
            filtersBase.Add(new FilterBaseViewModel(0, "حذف شده‌"));
            filtersBase.Add(new FilterBaseViewModel(1, "تمام شده‌"));
            filtersBase.Add(new FilterBaseViewModel(2, "غیر فعال شده"));
            filtersBase.Add(new FilterBaseViewModel(3, "مخفی شده"));

            pc = new PersianCalendar();


            productAEMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapping>());
            productAEMapper = productAEMapperConfig.CreateMapper();

            // paging initialization

            numberPerPage = 5;
            currentPage = 1;
            filterId = -1;
            allProductCount = _productService.GetAllProductsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allProductCount / numberPerPage);
        }

        public IActionResult Index()
        {
            filterId = -1;
            List<ProductViewModelAdmin> productViews = GetProducts(filterId);


            currentPage = 1;

            ViewBag.Filters = filtersBase;
            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return View(productViews);
        }

        [HttpGet]
        public IActionResult AllProductsOfFilter(int _filterId)
        {
            this.filterId = _filterId;
            List<ProductViewModelAdmin> productsViews = GetProducts(filterId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_ProductsTable", productsViews);
        }


        private List<ProductViewModelAdmin> GetProducts(int _filterId)
        {
            this.filterId = _filterId;
            var products = _productService.GetAllProductsPaged(currentPage, numberPerPage, filterId);

            List<ProductViewModelAdmin> productViews = new List<ProductViewModelAdmin>();



            foreach (var product in products)
            {
                productViews.Add(new ProductViewModelAdmin
                {
                    ProductId = product.ProductId,
                    Name = product.Name,

                    IsActivate = product.IsActivate,
                    SellPrice = product.SellPrice,
                    Count = product.Count,
                    BuyDate = string.Format("{0}/{1}/{2}", pc.GetYear(product.BuyDate), pc.GetMonth(product.BuyDate), pc.GetDayOfMonth(product.BuyDate)),
                    IsDelete = product.IsDelete,
                    IsShow = product.IsShow,

                });
            }

            allProductCount = _productService.GetAllProductsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allProductCount / numberPerPage);

            return productViews;
        }

        [HttpGet]
        public IActionResult ChangePage(int _filterId, int pageNumber = 1)
        {
            this.filterId = _filterId;
            currentPage = pageNumber;
            List<ProductViewModelAdmin> productsViews = GetProducts(filterId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_ProductsTable", productsViews);
        }

        [CheckAccess("DeleteProduct")]
        [Route("/Admin/ManageProducts/DeleteProduct")]
        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            bool resault = _productService.DeleteProduct(productId);

            return Json(new { success = resault });
        }

        [CheckAccess("ShowProduct")]
        [Route("/Admin/ManageProducts/ShowProduct")]
        [HttpPost]
        public ActionResult ShowProduct(int productId)
        {
            if (_productService.IsProductExist(productId))
            {
                bool resault = _productService.ShowProduct(productId);

                return Json(new { resault = resault });
            }

            return RedirectToAction("Index");

        }

        [CheckAccess("EditProductCategory")]
        [Route("/Admin/ManageProducts/EditProductCategory/{productId}")]
        [HttpGet]
        public IActionResult EditProductCategory(int productId)
        {
            ProductCategoryViewModelAdmin productCategory = new ProductCategoryViewModelAdmin();

            var categories = _productService.GetAllCategory();

            var product = _productService.GetProduct(productId);

            if (product != null)
            {
                productCategory.Name = product.Name;
                productCategory.ProductId = product.ProductId;


                foreach (var item in categories)
                {
                    CategoryViewModelAdmin categoryVM = new CategoryViewModelAdmin();
                    categoryVM.Name = item.Name;
                    categoryVM.Id = item.Id;
                    categoryVM.IsActive = false;


                    if (product.ProductCategories.Select(x => x.CategoryId).Contains(item.Id))
                    {
                        categoryVM.IsActive = true;
                        productCategory.CategoryText += item.Name + ", ";
                    }
                    productCategory.ProdcutCategories.Add(categoryVM);
                }
            }

            return View(productCategory);
        }

        [CheckAccess("EditProductCategory")]
        [Route("/Admin/ManageProducts/EditProductCategory/{productId}")]
        [HttpPost]
        public IActionResult EditProductCategory(ProductCategoryViewModelAdmin productCategory)
        {
            Product product = _productService.GetProduct(productCategory.ProductId);

            ICollection<ProductCategory> productCategories = new HashSet<ProductCategory>();


            foreach (var item in productCategory.ProdcutCategories)
            {
                if (item.IsActive)
                {
                    productCategories.Add(new ProductCategory() { CategoryId = item.Id, ProductId = productCategory.ProductId });
                }
            }

            product.ProductCategories = productCategories;

            _productService.UpdateProduct(product);


            return RedirectToAction("Index");

        }


        [CheckAccess("EditProduct")]
        [Route("/Admin/ManageProducts/EditProduct/{productId}")]
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {

            var product = _productService.GetProduct(productId);


            if (product != null)
            {
                // Use Mapper
                ProductAEViewModelAdmin productAE = productAEMapper.Map<ProductAEViewModelAdmin>(product);

                productAE.BuyDateSTR = string.Format("{0}/{1}/{2}", pc.GetYear(product.BuyDate), pc.GetMonth(product.BuyDate), pc.GetDayOfMonth(product.BuyDate));

                ViewBag.EditMode = true;
                ViewBag.ActionName = "EditProduct";

                return View("AddEditProduct", productAE);

            }

            return RedirectToAction("Index");

        }

        [CheckAccess("EditProduct")]
        [Route("/Admin/ManageProducts/EditProduct/{productId}")]
        [HttpPost]
        public IActionResult EditProduct(ProductAEViewModelAdmin productAE)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.ActionName = "EditProduct";

                return View("AddEditProduct", productAE);
            }

            Product product = productAEMapper.Map<Product>(productAE);
            productAE.BuyDateSTR = Tools.ConvertPersianToEnglishNumbers(productAE.BuyDateSTR);

            if (productAE.BuyDateSTR.All(c => char.IsDigit(c) || c == '/'))
            {
                try
                {
                    var dateParts = productAE.BuyDateSTR.Split('/');

                    product.BuyDate = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2]), pc);

                    _productService.UpdateProduct(product, productAE.ProductImageFile);


                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("BuyDateSTR", "لطفا فیلد تاریخ را به درستی (مثال: 1234/5/6) وارد کنید");
                }

            }
            else
            {
                ModelState.AddModelError("BuyDateSTR", "تاریخ باید به فرمت 1234/5/6 باشد");
            }

            ViewBag.EditMode = true;
            ViewBag.ActionName = "EditProduct";

            return View("AddEditProduct", productAE);
        }


        [CheckAccess("AddProduct")]
        [Route("/Admin/ManageProducts/AddProduct")]
        [HttpGet]
        public IActionResult AddProduct()
        {

            ProductAEViewModelAdmin productAE = new ProductAEViewModelAdmin();
            productAE.ProductImageName = "noimage_product.png";

            ViewBag.EditMode = false;
            ViewBag.ActionName = "AddProduct";

            return View("AddEditProduct", productAE);
        }

        [CheckAccess("AddProduct")]
        [Route("/Admin/ManageProducts/AddProduct")]
        [HttpPost]
        public IActionResult AddProduct(ProductAEViewModelAdmin productAE)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = false;
                ViewBag.ActionName = "AddProduct";
                return View("AddEditProduct", productAE);
            }

            Product product = productAEMapper.Map<Product>(productAE);
            productAE.BuyDateSTR = Tools.ConvertPersianToEnglishNumbers(productAE.BuyDateSTR);

            if (productAE.BuyDateSTR.All(c => char.IsDigit(c) || c == '/'))
            {
                try
                {
                    var dateParts = productAE.BuyDateSTR.Split('/');

                    product.BuyDate = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2]), pc);

                    _productService.AddProduct(product, productAE.ProductImageFile);


                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("BuyDateSTR", "لطفا فیلد تاریخ را به درستی (مثال: 1234/5/6) وارد کنید");
                }

            }
            else
            {
                ModelState.AddModelError("BuyDateSTR", "تاریخ باید به فرمت 1234/5/6 باشد");
            }

            ViewBag.EditMode = false;
            ViewBag.ActionName = "AddProduct";
            return View("AddEditProduct", productAE);
        }

    }
}
