using MyMediatR.Data.Entities;

namespace MyMediatR.Data.Repositories;

public  interface ICachedCustomers
{
    void CacheCustomer(Customer customer);

    Customer GetCustomerById(int customerId);
}
