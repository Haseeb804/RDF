using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDF
{
    public partial class admindashboard1 : UserControl
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");
        public admindashboard1()
        {
            InitializeComponent();
            displayTotalCashier();

            displayTotalCustomers();

            displayTotalIncome();

            displayTodayIncome();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayTotalCashier();

            displayTotalCustomers();

            displayTotalIncome();

            displayTodayIncome();
        }

        public void displayTotalCashier()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "Select Count(id) From users Where role = @role And status = @status";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            int count = Convert.ToInt32(dr[0]);
                            dashboardlbTC.Text = count.ToString();
                        }

                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed:" + ex, "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void displayTotalCustomers()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "Select Count(id) From customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            int count = Convert.ToInt32(dr[0]);
                            dashboardlbTCus.Text = count.ToString();
                        }

                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed:" + ex, "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void displayTotalIncome()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "SELECT SUM(total_price) FROM customers";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            decimal totalIncome = Convert.ToDecimal(result);
                            dashboardlbTinc.Text = "Rs:" + totalIncome.ToString("0.00");
                        }
                        else
                        {
                            dashboardlbTinc.Text = "Rs: 0.00"; // Display 0 if there are no records in the table
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed:" + ex.Message, "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public void displayTodayIncome()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "SELECT SUM(total_price) FROM customers WHERE date = @date";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        DateTime today = DateTime.Today;
                        string getToday = today.ToString("yyyy-MM-dd");

                        cmd.Parameters.AddWithValue("@date", getToday);

                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            decimal todayIncome = Convert.ToDecimal(result);
                            dashboardlbTodayin.Text = "Rs:" + todayIncome.ToString("0.00");
                        }
                        else
                        {
                            dashboardlbTodayin.Text = "Rs: 0.00"; // Display 0 if there are no records for today
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex.Message, "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
