using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Order;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IOrderService
    {
        #region Orders

        /// <summary>
        /// get username and check if user has an open order
        /// if user has an open order (status == false) or username invalid retrun -1
        /// else add new order and retrun id of order
        /// </summary>
        /// <param name="userName"></param>
        /// <returns> 
        /// if all things ok return id of order 
        /// else retrun -1
        /// </returns>
        int AddOrder(string userName);

        /// <summary>
        /// get username and return open order of user else retrun null
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>
        /// if all things ok return an object of order 
        /// else return null
        /// </returns>
        Order GetOpenOrder(string userName);

        /// <summary>
        /// get user and return open order of user else retrun null
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// if all things ok return an object of order 
        /// else return null
        /// </returns>
        Order GetOpenOrder(User user);

        bool DoesUserHasOrders(string userName);
        bool DoesUserHasOrder(string userName , int order_id);

        List<Order> GetAllOrdersOfUserPaged(string userName, int currentPage, int numberPerPage);
        List<Order> GetAllOrdersPaged(int orderStatusId, int currentPage, int numberPerPage);

        int GetAllOrdersCount(string userName);

        int GetAllOrdersCount(int orderStatusId);

        void UpdateOrder(Order order);

        Order GetOrderById(int orderId);

        Order GetOrderWithAllDetailById(int orderId);
        void AddProductToOrder(OrderProduct orderProduct);

        void IncreaseProductCountOfOrder(int productId, int orderId, int count);

        bool DeleteProductFromOrder(int orderId, int productId);

        OrderProduct GetOrderProduct(int orderId, int productId);

        List<OrderProduct> GetProductsOfOrder(int orderId);

        List<OrderStatus> GetOrderStatuses();
        List<DiscountViewModel> GetDiscounts();

        Discount GetDiscount(int discountId);

        OrderStatus GetOrderStatus(string orderStatusName);
        OrderStatus GetOrderStatus(int orderStatusId);

        #endregion

        #region Province and City
        List<Province> GetAllProvince();
        List<City> GetCitiesOfProvince(int provinceId);
        City GetCity(int id);
        Province GetProvince(int id);
        bool IsCityInProvince(int provinceId, int cityId);
        Address AddAddress(Address address);
        #endregion
    }
}
