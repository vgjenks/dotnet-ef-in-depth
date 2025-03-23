using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlServer("Server=127.0.0.1,1433;User Id=sa;Password=coff33time$;Database=blogga;Encrypt=False;");
}

public class Blog {
    public int BlogId { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; } = new();
}

public class Post {
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int RandomNumber { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}