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

            { "C001", new CustomerModel
                {
                    CustomerId = "C001",
                    Name = "สมชาย ใจดี",
                    Balance = 125000.50m,
                    QuantityGold = 2.50m
                }
            },
            { "C002", new CustomerModel
                {
                    CustomerId = "C002",
                    Name = "สุดา พานทอง",
                    Balance = 89000.00m,
                    QuantityGold = 1.25m
                }
            },
            { "C003", new CustomerModel
                {
                    CustomerId = "C003",
                    Name = "อนันต์ วิริยะ",
                    Balance = 250500.75m,
                    QuantityGold = 5.00m
                }
            },
            { "C004", new CustomerModel
                {
                    CustomerId = "C004",
                    Name = "กนกวรรณ ศรีสุข",
                    Balance = 45200.00m,
                    QuantityGold = 0.75m
                }
            },
            { "C005", new CustomerModel
                {
                    CustomerId = "C005",
                    Name = "ธีรภัทร รัตนชัย",
                    Balance = 999999.99m,
                    QuantityGold = 10.00m
                }
            },
            { "C006", new CustomerModel
                {
                    CustomerId = "C006",
                    Name = "พิมพ์ชนก วัฒนากุล",
                    Balance = 158000.25m,
                    QuantityGold = 3.20m
                }
            },
            { "C007", new CustomerModel
                {
                    CustomerId = "C007",
                    Name = "ณัฐพล บุญมี",
                    Balance = 72000.00m,
                    QuantityGold = 0.50m
                }
            },
            { "C008", new CustomerModel
                {
                    CustomerId = "C008",
                    Name = "ชลธิชา เกียรติชัย",
                    Balance = 305600.80m,
                    QuantityGold = 6.75m
                }
            },
            { "C009", new CustomerModel
                {
                    CustomerId = "C009",
                    Name = "เอกชัย แสงทอง",
                    Balance = 54000.00m,
                    QuantityGold = 1.00m
                }
            },
            { "C010", new CustomerModel
                {
                    CustomerId = "C010",
                    Name = "รัตนา สุขใจ",
                    Balance = 412300.40m,
                    QuantityGold = 8.15m
                }
            },
        };
    }
}
