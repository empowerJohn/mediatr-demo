using MediatR;
using MyMediatR.Data.Entities;

namespace MyMediatR.API.Application.Customers.Queries;

public record GetCustomerByIdQuery(int CustomerId) : IRequest<Customer>;
