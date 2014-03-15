﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class PromotionsModel
    {
        public bool HavePermission { get; set; }
        public string ErrorMessage { get; set; }
        public int StadiumId { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public List<Promotion> Promotions { get; set; }   
    }
}