using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private ZedShopContext _context;

        private IUserService _userService;

        public OrderService(ZedShopContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public int AddOrder(string userName)
        {
            User user = _userService.GetUserByUserName(userName);

            if (user != null) 
            {
                if (getOpenOrder(user) == null)
                {
                    Order order = new Order()
                    {
                        User = user,
                        Status = false,
                        Province = "",
                        Address = "",
                        City = "",
                        FinalDate = DateTime.Now
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    return order.Id;
                }
            }

            return -1;
        }

        public void AddProductToOrder(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
        }

        public void IncreaseProductCountOfOrder(int ProductId, int OrderId, int Count)
        {
            var orderProduct = GetOrderProduct(OrderId, ProductId);

            if(orderProduct != null)
            {
                orderProduct.Count += Count;
                _context.OrderProducts.Update(orderProduct);
                _context.SaveChanges();
            }
        }

        public OrderProduct GetOrderProduct(int orderId, int productId)
        {
            return _context.OrderProducts.SingleOrDefault(o => o.OrdrId == orderId && o.ProductId == productId);
        }

        public bool deleteProductFromOrder(int orderId, int productId)
        {
            var data = GetOrderProduct(orderId, productId);


            if (data != null)
            {
                _context.OrderProducts.Remove(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Order getOpenOrder(string userName)
        {
                            
            User user = _userService.GetUserByUserName(userName);
            var data = _context.Orders.Include(p=>p.OrderProducts).SingleOrDefault(c => c.UserId == user.UserId && c.Status == false);
            return data;
        }

        public Order getOpenOrder(User user)
        {
            var data = _context.Orders.Include(p => p.OrderProducts).SingleOrDefault(c => c.UserId == user.UserId && c.Status == false);
            return data;
        }

        public Order getOrderById(int orderId)
        {
            return _context.Orders.SingleOrDefault(c=>c.Id == orderId);
        }

        public List<OrderProduct> getProductsOfOrder(int orderId)
        {
            return _context.OrderProducts.Include(p=>p.Product).Where(o=>o.OrdrId == orderId).ToList();
        }

    }
}
