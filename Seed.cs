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
    private static async Task SeedData() {

        using var db = new CommerceContext();

        var faker = new Faker("en");

        //Customers
        if (!await db.Customer.AnyAsync()) {
            Console.WriteLine("...Seeding data, please wait.");

            await db.Customer.AddAsync(new() {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName()
            });
            await db.Customer.AddAsync(new() {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName()
            });
            await db.Customer.AddAsync(new() {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName()
            });
            await db.SaveChangesAsync();
        }

        //Items
        if (!await db.Item.AnyAsync()) {
            for (int x = 0; x < 20; x++) {
                await db.Item.AddAsync(new() {
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Lorem.Paragraph(),
                    Price = faker.Finance.Amount(1, 1000)
                });
            }
            await db.SaveChangesAsync();
        }

        //Orders and OrderItems
        if (!await db.Order.AnyAsync()) {
            List<Item> allItems = await db.Item.ToListAsync();

            foreach (var customer in await db.Customer.ToListAsync()) {
                List<OrderItem> orderItems = new();
                Random random = new();
                int randomItemIdx = random.Next(1, 20);
                int randomQty = random.Next(1, 5);
                List<Item> randomItems = allItems.OrderBy(o => randomItemIdx).Take(randomItemIdx).ToList();
                foreach (var item in randomItems) {
                    OrderItem orderItem = new() {
                        Item = item,
                        Quantity = faker.Random.Int(min: 1, max: 8)
                    };
                    await db.OrderItem.AddAsync(orderItem);
                    orderItems.Add(orderItem);
                }
                Order order = new() {
                    Customer = customer,
                    OrderItems = orderItems
                };
                await db.Order.AddAsync(order);
            }

            await db.SaveChangesAsync();
            Console.WriteLine("Seeding complete!");
        }
    }

    public static async Task Run() {
        try {
            await SeedData();
        }
        catch (Exception ex) {
            Console.WriteLine($"Could not execute Run(): {ex.Message}");
        }
    }
}
