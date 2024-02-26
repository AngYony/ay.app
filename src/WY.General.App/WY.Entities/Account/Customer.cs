using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Entities.Account
{
    public class Customer
    {
        public int Id { get; set; }
        public string customerNo { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
    }
}
