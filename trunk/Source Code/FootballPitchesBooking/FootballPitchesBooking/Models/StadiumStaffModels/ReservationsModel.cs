﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumStaffModels
{
    public class ReservationsModel
    {
        public bool HavePermission { get; set; }
        public string ErrorMessage { get; set; }
        public Stadium Stadium { get; set; }
        public int FieldCount { get; set; }
        public List<Stadium> Stadiums { get; set; }
        public List<Reservation> Reservations { get; set; }   
    }
}