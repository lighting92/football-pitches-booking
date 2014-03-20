using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class AddFieldPricesModel
    {
        public bool HavePermission { get; set; }
        public int StadiumId { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public FieldPriceModel FieldPrice { get; set; }
        public List<PriceTableModel> DefaultPriceTables { get; set; }
        public List<PriceTableModel> MondayPriceTables { get; set; }
        public List<PriceTableModel> TuesdayPriceTables { get; set; }
        public List<PriceTableModel> WednesdayPriceTables { get; set; }
        public List<PriceTableModel> ThurdayPriceTables { get; set; }
        public List<PriceTableModel> FridayPriceTables { get; set; }
        public List<PriceTableModel> SaturdayPriceTables { get; set; }
        public List<PriceTableModel> SundayPriceTables { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string SuccessMessage { get; set; }
    }
}