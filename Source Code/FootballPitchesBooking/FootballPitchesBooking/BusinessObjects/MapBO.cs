using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models.DistanceModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models.DistanceModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class MapBO
    {
        //return a list represent for distance street from origin Corrdinate to each destination, maximum is 
        //100 destination
        public static List<int?> SinglePointDistanceMatrix(DistanceMatrixResponse addresses)
        {
            if (string.IsNullOrEmpty(addresses.origin) || addresses.destinations.Count() < 1)
            {
                return null;
            }

            if (addresses.destinations.Count() > 50)
            {
                throw new Exception("Maximum quota per request!");

            }

            List<int?> result = new List<int?>();

            String url = "http://maps.googleapis.com/maps/api/distancematrix/json?";

            String from = addresses.origin;
            String tos = "";
            for (int i = 0; i < addresses.destinations.Count() - 1; i++)
            {
                tos = tos + addresses.destinations[i] + "|";
            }
            tos = tos + addresses.destinations[addresses.destinations.Count() - 1];

            String parameters = "origins=" + from + "&destinations=" + tos + "&mode=driving&language=en-EN&sensor=false"; // + "&key=AIzaSyCC-YejiwLCP4X7U5jmubdOXZ-oI-Z79-U";

            String requestUrl = url + parameters;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            webRequest.KeepAlive = false;

            WebResponse response = webRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            DistanceMatrixResponse matrixResponse = JsonConvert.DeserializeObject<DistanceMatrixResponse>(responsereader);


            for (int i = 0; i < matrixResponse.rows[0].elements.Count(); i++)
            {
                if (matrixResponse.rows[0].elements[i].status == "OK")
                {
                    result.Add(matrixResponse.rows[0].elements[i].distance.value);
                }
                else
                {
                    result.Add(null);
                }
            }

           

            return result;
        }
    }
}