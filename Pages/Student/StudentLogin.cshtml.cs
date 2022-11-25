using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;

namespace CapstoneProject.Pages.Student
{
    public class StudentLoginModel : PageModel
    {
        public List<STUDENT> stu_list = new List<STUDENT>();
        public STUDENT s = new STUDENT();
        public string success_msg = "";
        public string error_msg = "";
      
        public void OnPost()
        {
            try
            {
                string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";
                SqlConnection conn = new SqlConnection(connection);
                s.student_email = Request.Form["student_email"];
                s.student_password = Request.Form["student_password"];
                conn.Open();

                SqlCommand cmd = new SqlCommand("student_verify", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.Add("@semail", System.Data.SqlDbType.VarChar).Value = s.student_email;
                cmd.Parameters.Add("@spassword", System.Data.SqlDbType.VarChar).Value = s.student_password;
               // cmd.ExecuteNonQuery();

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
                if (check == 0)
                {
                    error_msg = "Enter the correct credentials";
                }
                reader.Close();

                SqlCommand cmd1 = new SqlCommand("student_fetch", conn);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;


                cmd1.Parameters.Add("@semail", System.Data.SqlDbType.VarChar).Value = s.student_email;
                cmd1.Parameters.Add("@spassword", System.Data.SqlDbType.VarChar).Value = s.student_password;

                SqlDataReader reader1 = cmd1.ExecuteReader();


                while (reader1.Read())
                { 
                  STUDENT s2=new STUDENT();

                    s2.student_email = reader1.GetString(0);
                    s2.student_password = reader1.GetString(1);

                    stu_list.Add(s2);
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

    public class STUDENT
    {
        public int student_id, course_id;
        public string student_name, student_department, student_email,student_password;
    }
}
