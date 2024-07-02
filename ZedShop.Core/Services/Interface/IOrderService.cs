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

        Order GetOrderById(int orderId);
        void AddProductToOrder(OrderProduct orderProduct);

        void IncreaseProductCountOfOrder(int productId, int orderId, int count);

        bool DeleteProductFromOrder(int orderId, int productId);

        OrderProduct GetOrderProduct(int orderId, int productId);

        List<OrderProduct> GetProductsOfOrder(int orderId);

    }
}
