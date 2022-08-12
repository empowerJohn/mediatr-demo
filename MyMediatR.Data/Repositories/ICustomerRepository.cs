using MyMediatR.Data.Entities;

namespace MyMediatR.Data.Repositories;

public interface ICustomerRepository
{
    Task<Customer> CreateCustomerAsync(string firstName, string lastName, CancellationToken cancellationToken);

    Task<Customer> GetCustomerByIdAsync(int customerId);
}
