using EFGetStarted.Models;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for a blog");
var blog = await db.Blogs
    .OrderBy(b => b.BlogId)
    .FirstAsync();

Console.WriteLine($"Found blog: {blog.Url}");

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(new Post { Title = "Announcing .NET 10", Content = "More content" });
await db.SaveChangesAsync();

// Delete
Console.WriteLine("Deleting the blog");
db.Remove(blog);
await db.SaveChangesAsync();
