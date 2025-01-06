using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDF
{
    public partial class adminMainForm : Form
    {
        public adminMainForm()
        {
            InitializeComponent();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

        }

        private void loadform(object Form)
        {
            if (this.shadowPanel.Controls.Count > 0)
                this.shadowPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.shadowPanel.Controls.Add(f);
            this.shadowPanel.Tag = f;
            f.Show();
        }

        private void DisplayUserControl(UserControl userControl)
        {

            shadowPanel.Controls.Clear();

            userControl.Dock = DockStyle.Left;
            shadowPanel.Controls.Add(userControl);
        }

        private void Cross_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exist?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to sign out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
            }
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminaddproduct addProduct = new adminaddproduct();
            DisplayUserControl(addProduct);
            addProduct.refreshData();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminAddUsers addUser = new adminAddUsers();
            DisplayUserControl(addUser);

            addUser.refreshData();
        }

        private void allCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cashierCustomerForm customerForm = new cashierCustomerForm();
            DisplayUserControl(customerForm);
            customerForm.refreshData();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cashierorderform addorder = new cashierorderform();
            DisplayUserControl(addorder);
            addorder.refreshData();
        }

        private void c_dashboard_Click(object sender, EventArgs e)
        {
            
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admindashboard1 admindashboard = new admindashboard1();
            DisplayUserControl(admindashboard);
            admindashboard.refreshData();
        }

        private void allProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allProducts allProducts = new allProducts();
            DisplayUserControl(allProducts);
            allProducts.refreshData();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allOrders allOrders = new allOrders();
            DisplayUserControl(allOrders);
            allOrders.refreshData();
        }

        private void allUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminViewUsers allusers = new adminViewUsers();
            DisplayUserControl(allusers);
            allusers.refreshData();
        }
    }
}
