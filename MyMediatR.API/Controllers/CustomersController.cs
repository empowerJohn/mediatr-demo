using Microsoft.AspNetCore.Mvc;
using MyMediatR.API.Models;
using MyMediatR.Data.Repositories;

namespace MyMediatR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly ICachedCustomers _cache;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(
        ICustomerRepository customersRepository,
        ICachedCustomers cachedCustomers,
        ILogger<CustomersController> logger)
    {
        _repository = customersRepository;
        _cache = cachedCustomers;
        _logger = logger;
    }

    [HttpPost()]
    public async Task<IActionResult> PostAsync([FromBody] CreateCustomer customer, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Before execution [CreateCustomerAsync]");

        var result = await _repository.CreateCustomerAsync(customer.FirstName, customer.LastName, cancellationToken);

        _logger.LogInformation("After execution [CreateCustomerAsync]");

        return result != null
            ? Ok(result)
            : BadRequest();
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int customerId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Before execution [GetCustomerByIdAsync]");

        var cachedCustomer = _cache.GetCustomerById(customerId);
        if (cachedCustomer != null)
        {
            _logger.LogInformation($"Getting Customer {customerId} from Cache");
            _logger.LogInformation("After execution [GetCustomerByIdAsync]");

            return Ok(cachedCustomer);
        }

        var customer = await _repository.GetCustomerByIdAsync(customerId);
        if (customer != null)
        {
            _cache.CacheCustomer(customer);
        }

        _logger.LogInformation("After execution [GetCustomerByIdAsync]");

        return customer != null
            ? Ok(customer)
            : NotFound();
    }
}
