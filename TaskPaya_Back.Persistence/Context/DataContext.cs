using Microsoft.EntityFrameworkCore;
using TaskPaya_Back.Domain.Entities.Orders;
using TaskPaya_Back.Domain.Entities.Products;
using TaskPaya_Back.Domain.Entities.Users;

namespace CleanArchitecture.Persistence.Context;

public class PayaTaskDataContext : DbContext
{
    public PayaTaskDataContext(DbContextOptions<PayaTaskDataContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    #region Disable Cascading Delete in DB
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cascades = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var cascade in cascades)
        {
            cascade.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(modelBuilder);
        int id = 100;
     
        
        #region ProductInitializer
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره یک", Profit = 0, isFragile = true, Price = 10000 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره دو", Profit = 0, isFragile = false, Price = 20000 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره سه", Profit = 0, isFragile = true, Price = 30000 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره چهار", Profit = 0, isFragile = true, Price = 10000 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره پنج", Profit = 0, isFragile = false, Price = 60000 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = id++, Title = "محصول شماره شش", Profit = 0, isFragile = false, Price = 40000 });
        #endregion
    }

    #endregion


}