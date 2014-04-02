using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.AccountModels
{
    public class EditReservationModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public string FieldNumber { get; set; }
        public string FieldType { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string VerifyCode { get; set; }
        public string CreateDate { get; set; }        
        public string Status { get; set; }
        public bool HasRival { get; set; }
        public string RivalStatus { get; set; }        
        public bool HavePermission { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public bool CanModify { get; set; }
    }
}