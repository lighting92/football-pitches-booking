using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class AddFieldModel
    {
        public int StadiumId { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public string Number { get; set; }
        public int FieldType { get; set; }
        public List<FieldPrice> Prices { get; set; }
        public int ChosenPrice { get; set; }
        public bool IsActive { get; set; }
        public List<Field> Parent { get; set; }
        public int? ChosenParent { get; set; }
        public bool HavePermission { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}