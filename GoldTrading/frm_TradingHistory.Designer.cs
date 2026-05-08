namespace GoldTrading
{
    partial class frm_TradingHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgv_TradingHistory = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_TradingHistory).BeginInit();
            SuspendLayout();
            // 
            // dgv_TradingHistory
            // 
            dgv_TradingHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_TradingHistory.Location = new Point(10, 12);
            dgv_TradingHistory.Name = "dgv_TradingHistory";
            dgv_TradingHistory.Size = new Size(692, 335);
            dgv_TradingHistory.TabIndex = 0;
            // 
            // frm_TradingHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 358);
            Controls.Add(dgv_TradingHistory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frm_TradingHistory";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trading History";
            Load += frm_TradingHistory_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_TradingHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgv_TradingHistory;
    }
}