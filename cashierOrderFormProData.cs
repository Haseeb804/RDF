using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace RDF
{
    internal class cashierOrderFormProData
    {
        //public int ID { get; set; }
       // public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Stock { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");
        public List<cashierOrderFormProData> availableProductsData()
        {
            List<cashierOrderFormProData> listData = new List<cashierOrderFormProData>();

            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "Select * From products  Where pro_status = @prostatus AND date_delete IS NULL";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@prostatus", "Available");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cashierOrderFormProData apd = new cashierOrderFormProData();
                           // apd.ID = (int)reader["ID"];
                           // apd.ProductID = reader["pro_id"].ToString();
                            apd.ProductName = reader["pro_name"].ToString();
                            apd.Type = reader["pro_type"].ToString();
                            apd.Stock = reader["pro_stock"].ToString();
                            apd.Price = reader["pro_price"].ToString();
                            apd.Status = reader["pro_status"].ToString();


                            listData.Add(apd);



                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed Connection:" + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return listData;
        }

    }
}
