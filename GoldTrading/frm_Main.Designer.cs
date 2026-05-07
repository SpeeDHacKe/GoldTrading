namespace GoldTrading
{
    partial class frm_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            label1 = new Label();
            txt_CurrentMarketPrice = new TextBox();
            groupBox1 = new GroupBox();
            btn_NewOrder = new Button();
            txt_QuotedPrice = new TextBox();
            txt_Quantity = new TextBox();
            cmb_OrderType = new ComboBox();
            txt_CustomerID = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(318, 21);
            label1.TabIndex = 0;
            label1.Text = "ราคาทองปัจจุบัน/บาท (น้ำหนักประมาณ 15.2 กรัม) :";
            // 
            // txt_CurrentMarketPrice
            // 
            txt_CurrentMarketPrice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_CurrentMarketPrice.Location = new Point(336, 13);
            txt_CurrentMarketPrice.Name = "txt_CurrentMarketPrice";
            txt_CurrentMarketPrice.ReadOnly = true;
            txt_CurrentMarketPrice.Size = new Size(112, 29);
            txt_CurrentMarketPrice.TabIndex = 1;
            txt_CurrentMarketPrice.Text = "72600";
            txt_CurrentMarketPrice.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_NewOrder);
            groupBox1.Controls.Add(txt_QuotedPrice);
            groupBox1.Controls.Add(txt_Quantity);
            groupBox1.Controls.Add(cmb_OrderType);
            groupBox1.Controls.Add(txt_CustomerID);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 59);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(436, 238);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "New Order";
            // 
            // btn_NewOrder
            // 
            btn_NewOrder.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_NewOrder.Location = new Point(40, 190);
            btn_NewOrder.Name = "btn_NewOrder";
            btn_NewOrder.Size = new Size(356, 29);
            btn_NewOrder.TabIndex = 8;
            btn_NewOrder.Text = "New Order";
            btn_NewOrder.UseVisualStyleBackColor = true;
            btn_NewOrder.Click += btn_NewOrder_Click;
            // 
            // txt_QuotedPrice
            // 
            txt_QuotedPrice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_QuotedPrice.Location = new Point(241, 146);
            txt_QuotedPrice.Name = "txt_QuotedPrice";
            txt_QuotedPrice.Size = new Size(155, 29);
            txt_QuotedPrice.TabIndex = 7;
            txt_QuotedPrice.TextAlign = HorizontalAlignment.Right;
            txt_QuotedPrice.KeyPress += txt_QuotedPrice_KeyPress;
            // 
            // txt_Quantity
            // 
            txt_Quantity.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Quantity.Location = new Point(241, 111);
            txt_Quantity.MaxLength = 6;
            txt_Quantity.Name = "txt_Quantity";
            txt_Quantity.Size = new Size(155, 29);
            txt_Quantity.TabIndex = 6;
            txt_Quantity.TextAlign = HorizontalAlignment.Right;
            txt_Quantity.KeyPress += txt_Quantity_KeyPress;
            // 
            // cmb_OrderType
            // 
            cmb_OrderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_OrderType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmb_OrderType.FormattingEnabled = true;
            cmb_OrderType.Location = new Point(241, 76);
            cmb_OrderType.Name = "cmb_OrderType";
            cmb_OrderType.Size = new Size(155, 29);
            cmb_OrderType.TabIndex = 5;
            // 
            // txt_CustomerID
            // 
            txt_CustomerID.CharacterCasing = CharacterCasing.Upper;
            txt_CustomerID.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_CustomerID.Location = new Point(241, 41);
            txt_CustomerID.MaxLength = 6;
            txt_CustomerID.Name = "txt_CustomerID";
            txt_CustomerID.Size = new Size(155, 29);
            txt_CustomerID.TabIndex = 4;
            txt_CustomerID.TextAlign = HorizontalAlignment.Center;
            txt_CustomerID.TextChanged += txt_CustomerID_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(136, 143);
            label5.Name = "label5";
            label5.Size = new Size(87, 21);
            label5.TabIndex = 3;
            label5.Text = "ราคาที่เสนอ :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(40, 110);
            label4.Name = "label4";
            label4.Size = new Size(183, 21);
            label4.TabIndex = 2;
            label4.Text = "ปริมาณ (เช่น 1.0, 0.5, 2.5) :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(117, 76);
            label3.Name = "label3";
            label3.Size = new Size(106, 21);
            label3.TabIndex = 1;
            label3.Text = "ประเภทรายการ :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(147, 44);
            label2.Name = "label2";
            label2.Size = new Size(76, 21);
            label2.TabIndex = 0;
            label2.Text = "รหัสลูกค้า :";
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 309);
            Controls.Add(groupBox1);
            Controls.Add(txt_CurrentMarketPrice);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frm_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gold Trading";
            Load += frm_Main_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_CurrentMarketPrice;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label4;
        private TextBox txt_QuotedPrice;
        private TextBox txt_Quantity;
        private ComboBox cmb_OrderType;
        private TextBox txt_CustomerID;
        private Label label5;
        private Button btn_NewOrder;
    }
}
