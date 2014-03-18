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
        public double Discount { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
    }
}