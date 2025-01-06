using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace RDF
{
    internal class adminAddusersdata
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8BL3MIG\\SQLEXPRESS;Initial Catalog=cafedb;Integrated Security=True;Encrypt=False");

       // public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string DateRegistered { get; set; }

        public List<adminAddusersdata> userListData()
        {
            List<adminAddusersdata> listData = new List<adminAddusersdata>();
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                    string selectData = "select * From users";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            adminAddusersdata userData = new adminAddusersdata();
                          //  userData.Id = (int)reader["ID"];
                            userData.Username = reader["username"].ToString();
                            userData.Password = reader["password"].ToString();
                            userData.Role = reader["role"].ToString();
                            userData.Status = reader["status"].ToString();
                            userData.Image = reader["profile_image"].ToString();
                            userData.DateRegistered = reader["date_reg"].ToString();

                            listData.Add(userData);
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

    }
}
