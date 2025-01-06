using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RDF
{
    internal class adminaddproductdata
    {
       // public int ID { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Stock { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public string DateInsert { get; set; }
        public string DateUpdate { get; set; }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

        public List<adminaddproductdata> productListData()
        {
            List<adminaddproductdata> listData = new List<adminaddproductdata>();
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "select * From products WHERE Date_delete is NULL ";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            adminaddproductdata apd = new adminaddproductdata();
                           // apd.ID = (int)reader["ID"];
                            apd.ProductNumber = reader["pro_id"].ToString();
                            apd.ProductName = reader["pro_name"].ToString();
                            apd.Type = reader["pro_type"].ToString();
                            apd.Image = reader["pro_image"].ToString();
                            apd.Stock = reader["pro_stock"].ToString();
                            apd.Price = reader["pro_price"].ToString();
                            apd.Status = reader["pro_status"].ToString();
                            apd.DateInsert = reader["date_insert"].ToString();
                            apd.DateUpdate = reader["date_update"].ToString();



                            listData.Add(apd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed:" + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return listData;
        }

        public List<adminaddproductdata> availableProductsData()
        {
            List<adminaddproductdata> listData = new List<adminaddproductdata>();

            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string selectData = "Select * From products";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            adminaddproductdata apd = new adminaddproductdata();
                           // apd.ID = (int)reader["ID"];
                            apd.ProductNumber = reader["pro_id"].ToString();
                            apd.ProductName = reader["pro_name"].ToString();
                            apd.Type = reader["pro_type"].ToString();
                            apd.Image = reader["pro_image"].ToString();
                            apd.Stock = reader["pro_stock"].ToString();
                            apd.Price = reader["pro_price"].ToString();
                            apd.Status = reader["pro_status"].ToString();
                            apd.DateInsert = reader["date_insert"].ToString();
                            apd.DateUpdate = reader["date_update"].ToString();

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
