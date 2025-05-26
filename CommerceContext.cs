using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class CommerceContext : DbContext {
    private readonly string connString = "Server=127.0.0.1,1433;User Id=sa;Password=coff33time$;Database=commerce;Encrypt=False;";
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Item> Item { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.EnableDetailedErrors().UseSqlServer(connString);
}

public class Customer {
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    public List<Order> Orders { get; } = new();
}

// [Index(nameof(BlogId))]
public class Order {
    [Key]
    public int Id { get; set; }

    public Customer Customer { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
}

// [Index(nameof(BlogId))]
public class OrderItem {
    [Key]
    public int Id { get; set; }

    public int Quantity { get; set; }

    public Item Item { get; set; }
}

public class Item {
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public decimal Price { get; set; }
}
