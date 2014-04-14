using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.AccountModels
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string RankName { get; set; }
        public int Point { get; set; }
        public string RoleName { get; set; }
        public DateTime JoinDate { get; set; }
        public List<string> ErrorMessages { get; set; }
        public int Result { get; set; }
    }
}