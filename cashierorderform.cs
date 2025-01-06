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
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms.VisualStyles;


namespace RDF
{
    public partial class cashierorderform : UserControl
    {

        public static int getCustID;

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public cashierorderform()
        {
            InitializeComponent();
            displayAvaiableProducts();
            displayAllOrders();
            displayTotalPrice();
            cashierorderform_orders.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayAvaiableProducts();
            displayAllOrders();
            displayTotalPrice();
        }

        public void displayAvaiableProducts()
        {
            cashierOrderFormProData allProd = new cashierOrderFormProData();
            List<cashierOrderFormProData> listData = allProd.availableProductsData();
            cashierorderform_menu.DataSource = listData;
        }

        public void displayAllOrders()
        {
            cashierOrderData allOrders = new cashierOrderData();
            List<cashierOrderData> listData = allOrders.orderListData();

            // Remove CID and ProductID from the list data
            var modifiedListData = listData.Select(order => new
            {
                order.ProName,
                order.ProType,
                order.Quantity,
                order.Price
            }).ToList();

            cashierorderform_orders.DataSource = modifiedListData;
        }

        private int totalPrice;

        public void displayTotalPrice()
        {
            iDGenerator();
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "SELECT SUM(pro_price) FROM orders WHERE pro_name = @proname";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@proname", cashierorderform_prodname.Text.Trim()); // Assuming cashierorderform_prodname.Text contains the product name
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value) // Check if the result is not DBNull
                        {
                            totalPrice = Convert.ToInt32(result);
                            cashierorderform_price2.Text = totalPrice.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed:" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private int idGen = 0;
        public void iDGenerator()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                string selectID = "Select MAX(customer_id) from customers";
                using (SqlCommand cmd = new SqlCommand(selectID, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int temp = Convert.ToInt32(result);
                        if (temp == 0)
                        {
                            idGen += 1;
                        }
                        else
                        {
                            idGen = temp + 1;
                        }
                    }
                    else
                    {
                        idGen = 1;
                    }
                    getCustID = idGen;
                }
            }
        }


