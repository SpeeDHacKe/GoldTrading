using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Models
{
    public class CustomerModel
    {
        public string? CustomerId { get; set; }
        public string? Name { get; set; }
        public decimal? Balance { get; set; }
        public decimal? QuantityGold { get; set; }
    }

    public static class MockDatabase
    {
        // ใช้ Dictionary เพื่อให้ค้นหาลูกค้าด้วย CustomerId ได้รวดเร็ว O(1)
        public static Dictionary<string, CustomerModel> Customers { get; set; } = new()
        {
            //{ "C001", new CustomerModel { CustomerId = "C001", Name = "คุณ สมชาย", Balance = 100000m, QuantityGold = 2.0m } },
            //{ "C002", new CustomerModel { CustomerId = "C002", Name = "คุณ สมหญิง", Balance = 50000m, QuantityGold = 5.0m } }

            { "C001", 
                new CustomerModel
                {
                    CustomerId = "C001",
                    Name = "สมชาย ใจดี",
                    Balance = 125000.50m,
                    QuantityGold = 2.50m
                } },
            { "C002",
                new CustomerModel
                {
                    CustomerId = "C002",
                    Name = "สุดา พานทอง",
                    Balance = 89000.00m,
                    QuantityGold = 1.25m
                }},
        };
    }
}
