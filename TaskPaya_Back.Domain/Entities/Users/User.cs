using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskPaya_Back.Domain.Common;
using TaskPaya_Back.Domain.Entities.Orders;

namespace TaskPaya_Back.Domain.Entities.Users;

public  class User : BaseEntity
{
 
    [DisplayName("نام و نام خانوادگی")]
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر بیش از حد مجاز است")]
    public string? FullName { get; set; }
    [DisplayName("آدرس")]
    [MaxLength(int.MaxValue, ErrorMessage = "تعداد کاراکتر بیش از حد مجاز است")]
    public string? Address { get; set; }


    public ICollection<Order> Orders { get; set; }

}