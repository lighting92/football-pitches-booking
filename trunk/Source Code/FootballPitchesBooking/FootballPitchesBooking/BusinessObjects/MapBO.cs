using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models.DistanceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using FootballPitchesBooking.Models;
using System.Xml.Serialization;
using FootballPitchesBooking.Models.RecommendationModels;

namespace FootballPitchesBooking.BusinessObjects
{

    public class MapBO
    {
        public static List<StadiumDistance> GetStadiumDistanceFromUser(string userName, string xmlFolderPath)
        {
            UserDAO userDAO = new UserDAO();
            UserDistanceDAO udDAO = new UserDistanceDAO();
            ConfigurationDAO confDAO = new ConfigurationDAO();
            StadiumDAO stadiumDAO = new StadiumDAO();
            var ggper = int.Parse(confDAO.GetConfigByName("Gg_Periodic").Value);
            var user = userDAO.GetUserByUserName(userName);
            var ud = udDAO.GetUserDistanceByUserName(userName);
            if (ud != null)
            {
                if (ud.UpdateDate.AddDays(ggper).Date.CompareTo(DateTime.Now.Date) >= 0)
                {
                    try
                    {
                        var xmlUserDistance = GetUserDistance(xmlFolderPath + ud.Path);
                        return xmlUserDistance.StadiumsDistance;
                    }
                    catch (FileNotFoundException)
                    {
                        var stadiums = stadiumDAO.GetAllStadiums();
                        var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                        DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                        addresses.origin = user.Address;
                        addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                        var distances = SinglePointDistanceMatrix(addresses);

                        List<StadiumDistance> lsd = new List<StadiumDistance>();

                        for (int i = 0; i < stadiumAddresses.Count(); i++)
                        {
                            var temp = new StadiumDistance();
                            temp.StadiumId = stadiumAddresses[i].StadiumId;
                            temp.StadiumAddress = stadiumAddresses[i].Address;
                            temp.Distance = distances[i].Value;
                            lsd.Add(temp);
                        }
                        XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                        newXMLUserDistance.UserId = user.Id;
                        newXMLUserDistance.UserAddress = user.Address;
                        newXMLUserDistance.UpdateDate = ud.UpdateDate.ToShortDateString();
                        newXMLUserDistance.StadiumsDistance = lsd;

                        UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + ud.Path);

                        return lsd;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        var stadiums = stadiumDAO.GetAllStadiums();
                        var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                        DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                        addresses.origin = user.Address;
                        addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                        var distances = SinglePointDistanceMatrix(addresses);

                        List<StadiumDistance> lsd = new List<StadiumDistance>();

                        for (int i = 0; i < stadiumAddresses.Count(); i++)
                        {
                            var temp = new StadiumDistance();
                            temp.StadiumId = stadiumAddresses[i].StadiumId;
                            temp.StadiumAddress = stadiumAddresses[i].Address;
                            temp.Distance = distances[i].Value;
                            lsd.Add(temp);
                        }
                        XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                        newXMLUserDistance.UserId = user.Id;
                        newXMLUserDistance.UserAddress = user.Address;
                        newXMLUserDistance.UpdateDate = ud.UpdateDate.ToShortDateString();
                        newXMLUserDistance.StadiumsDistance = lsd;

                        UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + ud.Path);

                        return lsd;
                    }
                }
                else
                {
                    ud.UpdateDate = DateTime.Now.Date;
                    int result = udDAO.UpdateUserDistance(ud);
                    if (result > 0)
                    {
                        var stadiums = stadiumDAO.GetAllStadiums();
                        var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                        DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                        addresses.origin = user.Address;
                        addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                        var distances = SinglePointDistanceMatrix(addresses);

                        List<StadiumDistance> lsd = new List<StadiumDistance>();

                        for (int i = 0; i < stadiumAddresses.Count(); i++)
                        {
                            var temp = new StadiumDistance();
                            temp.StadiumId = stadiumAddresses[i].StadiumId;
                            temp.StadiumAddress = stadiumAddresses[i].Address;
                            temp.Distance = distances[i].Value;
                            lsd.Add(temp);
                        }
                        XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                        newXMLUserDistance.UserId = user.Id;
                        newXMLUserDistance.UserAddress = user.Address;
                        newXMLUserDistance.UpdateDate = ud.UpdateDate.ToShortDateString();
                        newXMLUserDistance.StadiumsDistance = lsd;

                        UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + ud.Path);

                        return lsd;
                    }
                    else
                    {
                        try
                        {
                            var xmlUserDistance = GetUserDistance(xmlFolderPath + ud.Path);
                            return xmlUserDistance.StadiumsDistance;
                        }
                        catch (FileNotFoundException)
                        {
                            var stadiums = stadiumDAO.GetAllStadiums();
                            var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                            DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                            addresses.origin = user.Address;
                            addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                            var distances = SinglePointDistanceMatrix(addresses);

                            List<StadiumDistance> lsd = new List<StadiumDistance>();

                            for (int i = 0; i < stadiumAddresses.Count(); i++)
                            {
                                var temp = new StadiumDistance();
                                temp.StadiumId = stadiumAddresses[i].StadiumId;
                                temp.StadiumAddress = stadiumAddresses[i].Address;
                                temp.Distance = distances[i].Value;
                                lsd.Add(temp);
                            }
                            XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                            newXMLUserDistance.UserId = user.Id;
                            newXMLUserDistance.UserAddress = user.Address;
                            newXMLUserDistance.UpdateDate = ud.UpdateDate.ToShortDateString();
                            newXMLUserDistance.StadiumsDistance = lsd;

                            UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + ud.Path);

                            return lsd;
                        }
                        catch (DirectoryNotFoundException)
                        {
                            var stadiums = stadiumDAO.GetAllStadiums();
                            var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                            DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                            addresses.origin = user.Address;
                            addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                            var distances = SinglePointDistanceMatrix(addresses);

                            List<StadiumDistance> lsd = new List<StadiumDistance>();

                            for (int i = 0; i < stadiumAddresses.Count(); i++)
                            {
                                var temp = new StadiumDistance();
                                temp.StadiumId = stadiumAddresses[i].StadiumId;
                                temp.StadiumAddress = stadiumAddresses[i].Address;
                                temp.Distance = distances[i].Value;
                                lsd.Add(temp);
                            }
                            XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                            newXMLUserDistance.UserId = user.Id;
                            newXMLUserDistance.UserAddress = user.Address;
                            newXMLUserDistance.UpdateDate = ud.UpdateDate.ToShortDateString();
                            newXMLUserDistance.StadiumsDistance = lsd;

                            UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + ud.Path);

