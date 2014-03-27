using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.WebsiteStaffModels
{
    public class AdvertisementModel
    {
        public string Position { get; set; }
        public string AdvertiseDetail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Status { get; set; }
        public string Creator { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
    }
}