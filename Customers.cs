using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDF
{
    internal class Customers
    {

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public int CustomerID { set; get; }
       // public string ProductName { set; get; }
        public string TotalPrice { set; get; }
        public string Amount { set; get; }
        public string Change { set; get; }
        public string Date { set; get; }


        public List<Customers> allCustomersData()
        {
            List<Customers> list = new List<Customers>();

            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();

                    string selectData = "Select * From customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Customers cData = new Customers();
                            cData.CustomerID = (int)reader["customer_id"];
                         //   cData.ProductName = reader["pro_name"].ToString();
                            cData.TotalPrice = reader["total_price"].ToString();
                            cData.Amount = reader["amount"].ToString();
                            cData.Change = reader["change"].ToString();
                            cData.Date = reader["date"].ToString();

                            list.Add(cData);



                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed COnnection" + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return list;
        }
    }
}

