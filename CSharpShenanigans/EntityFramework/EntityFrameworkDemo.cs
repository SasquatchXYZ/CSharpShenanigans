using Microsoft.EntityFrameworkCore;

namespace CSharpShenanigans.EntityFramework;

public class EntityFrameworkDemo
{
    public async Task TestCrudDemo()
    {
        using var db = new AppDbContext();

        // Create
        Console.WriteLine("Inserting a new product");
        db.Add(new Product { Name = "Apple", Price = 0.50m });
        await db.SaveChangesAsync();

        // Read
        Console.WriteLine("Querying for a product");
        var product = await db.Products.FirstOrDefaultAsync(p => p.Name == "Apple");

        if (product is null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine($"Found product: {product.Name} with price {product.Price}");

        // Update
        Console.WriteLine("Updating the product");
        product.Price = 0.75m;
        await db.SaveChangesAsync();

        // Delete
        Console.WriteLine("Deleting the product");
        db.Remove(product);
        await db.SaveChangesAsync();
    }

    public async Task TestChangeTrackingDemo()
    {
        using var db = new AppDbContext();

        // Create a new product instance
        var newProduct = new Product { Name = "Banana", Price = 0.30m };

        // Add the product to the context
        db.Products.Add(newProduct);

        // Inspect the change tracker entries before saving
        Console.WriteLine("Before SaveChanges:");
        foreach (var entry in db.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }

        // Save change to the database
        await db.SaveChangesAsync();

        // Inspect the change tracker entries after saving
        Console.WriteLine("\nAfter SaveChanges:");
        foreach (var entry in db.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }

        // Modify the product
        newProduct.Price = 0.35m;

        // Inspect the change tracker after modification
        Console.WriteLine("\nAfter modification:");
        foreach (var entry in db.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }

        // Save the updated product
        await db.SaveChangesAsync();

        // Delete the product
        db.Products.Remove(newProduct);

        // Inspect the change tracker after deletion
        Console.WriteLine("\nAfter deletion:");
        foreach (var entry in db.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }

        // Save the deletion
        await db.SaveChangesAsync();
    }
}
