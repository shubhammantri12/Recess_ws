using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string email_id { get; set; }
        public string phone_nbr { get; set; }

        public string course { get; set; }
    }
}