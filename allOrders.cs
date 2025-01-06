using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDF.adminViewOrders;

namespace RDF
{
    public partial class allOrders : UserControl
    {
        public allOrders()
        {
            InitializeComponent();
            displayOrdersData();

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayOrdersData();
        }

        public void displayOrdersData()
        {
            cashierorderform_orders.DataSource = adminViewOrders.orderListData();
        }
    }
}
