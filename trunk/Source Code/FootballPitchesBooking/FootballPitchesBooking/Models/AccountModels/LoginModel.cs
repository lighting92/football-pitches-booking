using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.AccountModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string ErrorString { get; set; }
    }
}