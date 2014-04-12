using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.AccountModels
{
    public class BlockUserModel
    {
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsForever { get; set; }
        public string Reason { get; set; }
        public string Staff { get; set; }
        public string ErrorMessage { get; set; }
    }
}