using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Common.IocTags;
using WY.Entities.Account;

namespace WY.Application.Customers
{
    public interface ICustomerService 
    {
        Task<Customer> GetCusotmerAsync(string customerNo, string password);

        Task<List<Customer>> GetCustomersAsync();
    }
}
