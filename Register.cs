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
    public partial class Register : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public Register()
        {
            InitializeComponent();
        }

        private void Cross_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void show_pass1_CheckedChanged(object sender, EventArgs e)
        {
            res_passtxtbx.PasswordChar = show_pass1.Checked ? '\0' : '*';
            res_cpasstxtbx.PasswordChar = show_pass1.Checked ? '\0' : '*';
        }

        private void resbtn_Click(object sender, EventArgs e)
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
                        string selectUsernmae = "select * from users Where username = @usern ";
                        using (SqlCommand checkUsername = new SqlCommand(selectUsernmae, conn))
                        {
                            checkUsername.Parameters.AddWithValue("@usern", restxtbx.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count >= 1)
                            {
                                string usern = restxtbx.Text.Substring(0, 1).ToUpper() + restxtbx.Text.Substring(1);
                                MessageBox.Show(usern + "is already taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (res_passtxtbx.Text != res_cpasstxtbx.Text)
                            {
                                MessageBox.Show("Password does not match.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (res_passtxtbx.Text.Length < 8)
                            {
                                MessageBox.Show("Invalid password, at least 8 characters are needed.", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "insert into users (username,password,profile_image, role, status, date_reg )" +
                                   "values (@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Today;

                                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                                {
                                    cmd.Parameters.AddWithValue("@usern", restxtbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", res_passtxtbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", "");
                                    cmd.Parameters.AddWithValue("@role", "Cashier");
                                    cmd.Parameters.AddWithValue("@status", "Approvol");
                                    cmd.Parameters.AddWithValue("@date", today);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Registered Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Switch into login form

                                    Login login = new Login();
                                    login.Show();

                                    this.Hide();
                                }
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

        private void signinbtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            this.Hide();
        }

        public bool emptyFields()
        {
            if (restxtbx.Text == "" || res_passtxtbx.Text == "" || res_cpasstxtbx.Text == "")
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
