using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class FieldModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int FieldType { get; set; }
        public string Parent { get; set; }
        public bool IsActive { get; set; }
        public FieldPrice FieldPrice { get; set; }
    }
}