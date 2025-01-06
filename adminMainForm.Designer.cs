namespace RDF
{
    partial class adminMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminMainForm));
            this.Cross = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.logout = new System.Windows.Forms.ToolStripMenuItem();
            this.finalReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.earlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_report = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_orders = new System.Windows.Forms.ToolStripMenuItem();
            this.allUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_view = new System.Windows.Forms.ToolStripMenuItem();
            this.allCustomersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_customers = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c_add = new System.Windows.Forms.ToolStripMenuItem();
            this.c_dashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.minimize = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.shadowPanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Cross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cross
            // 
            this.Cross.BackColor = System.Drawing.Color.PeachPuff;
            this.Cross.Image = ((System.Drawing.Image)(resources.GetObject("Cross.Image")));
            this.Cross.ImageRotate = 0F;
            this.Cross.Location = new System.Drawing.Point(1877, 0);
            this.Cross.Name = "Cross";
            this.Cross.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Cross.Size = new System.Drawing.Size(41, 40);
            this.Cross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Cross.TabIndex = 46;
            this.Cross.TabStop = false;
            this.Cross.Click += new System.EventHandler(this.Cross_Click);
            // 
            // logout
            // 
            this.logout.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.logout.ForeColor = System.Drawing.Color.Crimson;
            this.logout.Image = ((System.Drawing.Image)(resources.GetObject("logout.Image")));
            this.logout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(175, 47);
            this.logout.Text = "Logout";
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // finalReportToolStripMenuItem
            // 
            this.finalReportToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("finalReportToolStripMenuItem.Image")));
            this.finalReportToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.finalReportToolStripMenuItem.Name = "finalReportToolStripMenuItem";
            this.finalReportToolStripMenuItem.Size = new System.Drawing.Size(264, 42);
            this.finalReportToolStripMenuItem.Text = "Final Report";
            // 
            // earlyToolStripMenuItem
            // 
            this.earlyToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.earlyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("earlyToolStripMenuItem.Image")));
            this.earlyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.earlyToolStripMenuItem.Name = "earlyToolStripMenuItem";
            this.earlyToolStripMenuItem.Size = new System.Drawing.Size(264, 42);
            this.earlyToolStripMenuItem.Text = "Early Report";
            // 
            // c_report
            // 
            this.c_report.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.earlyToolStripMenuItem,
            this.finalReportToolStripMenuItem});
            this.c_report.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.c_report.ForeColor = System.Drawing.Color.Crimson;
            this.c_report.Image = ((System.Drawing.Image)(resources.GetObject("c_report.Image")));
            this.c_report.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_report.Name = "c_report";
            this.c_report.Size = new System.Drawing.Size(174, 47);
            this.c_report.Text = "Report";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aToolStripMenuItem.Image")));
            this.aToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(278, 42);
            this.aToolStripMenuItem.Text = "Add Order";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // c_orders
            // 
            this.c_orders.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem});
            this.c_orders.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.c_orders.ForeColor = System.Drawing.Color.Crimson;
            this.c_orders.Image = ((System.Drawing.Image)(resources.GetObject("c_orders.Image")));
            this.c_orders.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_orders.Name = "c_orders";
            this.c_orders.Size = new System.Drawing.Size(176, 45);
            this.c_orders.Text = "Orders";
            // 
            // allUsersToolStripMenuItem
            // 
            this.allUsersToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allUsersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allUsersToolStripMenuItem.Image")));
            this.allUsersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allUsersToolStripMenuItem.Name = "allUsersToolStripMenuItem";
            this.allUsersToolStripMenuItem.Size = new System.Drawing.Size(278, 42);
            this.allUsersToolStripMenuItem.Text = "All Users";
            this.allUsersToolStripMenuItem.Click += new System.EventHandler(this.allUsersToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ordersToolStripMenuItem.Image")));
            this.ordersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(278, 42);
            this.ordersToolStripMenuItem.Text = "All Orders";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // allProductsToolStripMenuItem
            // 
            this.allProductsToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allProductsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allProductsToolStripMenuItem.Image")));
            this.allProductsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allProductsToolStripMenuItem.Name = "allProductsToolStripMenuItem";
            this.allProductsToolStripMenuItem.Size = new System.Drawing.Size(278, 42);
            this.allProductsToolStripMenuItem.Text = "All Products";
            this.allProductsToolStripMenuItem.Click += new System.EventHandler(this.allProductsToolStripMenuItem_Click);
            // 
            // c_view
            // 
            this.c_view.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allProductsToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.allUsersToolStripMenuItem});
            this.c_view.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_view.ForeColor = System.Drawing.Color.Crimson;
            this.c_view.Image = ((System.Drawing.Image)(resources.GetObject("c_view.Image")));
            this.c_view.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_view.Name = "c_view";
            this.c_view.Size = new System.Drawing.Size(141, 45);
            this.c_view.Text = "View";
            // 
            // allCustomersToolStripMenuItem
            // 
            this.allCustomersToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allCustomersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allCustomersToolStripMenuItem.Image")));
            this.allCustomersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allCustomersToolStripMenuItem.Name = "allCustomersToolStripMenuItem";
            this.allCustomersToolStripMenuItem.Size = new System.Drawing.Size(278, 42);
            this.allCustomersToolStripMenuItem.Text = "All Customers";
            this.allCustomersToolStripMenuItem.Click += new System.EventHandler(this.allCustomersToolStripMenuItem_Click);
            // 
            // c_customers
            // 
            this.c_customers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allCustomersToolStripMenuItem});
            this.c_customers.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.c_customers.ForeColor = System.Drawing.Color.Crimson;
            this.c_customers.Image = ((System.Drawing.Image)(resources.GetObject("c_customers.Image")));
            this.c_customers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_customers.Name = "c_customers";
            this.c_customers.Size = new System.Drawing.Size(230, 45);
            this.c_customers.Text = "Customers";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("usersToolStripMenuItem.Image")));
            this.usersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(220, 42);
            this.usersToolStripMenuItem.Text = "Cashiers";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // productsToolStripMenuItem
            // 
            this.productsToolStripMenuItem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("productsToolStripMenuItem.Image")));
            this.productsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.productsToolStripMenuItem.Name = "productsToolStripMenuItem";
            this.productsToolStripMenuItem.Size = new System.Drawing.Size(220, 42);
            this.productsToolStripMenuItem.Text = "Products";
            this.productsToolStripMenuItem.Click += new System.EventHandler(this.productsToolStripMenuItem_Click);
            // 
            // c_add
            // 
            this.c_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productsToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.c_add.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.c_add.ForeColor = System.Drawing.Color.Crimson;
            this.c_add.Image = ((System.Drawing.Image)(resources.GetObject("c_add.Image")));
            this.c_add.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_add.Name = "c_add";
            this.c_add.Size = new System.Drawing.Size(131, 47);
            this.c_add.Text = "Add";
            // 
            // c_dashboard
            // 
            this.c_dashboard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.c_dashboard.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.c_dashboard.ForeColor = System.Drawing.Color.Crimson;
            this.c_dashboard.Image = ((System.Drawing.Image)(resources.GetObject("c_dashboard.Image")));
            this.c_dashboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.c_dashboard.Name = "c_dashboard";
            this.c_dashboard.Size = new System.Drawing.Size(175, 45);
            this.c_dashboard.Text = "DashBoard";
            this.c_dashboard.Click += new System.EventHandler(this.c_dashboard_Click);
            // 
            // minimize
            // 
            this.minimize.BackColor = System.Drawing.Color.PeachPuff;
            this.minimize.Image = ((System.Drawing.Image)(resources.GetObject("minimize.Image")));
            this.minimize.ImageRotate = 0F;
            this.minimize.Location = new System.Drawing.Point(1830, 0);
            this.minimize.Name = "minimize";
            this.minimize.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.minimize.Size = new System.Drawing.Size(41, 40);
            this.minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minimize.TabIndex = 47;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.PeachPuff;
            this.mainMenu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_dashboard,
            this.c_add,
            this.c_customers,
            this.c_view,
            this.c_orders,
            this.c_report,
            this.logout});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1920, 49);
            this.mainMenu.TabIndex = 45;
            this.mainMenu.Text = "menuStrip";
            // 
            // shadowPanel
            // 
            this.shadowPanel.BackColor = System.Drawing.Color.Transparent;
            this.shadowPanel.FillColor = System.Drawing.Color.White;
            this.shadowPanel.Location = new System.Drawing.Point(0, 53);
            this.shadowPanel.Name = "shadowPanel";
            this.shadowPanel.ShadowColor = System.Drawing.Color.Black;
            this.shadowPanel.Size = new System.Drawing.Size(1918, 1025);
            this.shadowPanel.TabIndex = 48;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripMenuItem.Image")));
            this.viewToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(294, 42);
            this.viewToolStripMenuItem.Text = "View DashBoard";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // adminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.shadowPanel);
            this.Controls.Add(this.Cross);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "adminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Cross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox Cross;
        private System.Windows.Forms.ToolStripMenuItem logout;
        private System.Windows.Forms.ToolStripMenuItem finalReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem earlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_report;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_orders;
        private System.Windows.Forms.ToolStripMenuItem allUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_view;
        private System.Windows.Forms.ToolStripMenuItem allCustomersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_customers;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c_add;
        private System.Windows.Forms.ToolStripMenuItem c_dashboard;
        private Guna.UI2.WinForms.Guna2CirclePictureBox minimize;
        private System.Windows.Forms.MenuStrip mainMenu;
        private Guna.UI2.WinForms.Guna2ShadowPanel shadowPanel;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    }
}