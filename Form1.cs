using Microsoft.Win32;
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
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public Login()
        {
            InitializeComponent();
        }

        private void resgisterbtn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();

            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {

            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                        string selectAccount = "select  COUNT(*) from users Where username = @usern AND password = @pass AND status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectAccount, conn))
                        {
                            cmd.Parameters.AddWithValue("@usern", signintxtbx.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", signinpasstxtbx.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", "active");


                            int rowCount = (int)cmd.ExecuteScalar();

                            if (rowCount > 0)
                            {
                                string selectRole = "Select role from users Where username = @usern AND password = @pass";

                                using (SqlCommand getRole = new SqlCommand(selectRole, conn))
                                {
                                    getRole.Parameters.AddWithValue("@usern", signintxtbx.Text.Trim());
                                    getRole.Parameters.AddWithValue("@pass", signinpasstxtbx.Text.Trim());

                                    string userRole = getRole.ExecuteScalar() as string;
                                   // MessageBox.Show("LogIn Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (userRole == "Admin")
                                    {
                                        this.Hide();
                                        adminMainForm adminMainForm = new adminMainForm();
                                        adminMainForm.ShowDialog();
                                        this.Close();
                                    }
                                    else if (userRole == "Cashier")
                                    {
                                        this.Hide();
                                        addcashier addcashier = new addcashier();
                                        addcashier.ShowDialog();
                                        this.Close();
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Username/Password or there is no admin's approvel.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

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
        }

        private void show_pass1_CheckedChanged(object sender, EventArgs e)
        {
            signinpasstxtbx.PasswordChar = show_pass1.Checked ? '\0' : '*';
        }

        private void Cross_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public bool emptyFields()
        {
            if (signintxtbx.Text == "" || signinpasstxtbx.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
