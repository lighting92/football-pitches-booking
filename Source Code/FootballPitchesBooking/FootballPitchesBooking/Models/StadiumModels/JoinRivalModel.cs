using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class JoinRivalModel
    {
        public Reservation Resv { get; set; }
        public List<string> ErrorMessages { get; set; }
        public User UserInfo { get; set; }
    }
}