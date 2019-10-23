using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class UserModel
    {
        
        public string displayName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public string course { get; set; }
        public string photoUrl { get; set; }
        public bool emailVerified { get; set; }
    }
}