using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Models
{
    // Model สำหรับรับข้อมูลคำสั่งซื้อ
    public class OrderModel
    {
        public string? CustomerId { get; set; }
        public string? OrderType { get; set; } // "buy" or "sell"
        public decimal Quantity { get; set; } // Multiples of 0.5
        public decimal QuotedPrice { get; set; }
    }

    public class OrderResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
