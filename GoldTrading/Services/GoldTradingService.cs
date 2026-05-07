using GoldTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services
{
    public class GoldTradingService
    {
        private decimal _currentMarketPrice;

        public GoldTradingService(decimal currentMarketPrice)
        {
            _currentMarketPrice = currentMarketPrice;
        }

        // ฟังก์ชันสำหรับอัปเดตราคาตลาด (เผื่อกรณีต้องการเปลี่ยนราคาระหว่างรันโปรแกรม)
        public void UpdateMarketPrice(decimal newPrice)
        {
            _currentMarketPrice = newPrice;
        }

        public OrderResult ProcessOrder(OrderModel? order)
        {
            if (order == null || string.IsNullOrWhiteSpace(order.CustomerId))
                return new OrderResult { Success = false, Message = "ข้อมูลคำสั่งซื้อหรือรหัสลูกค้าไม่ถูกต้อง" };

            // ค้นหาลูกค้าจาก Static Memory
            if (!MockDatabase.Customers.TryGetValue(order.CustomerId, out var customer))
            {
                return new OrderResult { Success = false, Message = "ไม่พบข้อมูลลูกค้าในระบบ" };
            }

            string orderType = (order.OrderType ?? string.Empty).Trim().ToLower();
            if (orderType != "buy" && orderType != "sell" && orderType != "ซื้อ" && orderType != "ขาย")
                return new OrderResult { Success = false, Message = "ประเภทคำสั่งซื้อไม่ถูกต้อง" };

            if (order.Quantity <= 0 || order.Quantity % 0.5m != 0)
                return new OrderResult { Success = false, Message = "ปริมาณการซื้อขายต้องเป็นบวกและเพิ่มขึ้นทีละ 0.5 เท่านั้น" };

            if (order.QuotedPrice <= 0)
                return new OrderResult { Success = false, Message = "ราคาที่เสนอต้องมากกว่า 0" };

            decimal priceDifference = Math.Abs(order.QuotedPrice - _currentMarketPrice);
            decimal maxAllowedDifference = _currentMarketPrice * 0.02m;
            if (priceDifference > maxAllowedDifference)
                return new OrderResult { Success = false, Message = $"ราคาเสนอ ({order.QuotedPrice:N2}) ห่างจากราคาตลาด ({_currentMarketPrice:N2}) เกิน 2%" };

            decimal totalValue = order.Quantity * order.QuotedPrice;
            bool isBuyOrder = orderType == "buy" || orderType == "ซื้อ";

            // แปลงค่า null เป็น 0 เพื่อใช้ในการคำนวณ
            decimal currentBalance = customer.Balance ?? 0m;
            decimal currentGold = customer.QuantityGold ?? 0m;

            if (isBuyOrder)
            {
                if (currentBalance < totalValue)
                    return new OrderResult { Success = false, Message = $"ยอดเงินไม่พอ (ต้องการ: {totalValue:N2}, มี: {currentBalance:N2})" };

                // 2. อัปเดตข้อมูลลูกค้าใน Memory (ซื้อ: เงินลดลง, ทองเพิ่มขึ้น)
                customer.Balance = currentBalance - totalValue;
                customer.QuantityGold = currentGold + order.Quantity;
            }
            else
            {
                if (currentGold < order.Quantity)
                    return new OrderResult { Success = false, Message = $"ทองคำไม่พอขาย (ต้องการ: {order.Quantity}, มี: {currentGold})" };

                // 3. อัปเดตข้อมูลลูกค้าใน Memory (ขาย: เงินเพิ่มขึ้น, ทองลดลง)
                customer.Balance = currentBalance + totalValue;
                customer.QuantityGold = currentGold - order.Quantity;
            }

            return new OrderResult
            {
                Success = true,
                Message = $"ทำรายการ {(isBuyOrder ? "ซื้อ" : "ขาย")} สำเร็จ! เงินคงเหลือ: {customer.Balance:N2} บาท, ทองคำคงเหลือ: {customer.QuantityGold} บาททอง"
            };
        }
    }
}
