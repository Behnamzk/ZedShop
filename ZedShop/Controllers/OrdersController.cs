using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZedShop.Core.DTOs.Order;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.OpinionViewModel;
using ZedShop.Web.Areas.Admin.Models.ProductViewModel;

namespace ZedShop.Web.Controllers
{

    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly List<ProvinceViewModel> provincesViewModel;

        private int pageCount, currentPage, allOrdersCount, numberPerPage;


        private readonly PersianCalendar pc;

        public OrdersController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;

            pc = new PersianCalendar();


            // paging initialization
            numberPerPage = 5;
            currentPage = 1;
           

            provincesViewModel = GetAllProvince();
        }

        [Authorize]
        [Route("/Orders")]
        public IActionResult Index()
        {
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                Order order = _orderService.GetOpenOrder(username);

                if (order != null)
                {
                    // Show Open order to user
                    List<OrderProductViewModel> OrderProductList = new List<OrderProductViewModel>();

                    double total_price = 0;

                    foreach (var item in order.OrderProducts)
                    {
                        var pro = _productService.GetProduct(item.ProductId);
                        OrderProductViewModel model = new OrderProductViewModel()
                        {
                            OrderId = order.Id,
                            ProdcutCount = item.Count,
                            SellPrice = item.Price * item.Count,
                            ProductId = pro.ProductId,
                            ProductName = pro.Name,
                            ProductImageName = pro.ProductImageName
                        };
                        total_price += item.Price * item.Count;
                        OrderProductList.Add(model);
                    }

                    OPTableViewModel oPTable = new OPTableViewModel()
                    {
                        Items = OrderProductList,
                        TotalPrice = total_price,
                        OrderId = order.Id
                    };

                    return View(oPTable);

                }
                else if (_orderService.DoesUserHasOrders(username))
                {
                    // User Doesnt have Open order so Show orders list
                    return RedirectToAction("OrdersHistory", "Orders");

                }
                else
                {
                    // User Doesnt have any orders so Show user Message
                    return RedirectToAction("ShowOrderMessageToUser", "Orders", new { messageId = 3 });
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("/Orders/delete/{order_id}/{product_id}")]
        public JsonResult Index(int order_id, int product_id)
        {
            List<OrderProductViewModel> OrderProductList = new List<OrderProductViewModel>();

            double total_price = 0;

            var orderProduct = _orderService.GetOrderProduct(order_id, product_id);

            if (_orderService.DeleteProductFromOrder(order_id, product_id))
            {
                var data = _orderService.GetProductsOfOrder(order_id);

                foreach (var item in data)
                {
                    OrderProductViewModel model = new OrderProductViewModel()
                    {
                        OrderId = item.OrdrId,
                        ProdcutCount = item.Count,
                        SellPrice = item.Price * item.Count,
                        ProductId = item.ProductId,
                        ProductName = item.Product.Name,
                        ProductImageName = item.Product.ProductImageName
                    };
                    total_price += item.Price * item.Count;
                    OrderProductList.Add(model);
                }
                _productService.IncreaseProductCount(product_id, orderProduct.Count);

            }

            OPTableViewModel oPTable = new OPTableViewModel()
            {
                Items = OrderProductList,
                TotalPrice = total_price,
                OrderId = orderProduct.OrdrId
            };


            return Json(oPTable);
        }

        [Authorize]
        [Route("/Orders/OrdersHistory")]
        public IActionResult OrdersHistory()
        {
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                allOrdersCount = _orderService.GetAllOrdersCount(username);
                pageCount = (int)Math.Ceiling((double)allOrdersCount / numberPerPage);

                ViewBag.NumberOfPage = pageCount;
                ViewBag.CurrentPage = currentPage;

                List<OrderIndexViewModel> orderIndexes = GetAllOrdersOfHistory(username);

                return View(orderIndexes);
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult BuyProduct(OrderViewModel orderViewModel)
        {
            var username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            int orderId = -1;

            Product product = _productService.GetProduct(orderViewModel.ProductId);

            Order order = _orderService.GetOpenOrder(username);

            if (order == null)
            {
                orderId = _orderService.AddOrder(username);

                order = _orderService.GetOpenOrder(username);
            }

            orderId = order.Id;

            // check if product is exist in Order or not 
            if (order.OrderProducts.Any(o => o.ProductId == orderViewModel.ProductId))
            {
                // if we have enough products decrease count and
                // update db and return true
                // else return false
                if (_productService.DecreaseProductCount(product.ProductId, orderViewModel.Count))
                {
                    _orderService.IncreaseProductCountOfOrder(product.ProductId, orderId, orderViewModel.Count);
                }
            }
            else
            {
                if (orderId != -1 && product != null)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrdrId = orderId,
                        ProductId = product.ProductId,
                        Count = orderViewModel.Count,
                        Price = product.SellPrice
                    };

                    // if we have enough products decrease count and
                    // update db and return true
                    // else return false
                    if (_productService.DecreaseProductCount(product.ProductId, orderViewModel.Count))
                    {
                        _orderService.AddProductToOrder(orderProduct);
                    }

                }
            }



            return RedirectToAction("Index");
        }


        [Authorize]
        [Route("/Orders/CompletePurchase/{order_id}")]
        public ActionResult CompletePurchase(int order_id)
        {
            var username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            Order order = _orderService.GetOpenOrder(username);

            if (order != null)
            {
                if (order.Id == order_id)
                {
                    OrderPurchaseViewModel orderPVM = new OrderPurchaseViewModel()
                    {
                        Id = order_id,
                        OrderProducts = order.OrderProducts,
                        AddressVM = new AddressViewModel()
                    };



                    ViewBag.Provinces = this.provincesViewModel;

                    return View(orderPVM);
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("/Orders/CompletePurchase/{order_id}")]
        [HttpPost]
        public ActionResult CompletePurchase(OrderPurchaseViewModel orderPurchase)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Provinces = this.provincesViewModel;
                return View(orderPurchase);
            }

            if (orderPurchase != null)
            {
                if (orderPurchase.AddressVM.ProvinceId != -1)
                {
                    if (orderPurchase.AddressVM.CityId != -1)
                    {
                        try
                        {
                            City city = _orderService.GetCity((int)orderPurchase.AddressVM.CityId);
                            Province province = _orderService.GetProvince((int)orderPurchase.AddressVM.ProvinceId);
                            var currentuser = User.Identity.Name;

                            User user = _userService.GetUserByUserName(currentuser);

                            if (city != null && province != null && user != null)
                            {
                                if (_orderService.IsCityInProvince(province.Id, city.Id))
                                {
                                    // Save Data
                                    Address address = new Address()
                                    {
                                        City = city,
                                        Province = province,
                                        AddressContent = orderPurchase.AddressVM.Address,
                                        PostalCode = orderPurchase.AddressVM.PostalCode,
                                        CustomerFullName = orderPurchase.AddressVM.CustomerFullName,
                                        HouseNumber = orderPurchase.AddressVM.HouseNumber,
                                        CustomerPhoneNum = orderPurchase.AddressVM.CustomerPhoneNum,
                                        User = user
                                    };

                                    Address savedAddress = _orderService.AddAddress(address);

                                    if (savedAddress != null)
                                    {
                                        Order order = _orderService.GetOrderById(orderPurchase.Id);

                                        if (order != null)
                                        {
                                            order.Address = savedAddress;

                                            OrderStatus orderStatus = _orderService.GetOrderStatus("UnderReview");
                                            order.OrderStatus = orderStatus;
                                            order.Description = orderStatus.Description;

                                            _orderService.UpdateOrder(order);
                                            return RedirectToAction("ShowOrderMessageToUser", "Orders", new { messageId = 1 });
                                        }

                                    }

                                }

                            }
                            return RedirectToAction("ShowOrderMessageToUser", "Orders", new { messageId = 2 });


                        }
                        catch
                        {
                            return RedirectToAction("ShowOrderMessageToUser", "Orders", new { messageId = 2 });
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("AddressVM.CityId", "فیلد شهر اجباری است");

                    }
                }
                else
                {
                    ModelState.AddModelError("AddressVM.ProvinceId", "فیلد استان اجباری است");

                }
            }

            ViewBag.Provinces = this.provincesViewModel;
            return View(orderPurchase);
        }

        [Authorize]
        [Route("/Orders/ShowOrderMessageToUser/{messageId}")]

        public IActionResult ShowOrderMessageToUser(int messageId)
        {
            switch (messageId)
            {
                case 1:
                    ViewBag.Message = "درخواست خرید شما با موفقیت ثبت شد. پس از تایید سبد خرید، شما می‌توانید به ادامه روال خرید بپردازید.";
                    ViewBag.TargetController = "Orders";
                    ViewBag.TargetAction = "Index";
                    break;
                case 2:
                    ViewBag.Message = "درخواست خرید شما ثبت نشد. لطفا دیرتر اقدام کنید!";
                    ViewBag.TargetController = "Orders";
                    ViewBag.TargetAction = "Index";
                    break;
                case 3:
                    ViewBag.Message = "سفارشی از سمت شما ثبت نشده است، پس از افزودن کالا به سبد خرید می‌توانید سبد خرید خود را از این بخش مدیریت کنید!";
                    ViewBag.TargetController = "Home";
                    ViewBag.TargetAction = "Index";
                    break;
                default:
                    ViewBag.Message = "سفارشی از سمت شما ثبت نشده است، پس از افزودن کالا به سبد خرید می‌توانید سبد خرید خود را از این بخش مدیریت کنید!";
                    ViewBag.TargetController = "Home";
                    ViewBag.TargetAction = "Index";
                    break;

            }

            return View();
        }

        private List<ProvinceViewModel> GetAllProvince()
        {
            // ViewBag province
            var provinces = _orderService.GetAllProvince();

            List<ProvinceViewModel> provincesVM = new List<ProvinceViewModel>();
            foreach (var p in provinces)
            {
                ProvinceViewModel province = new ProvinceViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                };

                provincesVM.Add(province);

            }
            return provincesVM;
        }

        private List<OrderIndexViewModel> GetAllOrdersOfHistory(string username)
        {
            List<OrderIndexViewModel> orderIndexes = new List<OrderIndexViewModel>();

            List<Order> orders = _orderService.GetAllOrdersOfUserPaged(username, currentPage, numberPerPage);

            if (orders != null)
            {

                foreach (var orderItem in orders)
                {
                    OrderIndexViewModel orderVM = new OrderIndexViewModel()
                    {
                        Id = orderItem.Id,
                        OrderStatus = orderItem.OrderStatus,
                        OrderDate = string.Format("{0}/{1}/{2}", pc.GetYear(orderItem.FinalDate), pc.GetMonth(orderItem.FinalDate), pc.GetDayOfMonth(orderItem.FinalDate)),
                        OrderDescription = orderItem.Description
                    };

                    orderIndexes.Add(orderVM);
                }
            }
            return orderIndexes;
        }

        [HttpGet]
        public IActionResult AllCitiesOfProvince(int _provinceId)
        {
            var cities = _orderService.GetCitiesOfProvince(_provinceId);

            List<CityViewModel> citiesVM = new List<CityViewModel>();

            foreach (var c in cities)
            {
                CityViewModel city = new CityViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                };

                citiesVM.Add(city);
            }

            return PartialView("_CitiesDropDown", citiesVM);
        }

        [HttpGet]
        public IActionResult ChangePage(int pageNumber = 1)
        {
            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                currentPage = pageNumber;
                List<OrderIndexViewModel> orderIndexes = GetAllOrdersOfHistory(username);

                ViewBag.NumberOfPage = pageCount;
                ViewBag.CurrentPage = currentPage;

                return PartialView("_OrdersTable", orderIndexes);
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
