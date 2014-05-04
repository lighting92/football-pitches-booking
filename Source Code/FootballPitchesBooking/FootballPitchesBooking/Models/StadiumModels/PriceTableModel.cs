using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class PriceTableModel
    {
        public FieldPrice FieldPrice { get; set; }
        public List<Field> Fields { get; set; }
        public Stadium Stadium { get; set; }

        public List<PriceTable> Monday { get; set; }
        public PriceTable MondayElse { get; set; }
        public List<PriceTable> Tuesday { get; set; }
        public PriceTable TuesdayElse { get; set; }
        public List<PriceTable> Wednesday { get; set; }
        public PriceTable WednesdayElse { get; set; }
        public List<PriceTable> Thursday { get; set; }
        public PriceTable ThursdayElse { get; set; }
        public List<PriceTable> Friday { get; set; }
        public PriceTable FridayElse { get; set; }
        public List<PriceTable> Saturday { get; set; }
        public PriceTable SaturdayElse { get; set; }
        public List<PriceTable> Sunday { get; set; }
        public PriceTable SundayElse { get; set; }
        
    }
}