                            return lsd;
                        }
                    }
                }
            }
            else
            {
                UserDistance newUd = new UserDistance();
                newUd.UserId = user.Id;
                newUd.UpdateDate = DateTime.Now.Date;
                newUd.Path = user.UserName + ".xml";

                var stadiums = stadiumDAO.GetAllStadiums();
                var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                addresses.origin = user.Address;
                addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();

                var distances = SinglePointDistanceMatrix(addresses);

                if (distances == null)
                {
                    return null;
                }

                List<StadiumDistance> lsd = new List<StadiumDistance>();

                for (int i = 0; i < stadiumAddresses.Count(); i++)
                {
                    var temp = new StadiumDistance();
                    temp.StadiumId = stadiumAddresses[i].StadiumId;
                    temp.StadiumAddress = stadiumAddresses[i].Address;
                    temp.Distance = distances[i].Value;
                    lsd.Add(temp);
                }

                int create = udDAO.CreateUserDistance(newUd);
                if (create > 0)
                {
                    XMLUserDistance newXMLUserDistance = new XMLUserDistance();
                    newXMLUserDistance.UserId = user.Id;
                    newXMLUserDistance.UserAddress = user.Address;
                    newXMLUserDistance.UpdateDate = newUd.UpdateDate.ToShortDateString();
                    newXMLUserDistance.StadiumsDistance = lsd;

                    UpdateUserDistanceFile(newXMLUserDistance, xmlFolderPath + newUd.Path);

                    return lsd;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void UpdateUserDistanceFile(XMLUserDistance xmlUserDistance, string filePath)
        {
            try
            {
                string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(XMLUserDistance));
                TextWriter writer = new StreamWriter(filePath);
                serializer.Serialize(writer, xmlUserDistance);
                writer.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static XMLUserDistance GetUserDistance(string filePath)
        {
            try
            {
                XMLUserDistance userDistance;
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer mySerializer =
                new XmlSerializer(typeof(XMLUserDistance));
                // To read the file, create a FileStream.
                FileStream myFileStream =
                new FileStream(filePath, FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                userDistance = (XMLUserDistance)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return userDistance;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //return a list represent for distance street from origin Corrdinate to each destination, maximum is 
        //100 destination
        public static List<int?> SinglePointDistanceMatrix(DistanceMatrixResponse addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses.origin) || addresses.destinations.Count() < 1)
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
            string responsereader;

            try
            {
                WebResponse response = webRequest.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                responsereader = sreader.ReadToEnd();
                response.Close();
            }
            catch (Exception)
            {
                return null;
            }


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