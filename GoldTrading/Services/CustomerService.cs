using GoldTrading.Models;
using GoldTrading.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services
{
    public class CustomerService : ICustomerService
    {
        public decimal GetAvailableBalance(string customerId)
        {
            switch (customerId)
            {
                case "000001":
                    return 100000;
                case "000002":
                    return 200000;
                case "000003":
                    return 300000;
                case "000004":
                    return 400000;
                case "000005":
                    return 500000;
                case "000006":
                    return 600000;
                case "000007":
                    return 700000;
                case "000008":
                    return 800000;
                case "000009":
                    return 900000;
                case "000010":
                    return 1000000;
                default:
                    return 0;
            }
        }

        public CustomerModel? GetCustomer(string customerId)
        {
            return MockDataCustomers().Find(x => x.CustomerId == customerId);
        }

        public static List<CustomerModel> MockupDataCustomers = new List<CustomerModel>
        {
            new CustomerModel
            {
                CustomerId = "C001",
                Name = "สมชาย ใจดี",
                Balance = 125000.50m,
                QuantityGold = 2.50m
            },
            new CustomerModel
            {
                CustomerId = "C002",
                Name = "สุดา พานทอง",
                Balance = 89000.00m,
                QuantityGold = 1.25m
            },
            new CustomerModel
            {
                CustomerId = "C003",
                Name = "อนันต์ วิริยะ",
                Balance = 250500.75m,
                QuantityGold = 5.00m
            },
            new CustomerModel
            {
                CustomerId = "C004",
                Name = "กนกวรรณ ศรีสุข",
                Balance = 45200.00m,
                QuantityGold = 0.75m
            },
            new CustomerModel
            {
                CustomerId = "C005",
                Name = "ธีรภัทร รัตนชัย",
                Balance = 999999.99m,
                QuantityGold = 10.00m
            },
            new CustomerModel
            {
                CustomerId = "C006",
                Name = "พิมพ์ชนก วัฒนากุล",
                Balance = 158000.25m,
                QuantityGold = 3.20m
            },
            new CustomerModel
            {
                CustomerId = "C007",
                Name = "ณัฐพล บุญมี",
                Balance = 72000.00m,
                QuantityGold = 0.50m
            },
            new CustomerModel
            {
                CustomerId = "C008",
                Name = "ชลธิชา เกียรติชัย",
                Balance = 305600.80m,
                QuantityGold = 6.75m
            },
            new CustomerModel
            {
                CustomerId = "C009",
                Name = "เอกชัย แสงทอง",
                Balance = 54000.00m,
                QuantityGold = 1.00m
            },
            new CustomerModel
            {
                CustomerId = "C010",
                Name = "รัตนา สุขใจ",
                Balance = 412300.40m,
                QuantityGold = 8.15m
            }
        };

        public List<CustomerModel> MockDataCustomers()
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    CustomerId = "C001",
                    Name = "สมชาย ใจดี",
                    Balance = 125000.50m,
                    QuantityGold = 2.50m
                },
                new CustomerModel
                {
                    CustomerId = "C002",
                    Name = "สุดา พานทอง",
                    Balance = 89000.00m,
                    QuantityGold = 1.25m
                },
                new CustomerModel
                {
                    CustomerId = "C003",
                    Name = "อนันต์ วิริยะ",
                    Balance = 250500.75m,
                    QuantityGold = 5.00m
                },
                new CustomerModel
                {
                    CustomerId = "C004",
                    Name = "กนกวรรณ ศรีสุข",
                    Balance = 45200.00m,
                    QuantityGold = 0.75m
                },
                new CustomerModel
                {
                    CustomerId = "C005",
                    Name = "ธีรภัทร รัตนชัย",
                    Balance = 999999.99m,
                    QuantityGold = 10.00m
                },
                new CustomerModel
                {
                    CustomerId = "C006",
                    Name = "พิมพ์ชนก วัฒนากุล",
                    Balance = 158000.25m,
                    QuantityGold = 3.20m
                },
                new CustomerModel
                {
                    CustomerId = "C007",
                    Name = "ณัฐพล บุญมี",
                    Balance = 72000.00m,
                    QuantityGold = 0.50m
                },
                new CustomerModel
                {
                    CustomerId = "C008",
                    Name = "ชลธิชา เกียรติชัย",
                    Balance = 305600.80m,
                    QuantityGold = 6.75m
                },
                new CustomerModel
                {
                    CustomerId = "C009",
                    Name = "เอกชัย แสงทอง",
                    Balance = 54000.00m,
                    QuantityGold = 1.00m
                },
                new CustomerModel
                {
                    CustomerId = "C010",
                    Name = "รัตนา สุขใจ",
                    Balance = 412300.40m,
                    QuantityGold = 8.15m
                }
            };

            return customers;
        }
    }
}
