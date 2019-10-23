using Recess.API.Business;
using Recess.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
