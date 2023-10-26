using Microsoft.EntityFrameworkCore;
using JewelryShopAPI.Data;
using JewelryShopManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using JewelryShopAPI.DTO.CustomerDTO_s;

namespace JewelryShopAPI.Controllers;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        group.MapGet("/", async (JewelryShopAPIContext db) =>
        {
            return await db.Customer.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<JewelryShopAPI.DTO.CustomerDTO_s.CustomerGetByIdDTO>, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            /*
            return await db.Customer.AsNoTracking()
                .Include(model => model.Purchases)
                    .ThenInclude(model=> model.JoinTables)
                        .ThenInclude(model => model.JewelryItem)
                .FirstOrDefaultAsync(model => model.Id == id)
                is Customer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();*/
            var c = await db.Customer.AsNoTracking()
                .Include(model => model.Purchases)
                    .ThenInclude(model => model.JoinTables)
                        .ThenInclude(model => model.JewelryItem)
                .FirstOrDefaultAsync(model => model.Id == id);
            if(c is Customer model)
            {

                // Shape the data as needed (e.g., select only the required properties)
                JewelryShopAPI.DTO.CustomerDTO_s.CustomerGetByIdDTO result = new JewelryShopAPI.DTO.CustomerDTO_s.CustomerGetByIdDTO
                {
                    id = model.Id,
                    firstName = model.FirstName,
                    lastName = model.LastName,
                    email = model.Email,
                    birthdate = model.Birthdate,
                    purchases = model.Purchases.Select(purchase => new JewelryShopAPI.DTO.CustomerDTO_s.PurchaseGetCustomerById
                    {
                        id = purchase.Id,
                        purchaseDate = purchase.PurchaseDate,
                        totalAmount = purchase.TotalAmount,
                        joinTables = purchase.JoinTables.Select(joinTable => new JewelryShopAPI.DTO.CustomerDTO_s.JewelryItemPurchaseGetCustomerById
                        {
                            jewelryItemsId = joinTable.JewelryItemsId,
                            purchasesId = joinTable.PurchasesId,
                            jewelryItem = new JewelryShopAPI.DTO.CustomerDTO_s.JewelryItemGetCustomerById
                            {
                                id = joinTable.JewelryItem.Id,
                                name = joinTable.JewelryItem.Name,
                                sellingPrice = joinTable.JewelryItem.SellingPrice
                            }
                        }).ToList()
                    }).ToList()
                };
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound();
            }
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Customer customer, JewelryShopAPIContext db) =>
        {
            var affected = await db.Customer
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, customer.Id)
                  .SetProperty(m => m.FirstName, customer.FirstName)
                  .SetProperty(m => m.LastName, customer.LastName)
                  .SetProperty(m => m.Email, customer.Email)
                  .SetProperty(m => m.Birthdate, customer.Birthdate)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer customer, JewelryShopAPIContext db) =>
        {
            db.Customer.Add(customer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Customer/{customer.Id}",customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            var affected = await db.Customer
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
