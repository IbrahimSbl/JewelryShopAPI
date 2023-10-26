using Microsoft.EntityFrameworkCore;
using JewelryShopAPI.Data;
using JewelryShopManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using System.Security.Cryptography;

namespace JewelryShopAPI.Controllers;

public static class PurchaseEndpoints
{
    public static void MapPurchaseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Purchase").WithTags(nameof(Purchase));

        group.MapGet("/", async (JewelryShopAPIContext db) =>
        {
            return await db.Purchase.ToListAsync();
        })
        .WithName("GetAllPurchases")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Purchase>, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            return await db.Purchase.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Purchase model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPurchaseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Purchase purchase, JewelryShopAPIContext db) =>
        {
            var affected = await db.Purchase
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, purchase.Id)
                  .SetProperty(m => m.PurchaseDate, purchase.PurchaseDate)
                  .SetProperty(m => m.TotalAmount, purchase.TotalAmount)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePurchase")
        .WithOpenApi();

        group.MapPost("/", async (Purchase purchase, JewelryShopAPIContext db) =>
        {
            db.Purchase.Add(purchase);
            await db.SaveChangesAsync();
            
            return TypedResults.Created($"/api/Purchase/{purchase.Id}",purchase);
        })
        .WithName("CreatePurchase")
        .WithOpenApi();
        group.MapPost("associatePurchase/{id}", async Task<Results<Ok, NotFound>> (int id, Purchase purchase, JewelryShopAPIContext db) =>
        {
            Console.WriteLine("*************** Inside Second call *****************");
            // Load the existing employee and customer from your database
            var existingPurchase = await db.Purchase
                .Include(j => j.Employee) // Load the related Employees
                .Include(j => j.Customer) // Load the related Customers
                //.Include(j => j.JewelryItems) // Load the related JewelryItems
                .FirstOrDefaultAsync(model => model.Id == id);
            // Attach Customer and Employee entities if they exist in the database
            var existingCustomer = await db.Customer
                                            .FirstOrDefaultAsync(model => model.Id == purchase.Customer.Id);
            var existingEmployee = await db.Employee
                                            .FirstOrDefaultAsync(model => model.Id == purchase.Employee.Id);
            
            
            if (existingPurchase == null || existingCustomer == null|| existingEmployee == null)
            {
                // Handle not found scenarios
                Console.WriteLine("---------------Purchase or Customer or Employee not found------------------");
                return TypedResults.NotFound();
            }
            purchase.Customer = existingCustomer;
            purchase.Employee = existingEmployee;
            var affected = await db.SaveChangesAsync();

            return affected >= 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("Append")
        .WithOpenApi();
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            var affected = await db.Purchase
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePurchase")
        .WithOpenApi();
    }
}
