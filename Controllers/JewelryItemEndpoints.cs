using JewelryShopAPI.Data;
using JewelryShopManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JewelryShopAPI.Controllers;

public static class JewelryItemEndpoints

{
	public static async void MapJewelryItemEndpoints(this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/JewelryItem").WithTags(nameof(JewelryItem));

		group.MapGet("/", async (JewelryShopAPIContext db) =>
		{
			//return await db.JewelryItem.ToListAsync();
			return await db.JewelryItem
				.Select(x => new JewelryItem
				{
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
					Category = (x.Category == null) ? null : new Category
					{
						Id = x.Category.Id,
						Name = x.Category.Name
					}
				})
				.ToListAsync();
		})
		.WithName("GetAllJewelryItems")
		.WithOpenApi();

		group.MapGet("/Id", async (JewelryShopAPIContext db) =>
		{
			// Get the largest ID among all jewelry items
			int largestId = await db.JewelryItem.MaxAsync(x => x.Id);

			// Return the largest ID as an int
			return largestId;
		})
	   .WithName("GetLargestJewelryItemId")
	   .WithOpenApi();

		group.MapGet("/{id}", async Task<Results<Ok<JewelryItem>, NotFound>> (int id, JewelryShopAPIContext db) =>
		{
			/*
            return await db.JewelryItem
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is JewelryItem model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
            */
			return await db.JewelryItem
			.Where(x => x.Id == id)
			.Select(x => new JewelryItem
			{
				Id = x.Id,
				Name = x.Name,
				Metal = x.Metal,
				Discount = x.Discount,
				Type = x.Type,
				CostPrice = x.CostPrice,
				StockQuantity = x.StockQuantity,
				SellingPrice = x.SellingPrice,
				Weight = x.Weight,
				Gemstones = x.Gemstones,
				JewelryPicture = x.JewelryPicture,
				Category = (x.Category == null) ? null : new Category
				{
					Id = x.Category.Id,
					Name = x.Category.Name
				}
			}).FirstOrDefaultAsync()
			is JewelryItem model
			   ? TypedResults.Ok(model)
			   : TypedResults.NotFound();

		})
		.WithName("GetJewelryItemById")
		.WithOpenApi();
		group.MapGet("/JewelryItemsWithDiscount", async (JewelryShopAPIContext db) =>
		{
			return await db.JewelryItem
					.Where(model => model.Discount > 0)
					.Select(x => new JewelryItem
					{
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
						Category = (x.Category == null) ? null : new Category
						{
							Id = x.Category.Id,
							Name = x.Category.Name
						}
					})
				.ToListAsync();

		})
			.WithName("JewelryItemsWithDiscount")
			.WithOpenApi();

		group.MapGet("/associateCategory/{jid}/{cid}", async (int jid, int cid, JewelryShopAPIContext db) =>
		{
			// Load the existing JewelryItem and Category from your database
			var existingJewelryItem = await db.JewelryItem
				.Include(j => j.Category) // Load the related categories
				.FirstOrDefaultAsync(model => model.Id == jid);
			var existingCategory = await db.Category
				.FirstOrDefaultAsync(model => model.Id == cid);

			if (existingJewelryItem == null || existingCategory == null)
			{
				// Handle not found scenarios
				Console.WriteLine("---------------JewelryItem or Category not found------------------");
				return;
			}
			existingJewelryItem.Category = existingCategory;
			await db.SaveChangesAsync();

			// Optionally, return the updated JewelryItem or a success response
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("Jewelry Item ID : " + existingJewelryItem.Id);
			Console.WriteLine("Category Id : " + existingCategory.Id);
			Console.WriteLine("---------------- Done ------------------");
		})
		.WithName("AddCategoryToJewelryItemById")
		.WithOpenApi(); ;
		/*
        group.MapGet("/{id}", async Task<Results<Ok<JewelryItem>, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            return await db.JewelryItem
                .Include(jewelryItem => jewelryItem.Categories) // Include related categories
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is JewelryItem model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetJewelryItemANDCategoryById")
        .WithOpenApi();
        */
		/*
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryItem jewelryItem, JewelryShopAPIContext db) =>
        {
            var affected = await db.JewelryItem
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, jewelryItem.Id)
                  .SetProperty(m => m.Name, jewelryItem.Name)
                  .SetProperty(m => m.Type, jewelryItem.Type)
                  .SetProperty(m => m.Metal, jewelryItem.Metal)
                  .SetProperty(m => m.Gemstones, jewelryItem.Gemstones)
                  .SetProperty(m => m.Weight, jewelryItem.Weight)
                  .SetProperty(m => m.CostPrice, jewelryItem.CostPrice)
                  .SetProperty(m => m.SellingPrice, jewelryItem.SellingPrice)
                  .SetProperty(m => m.StockQuantity, jewelryItem.StockQuantity)
                  .SetProperty(m => m.Category , jewelryItem.Category)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateJewelryItem")
        .WithOpenApi();
        */
		group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryItem jewelryItem, JewelryShopAPIContext db) =>
		{
			Console.WriteLine("--------------------");
			if (jewelryItem.Category != null)
			{
				Console.WriteLine("Category id ===== " + jewelryItem.Category.Id);
			}
			var existingItem = await db.JewelryItem.FirstOrDefaultAsync(item => item.Id == id);

			if (existingItem == null)
			{
				return TypedResults.NotFound();
			}

			// Update the properties of the existing item
			existingItem.Name = jewelryItem.Name;
			existingItem.Type = jewelryItem.Type;
			existingItem.Discount = jewelryItem.Discount;
			existingItem.Metal = jewelryItem.Metal;
			existingItem.Gemstones = jewelryItem.Gemstones;
			existingItem.Weight = jewelryItem.Weight;
			existingItem.CostPrice = jewelryItem.CostPrice;
			existingItem.SellingPrice = jewelryItem.SellingPrice;
			existingItem.StockQuantity = jewelryItem.StockQuantity;
			existingItem.Category = jewelryItem.Category;
			existingItem.JewelryPicture = jewelryItem.JewelryPicture;
			var affected = await db.SaveChangesAsync();

			return affected >= 1 ? TypedResults.Ok() : TypedResults.NotFound();
		})
		.WithName("UpdateJewelryItem")
		.WithOpenApi();

		group.MapPost("/", async (JewelryItem jewelryItem, JewelryShopAPIContext db) =>
		{
			Category existingCategory = await db.Category
										.FirstOrDefaultAsync(model => model.Id == jewelryItem.Category.Id);
			JewelryItem jewelryItemDemoWithoutId = new JewelryItem
			{
				Name = jewelryItem.Name,
				Metal = jewelryItem.Metal,
				Discount = jewelryItem.Discount,
				Type = jewelryItem.Type,
				CostPrice = jewelryItem.CostPrice,
				StockQuantity = jewelryItem.StockQuantity,
				SellingPrice = jewelryItem.SellingPrice,
				Weight = jewelryItem.Weight,
				Gemstones = jewelryItem.Gemstones,
				JewelryPicture = jewelryItem.JewelryPicture,
				Category = existingCategory
			};
			db.JewelryItem.Add(jewelryItemDemoWithoutId);
			await db.SaveChangesAsync();
			/*
			var existingJewelryItem = await db.JewelryItem
                .OrderBy(model => model.Id)
                .LastOrDefaultAsync(model => model.Name == jewelryItem.Name && model.CostPrice == jewelryItem.CostPrice);
			return TypedResults.Created($"/api/JewelryItem/associateCategory/{existingJewelryItem.Id}/{jewelryItem.Category.Id}",jewelryItem);
            */
			//Console.WriteLine("************************ Category id = "+jewelryItem.Category.Id);
			return TypedResults.Created($"/api/JewelryItem/{jewelryItem.Id}", jewelryItem);
		})
		.WithName("CreateJewelryItem")
		.WithOpenApi();

		group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryShopAPIContext db) =>
		{
			var affected = await db.JewelryItem
				.Where(model => model.Id == id)
				.ExecuteDeleteAsync();

			return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
		})
		.WithName("DeleteJewelryItem")
		.WithOpenApi();
	}
}
