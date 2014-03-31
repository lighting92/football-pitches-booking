using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class ReservationModel
    {
        public int FieldId { get; set; }
        public List<Field> Fields { get; set; }
        public int UserId { get; set; }
        public string Customer { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime StartTime { get; set; }
        public double Duration { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public Promotion Promotion { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Approver { get; set; }
        public string Status { get; set; }
        public bool HasRival { get; set; }
        public string RivalUser { get; set; }
        public string RivalName { get; set; }
        public string RivalPhone { get; set; }
        public string RivalEmail { get; set; }
        public string RivalFinder { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
    }
}