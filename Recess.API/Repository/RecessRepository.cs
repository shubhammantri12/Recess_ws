﻿using Recess.API.Models;
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
                    DataSet _ds = new DataSet();
                    _adapter.Fill(_ds);
                    connection.Close();
                    getCourseContent courses = new getCourseContent();
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        courses.coursecontent = fillCourseContent(_ds.Tables[0]);
                    }
                    if (_ds.Tables[1].Rows.Count > 0)
                    {
                        courses.scheduledClasses = fillScheduledClasses(_ds.Tables[1]);
                    }
                    if (_ds.Tables[2].Rows.Count > 0)
                    {
                        courses.teachers = fillCourseTeacherDetails(_ds.Tables[2]);
                    }
                        //if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                    //{
                    //    courses.courseid = Convert.ToInt32(_dt.Rows[0]["Courseid"]);
                    //    courses.courseCategory = Convert.ToString(_dt.Rows[0]["courseCategory"]);
                    //    courses.title = Convert.ToString(_dt.Rows[0]["Title"]);
                    //    courses.description = Convert.ToString(_dt.Rows[0]["Description"]);
                    //    courses.submittedBy = Convert.ToString(_dt.Rows[0]["submittedby"]);
                    //    courses.VideoUrl = Convert.ToString(_dt.Rows[0]["videoUrl"]);
                    //    courses.imageUrl = Convert.ToString(_dt.Rows[0]["imageUrl"]);
                    //    courses.courseRating = Convert.ToDouble(_dt.Rows[0]["courseRating"]);
                    //    courses.totalRatingCount = Convert.ToInt32(_ds.Tables[0].Rows[0]["totalRatingCount"]);
                    //    courses.beginDate = Convert.ToDateTime(_dt.Rows[0]["beginDate"]);
                    //    courses.endDate = Convert.ToDateTime(_dt.Rows[0]["endDate"]);
                    //}
                    return courses;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private courseDetailsByCourseid fillCourseContent(DataTable _dt)
        {
            try
            {
                courseDetailsByCourseid courses = new courseDetailsByCourseid();
                courses.courseid = Convert.ToInt32(_dt.Rows[0]["Courseid"]);
                courses.courseCategory = Convert.ToString(_dt.Rows[0]["courseCategory"]);
                courses.title = Convert.ToString(_dt.Rows[0]["Title"]);
                courses.description = Convert.ToString(_dt.Rows[0]["Description"]);
                courses.submittedBy = Convert.ToString(_dt.Rows[0]["submittedby"]);
                courses.VideoUrl = Convert.ToString(_dt.Rows[0]["videoUrl"]);
                courses.imageUrl = Convert.ToString(_dt.Rows[0]["imageUrl"]);
                courses.courseRating = Convert.ToDouble(_dt.Rows[0]["courseRating"]);
                courses.totalRatingCount = Convert.ToInt32(_dt.Rows[0]["totalRatingCount"]);
                courses.beginDate = Convert.ToDateTime(_dt.Rows[0]["beginDate"]);
                courses.endDate = Convert.ToDateTime(_dt.Rows[0]["endDate"]);
                return courses;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private List<ScheduledClasses> fillScheduledClasses(DataTable _dt)
        {
            List<ScheduledClasses> classList = new List<ScheduledClasses>();
            try
            {
               
                classList = (from DataRow row in _dt.Rows
                             select new ScheduledClasses
                             {
                             classId = Convert.ToInt32(row["classid"]),
                            courseId = Convert.ToInt32(row["courseid"]),
                            classTitle = Convert.ToString(row["classTitle"]),
                            classDescription = Convert.ToString(row["classDescription"]),
                            beginDate = Convert.ToDateTime(row["beginTime"]),
                            endDate = Convert.ToDateTime(row["endTime"]),
                            teacherid = Convert.ToInt32(row["teacherid"])
            }).ToList();
                return classList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private CourseTeacherDetails fillCourseTeacherDetails(DataTable _dt)
        {
            try
            {
                CourseTeacherDetails teachers = new CourseTeacherDetails();
                teachers.teacherId = Convert.ToInt32(_dt.Rows[0]["teacherid"]);
                teachers.teacherName = Convert.ToString(_dt.Rows[0]["teachername"]);
                teachers.description = Convert.ToString(_dt.Rows[0]["description"]);
                teachers.teacherRating = Convert.ToDouble(_dt.Rows[0]["teacherRating"]);
                teachers.teacherRatingCount = Convert.ToInt32(_dt.Rows[0]["ratingCount"]);
                teachers.photoUrl = Convert.ToString(_dt.Rows[0]["photourl"]);
                teachers.zoomid = Convert.ToString(_dt.Rows[0]["zoomid"]);
                
                return teachers;
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
        public bool registerClass(RegisterClass registerObject)
        {
            try
            {
                string query = "RecessApp.dbo.RegisterClass";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@classid", SqlDbType.Int).Value = registerObject.classid;
                    command.Parameters.Add("@courseid", SqlDbType.Int).Value = registerObject.courseid;
                    command.Parameters.Add("@username", SqlDbType.VarChar, 25).Value = Convert.ToString(registerObject.username);
                    command.Parameters.Add("@useremail", SqlDbType.VarChar, 100).Value = Convert.ToString(registerObject.useremail);
                    command.Parameters.Add("@teacherid", SqlDbType.Int).Value = Convert.ToString(registerObject.teacherid); ;
                    command.Parameters.Add("@classlink", SqlDbType.VarChar, 100).Value = registerObject.classLink;
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
        public bool SaveUserReviews(SaveUserReviews SaveUserReviews)
        {
            try
            {
                string query = "RecessApp.dbo.SaveUserReviews";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = SaveUserReviews.username;
                    command.Parameters.Add("@reviewText", SqlDbType.VarChar, 1000).Value = SaveUserReviews.reviewText;
                    command.Parameters.Add("@useremail", SqlDbType.VarChar, 50).Value = SaveUserReviews.useremail;
                    command.Parameters.Add("@reviewFor", SqlDbType.VarChar, 10).Value = SaveUserReviews.reviewFor;
                    command.Parameters.Add("@submittedOn", SqlDbType.DateTime, 200).Value = SaveUserReviews.submittedOn;
                    command.Parameters.Add("@teacherid", SqlDbType.Int).Value = SaveUserReviews.teacherid;
                    command.Parameters.Add("@courseid", SqlDbType.Int).Value = SaveUserReviews.courseid;
                    command.Parameters.Add("@videoid", SqlDbType.Int).Value = SaveUserReviews.videoid;
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
        public List<SaveUserReviews> GetUserReviews(int id,string type)
        {
            try
            {
                string query = "recessApp.dbo.GetUserReviews";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = type.ToUpper();
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<SaveUserReviews> userReviews = new List<SaveUserReviews>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        userReviews = (from DataRow row in _dt.Rows
                                    select new SaveUserReviews
                                    {
                                        courseid = Convert.ToInt32(row["courseid"]),
                                        videoid = Convert.ToInt32(row["videoid"]),
                                        username = Convert.ToString(row["username"]),
                                        reviewText = Convert.ToString(row["reviewText"]),
                                        teacherid = Convert.ToInt32(row["teacherid"]),
                                        submittedOn = Convert.ToDateTime(row["submittedOn"])
                                    }).ToList();
                    }
                    return userReviews;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}