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
    public partial class cashierCustomerForm : UserControl
    {
        public cashierCustomerForm()
        {
            InitializeComponent();
            displayCustomersData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCustomersData();
        }

        public void displayCustomersData()
        {
            Customers cData = new Customers();
            List<Customers> list = cData.allCustomersData();
            customersgdv.DataSource = list;

        }
    }
}
