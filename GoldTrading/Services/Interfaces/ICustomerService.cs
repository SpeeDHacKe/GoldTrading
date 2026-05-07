using GoldTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services.Interfaces
{
    public interface ICustomerService
    {
        decimal GetAvailableBalance(string customerId);
        List<CustomerModel> MockDataCustomers();
        CustomerModel? GetCustomer(string customerId);
    }
}
