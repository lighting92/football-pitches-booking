using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class DistanceMatrixResponse
    {
        public string status { get; set; }
        public string origin { get; set; }
        public string[] destinations { get; set; }
        public Row[] rows { get; set; }


    }
    public class Row
    {
        public Element[] elements { get; set; }
    }
    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}