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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.XYDiagramPane xyDiagramPane1 = new DevExpress.XtraCharts.XYDiagramPane();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.CandleStickSeriesView candleStickSeriesView1 = new DevExpress.XtraCharts.CandleStickSeriesView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.資料庫ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新股票代碼ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自動更新股票歷史價格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.技術分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帳號ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帳號登入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.統計ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.個股買賣點ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.listBoxControl2 = new DevExpress.XtraEditors.ListBoxControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl2)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1469, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 資料庫ToolStripMenuItem
            // 
            this.資料庫ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新股票代碼ToolStripMenuItem,
            this.自動更新股票歷史價格ToolStripMenuItem,
            this.技術分析ToolStripMenuItem});
            this.資料庫ToolStripMenuItem.Name = "資料庫ToolStripMenuItem";
            this.資料庫ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.資料庫ToolStripMenuItem.Text = "資料庫";
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
            // 技術分析ToolStripMenuItem
            // 
            this.技術分析ToolStripMenuItem.Name = "技術分析ToolStripMenuItem";
            this.技術分析ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.技術分析ToolStripMenuItem.Text = "技術分析";
            this.技術分析ToolStripMenuItem.Click += new System.EventHandler(this.技術分析ToolStripMenuItem_ClickAsync);
            // 
            // 帳號ToolStripMenuItem
            // 
            this.帳號ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帳號登入ToolStripMenuItem});
            this.帳號ToolStripMenuItem.Name = "帳號ToolStripMenuItem";
            this.帳號ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.帳號ToolStripMenuItem.Text = "帳號";
            // 
            // 帳號登入ToolStripMenuItem
            // 
            this.帳號登入ToolStripMenuItem.Name = "帳號登入ToolStripMenuItem";
            this.帳號登入ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.帳號登入ToolStripMenuItem.Text = "帳號登入";
            // 
            // 統計ToolStripMenuItem
            // 
            this.統計ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.個股買賣點ToolStripMenuItem});
            this.統計ToolStripMenuItem.Name = "統計ToolStripMenuItem";
            this.統計ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.統計ToolStripMenuItem.Text = "統計";
            // 
            // 個股買賣點ToolStripMenuItem
            // 
            this.個股買賣點ToolStripMenuItem.Name = "個股買賣點ToolStripMenuItem";
            this.個股買賣點ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.個股買賣點ToolStripMenuItem.Text = "個股買賣點";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "DevExpress Dark Style";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 24);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabControl1.Size = new System.Drawing.Size(1469, 964);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.chartControl1);
            this.xtraTabPage2.Controls.Add(this.listBoxControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1463, 935);
            this.xtraTabPage2.Text = "分析圖";
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagramPane1.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagramPane1.Name = "Pane 1";
            xyDiagramPane1.PaneID = 0;
            xyDiagramPane1.Weight = 0.5D;
            xyDiagram1.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[] {
            xyDiagramPane1});
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(242, 0);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series";
            series1.View = candleStickSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(1166, 928);
            this.chartControl1.TabIndex = 1;
            // 
            // listBoxControl2
            // 
            this.listBoxControl2.Location = new System.Drawing.Point(3, 3);
            this.listBoxControl2.Name = "listBoxControl2";
            this.listBoxControl2.Size = new System.Drawing.Size(199, 236);
            this.listBoxControl2.TabIndex = 0;
            this.listBoxControl2.SelectedIndexChanged += new System.EventHandler(this.listBoxControl2_SelectedIndexChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.listBoxControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1463, 935);
            this.xtraTabPage1.Text = "狀態回報";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Location = new System.Drawing.Point(3, 3);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(634, 406);
            this.listBoxControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 988);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagramPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(candleStickSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl2)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem 技術分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 個股買賣點ToolStripMenuItem;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}

