using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.HomeModels
{
    public class FeedbackModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        public int Result { get; set; }
    }
}