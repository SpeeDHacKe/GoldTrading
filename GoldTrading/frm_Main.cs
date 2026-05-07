using GoldTrading.Models;
using GoldTrading.Services;
using GoldTrading.Services.Interfaces;
using System.Text;

namespace GoldTrading
{
    public partial class frm_Main : Form
    {
        private readonly ICustomerService _customerService;
        private readonly IMarketService _marketService;
        private readonly GoldOrderValidator _goldOrderValidator;
        public frm_Main()
        {
            InitializeComponent();
            _customerService = new CustomerService() ?? throw new ArgumentNullException(nameof(CustomerService));
            _marketService = new MarketService() ?? throw new ArgumentNullException(nameof(MarketService));
            _goldOrderValidator = new GoldOrderValidator(_customerService, _marketService) ?? throw new ArgumentNullException(nameof(GoldOrderValidator));
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            txt_CurrentMarketPrice.Text = _marketService.GetCurrentMarketPrice().ToString();
            cmb_OrderType.DataSource = new[] { new { name = "ซื้อ", value = "buy" }, new { name = "ขาย", value = "sell" } };
            //cmb_OrderType.DataSource = new List<dynamic> { new { Text = "ซื้อ", Value = "buy" }, new { Text = "Sell", Value = "sell" } };
            cmb_OrderType.DisplayMember = "name";
            cmb_OrderType.ValueMember = "value";
            txt_Quantity.Text = "0";
            txt_QuotedPrice.Text = "0";
        }

        public string InsertBreaks(string text, int interval = 25)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(text[i]);

                if ((i + 1) % interval == 0)
                {
                    sb.Append('\u200B');
                }
            }

            return sb.ToString();
        }

        private void btn_NewOrder_Click(object sender, EventArgs e)
        {
            var tradingService = new GoldTradingService(40000m);

            Console.WriteLine("--- ข้อมูลก่อนทำรายการ ---");
            var customer = MockDatabase.Customers["C001"];
            Console.WriteLine($"ลูกค้า: {customer.Name}, เงิน: {customer.Balance:N2}, ทอง: {customer.QuantityGold}");
            Console.WriteLine("--------------------------\n");

            // รายการที่ 1: ซื้อทอง 1.0 บาท ที่ราคา 39,800
            var order1 = new OrderModel { CustomerId = "C001", OrderType = "buy", Quantity = 1.0m, QuotedPrice = 39800m };
            Console.WriteLine(">> ส่งคำสั่งซื้อ 1.0 บาททอง...");
            var result1 = tradingService.ProcessOrder(order1);
            Console.WriteLine(result1.Message + "\n");

            // รายการที่ 2: ขายทอง 0.5 บาท ที่ราคา 40,100
            var order2 = new OrderModel { CustomerId = "C001", OrderType = "sell", Quantity = 0.5m, QuotedPrice = 40100m };
            Console.WriteLine(">> ส่งคำสั่งขาย 0.5 บาททอง...");
            var result2 = tradingService.ProcessOrder(order2);
            Console.WriteLine(result2.Message + "\n");

            // รายการที่ 3: พยายามซื้อทองเกินตัวเงินที่มีอยู่ (เงินเหลือประมาณ 80,000 จะซื้อ 5.0 บาท = 200,000)
            var order3 = new OrderModel { CustomerId = "C001", OrderType = "buy", Quantity = 5.0m, QuotedPrice = 40000m };
            Console.WriteLine(">> ส่งคำสั่งซื้อ 5.0 บาททอง (เกินงบ)...");
            var result3 = tradingService.ProcessOrder(order3);
            Console.WriteLine(result3.Message + "\n");
        }

        //private void btn_NewOrder_Click(object sender, EventArgs e)
        //{
        //    var reqData = new OrderModel { CustomerId = txt_CustomerID.Text, OrderType = cmb_OrderType.SelectedValue?.ToString(), Quantity = Convert.ToDecimal(txt_Quantity.Text ?? "0"), QuotedPrice = Convert.ToDecimal(txt_QuotedPrice.Text ?? "0") };
        //    var customer = _customerService.GetCustomer(reqData.CustomerId);
        //    if (customer == null)
        //    {
        //        MessageBox.Show("ไม่พบข้อมูลลูกค้า", "การทำรายการผิดพลาด!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    var resultProcess = _marketService.ProcessOrder(reqData, customer);
        //    MockDatabase.Customers.TryGetValue(reqData.CustomerId, out var customerID);
        //    //MessageBox.Show(
        //    //    InsertBreaks(resultProcess.Message),
        //    //    resultProcess.Success
        //    //        ? "การทำรายการเสร็จสมบูรณ์"
        //    //        : "การทำรายการผิดพลาด!",
        //    //    MessageBoxButtons.OK,
        //    //    resultProcess.Success
        //    //        ? MessageBoxIcon.Information
        //    //        : MessageBoxIcon.Error
        //    //);

        //    TaskDialog.ShowDialog(new TaskDialogPage()
        //    {
        //        Caption = resultProcess.Success
        //            ? "การทำรายการเสร็จสมบูรณ์"
        //            : "การทำรายการผิดพลาด!",

        //        Heading = "แจ้งเตือน",

        //        Text = resultProcess.Message,

        //        Buttons =
        //        {
        //            TaskDialogButton.OK
        //        },

        //        Icon = resultProcess.Success
        //            ? TaskDialogIcon.Information
        //            : TaskDialogIcon.Error
        //    });

        //    //MessageBox.Show(resultProcess.Message, resultProcess.Success ? "การทำรายการเสร็จสมบูรณ์" : "การทำรายการผิดพลาด!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        //}

        private void TextBoxDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? txt = sender as TextBox;

            // อนุญาต control key เช่น Backspace
            if (char.IsControl(e.KeyChar))
                return;

            // อนุญาตเฉพาะตัวเลข และ .
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // ห้ามมี . มากกว่า 1 ตัว
            if (e.KeyChar == '.' && this.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // ตรวจทศนิยมไม่เกิน 2 ตำแหน่ง
            if (this.Text.Contains("."))
            {
                string[] parts = this.Text.Split('.');

                // cursor อยู่หลังจุดทศนิยม
                if (parts.Length > 1 &&
                    txt.SelectionStart > txt.Text.IndexOf('.'))
                {
                    if (parts[1].Length >= 2)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txt_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxDecimal_KeyPress(sender, e);
        }

        private void txt_QuotedPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxDecimal_KeyPress(sender, e);
        }

        private void txt_CustomerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            // อนุญาต A-Z และ 0-9 เท่านั้น
            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // กันตัวอักษรพิเศษ / ภาษาอื่น
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
                  (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void txt_CustomerID_TextChanged(object sender, EventArgs e)
        {
            int pos = txt_CustomerID.SelectionStart;

            string filtered = "";
            foreach (char c in txt_CustomerID.Text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                {
                    filtered += char.ToUpper(c);
                }
                //if (char.IsLetterOrDigit(c)) // A-Z, a-z, 0-9
                //{
                //    filtered += char.ToUpper(c); // แปลงเป็นตัวพิมพ์ใหญ่
                //}
            }

            if (txt_CustomerID.Text != filtered)
            {
                txt_CustomerID.Text = filtered;
                txt_CustomerID.SelectionStart = pos > filtered.Length ? filtered.Length : pos;
            }
        }
    }
}
