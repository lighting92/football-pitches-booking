using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Utilities
{
    public class Utils
    {
        public DateTime DoubleToTime(double time)
        {
            DateTime dt = new DateTime(0,0,0,0,0,0);
            dt.AddHours((int)time);
            dt.AddMinutes((int)((time - (int)time) * 60));
            return dt;
        }

        public double TimeToDouble(DateTime time)
        {
            double dt = 0;
            dt = time.Hour + (time.Minute / 60);
            return dt;
        }

        public string DoubleToTimeString(double time)
        {
            string dt = "";
            dt = string.Concat(((int)time).ToString(), ":", ((int)((time - (int)time) * 60)).ToString()); 
            return dt;
        }

        public double TimeStringToDouble(string time)
        {
            double dt = 0;
            for(int i = 0; i < time.Length; i++)
            {
                if (time[i] == ':')
                {
                    dt = Int32.Parse(time.Substring(0, i)) + (Int32.Parse(time.Substring(i + 1, time.Length - i + 1)) / 60); ;
                }
            }
            return dt;
        }
    }
}