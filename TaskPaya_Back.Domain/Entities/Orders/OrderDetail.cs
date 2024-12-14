using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Common;
using TaskPaya_Back.Domain.Entities.Products;

namespace TaskPaya_Back.Domain.Entities.Orders
{
    public sealed class OrderDetail:BaseEntity
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
