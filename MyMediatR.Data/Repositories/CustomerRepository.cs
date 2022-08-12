using Microsoft.EntityFrameworkCore;
using MyMediatR.Data.Context;
using MyMediatR.Data.Entities;

namespace MyMediatR.Data.Repositories;

public class CustomersRepository : ICustomerRepository
{
    private readonly AppDbContext _db;

    public CustomersRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Customer> CreateCustomerAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName
        };

        await _db.Customers.AddAsync(customer, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _db.Customers.SingleOrDefaultAsync(x => x.CustomerId == customerId);
        return customer;
    }
}
