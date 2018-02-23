namespace AutoStockTransaction
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.資料庫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帳號ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.統計ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新股票代碼ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自動更新股票歷史價格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帳號登入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.資料庫ToolStripMenuItem,
            this.帳號ToolStripMenuItem,
            this.統計ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(885, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 資料庫ToolStripMenuItem
            // 
            this.資料庫ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新股票代碼ToolStripMenuItem,
            this.自動更新股票歷史價格ToolStripMenuItem});
            this.資料庫ToolStripMenuItem.Name = "資料庫ToolStripMenuItem";
            this.資料庫ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.資料庫ToolStripMenuItem.Text = "資料庫";
            // 
            // 帳號ToolStripMenuItem
            // 
            this.帳號ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帳號登入ToolStripMenuItem});
            this.帳號ToolStripMenuItem.Name = "帳號ToolStripMenuItem";
            this.帳號ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.帳號ToolStripMenuItem.Text = "帳號";
            // 
            // 統計ToolStripMenuItem
            // 
            this.統計ToolStripMenuItem.Name = "統計ToolStripMenuItem";
            this.統計ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.統計ToolStripMenuItem.Text = "統計";
            // 
            // 更新股票代碼ToolStripMenuItem
            // 
            this.更新股票代碼ToolStripMenuItem.Name = "更新股票代碼ToolStripMenuItem";
            this.更新股票代碼ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.更新股票代碼ToolStripMenuItem.Text = "更新股票代碼";
            this.更新股票代碼ToolStripMenuItem.Click += new System.EventHandler(this.更新股票代碼ToolStripMenuItem_Click);
            // 
            // 自動更新股票歷史價格ToolStripMenuItem
            // 
            this.自動更新股票歷史價格ToolStripMenuItem.Name = "自動更新股票歷史價格ToolStripMenuItem";
            this.自動更新股票歷史價格ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.自動更新股票歷史價格ToolStripMenuItem.Text = "自動更新股票歷史價格";
            this.自動更新股票歷史價格ToolStripMenuItem.Click += new System.EventHandler(this.自動更新股票歷史價格ToolStripMenuItem_Click);
            // 
            // 帳號登入ToolStripMenuItem
            // 
            this.帳號登入ToolStripMenuItem.Name = "帳號登入ToolStripMenuItem";
            this.帳號登入ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.帳號登入ToolStripMenuItem.Text = "帳號登入";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 447);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "執行狀態";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(473, 388);
            this.listBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 473);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 資料庫ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新股票代碼ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自動更新股票歷史價格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帳號ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帳號登入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 統計ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

