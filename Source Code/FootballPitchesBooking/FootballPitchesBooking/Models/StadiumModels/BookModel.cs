using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class BookModel
    {
        public Stadium Stadium { get; set; }
        public List<Field> AvailableFields { get; set; }
        public List<double> Prices { get; set; }
        public List<double> Discount { get; set; }
        public string ChosenField { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string FieldType { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        
    }
}