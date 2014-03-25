using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class BookModel
    {
        public Stadium Stadium { get; set; }
        public List<Field> Fields { get; set; }
        public BookingOptions Options { get; set; }
        public BookingUserInfo UserInfo { get; set; }
        public List<double> Prices { get; set; }
        public List<double> Discount { get; set; }        
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}