using Microsoft.AspNetCore.Mvc;
using TaskPaya_Back.Application.Common.DTOs;
using TaskPaya_Back.Application.Common.Utilities.Common;
using TaskPaya_Back.Application.Repositories.Interfaces;

namespace TabaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;

        }
        [HttpPost("add-order")]
        public async Task<IActionResult> AddProductToOrder([FromForm] UserOrderDTO order)
        {
            try
            {
                var userId = await userService.CreateUser(order.FullName, order.Address);
                if (order.orders.Count > 0)
                {
                    var time = DateTime.Now.Hour;
                    if (time < 19 || time > 8)
                    {
                        int sumorder = 0;
                        foreach (var orderItem in order.orders)
                        {

                            sumorder += (orderItem.price * orderItem.count) + (orderItem.profit * orderItem.count);
                        }
                        if (sumorder >= 50000)
                        {
                            foreach (var item in order.orders)
                            {

                                await orderService.AddProductToOrder(userId, item.id, item.count, item.isFragile);
                            }
                            if (order.discount != null)
                            {
                                double sumorders = 0;
                                foreach (var item in order.orders)
                                {
                                    sumorders = sumorders + ((item.count) * (item.price + item.profit));
                                }
                                if (order.isdiscountPercentage == "true")
                                {
                                    double disc = double.Parse(order.discount);
                                    double darsad = disc / 100;
                                    double takhfif = sumorders * darsad;
                                    sumorders = sumorders - takhfif;
                                }
                                else
                                {
                                    sumorders = (int)sumorders - int.Parse(order.discount);
                                }
                                return JsonResponseStatus.Success(sumorders);
                            }
                            else
                            {
                                return JsonResponseStatus.Success(sumorder);
                            }
                        }
                        else
                        {
                            return JsonResponseStatus.Error("حداقل ثبت سبد خرید بالای 50,000 تومان می باشد");
                        }
                    }
                    else
                    {
                        return JsonResponseStatus.Error("ساعت ثبت سفارش بین 8 صبح تا 7 شب می باشد");
                    }
                }
                else
                {
                    return JsonResponseStatus.Error("سبد خرید یافت نشد");

                }
            }
            catch (Exception)
            {
                return JsonResponseStatus.Error(new { message = "ارور" });
            }

        }


        [HttpGet("GetAllOrdersNormal")]
        public async Task<IActionResult> GetAllOrdersNormal()
        {
            try
            {
                var orderlist = await orderService.GetAllOrdersNormal();
                return JsonResponseStatus.Success(orderlist);
            }
            catch (Exception)
            {
                return JsonResponseStatus.Error(new { message = "ارور اردر کنترلر" });
            }


        }
        [HttpGet("GetAllOrdersPishtaz")]
        public async Task<IActionResult> GetAllOrdersPishtaz()
        {
            try
            {
                var orderlist = await orderService.GetAllOrdersPishtaz();
                return JsonResponseStatus.Success(orderlist);
            }
            catch (Exception)
            {
                return JsonResponseStatus.Error(new { message = "ارور اردر کنترلر" });

            }


        }

        [HttpGet("GetOrderListById/{id}")]
        public async Task<IActionResult> GetOrderListById(long id)
        {
            var orderlist = await orderService.GetOrderListById(id);

            return JsonResponseStatus.Success(orderlist);
        }

    }
}
