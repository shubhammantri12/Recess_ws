using Recess.API.Business;
using Recess.API.Models;
using System;
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
        //[HttpGet]
        //[Route("getStudentDetails")]
        //public HttpResponseMessage getStudentDetails()
        //{
        //    try
        //    {
        //        studentDetails obj = new studentDetails();
        //        obj.Name = "Shubham";
        //        obj.id = 1;
        //        return Request.CreateResponse(HttpStatusCode.OK, obj);

        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
        //    }
        //}
        [HttpPost]
        [Route("register")]
        public HttpResponseMessage register(LoginModel user)
        {
            try
            {
                bool response = _business.register(user);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        [HttpGet]
        [Route("getCourseDetails")]
        public HttpResponseMessage getCourseDetails()
        {
            try
            {
                List<CourseDetails> response = _business.getCourseDetails();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
            }
        }
        //[HttpPost]
        //[Route("PostUserDetails")]
        //public HttpResponseMessage PostUserDetails(string username, string password)
        //{
        //    try
        //    {
        //        bool response = _business.PostUserDetails(username, password);
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error");
        //    }
        //}
    }
}
