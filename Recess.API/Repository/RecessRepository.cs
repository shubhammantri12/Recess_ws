using Recess.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Recess.API.Repository
{
    public class RecessRepository
    {
        public bool register(LoginModel user)
        {
            try
            {
                string query = "insert into RecessApp.dbo.userdetails (username,email_id,phone_nbr,course) values (@username,@email_id,@phonenbr,@course)";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = user.username;
                    command.Parameters.Add("@email_id", SqlDbType.VarChar, 50).Value = user.email_id;
                    command.Parameters.Add("@phonenbr", SqlDbType.VarChar, 50).Value = user.phone_nbr;
                    command.Parameters.Add("@course", SqlDbType.VarChar, 50).Value = user.course;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public CourseDetails getCourseDetails()
        {
            try
            {
                string query = "select course_name from RecessApp.dbo.Course_details";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    CourseDetails courses = new CourseDetails();
                    ArrayList array = new ArrayList();
                    foreach (DataRow dataRow in _dt.Rows)
                        array.Add(string.Join(",", dataRow.ItemArray.Select(item => item.ToString())));
                    courses.courseName = array;
                    return courses;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<AllCourses> getAllCourses()
        {
            try
            {
                string query = "recessApp.dbo.getAllCourses";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<AllCourses> courses = new List<AllCourses>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        courses = (from DataRow row in _dt.Rows
                                   select new AllCourses
                                   {
                                       courseId = Convert.ToInt32(row["Courseid"]),
                                       courseCategory = Convert.ToString(row["courseCategory"]),
                                       title = Convert.ToString(row["Title"]),
                                       submittedBy = Convert.ToString(row["submittedby"]),
                                       teacherRating = Convert.ToDouble(row["teacherRating"]),
                                       imageUrl = Convert.ToString(row["imageUrl"])
                                   }).ToList();
                    }
                    return courses;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool isValidEmail(string email)
        {
            try
            {
                string query = "select * from RecessApp.dbo.userdetails where email_id = @email ";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}