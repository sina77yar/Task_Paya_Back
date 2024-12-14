using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Common;
using TaskPaya_Back.Domain.Entities.Users;

namespace TaskPaya_Back.Domain.Entities.Orders
{
    public sealed class Order : BaseEntity
    {
        public long UserId { get; set; }
        public bool isPay { get; set; }
        public bool isSent { get; set; } = false;
        public bool isPishtazSending { get; set; } = false;
        public bool isNormalSending { get; set; } = false;
        public Guid factorCode { get; set; }
        public DateTime OrderDate { get; set; }


        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
