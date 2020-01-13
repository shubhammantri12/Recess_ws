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
                                       teacherRating = Convert.ToDouble(row["courseRating"]),
                                       imageUrl = Convert.ToString(row["imageUrl"]),
                                       ratingCount = Convert.ToInt32(row["TotalRatingCount"])
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
        public bool isValidEmail(string email,string type)
        {
            try
            {
                string query = string.Empty;
                if (type.ToUpper() =="T")
                {
                    query = "select * from RecessApp.dbo.teacherDetails where email_id = @email ";
                }
                else
                {
                  query = "select * from RecessApp.dbo.userdetails where email_id = @email ";
                }
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
                    else
                    {
                        return true;
                    }
                }
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
                    if (_ds.Tables[3].Rows.Count > 0)
                    {
                        courses.similarCourses = fillSimilarCourses(_ds.Tables[3]);
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
                            teacherId = Convert.ToInt32(row["teacherid"])
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
        private List<AllCourses> fillSimilarCourses(DataTable _dt)
        {
            List<AllCourses> CourseList = new List<AllCourses>();
            try
            {

                CourseList = (from DataRow row in _dt.Rows
                             select new AllCourses
                             {
                                       courseId = Convert.ToInt32(row["Courseid"]),
                                       courseCategory = Convert.ToString(row["courseCategory"]).Trim(),
                                       title = Convert.ToString(row["Title"]),
                                       submittedBy = Convert.ToString(row["submittedby"]),
                                       teacherRating = Convert.ToDouble(row["teacherRating"]),
                                       imageUrl = Convert.ToString(row["imageUrl"])
                             }).ToList();
                return CourseList;
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
                                       totalcount = row["ratingCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["ratingCount"]) ,
                                       currentRating = row["teacherRating"] == DBNull.Value ? 0 : Convert.ToDouble(row["teacherRating"]),
                                       photourl = row["photourl"] == DBNull.Value ? "" : Convert.ToString(row["photourl"])
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
                    command.Parameters.Add("@teacherid", SqlDbType.Int).Value = Convert.ToString(registerObject.teacherId); ;
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
                    command.Parameters.Add("@teacherid", SqlDbType.Int).Value = SaveUserReviews.teacherId;
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
                                        teacherId = Convert.ToInt32(row["teacherid"]),
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
        public bool SaveTeacherDetails(SaveTeacherDetails TeacherDetails)
        {
            try
            {
                string query = "RecessApp.dbo.SaveTeacherDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Teachername", SqlDbType.VarChar, 50).Value = TeacherDetails.name;
                    command.Parameters.Add("@phoneNumber", SqlDbType.VarChar, 15).Value = TeacherDetails.phoneNumber;
                    command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = TeacherDetails.email;
                    command.Parameters.Add("@photourl", SqlDbType.VarChar, 250).Value = TeacherDetails.photourl;
                    command.Parameters.Add("@description", SqlDbType.VarChar, 500).Value = TeacherDetails.description;
                    command.Parameters.Add("@course", SqlDbType.Char, 20).Value = TeacherDetails.courseCategory;
                    command.Parameters.Add("@gender", SqlDbType.Char, 2).Value = TeacherDetails.Gender;
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
        public bool checkIfPreviouslyRegisterd(RegisterClass register)
        {
            try
            {
                string query = "select * from RecessApp.dbo.registedClasses where useremail = @email and classid = @classid ";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = register.useremail;
                    command.Parameters.Add("@classid", SqlDbType.VarChar, 50).Value = register.classid;
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
        public List<myRegisteredClasses> getMyRegisteredClasses(string emailId)
        {
            try
            {
                string query = "recessApp.dbo.getMyRegisteredClass";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@useremail", SqlDbType.VarChar, 50).Value = emailId;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<myRegisteredClasses> userReviews = new List<myRegisteredClasses>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        userReviews = (from DataRow row in _dt.Rows
                                       select new myRegisteredClasses
                                       {
                                           courseId = Convert.ToInt32(row["courseid"]),
                                          classId = Convert.ToInt32(row["classid"]),
                                           classTitle = Convert.ToString(row["classTitle"]),
                                           beginDate = Convert.ToDateTime(row["beginTime"]),
                                           endDate = Convert.ToDateTime(row["endTime"])
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
        public teacherContent GetTeacherInfo(int teacherId)
        {
            try
            {
                string query = "recessApp.dbo.getTeacherContent";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@teacherid", SqlDbType.Int).Value = teacherId;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    DataSet _ds = new DataSet();
                    _adapter.Fill(_ds);
                    connection.Close();
                    teacherContent teacherInfo = new teacherContent();
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        teacherInfo.teacherInfo = fillTeacherInfo(_ds.Tables[0],_ds);
                    }
                    if (_ds.Tables[1].Rows.Count > 0)
                    {
                        teacherInfo.Courses = fillTeacherCourseInfo(_ds.Tables[1]);
                    }
                    if (_ds.Tables[2].Rows.Count > 0)
                    {
                        teacherInfo.Videos = fillTeacherVideoInfo(_ds.Tables[2]);
                    }
                    //if (_ds.Tables.Count > 0)
                    //{
                    //    teacherInfo.teacherStatistics = fillTeacherStatistics(_ds);
                    //}
                    return teacherInfo;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private teacherInfo fillTeacherInfo(DataTable _dt,DataSet _ds)
        {
            try
            {
                teacherInfo teacherInfo = new teacherInfo();
                teacherInfo.teacherName = Convert.ToString(_dt.Rows[0]["teachername"]);
                teacherInfo.rating = Convert.ToDouble(_dt.Rows[0]["teacherRating"]);
                teacherInfo.ratingCount = Convert.ToInt32(_dt.Rows[0]["ratingCount"]);
                teacherInfo.imageUrl = Convert.ToString(_dt.Rows[0]["photourl"]);
                teacherInfo.emailId = Convert.ToString(_dt.Rows[0]["email_id"]);
                teacherInfo.description = Convert.ToString(_dt.Rows[0]["description"]);
                teacherInfo.videoCount = Convert.ToInt32(_ds.Tables[3].Rows[0]["videoCount"]);
                teacherInfo.classCount = Convert.ToInt32(_ds.Tables[4].Rows[0]["classCount"]);
                teacherInfo.courseCount = Convert.ToInt32(_ds.Tables[5].Rows[0]["courseCount"]);

                return teacherInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private List<teacherCourseContent> fillTeacherCourseInfo(DataTable _dt)
        {
            try
            {
                List<teacherCourseContent> teacherCourses = new List<teacherCourseContent>();
                teacherCourses = (from DataRow row in _dt.Rows
                                  select new teacherCourseContent
                                  {
                                      Id = Convert.ToInt32(row["Courseid"]),
                                      imageUrl = Convert.ToString(row["imageUrl"]),
                                      title = Convert.ToString(row["Title"]),
                                      description = Convert.ToString(row["description"]),
                                      submittedBy = Convert.ToString(row["submittedBy"]),
                                      rating = Convert.ToDouble(row["courseRating"]),
                                      ratingCount = Convert.ToInt32(row["TotalRatingCount"])
                                      //category = Convert.ToString(row["courseCategory"]),
                                      //beginDate = Convert.ToDateTime(row["beginDate"]),
                                      //endDate = Convert.ToDateTime(row["endDate"])
                                  }).ToList();
                return teacherCourses;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private List<teacherVideoContent> fillTeacherVideoInfo(DataTable _dt)
        {
            try
            {
                List<teacherVideoContent> teacherVideos = new List<teacherVideoContent>();
                teacherVideos = (from DataRow row in _dt.Rows
                                  select new teacherVideoContent
                                  {
                                      Id = Convert.ToInt32(row["videoid"]),
                                      imageUrl = Convert.ToString(row["imageUrl"]),
                                      title = Convert.ToString(row["videoTitle"]),
                                      category = Convert.ToString(row["videoCategory"]),
                                      submittedOn = Convert.ToDateTime(row["submittedOn"]),
                                      submittedBy = Convert.ToString(row["submittedBy"]),
                                      rating = Convert.ToDouble(row["videoRating"]),
                                      ratingCount = Convert.ToInt32(row["totalRatingCount"])
                                  }).ToList();
                return teacherVideos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private teacherStatistics fillTeacherStatistics(DataSet _ds)
        {
            try
            {
                teacherStatistics teacherStatistics = new teacherStatistics();
                teacherStatistics.videoCount = Convert.ToInt32(_ds.Tables[3].Rows[0]["videoCount"]);
                teacherStatistics.classCount = Convert.ToInt32(_ds.Tables[4].Rows[0]["classCount"]);
                teacherStatistics.courseCount = Convert.ToInt32(_ds.Tables[5].Rows[0]["courseCount"]);
               
                return teacherStatistics;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public videoContent GetVideoInfo(int videoId)
        {
            try
            {
                string query = "recessApp.dbo.getVideoDetails";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@videoid", SqlDbType.Int).Value = videoId;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    DataSet _ds = new DataSet();
                    _adapter.Fill(_ds);
                    connection.Close();
                    videoContent videoInfo = new videoContent();
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        videoInfo.videoInfo = fillVideoInfo(_ds.Tables[0]);
                    }
                    //if (_ds.Tables[1].Rows.Count > 0)
                    //{
                    //    videoInfo.teacherInfo = fillVideoTeacherInfo(_ds.Tables[1]);
                    //}
                    if (_ds.Tables[3].Rows.Count > 0)
                    {
                        videoInfo.relatedCourses = fillRelatedCourseInfo(_ds.Tables[3]);
                    }
                    if (_ds.Tables[2].Rows.Count > 0)
                    {
                        videoInfo.similarVideos = fillSimilarVideoInfo(_ds.Tables[2]);
                    }
                    return videoInfo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private videoInfo fillVideoInfo(DataTable _dt)
        {
            try
            {
                videoInfo videoInfo = new videoInfo();
                videoInfo.title = Convert.ToString(_dt.Rows[0]["videoTitle"]);
                videoInfo.description = Convert.ToString(_dt.Rows[0]["videoDescription"]);
                videoInfo.category = Convert.ToString(_dt.Rows[0]["videoCategory"]);
                videoInfo.videoUrl = Convert.ToString(_dt.Rows[0]["VideoUrl"]);
                videoInfo.submittedOn = Convert.ToDateTime(_dt.Rows[0]["submittedOn"]);
                videoInfo.teacherId = Convert.ToInt32(_dt.Rows[0]["teacherId"]);
                videoInfo.teacherName = Convert.ToString(_dt.Rows[0]["submittedBy"]);
                videoInfo.rating = Convert.ToDouble(_dt.Rows[0]["videoRating"]);
                videoInfo.ratingCount = Convert.ToInt32(_dt.Rows[0]["totalRatingCount"]);
                return videoInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private teacherInfoForVideo fillVideoTeacherInfo(DataTable _dt)
        {
            try
            {
                teacherInfoForVideo teacherInfo = new teacherInfoForVideo();
                teacherInfo.id = Convert.ToInt32(_dt.Rows[0]["teacherid"]);
                teacherInfo.name = Convert.ToString(_dt.Rows[0]["teachername"]);
                teacherInfo.description = Convert.ToString(_dt.Rows[0]["description"]);
                teacherInfo.photoUrl = Convert.ToString(_dt.Rows[0]["photourl"]);
                teacherInfo.rating = Convert.ToDouble(_dt.Rows[0]["teacherRating"]);
                teacherInfo.ratingCount = Convert.ToInt32(_dt.Rows[0]["ratingCount"]);
                return teacherInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private List<videoCourseContent> fillRelatedCourseInfo(DataTable _dt)
        {
            try
            {
                List<videoCourseContent> Courses = new List<videoCourseContent>();
                Courses = (from DataRow row in _dt.Rows
                                  select new videoCourseContent
                                  {
                                      id = Convert.ToInt32(row["Courseid"]),
                                      imageUrl = Convert.ToString(row["imageUrl"]),
                                      title = Convert.ToString(row["Title"]),
                                      category = Convert.ToString(row["courseCategory"]),
                                      beginDate = Convert.ToDateTime(row["beginDate"]),
                                      endDate = Convert.ToDateTime(row["endDate"]),
                                      rating = Convert.ToDouble(row["CourseRating"]),
                                      ratingCount = Convert.ToInt32(row["TotalRatingCount"]),
                                      submittedBy = Convert.ToString(row["submittedBy"])
                                  }).ToList();
                return Courses;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private List<similarVideoDetails> fillSimilarVideoInfo(DataTable _dt)
        {
            try
            {
                List<similarVideoDetails> similarVideos = new List<similarVideoDetails>();
                similarVideos = (from DataRow row in _dt.Rows
                                 select new similarVideoDetails
                                 {
                                     id = Convert.ToInt32(row["videoid"]),
                                     imageUrl = Convert.ToString(row["imageUrl"]),
                                     title = Convert.ToString(row["videoTitle"]),
                                     category = Convert.ToString(row["videoCategory"]),
                                     submittedOn = Convert.ToDateTime(row["submittedOn"]),
                                     submittedBy = Convert.ToString(row["submittedBy"]),
                                     rating = Convert.ToDouble(row["videoRating"]),
                                     ratingCount = Convert.ToInt32(row["totalRatingCount"])
            }).ToList();
                return similarVideos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ValidateTeacher(string email)
        {
            try
            {
                string query = string.Empty;
                query = "select * from RecessApp.dbo.teacherdetails where email_id = @email ";
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SearchModel> search(string searchText,string type)
        {
            try
            {
                string query = "recessApp.dbo.getSearchResults";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@searchText", SqlDbType.VarChar,50).Value = searchText;
                    command.Parameters.Add("@type", SqlDbType.VarChar,5).Value = type;
                    connection.Open();
                    SqlDataAdapter _adapter = new SqlDataAdapter(command);
                    DataTable _dt = new DataTable();
                    _adapter.Fill(_dt);
                    connection.Close();
                    List<SearchModel> searchResult = new List<SearchModel>();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        searchResult = (from DataRow row in _dt.Rows
                                        select new SearchModel
                                        {
                                            id = Convert.ToInt32(row["id"]),
                                            title = Convert.ToString(row["title"])
                                        }).ToList();
                    }
                    return searchResult;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}