using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDF.adminViewProducts;

namespace RDF
{
    public partial class allProducts : UserControl
    {
        public allProducts()
        {
            InitializeComponent();
            displayProductsData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayProductsData();
        }

        public void displayProductsData()
        {
            
           
            dataGridView1.DataSource = adminViewProducts.productListData();

        }
    }
}
