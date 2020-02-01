using Newtonsoft.Json;
using Recess.API.Business;
using Recess.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Recess.API.Controllers
{
    [RoutePrefix("api/Recess")]
    public class RecessController : ApiController
    {
        RecessBusiness _business = new RecessBusiness(); 
        
            [HttpGet]
            [Route("TestApi")]
            public string TestApi()
            {
                try
                {
                    return "Hello";
                }
                catch (Exception)
                {
                    throw;
                }
            }
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage login(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                
                    response = _business.login(request);
                    return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage logout(LoginRequest request)
        {
            try
            {
                bool response = false;

                response = _business.logout(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("registerUser")]
        public HttpResponseMessage registerUser(UserModel user)
        {
            try
            {
                bool response = false;
                if (_business.IsValidEmail(user.email,"U"))
                {                
                  response = _business.register(user);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Email_Id is already registered");
                }
                                 
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getCourseDetails")]
        public HttpResponseMessage getCourseDetails()
        {
            try
            {
                CourseDetails response = _business.getCourseDetails();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        [HttpGet]
        [Route("getAllCourses")]
        public HttpResponseMessage getAllCourses()
        {
            try
            {
                List<AllCourses> response = _business.getAllCourses();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        [HttpGet]
        [Route("getAppDetails")]
        public HttpResponseMessage getAppDetails()
        {
            try
            {
                List<AppDetails> response = _business.getAppDetails();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public HttpResponseMessage UpdateUser(UserModel user)
        {
            try
            {
                bool response = false;     
                response = _business.UpdateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("SaveCourseDetails")]
        public HttpResponseMessage SaveCourseDetails(SaveCourseDetails course)
        {
            try
            {
                
                bool response = false;
                //var multipartstreamprovider = new MultipartMemoryStreamProvider();
                //await Request.Content.ReadAsMultipartAsync(multipartstreamprovider , new CancellationToken());
                //string rawRequest;
                //using (var stream = new StreamReader(multipartstreamprovider.Contents[0].ReadAsStreamAsync().Result))
                //{
                //    stream.BaseStream.Position = 0;
                //    rawRequest = stream.ReadToEnd();
                //}
                //var course = JsonConvert.DeserializeObject<SaveCourseDetails>(rawRequest);
                //    var httpFiles = HttpContext.Current.Request.Files;
                if (_business.IsValidCourseTitle(course.title))
                {
                    response = _business.SaveCourseDetails(course);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A course with same title already exists");
                }
                    return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getCourseContent")]
        public HttpResponseMessage getCourseContent(int courseid)
        {
            try
            {

                getCourseContent response = new getCourseContent();               
                response = _business.getCourseContent(courseid);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getAllTeacherDetails")]
        public HttpResponseMessage getAllTeacherDetails()
        {
            try
            {
                List<TeacherDetails> response = _business.getAllTeacherDetails();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("getAllVideos")]
        public HttpResponseMessage getAllVideos()
        {
            try
            {
                List<AllCourses> response = _business.getAllVideos();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("RegisterClass")]
        public HttpResponseMessage RegisterClass(RegisterClass registerObject)
        {
            try
            {
                List<myRegisteredClasses> response = _business.registerClass(registerObject);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("SaveUserReviews")]
        public HttpResponseMessage SaveUserReviews(SaveUserReviews SaveUserReviews)
        {
            try
            {
                bool response = _business.SaveUserReviews(SaveUserReviews);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetUserReviews")]
        public HttpResponseMessage GetUserReviews(int id,string type)
        {
            try
            {
                List<SaveUserReviews> response = _business.GetUserReviews(id,type);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("SaveTeacherDetails")]
        public HttpResponseMessage SaveTeacherDetails(SaveTeacherDetails TeacherDetails)
        {
            try
            {
                if (_business.IsValidEmail(TeacherDetails.email,"T"))
                {
                    bool response = _business.SaveTeacherDetails(TeacherDetails);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Email_Id is already registered");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetMyRegisteredClasses")]
        public HttpResponseMessage GetMyRegisteredClasses(string emailId)
        {
            try
            {
                List<myRegisteredClasses> response = _business.GetMyRegisteredClasses(emailId);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetTeacherInfo")]
        public HttpResponseMessage GetTeacherInfo(int teacherId)
        {
            try
            {
                teacherContent response = _business.GetTeacherInfo(teacherId);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetVideoInfo")]
        public HttpResponseMessage GetVideoInfo(int videoId)
        {
            try
            {
                videoContent response = _business.GetVideoInfo(videoId);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("ValidateTeacher")]
        public HttpResponseMessage ValidateTeacher(string email)
        {
            try
            {
                bool response = _business.ValidateTeacher(email);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("search")]
        public HttpResponseMessage search(string searchText, string type)
        {
            try
            {
                List<SearchModel> response = _business.search(searchText, type);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("globalSearch")]
        public HttpResponseMessage globalSearch(string searchText,string type)
        {
            try
            {
                GlobalSearch response = _business.globalSearch(searchText,type);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("SaveVideoDetails")]
        public HttpResponseMessage SaveVideoDetails(SaveVideoDetails video)
        {
            try
            {

                bool response = false;
                if (_business.IsValidVideoTitle(video.title))
                {
                    response = _business.SaveVideoDetails(video);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A video with same title already exists");
                }
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("ViewAllCourses")]
        public HttpResponseMessage ViewAllCourses(string category)
        {
            try
            {
                List<AllCourses> response = _business.ViewAllCourses(category);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("ViewAllVideos")]
        public HttpResponseMessage ViewAllVideos(string category)
        {
            try
            {
                List<VideoLessons> response = _business.ViewAllVideos(category);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("viewAllDetails")]
        public HttpResponseMessage viewAllDetails(string type, string category, int pageIndex, int count)
        {
            try
            {
                ViewAllDetails response = _business.viewAllDetails(type, category,pageIndex,count);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
