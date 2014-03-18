using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Utilities
{
    public class Utils
    {
        public int DoubleToMinute(double min)
        {
            return (int)(60 * min);
        }

        public int MinuteToDouble(int min)
        {
            return (int)(min / 60);
        }
    }
}