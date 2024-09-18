using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Order;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ZedShopContext _context;

        private readonly IUserService _userService;

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
                if (GetOpenOrder(user) == null)
                {
                    OrderStatus orderStatus = GetOrderStatus("Open");
                    Order order = new Order()
                    {
                        User = user,
                        Address = null,
                        OrderStatus = orderStatus,
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

        public void IncreaseProductCountOfOrder(int productId, int orderId, int count)
        {
            var orderProduct = GetOrderProduct(orderId, productId);

            if (orderProduct != null)
            {
                orderProduct.Count += count;
                _context.OrderProducts.Update(orderProduct);
                _context.SaveChanges();
            }
        }

        public OrderProduct GetOrderProduct(int orderId, int productId)
        {
            return _context.OrderProducts.SingleOrDefault(o => o.OrdrId == orderId && o.ProductId == productId);
        }

        public bool DeleteProductFromOrder(int orderId, int productId)
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

        public Order GetOpenOrder(string userName)
        {
            OrderStatus orderStatus = GetOrderStatus("Open");
            User user = _userService.GetUserByUserName(userName);
            return _context.Orders.Include(p => p.OrderProducts).SingleOrDefault(c => c.UserId == user.UserId && c.OrderStatusId == orderStatus.Id);
        }

        public Order GetOpenOrder(User user)
        {
            OrderStatus orderStatus = GetOrderStatus("Open");
            return _context.Orders.Include(p => p.OrderProducts).SingleOrDefault(c => c.UserId == user.UserId && c.OrderStatusId == orderStatus.Id);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.SingleOrDefault(c => c.Id == orderId);
        }

        public bool DoesUserHasOrders(string userName)
        {
            User user = _userService.GetUserByUserName(userName);
            
            if(user != null)
            {
                return _context.Orders.Any(o=>o.UserId == user.UserId);
            }

            return false;

        }

        public List<Order> GetAllOrdersOfUser(string userName)
        {
            User user = _userService.GetUserByUserName(userName);

            if (user != null)
            {
                return _context.Orders.Include(o=>o.OrderStatus).Where(o => o.UserId == user.UserId).ToList();
            }

            return null;
        }

        public List<OrderProduct> GetProductsOfOrder(int orderId)
        {
            return _context.OrderProducts.Include(p => p.Product).Where(o => o.OrdrId == orderId).ToList();
        }

        public List<Province> GetAllProvince()
        {
            return _context.Provinces.ToList();

        }

        public List<City> GetCitiesOfProvince(int provinceId)
        {
            return _context.Cities.Where(c => c.ProvinceId == provinceId).ToList();
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            return _context.OrderStatuses.ToList();
        }

        public OrderStatus GetOrderStatus(string orderStatusName)
        {
            return _context.OrderStatuses.SingleOrDefault(c => c.Name == orderStatusName);
        }

        public OrderStatus GetOrderStatus(int orderStatusId)
        {
            return _context.OrderStatuses.SingleOrDefault(c => c.Id == orderStatusId);

        }

        public City GetCity(int id)
        {
            return _context.Cities.SingleOrDefault(c => c.Id == id);
        }

        public Province GetProvince(int id)
        {
            return _context.Provinces.SingleOrDefault(p => p.Id == id);
        }

        public bool IsCityInProvince(int provinceId, int cityId)
        {
            Province province = _context.Provinces.Include(p => p.Cities).SingleOrDefault(p => p.Id == provinceId);

            if (province != null)
            {
                foreach (var c in province.Cities)
                {
                    if (c.Id == cityId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void UpdateOrder(Order order)
        {
            if(order != null)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public Address AddAddress(Address address)
        {

            if (address != null) {
                _context.Addresses.Add(address);
                _context.SaveChanges();

                return address;
            }

            return null;

        }


    }
}