        private void cashierorderform_addbtn_Click(object sender, EventArgs e)
        {
            iDGenerator();

            if (cashierorderform_typecmbx.SelectedIndex == -1 ||
                cashierorderform_proidcmbx.SelectedIndex == -1 ||
                cashierorderform_prodname.Text == "" ||
                cashierorderform_quan.Value == 0 ||
                cashierorderform_price.Text == "")
            {
                MessageBox.Show("Please Select Product First", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Open();

                    float getPrice = 0;

                    string selectOrder = "Select pro_price, pro_stock From products Where pro_name = @proname";

                    using (SqlCommand getOrder = new SqlCommand(selectOrder, conn))
                    {
                        getOrder.Parameters.AddWithValue("@proname", cashierorderform_prodname.Text.Trim());
                        SqlDataReader reader = getOrder.ExecuteReader();
                        if (reader.Read())
                        {
                            getPrice = Convert.ToSingle(reader["pro_price"]);
                            int stockQuantity = Convert.ToInt32(reader["pro_stock"]);
                            reader.Close();

                            if (stockQuantity < (int)cashierorderform_quan.Value)
                            {
                                MessageBox.Show("Insufficient stock available for this product.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Update stock quantity
                            string updateStockQuery = "UPDATE products SET pro_stock = @newStock WHERE pro_name = @proname";

                            using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn))
                            {
                                updateStockCmd.Parameters.AddWithValue("@newStock", stockQuantity - (int)cashierorderform_quan.Value);
                                updateStockCmd.Parameters.AddWithValue("@proname", cashierorderform_prodname.Text.Trim());
                                updateStockCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            reader.Close();
                            MessageBox.Show("Product not found in the database.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertOrder = "INSERT INTO orders(customer_id, pro_id, pro_name, pro_type, quantity, pro_price, order_date) " +
                                         "VALUES(@cusID, @proid, @proname, @protype, @quantity, @proprice, @orderdate)";
                    DateTime today = DateTime.Now;
                    using (SqlCommand cmd = new SqlCommand(insertOrder, conn))
                    {
                        cmd.Parameters.AddWithValue("@cusID", idGen);
                        cmd.Parameters.AddWithValue("@proid", cashierorderform_proidcmbx.Text.Trim());
                        cmd.Parameters.AddWithValue("@proname", cashierorderform_prodname.Text.Trim());
                        cmd.Parameters.AddWithValue("@protype", cashierorderform_typecmbx.Text.Trim());
                        float totalPrice = getPrice * ((int)cashierorderform_quan.Value);
                        cmd.Parameters.AddWithValue("@quantity", cashierorderform_quan.Value);
                        cmd.Parameters.AddWithValue("@proprice", totalPrice);
                        cmd.Parameters.AddWithValue("@orderdate", today);

                        cmd.ExecuteNonQuery();

                        // Update total price for the customer
                        string selectTotalPrice = "SELECT SUM(pro_price) FROM orders WHERE customer_id = @cusID";
                        using (SqlCommand getTotalPriceCmd = new SqlCommand(selectTotalPrice, conn))
                        {
                            getTotalPriceCmd.Parameters.AddWithValue("@cusID", idGen);
                            object totalResult = getTotalPriceCmd.ExecuteScalar();
                            if (totalResult != DBNull.Value)
                            {
                                totalPrice = Convert.ToSingle(totalResult);
                                cashierorderform_price2.Text = totalPrice.ToString();
                            }
                        }

                        displayAllOrders();
                        refreshData();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed Connection: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }



        public void clearFields()
        {

            
            cashierorderform_amounttxt.Text = "";
            cashierorderform_change.Text = "";
            cashierorderform_typecmbx.SelectedIndex= -1;
            cashierorderform_proidcmbx.SelectedIndex= -1;
            cashierorderform_prodname.Text = "";
            cashierorderform_quan.Text = "";
            cashierorderform_price.Text = "";
            cashierorderform_price2.Text = "";
            cashierorderform_amounttxt.Text = "";
            cashierorderform_change.Text = "";


        }

        private void cashierorderform_clear2_Click(object sender, EventArgs e)
        {
            clearFields();
        }


        private void cashierorderform_clear_Click(object sender, EventArgs e)
        {
            clearFields();  
        }

        private int rowIndex = 0;

        private void cashierorderform_rbtn_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowIndex = 0;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            displayTotalPrice();
            float y = 0;
            int count = 0;
            int colWidth = 120;
            int headerMargin = 10;
            int tableMargin = 20;

            Font font = new Font("Arial", 12);
            Font bold = new Font("Arial", 12, FontStyle.Bold);
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font labelFont = new Font("Arial", 14, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCentre = new StringFormat();
            alignCentre.Alignment = StringAlignment.Center;
            alignCentre.LineAlignment = StringAlignment.Center;

            string headerText = "Royal Diary Form";
            y = margin + headerMargin;
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, e.MarginBounds.Left
                + cashierorderform_orders.Columns.Count / 2 * colWidth, y, alignCentre);

            count++;
            y += headerFont.GetHeight(e.Graphics) + tableMargin;

            string[] header = {"ProductName", "Type", "Quantity", "Price" };

            for (int i = 0; i < header.Length; i++)
            {
                y = margin + count * bold.GetHeight(e.Graphics) + tableMargin;
                e.Graphics.DrawString(header[i], bold, Brushes.Black, e.MarginBounds.Left + i * colWidth, y, alignCentre);
            }
            count++;

            float rSpace = e.MarginBounds.Bottom;

            while (rowIndex < cashierorderform_orders.RowCount)
            {
                DataGridViewRow row = cashierorderform_orders.Rows[rowIndex];
                for (int i = 0; i < cashierorderform_orders.Columns.Count; i++)
                {
                    object cellValue = row.Cells[i].Value;
                    string cell = (cellValue != null) ? cellValue.ToString() : string.Empty;

                    y = margin + count * font.GetHeight(e.Graphics) + tableMargin;
                    e.Graphics.DrawString(cell, font, Brushes.Black, e.MarginBounds.Left + i * colWidth, y, alignCentre);
                }
                count++;
                rowIndex++;

                if (y + font.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            int labelMargin = (int)Math.Min(rSpace, 90);

            DateTime today = DateTime.Now;

            float labelX = e.MarginBounds.Right - e.Graphics.MeasureString("------------------------------", labelFont).Width;

            y = e.MarginBounds.Bottom - labelMargin - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString("Total Price: Rs" + totalPrice + "\nAmount: Rs"
                + cashierorderform_amounttxt.Text + "\n----------\nChange: Rs" + cashierorderform_change.Text, labelFont, Brushes.Black, labelX, y);

            labelMargin = (int)Math.Min(rSpace, -40);

            string labelText = today.ToString();

            y = e.MarginBounds.Bottom - labelMargin - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(labelText, labelFont, Brushes.Black, e.MarginBounds.Right - e.Graphics.MeasureString("------------------------------", labelFont).Width, y);
        }

        private void cashierorderform_pbtn_Click(object sender, EventArgs e)
        {
            if (cashierorderform_amounttxt.Text == "" || cashierorderform_orders.Rows.Count < 0)
            {
                MessageBox.Show("Something went wrong.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure for Paying?", "Confirmation Message:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                    try
                    {
                    conn.Open();
                    string insertData = "INSERT INTO customers (customer_id, total_price, amount, change, date, product_name) VALUES (@cusID, @proprice, @amount, @change, @date, @productName)";
                    DateTime today = DateTime.Now;

                    using (SqlCommand cmd = new SqlCommand(insertData, conn))
                    {
                        cmd.Parameters.AddWithValue("@cusID", getCustID);
                        cmd.Parameters.AddWithValue("@proprice", cashierorderform_price2.Text.Trim()); 
                        cmd.Parameters.AddWithValue("@amount", cashierorderform_amounttxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@change", cashierorderform_change.Text.Trim());
                        cmd.Parameters.AddWithValue("@date", today);
                        cmd.Parameters.AddWithValue("@productName", cashierorderform_prodname.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Paid Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            displayTotalPrice();
        }


        private void cashierorderform_amounttxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    float enteredAmount = Convert.ToSingle(cashierorderform_amounttxt.Text);
                    float totalPrice = Convert.ToSingle(cashierorderform_price2.Text);

                    if (enteredAmount >= totalPrice)
                    {
                        float change = enteredAmount - totalPrice;
                        cashierorderform_change.Text = change.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Amount entered is insufficient.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cashierorderform_amounttxt.Text = "";
                        cashierorderform_change.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid input: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierorderform_amounttxt.Text = "";
                    cashierorderform_change.Text = "";
                }
            }
        }


        private void cashierorderform_typecmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashierorderform_proidcmbx.SelectedIndex = -1;
            cashierorderform_proidcmbx.Items.Clear();
            cashierorderform_prodname.Text = "";
            cashierorderform_price.Text = "";

            string selectedValue = cashierorderform_typecmbx.SelectedItem as string;
            if (selectedValue != null)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False"))
                    {
                        conn.Open();
                        string selectData = $"select * from products where pro_type = '{selectedValue}' AND pro_status = @prostatus AND date_delete Is NULl ";

                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                        {
                            cmd.Parameters.AddWithValue("@prostatus", "Available");
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["pro_name"].ToString();
                                    cashierorderform_proidcmbx.Items.Add(value);
                                }
                            }
                        }
                    }
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Error:" + exx, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cashierorderform_proidcmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cashierorderform_proidcmbx.SelectedItem as string;

            if (selectedValue != null)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False"))
                    {
                        conn.Open();
                        string selectData = $"select * from products where pro_name= @proName AND pro_status = @prostatus  AND date_delete is NUll    ";

                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                        {
                            cmd.Parameters.AddWithValue("@proName", selectedValue);
                            cmd.Parameters.AddWithValue("@prostatus", "Available");
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string protype = reader["pro_type"].ToString();
                                    string proname = reader["pro_name"].ToString();
                                    string proprice = reader["pro_price"].ToString();

                                    cashierorderform_proidcmbx.Text = protype;
                                    cashierorderform_prodname.Text = proname;
                                    cashierorderform_price.Text = proprice;
                                }
                            }
                        }
                    }
                }
                catch (Exception exx)
                {
                    MessageBox.Show("Error:" + exx, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool emptyFields()
        {
            if (cashierorderform_typecmbx.Text == "" || cashierorderform_proidcmbx.Text == "" || cashierorderform_quan.Text == "" || cashierorderform_prodname.Text == "" || cashierorderform_price.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void cashierorderform_del_Click(object sender, EventArgs e)
        {
            string ProName = cashierorderform_proidcmbx.SelectedItem?.ToString();
            string proType = cashierorderform_typecmbx.SelectedItem?.ToString();
            string price = cashierorderform_price.Text;
            string Quantity = cashierorderform_quan.Value.ToString();

            if (emptyFields())
            {
                MessageBox.Show("All Fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Delete Order:" + cashierorderform_prodname.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        try
                        {
                            conn.Open();

                            // Find the unique customer ID for the current order
                            string uniqueCustomerQuery = "SELECT DISTINCT customer_id FROM orders WHERE pro_name = @ProName AND pro_type = @proType AND pro_price = @price AND quantity = @Quantity";

                            int uniqueCustomerID = 0;
                            using (SqlCommand cmd = new SqlCommand(uniqueCustomerQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@ProName", ProName);
                                cmd.Parameters.AddWithValue("@proType", proType);
                                cmd.Parameters.AddWithValue("@price", float.Parse(price));
                                cmd.Parameters.AddWithValue("@Quantity", int.Parse(Quantity));
                                uniqueCustomerID = (int)cmd.ExecuteScalar();
                            }

                            // Delete the orders with the unique customer ID
                            if (uniqueCustomerID > 0)
                            {
                                string deleteOrderQuery = "DELETE FROM orders WHERE customer_id = @cusID";

                                using (SqlCommand delOrderCmd = new SqlCommand(deleteOrderQuery, conn))
                                {
                                    delOrderCmd.Parameters.AddWithValue("@cusID", uniqueCustomerID);
                                    delOrderCmd.ExecuteNonQuery();
                                }

                                // Delete the corresponding entry from the customers table
                                string deleteCustomerQuery = "DELETE FROM customers WHERE customer_id = @cusID";

                                using (SqlCommand delCustomerCmd = new SqlCommand(deleteCustomerQuery, conn))
                                {
                                    delCustomerCmd.Parameters.AddWithValue("@cusID", uniqueCustomerID);
                                    delCustomerCmd.ExecuteNonQuery();
                                }

                                MessageBox.Show("Deleted Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayAllOrders();
                                refreshData();
                                clearFields();
                            }
                            else
                            {
                                MessageBox.Show("No order found to delete.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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




        private void cashierorderform_orders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cashierorderform_orders.Rows.Count > 0) 
            {
                string ProName= cashierorderform_orders.Rows[0].Cells["ProName"].Value.ToString();
                string proType= cashierorderform_orders.Rows[0].Cells["Protype"].Value.ToString();
                int price= Convert.ToInt32(cashierorderform_orders.Rows[0].Cells["Price"].Value.ToString());
                int Quantity= Convert.ToInt32(cashierorderform_orders.Rows[0].Cells["Quantity"].Value.ToString());

                cashierorderform_typecmbx.Text= proType;
                cashierorderform_proidcmbx.Text = ProName;
                cashierorderform_prodname.Text = ProName;
                cashierorderform_price.Text =Convert.ToString( price);

                cashierorderform_quan.Text=Convert.ToString( Quantity); 
            }
        }
    }
}
