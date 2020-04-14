﻿using Recess.API.Models;
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
        public LoginResponse login(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                Guid token;
                string ip = string.Empty;
                HttpContext context = HttpContext.Current;
                string ipAdd = context.Request.ServerVariables["HTTP_X_FORWADED_FOR"];
                if (!string.IsNullOrEmpty(ipAdd))
                {
                    string[] ipaddress = ipAdd.Split(',');
                    if (ipaddress.Length > 0)
                    {
                        ip = ipaddress[0];
                    }
                }
                else
                {
                    ip = context.Request.ServerVariables["REMOTE_ADDR"];
                }
                token = Guid.NewGuid();
                bool userExists = _repository.CheckIfUserExists(request.emailId);
                if (userExists)
                {
                    _repository.UpdateTokenForUser(request.emailId, token, ip);
                    response.emailId = request.emailId;
                    response.token = token;
                    return response;
                }
                else
                {
                    _repository.AddUserToken(request.emailId, token, ip);
                    response.emailId = request.emailId;
                    response.token = token;
                    return response;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public bool logout(LoginRequest request)
        {
            try
            {
                bool response = _repository.logout(request.emailId);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
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
        public bool IsValidToken(string Token)
        {
            try
            {
                bool Response = _repository.IsValidToken(Token);
                return Response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool IsValidEmail(string email,string type)
        {
            try
            {
                bool Response = _repository.isValidEmail(email, type);
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
        public bool IsValidVideoTitle(string name)
        {
            try
            {
                bool Response = _repository.checkValidVideoTitle(name);
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

                bool response = false;
                 int Courseid = _repository.SaveCourseDetails(courseDetails);
                if(Courseid !=null || Courseid !=0)
                {
                    foreach(saveScheduleClass classes in courseDetails.scheduledClass)
                    {
                        classes.courseId = Courseid;
                        classes.teacherId = courseDetails.teacherId;
                        //classes.teacherName = courseDetails.submittedBy;
                        _repository.scheduleClass(classes);
                    }
                    response = true;
                }
                else
                {
                    throw new Exception("An error occured while adding the course");
                }
               
                    return response;
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
        public List<AppDetails> getTeacherFunctions()
        {
            try
            {
                List<AppDetails> courses = _repository.getTeacherFunctions();
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
        public List<myRegisteredClasses> registerClass(RegisterClass registerObject)
        {
            try
            {
                bool isValid = checkIfPreviouslyRegisterd(registerObject);
                if (isValid)
                {
                    bool register = _repository.registerClass(registerObject);

                    if (register)
                    {
                        sendmailtouser(registerObject);

                    }
                }
                else
                {
                    throw new Exception("User has already registered for the class");

                }
                List<myRegisteredClasses> response = _repository.getMyRegisteredClasses(registerObject.useremail);
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private bool checkIfPreviouslyRegisterd(RegisterClass register)
        {
           return _repository.checkIfPreviouslyRegisterd(register);
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
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("shubhammantri27@gmail.com", "9163131886");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public int SaveTeacherDetails(SaveTeacherDetails TeacherDetails)
        {
            try
            {
                if (TeacherDetails.photourl == null)
                {
                    TeacherDetails.photourl = "";
                }
                if (TeacherDetails.courseCategory == null)
                {
                    TeacherDetails.courseCategory = "";
                }
                if (TeacherDetails.Gender == null)
                {
                    TeacherDetails.Gender = "";
                }
                int teacherid = _repository.SaveTeacherDetails(TeacherDetails);
                if(teacherid !=0)
                {
                    sendmailtoteacher(TeacherDetails);
                }
                return teacherid;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void sendmailtoteacher(SaveTeacherDetails teacherDetails)
        {
            StringBuilder sb = new StringBuilder();
            string mailheader = "<br><br>This is an autogenerated mail. Please do not reply to this email.<br><br>";
            sb.Append(mailheader);
            sb.Append("Hello " + teacherDetails.name + ", <br><br>");
            sb.Append("<br/> Thank you for registering yourself to be a part of Recess Teacher family. <br> Your request has been successfully submitted and is under consieration.");
            sb.Append("<br><br> You will be notified once your request gets approved. <br><br>");
            sb.Append("<br>Please do not forward this email as it may lead to a security breach<br> <br> Your sincerely,<br>Recess");
            sendmailtoteacher(sb, teacherDetails);
        }
        private void sendmailtoteacher(StringBuilder sb, SaveTeacherDetails teacherDetails)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("shubhammantri27@gmail.com");
                message.To.Add(new MailAddress(teacherDetails.email));
                message.Subject = "Thankyou for registering to become a part of Recess Techer family";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = sb.ToString();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("shubhammantri27@gmail.com", "9163131886");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<myRegisteredClasses> GetMyRegisteredClasses(string emailId)
        {
            try
            {
                List<myRegisteredClasses> registeredClasses = _repository.getMyRegisteredClasses(emailId);
                return registeredClasses;
            }
            catch(Exception)
            {
                throw;
            }


        }
        public teacherContent GetTeacherInfo (int teacherId)
        {
            try
            {
                teacherContent teacherInfo = _repository.GetTeacherInfo(teacherId);
                return teacherInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public videoContent GetVideoInfo(int videoId)
        {
            try
            {
                videoContent videoInfo = _repository.GetVideoInfo(videoId);
                return videoInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ValidateTeacher(string email)
        {
            try
            {
                bool isValid = _repository.ValidateTeacher(email);
                return isValid;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SearchModel> search(string searchText,string type)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = "";
                }
                List<SearchModel> response = _repository.search(searchText,type);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public GlobalSearch globalSearch(string searchText,string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = "";
                }
                GlobalSearch response = _repository.globalSearch(searchText,type);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveVideoDetails(SaveVideoDetails video)
        {
            try
            {
                bool response = _repository.SaveVideoDetails(video);
                return response;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public List<AllCourses> ViewAllCourses(string category)
        {
            try
            {
                List<AllCourses> courses = _repository.ViewAllCourses(category);
                // CourseDetails[] Response = courses.ToArray();
                return courses;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<VideoLessons> ViewAllVideos(string category)
        {
            try
            {
                List<VideoLessons> videos = _repository.ViewAllVideos(category);
                // CourseDetails[] Response = courses.ToArray();
                return videos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public seeAllDetails viewAllDetails(string type, string category, int pageIndex, int count)
        {
            try
            {
                if(string.IsNullOrEmpty(category))
                {
                    category = "";
                }
                seeAllDetails response = _repository.ViewAllDetails(type,category,pageIndex,count);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool scheduleClass(saveScheduleClass classes)
        {
            if (_repository.checkIfClasssExists(classes))
            {
                return _repository.scheduleClass(classes);
            }
            else
            {
                throw new Exception("A class at same time already scheduled");
            }

        }
        public InstructorInfo GetTeacherInfoByEmail(string emailId)
        {
            try
            {
                InstructorInfo teacherInfo = _repository.GetTeacherInfoByEmail(emailId);
                return teacherInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string paymentProcess(PaytmPaymentProcess payment)
        {
            Dictionary<String, String> paytmParams = new Dictionary<String, String>();

            /* Find your MID in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys */
            paytmParams.Add("MID", "fSfBHM68239464962268");

            /* Find your WEBSITE in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys */
            paytmParams.Add("WEBSITE", "WEBSTAGING");

            /* Find your INDUSTRY_TYPE_ID in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys */
            paytmParams.Add("INDUSTRY_TYPE_ID", "Retail");

            /* WEB for website and WAP for Mobile-websites or App */
            paytmParams.Add("CHANNEL_ID", "WEB");

            /* Enter your unique order id */
            paytmParams.Add("ORDER_ID", payment.ORDER_ID);

            /* unique id that belongs to your customer */
            paytmParams.Add("CUST_ID", payment.CUST_ID);

            /* customer's mobile number */
            paytmParams.Add("MOBILE_NO", payment.MOBILE_NO);

            /* customer's email */
            paytmParams.Add("EMAIL", payment.EMAIL);

            /**
            * Amount in INR that is payble by customer
            * this should be numeric with optionally having two decimal points
*/
            paytmParams.Add("TXN_AMOUNT", payment.TXN_AMOUNT);

            /* on completion of transaction, we will send you the response on this URL */
            paytmParams.Add("CALLBACK_URL", "https://recessws.azurewebsites.net/api/Recess/paymentCallback");

            /**
            * Generate checksum for parameters we have
            * You can get Checksum DLL from https://developer.paytm.com/docs/checksum/
            * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
*/
            String checksum = paytm.CheckSum.generateCheckSum("sh1S6vfwjga8dGlT", paytmParams);

            /* for Staging */
            String url = "https://securegw-stage.paytm.in/order/process";

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + url + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in paytmParams.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + paytmParams[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
             return outputHTML;

            

        }
        //public void paymeentCallback(PaytmPaymentProcess payment)
        //    {
        //    String merchantKey = “merchantKey value” ; // Replace the with the Merchant Key provided by Paytm at the time of registration.

        //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //string paytmChecksum = "";
        //    foreach (string key in Request.Form.Keys)
        //    {
        //        parameters.Add(key.Trim(), Request.Form[key].Trim());
        //    }

        //    if (parameters.ContainsKey("CHECKSUMHASH"))
        //    {
        //        paytmChecksum = parameters["CHECKSUMHASH"];
        //        parameters.Remove("CHECKSUMHASH");
        //    }

        //    if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
        //    {
        //        Response.Write("Checksum Matched");
        //    }
        //    else
        //    {
        //        Response.Write("Checksum MisMatch");
        //    }
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