using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RDF.allUsers;

namespace RDF
{
    public partial class adminViewUsers : UserControl
    {
        public adminViewUsers()
        {
            InitializeComponent();
            displayUsersData();

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayUsersData();
        }

        public void displayUsersData()
        {


            dataGridView1.DataSource = allUsers.userListData();

        }
    }
}
