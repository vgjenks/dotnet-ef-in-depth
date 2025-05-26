using dotnet_ef_in_depth;
using Microsoft.EntityFrameworkCore;

//seed if needed
await Seed.Run();

using var db = new CommerceContext();

//both types in question
int[] arrayFilters = { 0, 3, 8, 2 };
List<int> listFilters = [0, 3, 8, 2];

//pull using Contains
// var filteredData = db.OrderItem
//     .TagWith("Counts with Contains filter")
//     .Where(x => arrayFilters.Contains(x.Quantity));
var filteredData = db.OrderItem
    .TagWith("Counts with Contains filter")
    .Where(x => EF.Constant(listFilters).Contains(x.Quantity));
// var filteredData = db.OrderItem
//     .TagWith("Counts with Contains filter")
//     .Where(x => listFilters.Contains(x.Quantity));

//SQL output w/ tag
Console.WriteLine(filteredData.ToQueryString());

//exec query
int totalCount = await db.OrderItem.CountAsync();
var randoPosts = await filteredData.ToListAsync();

Console.WriteLine($"Total count: {totalCount}, Filtered count: {randoPosts.Count}");

//generate a CTE with EF using a base query
var baseQuery = db.Order;
var orderTotalQuery = baseQuery
    .Include(o => o.OrderItems)
    .Include(o => o.Customer)
    .Select(x => new {
        x.Id,
        x.Customer.FirstName,
        x.Customer.LastName,
        x.OrderItems.Count
    })
    .Where(x => x.Count > 3);
Console.WriteLine(orderTotalQuery.ToQueryString());
var result = orderTotalQuery.ToList();