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
        [Route("registerUser")]
        public HttpResponseMessage registerUser(UserModel user)
        {
            try
            {
                bool response = false;
                if (_business.IsValidEmail(user.email))
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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        [HttpPost]
        [Route("RegisterClass")]
        public HttpResponseMessage RegisterClass(RegisterClass registerObject)
        {
            try
            {
                bool response = _business.registerClass(registerObject);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }

    }
}
