using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class PromotionModel
    {
        public int FieldId { get; set; }
        public List<Field> Fields { get; set; }
        public DateTime PromotionFrom { get; set; }
        public DateTime PromotionTo { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
        public int StadiumId { get; set; }
    }
}