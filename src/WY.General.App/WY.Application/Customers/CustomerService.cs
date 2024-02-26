using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Entities.Account;
using WY.EntityFramework.Repositories;

namespace WY.Application.Customers
{
    public class CustomerService : BaseService<Customer, int>, ICustomerService
    {
        public CustomerService(IRepository<Customer, int> repository) : base(repository)
        {

        }

        public async Task<Customer> GetCusotmerAsync(string customerNo, string password)
        {
            return await Repository.GetIQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Password == password && x.customerNo == customerNo);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Repository.GetAllListAsync();
        }

    }
}
