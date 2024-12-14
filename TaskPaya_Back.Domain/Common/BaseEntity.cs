using System.ComponentModel.DataAnnotations;

namespace TaskPaya_Back.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}