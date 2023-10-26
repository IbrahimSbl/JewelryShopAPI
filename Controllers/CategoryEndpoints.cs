using Microsoft.EntityFrameworkCore;
using JewelryShopAPI.Data;
using JewelryShopManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace JewelryShopAPI.Controllers;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Category").WithTags(nameof(Category));

        group.MapGet("/", async (JewelryShopAPIContext db) =>
        {
            return await db.Category.ToListAsync();
        })
        .WithName("GetAllCategories")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Category>, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            /*
            return await db.Category.AsNoTracking()
                .Include(Category => Category.JewelryItems)
                .FirstOrDefaultAsync(model => model.Id == id)
                is Category model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
            */
            var categoryWithItems = await db.Category
                .AsNoTracking()
                .Include(category => category.JewelryItems)
                .FirstOrDefaultAsync(model => model.Id == id);

            var categoryDto = new Category
            {
                Id = categoryWithItems.Id,
                Name = categoryWithItems.Name,
                CategoryPicture = categoryWithItems.CategoryPicture,
                JewelryItems = categoryWithItems.JewelryItems.Select(x => new JewelryItem
                {
                    // Map JewelryItem properties to JewelryItemDto
                    Id = x.Id,
                    Name = x.Name,
                    Discount = x.Discount,
                    Metal = x.Metal,
                    Type = x.Type,
                    CostPrice = x.CostPrice,
                    StockQuantity = x.StockQuantity,
                    SellingPrice = x.SellingPrice,
                    Weight = x.Weight,
                    Gemstones = x.Gemstones,
                    JewelryPicture = x.JewelryPicture,
                    Category = null
                }).ToList()
            };
            return categoryWithItems != null
                ? TypedResults.Ok(categoryDto)
                : TypedResults.NotFound();
        })
        .WithName("GetCategoryById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Category category, JewelryShopAPIContext db) =>
        {
            Console.WriteLine("************ Request to update **************");
            var affected = await db.Category
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Name, category.Name)
                  .SetProperty(m => m.CategoryPicture , category.CategoryPicture)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCategory")
        .WithOpenApi();

        group.MapPost("/", async (Category category, JewelryShopAPIContext db) =>
        {
            db.Category.Add(category);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Category/{category.Id}",category);
        })
        .WithName("CreateCategory")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            var affected = await db.Category
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCategory")
        .WithOpenApi();
    }
}
