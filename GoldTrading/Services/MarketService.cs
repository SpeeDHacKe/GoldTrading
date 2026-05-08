using GoldTrading.Models;
using GoldTrading.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Services
{
    public class MarketService : IMarketService
    {
        // ราคาตลาดปัจจุบัน
        private static decimal _currentMarketPrice = 71560;
        private static decimal _feePercent = 0.5m;
        public decimal GetCurrentMarketPrice()
        {
            return _currentMarketPrice;
        }

        public decimal CalculateFee(decimal price)
        {
            return price * (_feePercent / 100);
        }

        // ฟังค์ชั่นเซ็ตราคาทองปัจจุบัน
        public void SetCurrentMarketPrice(decimal currentMarketPrice)
        {
            _currentMarketPrice = currentMarketPrice;
        }

        public OrderResult ProcessOrder(OrderModel? order, CustomerModel? customer)
        {
            // ป้องกัน Null Exception หากข้อมูลที่ส่งเข้ามาไม่มีค่า
            if (order == null) return new OrderResult { Success = false, Message = "ข้อมูลคำสั่งซื้อไม่ถูกต้อง (Null)" };
            if (customer == null) return new OrderResult { Success = false, Message = "ข้อมูลลูกค้าไม่ถูกต้อง (Null)" };
            if (string.IsNullOrWhiteSpace(order.CustomerId) || order.CustomerId != customer.CustomerId)
                return new OrderResult { Success = false, Message = "รหัสลูกค้าไม่ตรงกันหรือไม่ระบุ" };

            // แปลง OrderType เป็นตัวพิมพ์เล็กและลบช่องว่าง เพื่อให้ง่ายต่อการตรวจสอบ
            string orderType = (order.OrderType ?? string.Empty).Trim().ToLower();

            // 1. ตรวจสอบประเภทคำสั่งซื้อต้องเป็น "buy" หรือ "sell" (รองรับ "ซื้อ" / "ขาย")
            if (orderType != "buy" && orderType != "sell" && orderType != "ซื้อ" && orderType != "ขาย")
            {
                return new OrderResult { Success = false, Message = "ประเภทคำสั่งซื้อไม่ถูกต้อง ต้องเป็น 'buy' หรือ 'sell' เท่านั้น" };
            }

            // 2. ปริมาณต้องเป็นบวกและเพิ่มขึ้นทีละ 0.5 บาท
            if (order.Quantity <= 0)
            {
                return new OrderResult { Success = false, Message = "ปริมาณการซื้อขายต้องมากกว่า 0" };
            }
            if (order.Quantity % 0.5m != 0)
            {
                return new OrderResult { Success = false, Message = "ปริมาณการซื้อขายต้องเพิ่มขึ้นทีละ 0.5 เท่านั้น (เช่น 0.5, 1.0, 1.5)" };
            }

            // 3. ราคาที่เสนอต้องเป็นบวก
            if (order.QuotedPrice <= 0)
            {
                return new OrderResult { Success = false, Message = "ราคาที่เสนอต้องมากกว่า 0" };
            }

            // 4. ไม่สามารถสั่งซื้อสินค้าที่มีน้ำหนักรวมเกิน 5 บาทต่อวัน
            var sumBuyGold = HistoryModel.OrdersHistory.Where(x => x.CustomerId == customer.CustomerId && x.OrderType == "buy").Sum(x => x.Quantity);
            if (sumBuyGold >= 5)
            {
                return new OrderResult { Success = false, Message = $"ยอดรวมรายวันของลูกค้าเกินขีดจำกัด (หนักรวมเกิน 5 บาทต่อวัน) ยอดเหลือซื้อได้อีก {5 - sumBuyGold:N2} บาท" };
            }

            // 5. ความทันสมัยของราคา: ราคาต้องอยู่ภายใน 2% ของราคาตลาดปัจจุบัน
            decimal priceDifference = Math.Abs(order.QuotedPrice - _currentMarketPrice);
            decimal maxAllowedDifference = _currentMarketPrice * 0.02m;
            if (priceDifference > maxAllowedDifference)
            {
                return new OrderResult
                {
                    Success = false,
                    Message = $"ราคาที่เสนอ ({order.QuotedPrice:N2}) แตกต่างจากราคาตลาด ({_currentMarketPrice:N2}) เกิน 2% กรุณาอัปเดตราคา"
                };
            }

            // 4. ตรวจสอบยอดคงเหลือ (Balance สำหรับซื้อ, QuantityGold สำหรับขาย)
            decimal totalValue = order.Quantity * order.QuotedPrice;
            decimal priceTotal = 0m;
            bool isBuyOrder = orderType == "buy" || orderType == "ซื้อ";

            if (isBuyOrder)
            {
                // คำสั่งซื้อ: ยอดเงิน (Balance) ต้องเพียงพอ
                decimal currentBalance = customer.Balance ?? 0m;
                if (currentBalance < totalValue)
                {
                    return new OrderResult
                    {
                        Success = false,
                        Message = $"ยอดเงินคงเหลือไม่เพียงพอ (ต้องการ: {totalValue:N2} | เงินคงเหลือ: {currentBalance:N2})"
                    };
                }
                else
                {
                    //newBalance = currentBalance - totalValue;
                    priceTotal = totalValue + CalculateFee(totalValue);
                    customer.Balance -= priceTotal;
                    customer.QuantityGold += order.Quantity;
                }
            }
            else
            {
                // คำสั่งขาย: ปริมาณทองคำ (QuantityGold) ต้องเพียงพอ (เพิ่มเติมนอกเหนือจากโจทย์เพื่อความสมบูรณ์ของระบบ)
                decimal currentGold = customer.QuantityGold ?? 0m;
                if (currentGold < order.Quantity)
                {
                    return new OrderResult
                    {
                        Success = false,
                        Message = $"ปริมาณทองคำคงเหลือไม่เพียงพอสำหรับการขาย (ต้องการ: {order.Quantity} | ทองคงเหลือ: {currentGold})"
                    };
                }
                else
                {
                    //customer.Balance = totalValue + customer.Balance ?? 0m;
                    priceTotal = totalValue - CalculateFee(totalValue);
                    customer.Balance += priceTotal;
                    customer.QuantityGold -= order.Quantity;
                }
            }

            // หากผ่านทุกเงื่อนไข
            HistoryModel.OrdersHistory.Add(new OrderHistoryModel { CustomerId = order.CustomerId, OrderType = order.OrderType, Quantity = order.Quantity, QuotedPrice = order.QuotedPrice, TotalPrice = totalValue, CreateDate = DateTime.Now });
            return new OrderResult
            {
                Success = true,
                Message = $"คำสั่ง{(isBuyOrder ? "ซื้อ" : "ขาย")}ทองคำสำหรับ {customer.Name} \r\nจำนวน {order.Quantity} สำเร็จ \r\nที่ราคา {order.QuotedPrice:N2} \r\nค่าธรรมเนียม {CalculateFee(totalValue):N2} \r\nราคาสุทธิ {priceTotal:N2} \r\nยอดคงเหลือใหม่ {customer.Balance:N2}"
                //Message = $"ทำรายการ {(isBuyOrder ? "ซื้อ" : "ขาย")} ทองคำจำนวน {order.Quantity} สำเร็จ ที่ราคา {order.QuotedPrice:N2}"
            };
        }
    }
}
