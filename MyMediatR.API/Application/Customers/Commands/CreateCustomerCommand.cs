using MediatR;
using MyMediatR.Data.Entities;

namespace MyMediatR.API.Application.Customers.Commands;

public record CreateCustomerCommand(string FirstName, string LastName) : IRequest<Customer>;
