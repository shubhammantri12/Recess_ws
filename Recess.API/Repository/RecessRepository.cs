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
        public bool register(UserModel user)
        {
            try
            {
                string query = "RecessApp.dbo.insertUserDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.VarChar, 30).Value = user.displayName;
                    command.Parameters.Add("@email_id", SqlDbType.VarChar, 50).Value = user.email;
                    command.Parameters.Add("@phonenbr", SqlDbType.VarChar, 15).Value = Convert.ToString(user.phoneNumber) ;
                    command.Parameters.Add("@course", SqlDbType.VarChar, 15).Value = Convert.ToString(user.course);
                    command.Parameters.Add("@photoUrl", SqlDbType.VarChar, 100).Value = Convert.ToString(user.photoUrl); ;
                    command.Parameters.Add("@emailVerified", SqlDbType.VarChar, 50).Value = user.emailVerified;
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

        public bool UpdateUser(UserModel user)
        {
            try
            {
                string query = "RecessApp.dbo.UpdateUserDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.VarChar, 30).Value = user.displayName;
                    command.Parameters.Add("@email_id", SqlDbType.VarChar, 50).Value = user.email;
                    command.Parameters.Add("@phonenbr", SqlDbType.VarChar, 15).Value = user.phoneNumber;
                    command.Parameters.Add("@course", SqlDbType.VarChar, 15).Value = user.course;
                    command.Parameters.Add("@photoUrl", SqlDbType.VarChar, 100).Value = user.photoUrl;
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
                                       courseCategory = Convert.ToString(row["courseCategory"]).Trim(),
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
        public List<AppDetails> getAppDetails()
        {
            try
            {
                string query = "select * from recessApp.dbo.AppDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<AppDetails> courses = new List<AppDetails>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        courses = (from DataRow row in _dt.Rows
                                   select new AppDetails
                                   {
                                       Id = Convert.ToInt32(row["id"]),
                                       Description = Convert.ToString(row["description"]),
                                       Title = Convert.ToString(row["title"]),
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
        public bool SaveCourseDetails(SaveCourseDetails courseDetails)
        {
            try
            {
                string query = "RecessApp.dbo.SaveCourseDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@title", SqlDbType.VarChar, 30).Value = courseDetails.title;
                    command.Parameters.Add("@description", SqlDbType.VarChar, 50).Value = courseDetails.description;
                    command.Parameters.Add("@submittedBy", SqlDbType.VarChar, 15).Value = courseDetails.submittedBy;
                    command.Parameters.Add("@course", SqlDbType.VarChar, 15).Value = courseDetails.courseCategory;
                    command.Parameters.Add("@photoUrl", SqlDbType.VarChar, 200).Value = courseDetails.imageUrl;
                    command.Parameters.Add("@VideoUrl", SqlDbType.VarChar, 200).Value = courseDetails.VideoUrl;
                    command.Parameters.Add("@beginDate", SqlDbType.DateTime).Value = courseDetails.beginDate;
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = courseDetails.endDate;
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
        public bool checkValidCourseTitle(string name)
        {
            try
            {
                string query = "select * from RecessApp.dbo.courselist where title = @name ";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
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
        public getCourseContent getCourseContent(int courseid)
        {
            try
            {
                string query = "recessApp.dbo.getCourseContent";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@courseid", SqlDbType.Int).Value = courseid;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    getCourseContent courses = new getCourseContent();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        courses.courseid = Convert.ToInt32(_dt.Rows[0]["Courseid"]);
                        courses.courseCategory = Convert.ToString(_dt.Rows[0]["courseCategory"]);
                        courses.title = Convert.ToString(_dt.Rows[0]["Title"]);
                        courses.description = Convert.ToString(_dt.Rows[0]["Description"]);
                        courses.submittedBy = Convert.ToString(_dt.Rows[0]["submittedby"]);
                        courses.VideoUrl = Convert.ToString(_dt.Rows[0]["videoUrl"]);
                        courses.imageUrl = Convert.ToString(_dt.Rows[0]["imageUrl"]);
                        courses.teacherRating = Convert.ToDouble(_dt.Rows[0]["teacherRating"]);
                        courses.beginDate = Convert.ToDateTime(_dt.Rows[0]["beginDate"]);
                        courses.endDate = Convert.ToDateTime(_dt.Rows[0]["endDate"]);
                    }
                    return courses;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<TeacherDetails> getAllTeacherDetails()
        {
            try
            {
                string query = "recessApp.dbo.getAllTeacherDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<TeacherDetails> teachers = new List<TeacherDetails>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        teachers = (from DataRow row in _dt.Rows
                                   select new TeacherDetails
                                   {
                                       teacherId = Convert.ToInt32(row["teacherid"]),
                                       name = Convert.ToString(row["teachername"]),
                                       description = Convert.ToString(row["description"]),
                                       totalcount = Convert.ToInt32(row["ratingCount"]),
                                       currentRating = Convert.ToDouble(row["teacherRating"]),
                                       photourl = Convert.ToString(row["photourl"])
                                   }).ToList();
                    }
                    return teachers;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<AllCourses> getAllVideos()
        {
            try
            {
                string query = "recessApp.dbo.getAllVideos";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<AllCourses> videos = new List<AllCourses>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        videos = (from DataRow row in _dt.Rows
                                   select new AllCourses
                                   {
                                       courseId = Convert.ToInt32(row["videoid"]),
                                       title = Convert.ToString(row["videoTitle"]),
                                       courseCategory= Convert.ToString(row["videoCategory"]),
                                       //videodescription = Convert.ToString(row["videoDescription"]),
                                       //videoRatingCount = Convert.ToInt32(row["totalRatingCount"]),
                                       //submittedOn = Convert.ToDateTime(row["submittedOn"]),
                                       submittedBy = Convert.ToString(row["submittedBy"]),
                                       //Teacherid= Convert.ToInt32(row["teacherId"]),
                                       teacherRating = Convert.ToDouble(row["videoRating"]),
                                       imageUrl = Convert.ToString(row["imageUrl"])
                                   }).ToList();
                    }
                    return videos;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}