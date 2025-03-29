using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext {
    private readonly string connString = "Server=127.0.0.1,1433;User Id=sa;Password=coff33time$;Database=blogga;Encrypt=False;";
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => 
        options.EnableDetailedErrors().UseSqlServer(connString);
}

public class Blog {
    [Key]
    public int BlogId { get; set; }

    [MaxLength(500)]
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

[Index(nameof(BlogId))]
public class Post {
    [Key]
    public int PostId { get; set; }

    [MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string Content { get; set; }

    public int RandomNumber { get; set; }

    public int BlogId { get; set; }

    public Blog Blog { get; set; }
}