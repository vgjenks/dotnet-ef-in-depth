using dotnet_ef_in_depth;
using Microsoft.EntityFrameworkCore;

//seed if needed
await Seed.Run();

using var db = new BloggingContext();

//both types in question
int[] arrayFilters = {0, 3, 8, 2};
List<int> listFilters = [0, 3, 8, 2];

//pull using Contains
// var query = db.Posts
//     .TagWith("Counts with Contains filter")
//     .Where(x => arrayFilters.Contains(x.RandomNumber));
var query = db.Posts
    .TagWith("Counts with Contains filter")
    .Where(x => listFilters.Contains(x.RandomNumber));

//SQL output w/ tag
Console.WriteLine(query.ToQueryString());

//exec query
int totalCount = await db.Posts.CountAsync();
var randoPosts = await query.ToListAsync();

Console.WriteLine($"Total count: {totalCount}, Filtered count: {randoPosts.Count}");