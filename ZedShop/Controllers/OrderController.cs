using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZedShop.Core.DTOs.Order;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [Authorize]
        [Route("/MyOrders")]
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                Order order = _orderService.getOpenOrder(username);

                List<OrderProductViewModel> OrderProductList= new List<OrderProductViewModel>();

                double total_price = 0;
                
                foreach ( var item in order.OrderProducts)
                {
                    var pro = _productService.GetProduct(item.ProductId);
                    OrderProductViewModel model = new OrderProductViewModel()
                    {
                        OrderId = order.Id,
                        ProdcutCount = item.Count,
                        SellPrice = item.price * item.Count,
                        ProductId = pro.ProductId,
                        ProductName = pro.Name,
                        ProductImageName = pro.ProductImageName
                    };
                    total_price += item.price * item.Count;
                    OrderProductList.Add(model);
                }

                OPTableViewModel oPTable = new OPTableViewModel()
                {
                    items = OrderProductList,
                    total_price = total_price
                };

                return View(oPTable);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("/MyOrders/delete/{order_id}/{product_id}")]
        public JsonResult Index(int order_id, int product_id)
        {
            List<OrderProductViewModel> OrderProductList = new List<OrderProductViewModel>();

            double total_price = 0;

            var orderProduct = _orderService.GetOrderProduct(order_id, product_id);

            if (_orderService.deleteProductFromOrder(order_id, product_id))
            {
                var data = _orderService.getProductsOfOrder(order_id);
                
                foreach ( var item in data)
                {
                    OrderProductViewModel model = new OrderProductViewModel()
                    {
                        OrderId = item.OrdrId,
                        ProdcutCount = item.Count,
                        SellPrice = item.price * item.Count,
                        ProductId = item.ProductId,
                        ProductName = item.Product.ProductImageName,
                        ProductImageName = item.Product.ProductImageName
                    };
                    total_price += item.price * item.Count;
                    OrderProductList.Add(model);
                }
                _productService.IncreaseProductCount(product_id, orderProduct.Count);

            }

            OPTableViewModel oPTable = new OPTableViewModel()
            {
                items = OrderProductList,
                total_price = total_price
            };


            return Json(oPTable);
        }

            [Authorize]
        public ActionResult BuyProduct(OrderViewModel orderViewModel)
        {
            var username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            
            int order_id = -1;

            Product product = _productService.GetProduct(orderViewModel.ProductId);

            Order order = _orderService.getOpenOrder(username);

            if (order == null)
            {
                order_id = _orderService.AddOrder(username);
            }
            else
            {
                order_id = order.Id;
                
                // check if product is exist in Order or not 
                if(order.OrderProducts.Any(o => o.ProductId == orderViewModel.ProductId))
                {
                    // if we have enough products decrease count and
                    // update db and return true
                    // else return false
                    if (_productService.DecreaseProductCount(product.ProductId, orderViewModel.Count))
                    {
                        _orderService.IncreaseProductCountOfOrder(product.ProductId, order_id, orderViewModel.Count);
                    }
                }
                else
                {
                    if (order_id != -1 && product != null)
                    {
                        OrderProduct orderProduct = new OrderProduct()
                        {
                            OrdrId = order_id,
                            ProductId = product.ProductId,
                            Count = orderViewModel.Count,
                            price = product.SellPrice
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

            }
            
            return RedirectToAction("Index");
        }
    }
}
