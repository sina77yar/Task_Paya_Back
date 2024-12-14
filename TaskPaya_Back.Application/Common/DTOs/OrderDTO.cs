using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPaya_Back.Application.Common.DTOs
{
    public class OrderDTO
    {
        public int id { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int profit { get; set; }
        public bool isFragile { get; set; }
    }
}
