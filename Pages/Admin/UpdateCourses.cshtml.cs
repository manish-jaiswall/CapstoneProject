using CapstoneProject.Pages.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Admin
{
    
    public class UpdateCoursesModel : PageModel
    {
        public courses course_update1 = new courses();
        public courses course_update2 = new courses();
        public string success_msg = "";
        public string error_msg = "";
        public void OnGet()
        {
            try
            {
                
                course_update1.student_id = Convert.ToInt32(Request.Query["course_id"]);



                string ConnectionString = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";

                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();
                string query = "select course1,course2,course3,course4,course5 from StudentCourse where student_id=@course_id";
                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.Add("@course_id", System.Data.SqlDbType.Int).Value = course_update1.student_id;


                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    course_update1.course1 = reader.GetString(0);
                    course_update1.course2 = reader.GetString(1);
                    course_update1.course3 = reader.GetString(2);
                    course_update1.course4 = reader.GetString(3);
                    course_update1.course5 = reader.GetString(4);




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
        public void OnPost()
        {
            try
            {
               
                course_update2.student_id = Convert.ToInt32(Request.Query["course_id"]);
                string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";

                SqlConnection conn = new SqlConnection(connection);
                conn.Open();
                course_update2.course1 = Request.Form["Course1"];

                course_update2.course2 = Request.Form["Course2"];
                course_update2.course3 = Request.Form["Course3"];
                course_update2.course4 = Request.Form["Course4"];
                course_update2.course5= Request.Form["Course5"];




               


                SqlCommand cmd = new SqlCommand("update_course", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@course_id", System.Data.SqlDbType.Int).Value = course_update2.student_id;
                cmd.Parameters.Add("@course1", System.Data.SqlDbType.VarChar).Value = course_update2.course1;
                cmd.Parameters.Add("@course2", System.Data.SqlDbType.VarChar).Value = course_update2.course2;
                cmd.Parameters.Add("@course3", System.Data.SqlDbType.VarChar).Value = course_update2.course3;
                cmd.Parameters.Add("@course4", System.Data.SqlDbType.VarChar).Value = course_update2.course4;
                cmd.Parameters.Add("@course5", System.Data.SqlDbType.VarChar).Value = course_update2.course5;
               

                cmd.ExecuteNonQuery();
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
                return;
            }
            success_msg = "Successfully Updated";
        }
    }

    public class courses
    {
        public int student_id;
        public string course1, course2, course3, course4, course5;
    }

}
