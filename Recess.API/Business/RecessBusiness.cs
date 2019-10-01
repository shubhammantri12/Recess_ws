using Recess.API.Models;
using Recess.API.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Recess.API.Business
{
    public class RecessBusiness
    {
        RecessRepository _repository = new RecessRepository();
        public bool register(LoginModel user)
        {
            try
            {
                bool Response = _repository.register(user);
                return Response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<CourseDetails> getCourseDetails()
        {
            try
            {
                List<CourseDetails> Response = _repository.getCourseDetails();
                return Response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void createPassword(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool login(string username,string password)
        {
            byte[] passwordhash, passwordsalt;
            createPassword(password, out passwordhash, out passwordsalt);
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                byte[] computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i< computedhash.Length;i++)
                {
                    if(computedhash[i]!=passwordhash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool PostUserDetails(string username, string password)
        {
            try
            {
                string query = "insert into RecessApp.dbo.userdetails values (@username,@pwd)";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlServerConnection"].ToString()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                    command.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = password;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
            }
    }
}