using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.DTOs.Order;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Infrastructure;
using ZedShop.Web.Areas.Admin.Models.OpinionViewModel;
using ZedShop.Web.Areas.Admin.Models.OrdersViewModel;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteOwner")] //Only Admin and SiteOwner
    public class ManageOrdersController : Controller
    {
        private readonly IOrderService _orderService;

        private int pageCount, currentPage, allOrdersCount, numberPerPage, statusId;

        private readonly List<FilterBaseViewModel> filtersBase = new List<FilterBaseViewModel>();

        private readonly PersianCalendar pc;


        public ManageOrdersController(IOrderService orderService)
        {
            _orderService = orderService;

            var orderStatuses = _orderService.GetOrderStatuses();

            filtersBase.Add(new FilterBaseViewModel(-1, "همه سفارشات"));

            foreach (var item in orderStatuses)
            {
                filtersBase.Add(new FilterBaseViewModel(item.Id, item.DisplayName));
            }

            pc = new PersianCalendar();

            // paging initialization
            numberPerPage = 5;
            currentPage = 1;
            statusId = -1;
            allOrdersCount = _orderService.GetAllOrdersCount(statusId);
            pageCount = (int)Math.Ceiling((double)allOrdersCount / numberPerPage);

        }

        public IActionResult Index()
        {
            statusId = -1;
            List<OrderViewModelAdmin> ordersViews = GetOrders(statusId);

            currentPage = 1;

            ViewBag.Filters = filtersBase;
            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.StatusId = statusId;
            return View(ordersViews);
        }

        private List<OrderViewModelAdmin> GetOrders(int _statusId)
        {
            List<OrderViewModelAdmin> ordersVM = new List<OrderViewModelAdmin>();

            this.statusId = _statusId;

            List<Order> orders = _orderService.GetAllOrdersPaged(statusId, currentPage, numberPerPage);

            if (orders != null)
            {

                foreach (var orderItem in orders)
                {
                    OrderViewModelAdmin orderVM = new OrderViewModelAdmin()
                    {
                        Id = orderItem.Id,
                        OrderStatus = orderItem.OrderStatus.DisplayName,
                        OrderDate = string.Format("{0}/{1}/{2}", pc.GetYear(orderItem.FinalDate), pc.GetMonth(orderItem.FinalDate), pc.GetDayOfMonth(orderItem.FinalDate)),
                        Username = orderItem.User.UserName
                    };

                    ordersVM.Add(orderVM);
                }
            }
            return ordersVM;
        }

        [HttpGet]
        public IActionResult AllOrdersOfFilter(int _statusId)
        {
            this.statusId = _statusId;
            List<OrderViewModelAdmin> OrdersViews = GetOrders(statusId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.StatusId = statusId;

            return PartialView("_OrdersTable", OrdersViews);
        }

        [HttpGet]
        public IActionResult ChangePage(int _statusId, int pageNumber = 1)
        {
            this.statusId = _statusId;
            currentPage = pageNumber;
            List<OrderViewModelAdmin> OrdersViews = GetOrders(statusId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.StatusId = statusId;

            return PartialView("_OrdersTable", OrdersViews);
        }

        [CheckAccess("EditOrder")]
        [Route("/Admin/ManageOrders/EditOrder/{_orderId}")]
        [HttpGet]
        public IActionResult EditOrder(int _orderId)
        {
            Order order = _orderService.GetOrderWithAllDetailById(_orderId);

            if (order != null)
            {
                EditOrderViewModel editOrder = new EditOrderViewModel();

                // Order Detail
                editOrder.order = order;

                // Order Products
                var orderProducts = _orderService.GetProductsOfOrder(_orderId);

                List<OrderDetailProductsViewModel> products = new List<OrderDetailProductsViewModel>();

                foreach (var item in orderProducts)
                {
                    OrderDetailProductsViewModel pVM = new OrderDetailProductsViewModel()
                    {
                        ProductId = item.ProductId,
                        Name = item.Product.Name,
                        Count = item.Count,
                        SellPrice = item.Price * item.Count,
                        ProductImageName = item.Product.ProductImageName
                    };

                    editOrder.ProductsPrice += item.Price * item.Count;

                    products.Add(pVM);
                }

                editOrder.Products = products;

                // Discount Information
                if (order.Discount != null)
                {
                    if (order.Discount.IsShow && order.Discount.IsActive)
                    {
                        editOrder.ProductsPrice = editOrder.ProductsPrice - (editOrder.ProductsPrice * order.Discount.Value);
                    }

                }


                // TotalPrice
                if (order.PostDelivery == null)
                {
                    editOrder.TotalPrice = editOrder.ProductsPrice;
                }
                else
                {
                    editOrder.TotalPrice = editOrder.ProductsPrice + order.PostDelivery.TotalPrice;

                }

                return View(editOrder);

            }

            return RedirectToAction("Index", "ManageOrders");

        }


    }
}
