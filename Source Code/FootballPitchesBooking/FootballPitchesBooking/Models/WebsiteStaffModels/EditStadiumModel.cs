using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.WebsiteStaffModels
{
    public class EditStadiumModel
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public List<string> Images { get; set; }
        public List<string> ImageIds { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }        
        public bool IsActive { get; set; }
        public string MainOwner { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}