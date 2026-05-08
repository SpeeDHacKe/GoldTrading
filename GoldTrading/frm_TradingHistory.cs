using GoldTrading.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldTrading
{
    public partial class frm_TradingHistory : Form
    {
        public frm_TradingHistory()
        {
            InitializeComponent();
        }

        private void frm_TradingHistory_Load(object sender, EventArgs e)
        {
            dgv_TradingHistory.DataSource = HistoryModel.OrdersHistory
                .Select((x, index) => new
                {
                    No = index + 1,
                    x.CustomerId,
                    x.OrderType,
                    x.Quantity,
                    x.QuotedPrice,
                    x.TotalPrice,
                    x.CreateDate
                }).ToList();

            // ===== Header Style =====
            dgv_TradingHistory.EnableHeadersVisualStyles = false;
            dgv_TradingHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_TradingHistory.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_TradingHistory.Font, FontStyle.Bold);
            dgv_TradingHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;


            // ===== Column Alignment =====

            // Column No อยู่ตรงกลาง
            dgv_TradingHistory.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_TradingHistory.Columns["No"].Width = 30;
            dgv_TradingHistory.Columns["CustomerId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_TradingHistory.Columns["CustomerId"].Width = 110;
            dgv_TradingHistory.Columns["OrderType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_TradingHistory.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_TradingHistory.Columns["QuotedPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_TradingHistory.Columns["TotalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_TradingHistory.Columns["CreateDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // ===== Optional =====
            // เปลี่ยนชื่อ Header
            dgv_TradingHistory.Columns["No"].HeaderText = "No.";
            dgv_TradingHistory.Columns["CustomerId"].HeaderText = "Customer ID";
            dgv_TradingHistory.Columns["OrderType"].HeaderText = "Order Type";
        }
    }
}
