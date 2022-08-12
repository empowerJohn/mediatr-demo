using MediatR;
using MyMediatR.Data.Entities;
using MyMediatR.Data.Repositories;

namespace MyMediatR.API.Application.Customers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.CreateCustomerAsync(request.FirstName, request.LastName, cancellationToken);

        return customer;
    }
}
