using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Application.Repositories.Interfaces;
using TaskPaya_Back.Application.Repositories;
using TaskPaya_Back.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace TaskPaya_Back.Persistence.Repositories.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> orderRepository;

        private readonly IGenericRepository<OrderDetail> orderDetailRepository;
        private readonly IUserService userService;
        private readonly IProductService productService;
        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IUserService userService, IProductService productService)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.userService = userService;
            this.productService = productService;

        }



        //ساخت سبد نرمال
        public async Task<Order> CreateUserOrderNormal(long userId)
        {

            var order = new Order
            {
                UserId = userId,
                factorCode = Guid.NewGuid(),
                isNormalSending = true,
                OrderDate = DateTime.Now,
            };
            await orderRepository.AddEntity(order);
            await orderRepository.SaveChanges();
            return order;
        }

        //ساخت سبد پیشتاز
        public async Task<Order> CreateUserOrderPishtaz(long userId)
        {

            var order = new Order
            {
                UserId = userId,
                factorCode = Guid.NewGuid(),
                isPishtazSending = true,
                OrderDate = DateTime.Now,
            };
            await orderRepository.AddEntity(order);
            await orderRepository.SaveChanges();
            return order;
        }

        public async Task AddProductToOrder(long userId, long productId, int count, bool? isFragile)
        {
            var user = await userService.GetUserById(userId);
            var product = await productService.GetProductById(productId);
            var order = new Order();
            if (user != null && product != null)
            {
                if (count < 1) count = 1;
                if (isFragile == false)
                {
                    order = await GetUserOpenOrderNormal(userId);
                }
                else
                {
                    order = await GetUserOpenOrderPishtaz(userId);
                }
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Count = count,
                    Price = (int)product.Price + product.Profit,
                };
                await orderDetailRepository.AddEntity(orderDetail);
                await orderDetailRepository.SaveChanges();
            }
        }

        public async Task<Order> GetUserOpenOrderPishtaz(long userId)
        {
            var order = await orderRepository.GetEntitiesQuery().SingleOrDefaultAsync(a => a.UserId == userId && !a.isPay && !a.IsDeleted && a.isPishtazSending);
            if (order == null)
            {
                order = await this.CreateUserOrderPishtaz(userId);
            }
            return order;
        }
        public async Task<Order> GetUserOpenOrderNormal(long userId)
        {
            var order = await orderRepository.GetEntitiesQuery().SingleOrDefaultAsync(a => a.UserId == userId && !a.isPay && !a.IsDeleted && a.isNormalSending);
            if (order == null)
            {
                order = await this.CreateUserOrderNormal(userId);
            }
            return order;
        }
        public async Task<List<Order>> GetUserAllOrders(long userId)
        {
            var orders = await orderRepository.GetEntitiesQuery().Where(a => a.UserId == userId).ToListAsync();

            return orders;
        }
        public async Task<Order> GetUserOrderListNormal(long userId)
        {
            var order = await orderRepository.GetEntitiesQuery().Include(s => s.OrderDetails).Include(s => s.OrderDetails).SingleOrDefaultAsync(a => a.UserId == userId && !a.isPay && !a.IsDeleted);
            if (order == null)
            {
                return await this.CreateUserOrderNormal(userId);
            }
            var orderdetail = await orderRepository.GetEntitiesQuery().SelectMany(a => a.OrderDetails.Where(b => b.OrderId == order.Id)).Include(e => e.Product).ToListAsync();
            //order.OrderDetails = orderdetail;
            return order;
        }
        public async Task<Order> GetUserOrderListPishtaz(long userId)
        {
            var order = await orderRepository.GetEntitiesQuery().Include(s => s.OrderDetails).Include(s => s.OrderDetails).SingleOrDefaultAsync(a => a.UserId == userId && !a.isPay && !a.IsDeleted);
            if (order == null)
            {
                return await this.CreateUserOrderPishtaz(userId);
            }
            var orderdetail = await orderRepository.GetEntitiesQuery().SelectMany(a => a.OrderDetails.Where(b => b.OrderId == order.Id)).Include(e => e.Product).ToListAsync();
            //order.OrderDetails = orderdetail;
            return order;
        }
        public async Task<Order> GetUserOrderByFactorCode(string factorCode)
        {
            var order = await orderRepository.GetEntitiesQuery().SingleOrDefaultAsync(a => a.factorCode.ToString() == factorCode);

            return order;
        }

        public async Task<List<OrderDetail>> GetOrderListById(long id)
        {
            var orderdetail = await orderRepository.GetEntitiesQuery().SelectMany(a => a.OrderDetails.Where(b => b.OrderId == id)).Include(e => e.Product).ToListAsync();
            return orderdetail;
        }
        public void Dispose()
        {
            orderRepository.Dispose();
            orderDetailRepository.Dispose();
        }

        public async Task SetOrderSent(long orderId)
        {
            var order = await orderRepository.GetEntityById(orderId);
            order.isSent = true;
            orderRepository.UpdateEntity(order);
            await orderRepository.SaveChanges();
        }


        public async Task<List<Order>> GetAllOrdersNormal()
        {
            return await orderRepository.GetEntitiesQuery().Where(a => a.isNormalSending).Include(a => a.OrderDetails).ThenInclude(a => a.Product).Include(a => a.User).AsNoTracking().ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersPishtaz()
        {
            return await orderRepository.GetEntitiesQuery().Where(a => a.isPishtazSending).Include(a => a.OrderDetails).ThenInclude(a=>a.Product).Include(a => a.User).AsNoTracking().ToListAsync();

        }
    }
}
