using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace RDF
{
    public partial class adminAddUsers : UserControl
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");


        public adminAddUsers()
        {
            InitializeComponent();
            displayAddussersdata();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void displayAddussersdata()
        {
            adminAddusersdata userData = new adminAddusersdata();
            List<adminAddusersdata> listData = userData.userListData();

            dataGridView1.DataSource = listData;
        }
        public void refreshData()
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayAddussersdata();
        }

        public void clearFields()
        {
            adminaddnametxt.Text = "";
            adminaddpasstxt.Text = "";
            adminaddrolecmbx.SelectedIndex = -1;
            adminaddstatuscmbx.SelectedIndex = -1;
        }

        public bool emptyFields()
        {
            if (adminaddnametxt.Text == "" || adminaddpasstxt.Text == "" || adminaddrolecmbx.Text == "" || adminaddstatuscmbx.Text == "" || adminaddpic_view.Image == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void adduserbtn_Click(object sender, EventArgs e)
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
                            checkUsername.Parameters.AddWithValue("@usern", adminaddnametxt.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count >= 1)
                            {
                                string usern = adminaddnametxt.Text.Substring(0, 1).ToUpper() + adminaddnametxt.Text.Substring(1);
                                MessageBox.Show(usern + "is already taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "insert into users (username,password,profile_image, role, status, date_reg )" +
                                   "values (@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Today;

                                string path = Path.Combine("D:\\4th Semester\\DB\\Cafe\\Cafe_Management _System\\Resources\\" + adminaddnametxt.Text.Trim() + ".jpg");

                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(adminaddpic_view.ImageLocation, path, true);

                                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                                {
                                    cmd.Parameters.AddWithValue("@usern", adminaddnametxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", adminaddpasstxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path);
                                    cmd.Parameters.AddWithValue("@role", adminaddrolecmbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@status", adminaddstatuscmbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", today);

                                    cmd.ExecuteNonQuery();
                                    clearFields();

                                    MessageBox.Show("Added Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    displayAddussersdata();

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

        private void clearfieldbtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private int id = 0;

        private void deluserbtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete Username:" + adminaddnametxt.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();
                            string deleteData = "DELETE FROM users WHERE username = @username";

                            using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                            {
                                cmd.Parameters.AddWithValue("@username", adminaddnametxt.Text.Trim());

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Deleted Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayAddussersdata(); // Refresh data in DataGridView
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("No rows were deleted.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void upduserbtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update Username:" + adminaddnametxt.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();
                            string updateData = "UPDATE users SET password = @pass , role = @role, status = @status WHERE username = @username";

                            using (SqlCommand cmd = new SqlCommand(updateData, conn))
                            {
                                cmd.Parameters.AddWithValue("@pass", adminaddpasstxt.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", adminaddrolecmbx.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", adminaddstatuscmbx.Text.Trim());
                                cmd.Parameters.AddWithValue("@username", adminaddnametxt.Text.Trim());

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Updated Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayAddussersdata(); // Refresh data in DataGridView
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("No rows were updated.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void adminaddimport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imageapth = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageapth = openFileDialog.FileName;
                    adminaddpic_view.ImageLocation = imageapth;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                adminaddnametxt.Text = row.Cells["Username"].Value.ToString();
                adminaddpasstxt.Text = row.Cells["Password"].Value.ToString();
                adminaddrolecmbx.Text = row.Cells["Role"].Value.ToString();
                adminaddstatuscmbx.Text = row.Cells["Status"].Value.ToString();
                adminaddpic_view.Text = row.Cells["Image"].Value.ToString();
               

            }
        }
    }
}
