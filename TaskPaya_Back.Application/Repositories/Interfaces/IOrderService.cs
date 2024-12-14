using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Entities.Orders;

namespace TaskPaya_Back.Application.Repositories.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<Order> CreateUserOrderNormal(long userId);
        Task<Order> CreateUserOrderPishtaz(long userId);
        //چک کردن سبد خرید فعال
        Task<Order> GetUserOpenOrderNormal(long userId);
        Task<Order> GetUserOpenOrderPishtaz(long userId);
        Task<Order> GetUserOrderListNormal(long userId);
        Task<Order> GetUserOrderListPishtaz(long userId);
        Task<List<Order>> GetUserAllOrders(long userId);
        Task<Order> GetUserOrderByFactorCode(string factorCode);
        /// <summary>
        //برای بخش مدیریت ادمین
        /// </summary>
        Task<List<Order>> GetAllOrdersNormal();
        Task<List<Order>> GetAllOrdersPishtaz();

        Task<List<OrderDetail>> GetOrderListById(long id);

        Task AddProductToOrder(long userId, long productId, int count,bool? isFragile);
 
    }
}
