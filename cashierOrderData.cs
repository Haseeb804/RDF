using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace RDF
{
    internal class cashierOrderData
    {

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        //public int CID { get; set; }

        //public string ProID { get; set; }
        public string ProName { get; set; }
        public string ProType { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }


        public List<cashierOrderData> orderListData()
        {
            List<cashierOrderData> listData = new List<cashierOrderData>();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                try
                {
                    string selectOrders = "SELECT * FROM orders";
                    using (SqlCommand cmd = new SqlCommand(selectOrders, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cashierOrderData orderData = new cashierOrderData();
                            orderData.ProName = reader["pro_name"].ToString();
                            orderData.ProType = reader["pro_type"].ToString();
                            orderData.Quantity = (int)reader["quantity"];
                            orderData.Price = reader["pro_price"].ToString();
                            listData.Add(orderData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed: " + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return listData;
        }

}   }
