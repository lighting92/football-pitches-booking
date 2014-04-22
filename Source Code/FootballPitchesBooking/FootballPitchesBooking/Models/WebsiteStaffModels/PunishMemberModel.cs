using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.WebsiteStaffModels
{
    public class PunishMemberModel
    {
        public string StaffUserName { get; set; }
        public string PunishedUserName { get; set; }
        public string Date { get; set; }
        public string ExpiredDate { get; set; }
        public string Reason { get; set; }
        public string[] UserNames { get; set; }
        public bool IsForever { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
    }
}