using GoldTrading.Models;
using GoldTrading.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services
{
    public class GoldOrderValidator
    {
        private readonly ICustomerService _customerService;
        private readonly IMarketService _marketService;

        public GoldOrderValidator(ICustomerService customerService, IMarketService marketService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
        }

        public ValidationResultModel Validate(OrderModel order)
        {
            var result = new ValidationResultModel();

            if (order == null)
            {
                result.Errors.Add("คำสั่งซื้อไม่สามารถค่าว่างได้");
                //result.Errors.Add("Order cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(order.CustomerId))
            {
                result.Errors.Add("รหัสลูกค้าไม่สามารถค่าว่างได้");
                //result.Errors.Add("CustomerID cannot be null.");
                return result;
            }

            // Rule 1: Order type must be either "buy" or "sell"
            bool isBuyOrder = string.Equals(order.OrderType?.ToLower(), "buy", StringComparison.OrdinalIgnoreCase);
            bool isSellOrder = string.Equals(order.OrderType?.ToLower(), "sell", StringComparison.OrdinalIgnoreCase);

            if (!isBuyOrder && !isSellOrder)
            {
                result.Errors.Add("ประเภทคำสั่งซื้อต้องเป็น 'ซื้อ' หรือ 'ขาย'");
                //result.Errors.Add("Order type must be 'buy' or 'sell'.");
            }

            // Rule 2: Quantity must be positive and in valid increments (multiples of 0.5)
            if (order.Quantity <= 0)
            {
                result.Errors.Add("ปริมาณต้องมากกว่าศูนย์");
                //result.Errors.Add("Quantity must be greater than zero.");
            }
            else if (order.Quantity % 0.5m != 0)
            {
                result.Errors.Add("ปริมาณต้องเป็นหน่วยที่ถูกต้อง (เช่น 0.5 บาท)");
                //result.Errors.Add("Quantity must be in valid increments (multiples of 0.5 baht-weight).");
            }

            // Rule 3: Quoted price must be positive
            if (order.QuotedPrice <= 0)
            {
                result.Errors.Add("ราคาที่เสนอต้องมากกว่าศูนย์");
                //result.Errors.Add("Quoted price must be greater than zero.");
            }

            // ดำเนินการต่อเมื่อข้อมูลพื้นฐานถูกต้องเท่านั้นเพื่อป้องกัน Error คลาดเคลื่อน
            if (!result.IsValid) return result;

            // Rule 4: For buy orders, customer balance must be sufficient
            if (isBuyOrder)
            {
                decimal availableBalance = _customerService.GetAvailableBalance(order.CustomerId);
                decimal totalRequired = order.Quantity * order.QuotedPrice;

                if (availableBalance < totalRequired)
                {
                    result.Errors.Add($"ยอดเงินไม่เพียงพอ ต้องใช้: {totalRequired}, คงเหลือ: {availableBalance}");
                }
            }

            // Rule 5: Price freshness (within 2% of the current market price)
            decimal currentMarketPrice = _marketService.GetCurrentMarketPrice();

            // สูตรคำนวณ % ความต่าง: |Current - Quoted| / Current
            decimal priceDifference = Math.Abs(currentMarketPrice - order.QuotedPrice);
            decimal percentageDifference = priceDifference / currentMarketPrice;

            if (percentageDifference > 0.02m)
            {
                result.Errors.Add("ราคาที่แจ้งไว้เป็นราคาปัจจุบัน และมีค่าเบี่ยงเบนจากราคาตลาดปัจจุบันเกินกว่า 2% ซึ่งเป็นไปตามที่อนุญาต");
                //result.Errors.Add("Quoted price is stale. It exceeds the 2% allowed deviation from the current market price.");
            }

            return result;
        }
    }
}
