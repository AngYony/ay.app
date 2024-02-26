using Microsoft.AspNetCore.Mvc;
using WY.Application.Customers;
using WY.Entities.Account;

namespace WY.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("list")]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await customerService.GetCustomersAsync();
        }
    }
}
