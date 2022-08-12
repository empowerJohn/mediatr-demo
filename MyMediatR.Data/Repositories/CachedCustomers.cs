using Microsoft.Extensions.Caching.Memory;
using MyMediatR.Data.Entities;

namespace MyMediatR.Data.Repositories;

public class CachedCustomers : ICachedCustomers
{
    private readonly IMemoryCache _customers;

    public CachedCustomers(IMemoryCache memoryCache)
    {
        _customers = memoryCache;
    }

    public void CacheCustomer(Customer customer)
    {
        _customers.Set(customer.CustomerId, customer);
    }

    public Customer GetCustomerById(int customerId)
    {
        _customers.TryGetValue<Customer>(customerId, out var customer);
        return customer;
    }
}
