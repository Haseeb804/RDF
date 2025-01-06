using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDF
{
    public partial class adminaddproduct : UserControl
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public adminaddproduct()
        {
            InitializeComponent();
            displayproductListData();
            dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayproductListData();

        }


        public void displayproductListData()
        {
            adminaddproductdata proData = new adminaddproductdata();
            List<adminaddproductdata> listData = proData.productListData();
            dataGridView1.DataSource = listData;
        }

        public bool emptyFields()
        {
            if (adminaddp_idtxt.Text == "" || adminaddp_nametxt.Text == "" || adminaddp_typecmbx.SelectedIndex == -1 || adminaddp_statuscmbx.SelectedIndex == -1 || adminaddp_pricetxt.Text == "" || adminaddp_stocktxt.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void addproductbtn_Click(object sender, EventArgs e)
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

                        // check if the product id is already exist.
                        string selectproID = "select * from products WHERE pro_id = @proID";
                        using (SqlCommand selectPID = new SqlCommand(selectproID, conn))
                        {
                            selectPID.Parameters.AddWithValue("@proID", adminaddp_idtxt.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(selectPID);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count >= 1)
                            {
                                MessageBox.Show("Product ID:" + adminaddp_idtxt.Text.Trim() + "is taken already", "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO products (pro_id, pro_name, pro_type, pro_image, pro_stock, pro_price, pro_status, date_insert) " +
                                                    "VALUES (@proID, @proname, @protype, @proimage, @prostock, @proprice, @prostatus, @dateinsert)";

                                DateTime now = DateTime.Now;

                                string path = Path.Combine("D:\\4th Semester\\DB\\Cafe\\Cafe_Management _System\\Resources\\", adminaddp_idtxt.Text.Trim() + ".jpg");

                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(adminaddp_pic_view.ImageLocation, path, true);

                                using (SqlCommand cmd = new SqlCommand(insertData, conn))
                                {
                                    cmd.Parameters.AddWithValue("@proID", adminaddp_idtxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@proname", adminaddp_nametxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@proimage", path);
                                    cmd.Parameters.AddWithValue("@protype", adminaddp_typecmbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prostock", adminaddp_stocktxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@proprice", adminaddp_pricetxt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prostatus", adminaddp_statuscmbx.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dateinsert", now);

                                    cmd.ExecuteNonQuery();
                                    clearFields();

                                    MessageBox.Show("Added Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayproductListData();

                                }
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection Failed:" + ex, "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { conn.Close(); }

                }
            }
        }

        public void clearFields()
        {
            adminaddp_idtxt.Text = "";
            adminaddp_nametxt.Text = "";
            adminaddp_typecmbx.SelectedIndex = -1;
            adminaddp_statuscmbx.SelectedIndex = -1;
            adminaddp_pricetxt.Text = "";
            adminaddp_stocktxt.Text = "";
            adminaddp_pic_view.Image = null;
        }


        private void adminaddp_import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imageapth = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageapth = openFileDialog.FileName;
                    adminaddp_pic_view.ImageLocation = imageapth;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delproductbtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Delete Prodcut ID:" + adminaddp_idtxt.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();
                            string updateData = "Update products  SET date_delete = @deletedate WHERE pro_id = @proID";
                            DateTime now = DateTime.Now;


                            using (SqlCommand del = new SqlCommand(updateData, conn))
                            {
                                del.Parameters.AddWithValue("@proID", adminaddp_idtxt.Text.Trim());

                                del.Parameters.AddWithValue("@deletedate", now);

                                del.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Deleted Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayproductListData();

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

        private void updproductbtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update Username:" + adminaddp_idtxt.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();
                            string updateData = "update products Set pro_id =  @proID, pro_name = @proname, pro_type = @protype, pro_stock = @prostock, pro_price = @proprice, pro_status = @prostatus, date_update = @dateupdate WHERE pro_id = @proID";
                            DateTime now = DateTime.Now;


                            using (SqlCommand update = new SqlCommand(updateData, conn))
                            {
                                update.Parameters.AddWithValue("@proID", adminaddp_idtxt.Text.Trim());
                                update.Parameters.AddWithValue("@proname", adminaddp_nametxt.Text.Trim());
                                //update.Parameters.AddWithValue("@proimage", path);
                                update.Parameters.AddWithValue("@protype", adminaddp_typecmbx.Text.Trim());
                                update.Parameters.AddWithValue("@prostock", adminaddp_stocktxt.Text.Trim());
                                update.Parameters.AddWithValue("@proprice", adminaddp_pricetxt.Text.Trim());
                                update.Parameters.AddWithValue("@prostatus", adminaddp_statuscmbx.Text.Trim());
                                update.Parameters.AddWithValue("@dateupdate", now);

                                update.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Updated Successfully!.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                displayproductListData();

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

        private void clearproductbtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a valid row index is clicked
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                adminaddp_idtxt.Text = selectedRow.Cells["ProductNumber"].Value.ToString();
                adminaddp_nametxt.Text = selectedRow.Cells["ProductName"].Value.ToString();
                adminaddp_typecmbx.Text = selectedRow.Cells["Type"].Value.ToString();
                adminaddp_statuscmbx.Text = selectedRow.Cells["Status"].Value.ToString();
                adminaddp_pricetxt.Text = selectedRow.Cells["Price"].Value.ToString();
                adminaddp_stocktxt.Text = selectedRow.Cells["Stock"].Value.ToString();

                string imagePath = selectedRow.Cells["Image"].Value.ToString();
                try
                {
                    if (File.Exists(imagePath))
                    {
                        adminaddp_pic_view.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        adminaddp_pic_view.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
