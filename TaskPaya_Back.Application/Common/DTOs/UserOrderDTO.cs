using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Entities.Orders;

namespace TaskPaya_Back.Application.Common.DTOs
{
    public class UserOrderDTO
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? discount { get; set; }
        public string? isdiscountPercentage { get; set; }
        public List<OrderDTO> orders { get; set; }
    }
}
