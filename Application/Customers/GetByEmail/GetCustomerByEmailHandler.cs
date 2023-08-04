using Customers.Common;
using Domain.Customers;

namespace Application.Customers.GetByEmail
{
    internal sealed class GetCustomerByEmailHandler : IRequestHandler<GetCustomerByEmail, ErrorOr<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByEmailHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByEmail query, CancellationToken cancellationToken)
        {
            if (await _customerRepository.GetByEmailAsync(query.Email) is not Customer customer)
            {
                return Error.NotFound("Customer.NotFound", "The customer with the provided email was not found.");
            }

            return new CustomerResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                customer.Password,
                customer.PhoneNumber.Value,
                new AddressResponse(
                    customer.Address.Country,
                    customer.Address.Line1,
                    customer.Address.Line2,
                    customer.Address.City,
                    customer.Address.State,
                    customer.Address.ZipCode),
                customer.Active);
        }
    }
}
