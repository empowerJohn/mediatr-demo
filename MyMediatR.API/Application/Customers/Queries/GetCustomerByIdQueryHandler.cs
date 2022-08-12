using MediatR;
using MyMediatR.Data.Entities;
using MyMediatR.Data.Repositories;

namespace MyMediatR.API.Application.Customers.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _repository;
    private readonly ICachedCustomers _cache;
    private readonly ILogger<GetCustomerByIdQueryHandler> _logger;

    public GetCustomerByIdQueryHandler(
        ICustomerRepository repository,
        ICachedCustomers cache,
        ILogger<GetCustomerByIdQueryHandler> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedCustomer = _cache.GetCustomerById(request.CustomerId);
        if (cachedCustomer != null)
        {
            _logger.LogInformation($"Getting Customer {request.CustomerId} from Cache");
            return cachedCustomer;
        }

        var customer = await _repository.GetCustomerByIdAsync(request.CustomerId);
        if (customer != null)
        {
            _cache.CacheCustomer(customer);
        }

        return customer;
    }
}
