using CapstoneProject.Pages.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Admin
{
    public class EditStudentModel : PageModel
    {
      
        public STUDENT update = new STUDENT();
        public string success_msg = "";
        public string error_msg = "";
        public void OnGet()
        {
            try
            {
                update.student_id= Convert.ToInt32(Request.Query["id"]);



                string ConnectionString = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";

                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                sqlCon.Open();
                string query = "select student_name,student_email,student_department,student_password,course_id from Student where student_id=@id";


                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = update.student_id;
               

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();



               

                while (reader.Read())
                {

                    update.student_name = reader.GetString(0);
                    update.student_email = reader.GetString(1);
                    update.student_department = reader.GetString(2);
                    update.student_password = reader.GetString(3);
                    update.course_id = reader.GetInt32(4);

                    


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

                update.student_id = Convert.ToInt32(Request.Query["id"]);
                string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";
                
                SqlConnection conn = new SqlConnection(connection);
               
                update.student_name = Request.Form["Student_Name"];
                
                update.student_email = Request.Form["Student_Email"];
                update.student_department =Request.Form["Student_Department"];
                update.student_password = Request.Form["Student_Password"];
                update.course_id = Convert.ToInt32(Request.Form["Student_Courseid"]);

                


                conn.Open();


                SqlCommand cmd = new SqlCommand("update_student", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@sid", System.Data.SqlDbType.VarChar).Value = update.student_id;
                cmd.Parameters.Add("@sname", System.Data.SqlDbType.VarChar).Value = update.student_name;
                cmd.Parameters.Add("@semail", System.Data.SqlDbType.VarChar).Value = update.student_email;
                cmd.Parameters.Add("@sdepartment", System.Data.SqlDbType.VarChar).Value = update.student_department;
                cmd.Parameters.Add("@spassword", System.Data.SqlDbType.VarChar).Value = update.student_password;
                cmd.Parameters.Add("@scourseid",System.Data.SqlDbType.Int).Value= update.course_id;

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
}
