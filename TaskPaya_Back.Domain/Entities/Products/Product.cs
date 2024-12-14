using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Common;
using TaskPaya_Back.Domain.Entities.Orders;

namespace TaskPaya_Back.Domain.Entities.Products
{
    public  class Product : BaseEntity
    {
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر بیش از حد مجاز است")]
        public string Title { get; set; }

        [DisplayName("قیمت")]
        [Required(ErrorMessage = "لطفا قیمت را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر بیش از حد مجاز است")]
        public long Price { get; set; }
        [DisplayName("میزان سود")]
        public int Profit { get; set; } = 0;

        [DisplayName("شکستنی")]
        public bool isFragile { get; set; } = false;


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
