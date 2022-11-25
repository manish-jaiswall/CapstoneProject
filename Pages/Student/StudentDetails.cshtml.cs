using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CapstoneProject.Pages.Student
{
    public class StudentDetailsModel : PageModel
    {
        public List<STUDENT> stu_list = new List<STUDENT>();
        public void OnGet()
        {
            try
            {
                string connection = "Data Source=INLPF2RXRDD\\MSSQLSERVER1;Initial Catalog=Capstone;trusted_connection=true";
                
                SqlConnection conn = new SqlConnection(connection);
                conn.Open();

                SqlCommand cmd = new SqlCommand("stu_details", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    STUDENT s1 = new STUDENT();
                    s1.student_id = reader.GetInt32(0);
                    s1.student_name = reader.GetString(1);
                    s1.student_email = reader.GetString(2);
                    s1.student_department = reader.GetString(3);
                    s1.student_password= reader.GetString(4);
                    s1.course_id = reader.GetInt32(5);


                    stu_list.Add(s1);


                }

                
                conn.Close();
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sql Related Issue");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine("C# related Issue");
            }
        }
    }
}
