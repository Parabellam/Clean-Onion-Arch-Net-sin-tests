using Customers.Common;
using MediatR;

namespace Application.Customers.GetByEmail
{
    public record GetCustomerByEmail(string Email) : IRequest<ErrorOr<CustomerResponse>>;
}
