using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Student
{
    public class StudentSignUpModel : PageModel
    {
        public STUDENT signup = new STUDENT();
        public string success_msg = "";
        public string error_msg = "";

        public void OnPost()
        {
            try
            {
                string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";

                SqlConnection conn = new SqlConnection(connection);
                conn.Open();
                signup.student_name = Request.Form["student_name"];
                signup.student_email = Request.Form["student_email"];
                signup.student_department = Request.Form["student_department"];
                signup.student_password= Request.Form["student_password"];
                signup.course_id = Convert.ToInt32(Request.Form["course_id"]);

                SqlCommand cmd = new SqlCommand("student_signup",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@sname", System.Data.SqlDbType.VarChar).Value = signup.student_name;
                cmd.Parameters.Add("@semail", System.Data.SqlDbType.VarChar).Value = signup.student_email;
                cmd.Parameters.Add("@sdepartment", System.Data.SqlDbType.VarChar).Value = signup.student_department;
                cmd.Parameters.Add("@spassword", System.Data.SqlDbType.VarChar).Value = signup.student_password;
                cmd.Parameters.Add("@scourse", System.Data.SqlDbType.Int).Value = signup.course_id;


                cmd.ExecuteNonQuery();

                conn.Close();
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sql Related Issue");
                error_msg = "Error -sql problem";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine("C# related Issue");
                error_msg = "Error C# problem"; 
            }
            success_msg = "SignUp Successful";
        }
    }
}
