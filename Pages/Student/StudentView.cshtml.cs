using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Student
{
    public class StudentViewModel : PageModel
    {
        public STUDENT s1 = new STUDENT();
        public courses c1 = new courses();
        public string success_msg="";
        public string error_msg = "";
        public string student_n="";


        public void OnGet()
        {
            try
            {
                s1.student_email = Request.Query["student_email"];
                s1.student_password = Request.Query["student_password"];



                string ConnectionString = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";

                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();
                string query = "select student_id,student_name,student_email,student_department,course_id from Student where student_email=@semail and student_password=@spassword";
                SqlCommand cmd = new SqlCommand(query, sqlCon);

                /*cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = update.student_id;*/
                cmd.Parameters.Add("@semail", System.Data.SqlDbType.VarChar).Value = s1.student_email;
                cmd.Parameters.Add("@spassword", System.Data.SqlDbType.VarChar).Value = s1.student_password;


               // cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    s1.student_id = reader.GetInt32(0);
                    s1.student_name = reader.GetString(1);
                    s1.student_email = reader.GetString(2);
                    s1.student_department = reader.GetString(3);
                    s1.course_id = reader.GetInt32(4);




                }
               
                student_n = "Hi " + s1.student_name+",";
                reader.Close();
                string query1 = "select course1,course2,course3,course4,course5 from StudentCourse where student_id=@course_id ";
                SqlCommand cmd1 = new SqlCommand(query1, sqlCon);
                cmd1.Parameters.Add("@course_id", System.Data.SqlDbType.Int).Value = s1.course_id;

                //cmd1.ExecuteNonQuery();

               

                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    c1.course1 = reader1.GetString(0);
                    c1.course2 = reader1.GetString(1);
                    c1.course3 = reader1.GetString(2);
                    c1.course4 = reader1.GetString(3);
                    c1.course5 = reader1.GetString(4);




                }



        
                sqlCon.Close();
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
                return;
            }
        }
        public class courses
        {
            public int student_id;
            public string course1, course2, course3, course4, course5;
        }
    }
}
