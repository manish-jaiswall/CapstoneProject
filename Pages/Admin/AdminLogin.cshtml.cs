using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        public admin a =new admin();
        public string success_msg = "";
        public string error_msg = "";
        public void OnPost()
        {

            try { 
            string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";
            SqlConnection conn = new SqlConnection(connection);
            a.name = Request.Form["admin_name"];
            a.password = Request.Form["admin_password"];
            conn.Open();

            SqlCommand cmd = new SqlCommand("admin_verify", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = a.name;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = a.password;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                int check = 0;
                while (reader.Read())
                {
                     check = reader.GetInt32(0);

                    if (check == 1)
                    {
                        success_msg = "Successfully Logged In";
                    }

                }
                if(check == 0)
                {
                    error_msg = "Enter the correct credentials";
                }



                conn.Close();
             }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sql Related Issue");
                error_msg = "Error -sql problem";
                
                return;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine("C# related Issue");
                error_msg = "Error C# problem";
                
                return;
            }
            
            

        }
    }

    public class admin
    {
        public int id;
        public string name, password;
    }
}
