using GoldTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services.Interfaces
{
    public interface IMarketService
    {
        decimal GetCurrentMarketPrice();
        decimal CalculateFee(decimal price);
        void SetCurrentMarketPrice(decimal currentMarketPrice);
        OrderResult ProcessOrder(OrderModel? order, CustomerModel? customer);
    }
}
