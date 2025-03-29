using Bogus;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ef_in_depth;

class ProgressCounter {
    int Progress { get; set; } = 0;

    internal async Task Show(int total) {
        Progress++;
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.WriteLine($"\rProgress: {Progress}/{total}");
        await Task.Delay(0);
    }
}

public class Seed {
    private const int TOTAL_SEED = 100_000;

    private static async Task SeedData() {
        using var db = new BloggingContext();

        var faker = new Faker("en");

        //parent records
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

        //loop and add children
        ProgressCounter progress = new();
        Random random = new();
        foreach (var blog in blogs) {
            if (!await db.Posts.AnyAsync(x => x.BlogId == blog.BlogId)) {
                for (int i=0; i<TOTAL_SEED; i++) {
                    await db.Posts.AddAsync(new() {
                        BlogId = blog.BlogId,
                        Title = faker.Lorem.Sentence(wordCount: 6),
                        Content = faker.Lorem.Paragraph(),
                        RandomNumber = random.Next(10)
                    });
                    //terminal progress bar
                    await progress.Show(TOTAL_SEED * blogs.Count);
                }
            }
        }
        //commit
        await db.SaveChangesAsync();
    }

    public static async Task Run() {
        try {
            await SeedData();
        } catch (Exception ex) {
            Console.WriteLine($"Could not execute Run(): {ex.Message}");
        }
    }
}
