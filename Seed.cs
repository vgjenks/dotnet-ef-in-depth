using Bogus;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ef_in_depth;

public class Seed {
    public static async Task Run() {
        using var db = new BloggingContext();

        var faker = new Faker("en");

        if (!await db.Blogs.AnyAsync()) {
            string protocol = "https";
            string domain = "blogga.com";
            await db.Blogs.AddAsync(new() {
                Url = faker.Internet.UrlWithPath(protocol, domain)
            });
            await db.Blogs.AddAsync(new() {
                Url = faker.Internet.UrlWithPath(protocol, domain)
            });
            await db.Blogs.AddAsync(new() {
                Url = faker.Internet.UrlWithPath(protocol, domain)
            });
            await db.SaveChangesAsync();
        }

        var blogs = await db.Blogs.ToListAsync();

        foreach (var blog in blogs) {
            if (!await db.Posts.AnyAsync(x => x.BlogId == blog.BlogId)) {
                Random random = new();
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.Posts.AddAsync(new() {
                    BlogId = blog.BlogId,
                    Title = faker.Lorem.Sentence(wordCount: 6),
                    Content = faker.Lorem.Paragraph(),
                    RandomNumber = random.Next(10)
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
