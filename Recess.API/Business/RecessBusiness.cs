using Recess.API.Models;
using Recess.API.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Recess.API.Business
{
    public class RecessBusiness
    {
        RecessRepository _repository = new RecessRepository();
        public bool register(UserModel user)
        {
            try
            {
                if (user.course == null)
                {
                    user.course = "";
                }
                if (user.phoneNumber == null)
                {
                    user.phoneNumber = "";
                }
                if (user.photoUrl == null)
                {
                    user.photoUrl = "";
                }
                
                bool Response = _repository.register(user);
                return Response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public bool UpdateUser(UserModel user)
        {
            try
            {
                if (user.course == null)
                {
                    user.course = "";
                }
                if (user.phoneNumber == null)
                {
                    user.phoneNumber = "";
                }
                if (user.photoUrl == null)
                {
                    user.photoUrl = "";
                }
                bool Response = _repository.UpdateUser(user);
                return Response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                bool Response = _repository.isValidEmail(email);
                return Response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool IsValidCourseTitle(string name)
        {
            try
            {
                bool Response = _repository.checkValidCourseTitle(name);
                return Response;
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
                getCourseContent Response = _repository.getCourseContent(courseid);
                return Response;
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
                //string filekey = string.Empty;
                //string filetype = string.Empty;
                //string fileLocation = string.Empty;
                //int i = 0;
                //if(lstfiles.Count>0)
                //{
                //    for(int j=0;j<lstfiles.Count;j++)
                //    {
                //        var file = lstfiles[j];
                //        if(file.ContentLength > 0)
                //        {
                //            filekey = (HttpContext.Current.Request.Files).GetKey(i);
                //            filetype = filekey.Substring(0, 1);

                //            if(filetype.ToUpper()=="I")
                //            {
                //                fileLocation = ConfigurationManager.AppSettings["path"] + courseDetails.imageUrl;
                //                file.SaveAs(fileLocation);
                //            }
                //            else if(filetype.ToUpper()=="V")
                //            {
                //                fileLocation = ConfigurationManager.AppSettings["path"] + courseDetails.VideoUrl;
                //                file.SaveAs(fileLocation);
                //            }
                //        }

                //    }
                //}
                
                
                 bool Response = _repository.SaveCourseDetails(courseDetails);
               
                    return Response;
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
                CourseDetails courses = _repository.getCourseDetails();
               // CourseDetails[] Response = courses.ToArray();
                return courses;
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
                List<AllCourses> courses = _repository.getAllCourses();
                // CourseDetails[] Response = courses.ToArray();
                return courses;
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
                List<AppDetails> courses = _repository.getAppDetails();
                // CourseDetails[] Response = courses.ToArray();
                return courses;
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
                List<TeacherDetails> teachers = _repository.getAllTeacherDetails();
                // CourseDetails[] Response = courses.ToArray();
                return teachers;
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
                List<AllCourses> videos = _repository.getAllVideos();
                // CourseDetails[] Response = courses.ToArray();
                return videos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool registerClass(RegisterClass registerObject)
        {
            bool register = _repository.registerClass(registerObject);
            if(register)
            {
                sendmailtouser(registerObject);
            }
            return true;
        }
        public bool SaveUserReviews(SaveUserReviews SaveUserReviews)
        {
            try
            {
                bool response = _repository.SaveUserReviews(SaveUserReviews);
                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<SaveUserReviews> GetUserReviews(int id,string type)
        {
            try
            {
                List<SaveUserReviews> response = _repository.GetUserReviews(id,type);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void sendmailtouser(RegisterClass registerObject)
        {
            StringBuilder sb = new StringBuilder();
            string mailheader = "<br><br>This is an autogenerated mail. Please do not reply to this email.<br><br>";
            sb.Append(mailheader);
            sb.Append("Hello " + registerObject.username + ", <br><br>");
            sb.Append("<br/> Thank you for registering for "+ registerObject.classTitle+". Please click on below link to join the class");
            sb.Append("<br><br>" + registerObject.classLink + "<br><br>");
            sb.Append("<br>Please do not forward this email as it may lead to a security breach<br> <br> Your sincerely,<br>Recess");
            sendmail(sb,registerObject);
        }
        private void sendmail(StringBuilder sb,RegisterClass register)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("shubhammantri27@gmail.com");
                message.To.Add(new MailAddress(register.useremail));
                message.Subject = "Thankyou for registering for the class";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = sb.ToString();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                                              smtp.EnableSsl = true;
                                              smtp.UseDefaultCredentials = false;
                                              smtp.Credentials = new NetworkCredential("shubhammantri27@gmail.com", "9163131886");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        //private void createPassword(string password, out byte[] passwordhash, out byte[] passwordsalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordsalt = hmac.Key;
        //        passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
        //public bool login(string username,string password)
        //{
        //    byte[] passwordhash, passwordsalt;
        //    createPassword(password, out passwordhash, out passwordsalt);
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
        //    {
        //        byte[] computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for(int i=0;i< computedhash.Length;i++)
        //        {
        //            if(computedhash[i]!=passwordhash[i])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

    }
}