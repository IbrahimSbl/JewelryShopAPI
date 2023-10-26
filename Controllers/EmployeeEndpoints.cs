using Microsoft.EntityFrameworkCore;
using JewelryShopAPI.Data;
using JewelryShopManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using JewelryShopAPI.DTO;
using JewelryShopAPI.DTO.EmployeeDTO_s;

namespace JewelryShopAPI.Controllers;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Employee").WithTags(nameof(Employee));

        group.MapGet("/", async (JewelryShopAPIContext db) =>
        {
            return await db.Employee
                .Include(model => model.Purchases)
                .ToListAsync();
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();
        //Get specific employee with all his purchases and
        //for each purchase return customer and all jewelry items in that purchase 
        group.MapGet("/{id}", async Task<Results<Ok<EmployeeGetByIdDTO>, NotFound>>(int id, JewelryShopAPIContext db) =>
        {
            /*
            Employee e = await db.Employee.AsNoTracking()
                .Include(model => model.Purchases)
                    .ThenInclude(purchase => purchase.JoinTables)
                        .ThenInclude(jewelryItem => jewelryItem.JewelryItem)
                .FirstOrDefaultAsync(model => model.Id == id);
            
             if (e is Employee model){
                /*
                foreach (var purchase in e.Purchases)
                {
                    foreach (var joinTable in purchase.JoinTables)
                    {
                        // Assuming you have a DbSet for JewelryItems in your context
                        var jewelryItemFromDb = await db.JewelryItem
                            .FirstOrDefaultAsync(item => item.Id == joinTable.JewelryItemsId);

                        if (jewelryItemFromDb != null)
                        {
                            Console.WriteLine("adding jewelryItem");
                            joinTable.JewelryItem = jewelryItemFromDb;
                        }
                    }
                }
                return TypedResults.Ok(e);
            }
            else return TypedResults.NotFound();
             */
        
        var employee = await db.Employee.AsNoTracking()
                                .Include(model => model.Purchases)
                                    .ThenInclude(customer => customer.Customer)//Include customer
                                .Include(model => model.Purchases)
                                    .ThenInclude(purchase => purchase.JoinTables)
                                        .ThenInclude(joinTable => joinTable.JewelryItem)
                                .FirstOrDefaultAsync(model => model.Id == id);

        if (employee is Employee model)
        {
                // Shape the data as needed (e.g., select only the required properties)
                EmployeeGetByIdDTO result = new EmployeeGetByIdDTO
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = model.Role,
                    Salary = model.Salary,
                    Purchases = model.Purchases.Select(purchase => new PurchaseGetEmployeeByIdDTO
                    {
                        Id = purchase.Id,
                        PurchaseDate = purchase.PurchaseDate,
                        TotalAmount = purchase.TotalAmount,
                        CustomerId = purchase.CustomerId,
                        EmployeeId = purchase.EmployeeId,
                        JoinTables = purchase.JoinTables.Select(joinTable => new JewelryItemPurchaseGetEmployeeByIdDTO
                        {
                            JewelryItemsId = joinTable.JewelryItemsId,
                            PurchasesId = joinTable.PurchasesId,
                            // Include properties of the associated JewelryItem
                            JewelryItem = new JewelryItemGetEmployeeByIdDTO
                            {
                                // Include properties you want to return from JewelryItem
                                Id = joinTable.JewelryItem.Id,
                                Name = joinTable.JewelryItem.Name,
                                SellingPrice = joinTable.JewelryItem.SellingPrice
                            }
                        }).ToList(),
                        Customer =  new CustomerGetEmployeeByIdDTO
                        {
                            Id = purchase.Customer.Id,
                            FirstName = purchase.Customer.FirstName,
                            LastName = purchase.Customer.LastName
                        }
                    }).ToList()
            };
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound();
            }
        })
        .WithName("GetEmployeeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Employee employee, JewelryShopAPIContext db) =>
        {
            var affected = await db.Employee
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.FirstName, employee.FirstName)
                  .SetProperty(m => m.LastName, employee.LastName)
                  .SetProperty(m => m.Role, employee.Role)
                  .SetProperty(m => m.Salary, employee.Salary)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmployee")
        .WithOpenApi();

        group.MapPost("/", async (Employee employee, JewelryShopAPIContext db) =>
        {
            db.Employee.Add(employee);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Employee/{employee.Id}",employee);
        })
        .WithName("CreateEmployee")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, JewelryShopAPIContext db) =>
        {
            var affected = await db.Employee
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmployee")
        .WithOpenApi();
    }
}
