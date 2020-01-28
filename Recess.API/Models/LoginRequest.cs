using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class LoginRequest
    {
        public string emailId { get; set; }
    }
    public class LoginResponse
    {
        public string emailId { get; set; }
        public Guid token { get; set; }
    }
}