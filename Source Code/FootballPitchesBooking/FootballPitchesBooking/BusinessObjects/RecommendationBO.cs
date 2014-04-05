using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.DistanceModels;
using FootballPitchesBooking.Models.RecommendationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class RecommendationBO
    {

        

        public List<RecommendStadium> FindBestStadiums(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // Most reservations
                ReservationDAO resDAO = new ReservationDAO();
                var reservations = resDAO.GetAllReservationsOfUser(userName);
                var notCancelReservations = reservations.Where(r => !r.Status.ToLower().Equals("canceled")).ToList();
                var nearestReservations = notCancelReservations.Where(r => r.Date.CompareTo(DateTime.Now.AddMonths(-3)) >= 0).ToList();
                var mostReservationStadiums = nearestReservations.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var reservationsPriority = new List<PriorityModel>();
                if (mostReservationStadiums != null && mostReservationStadiums.Count() != 0)
                {
                    // list<int stadiumId, count>
                    var dif = mostReservationStadiums.First().Count - mostReservationStadiums.Last().Count;
                    var e = 0;
                    if (dif != 0)
                    {
                        e = 10 / dif;
                    }

                    var max = mostReservationStadiums.First().Count;



                    for (int i = 0; i < mostReservationStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = mostReservationStadiums[i].StadiumId; //luu stadiumId vao PriorityId thi stadium nam trong priority do
                        temp.Priority = 10 - ((max - mostReservationStadiums[i].Count) * e);
                        reservationsPriority.Add(temp);
                    }
                }

                // Most promotions
                PromotionDAO proDAO = new PromotionDAO();
                var promotions = proDAO.GetAllActivePromotions();
                var promotionStadiums = promotions.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var promotionsPriority = new List<PriorityModel>();
                if (promotionStadiums != null && promotionStadiums.Count() != 0)
                {
                    promotions = promotions.OrderByDescending(p => p.Discount).ToList();

                    double difPro = promotions.First().Discount - promotions.Last().Discount;
                    double e = 0;
                    if (difPro != 0)
                    {
                        e = 10 / difPro;
                    }
                    var maxPro = promotions.First().Discount;


                    for (int i = 0; i < promotionStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = promotionStadiums[i].StadiumId;
                        var maxProOfSta = promotions.Where(p => p.Field.Stadium.Id == promotionStadiums[i].StadiumId).OrderByDescending(p => p.Discount).FirstOrDefault();
                        temp.Priority = 10 - ((maxPro - maxProOfSta.Discount) * e);
                        promotionsPriority.Add(temp);
                    }
                }

                // Nearest stadium

                UserDAO userDAO = new UserDAO();
                var userAddress = userDAO.GetUserByUserName(userName).Address;
                StadiumDAO stadiumDAO = new StadiumDAO();
                var stadiums = stadiumDAO.GetAllStadiums();
                var distancePriority = new List<PriorityModel>();
                if (stadiums != null && stadiums.Count() != 0)
                {
                    var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                    DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                    addresses.origin = userAddress;
                    addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();
                    var distanceList = MapBO.SinglePointDistanceMatrix(addresses);

                    //stadium - 1 record: id, address | distancelist: 1record tuong ung voi 1 stadium address
                    // ex: stadium id 1, address A, index: 0, distance[0]
                    for (int i = 0; i < distanceList.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = stadiumAddresses[i].StadiumId;
                        if (distanceList[i] != null)
                        {
                            if (distanceList[i] <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distanceList[i] <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distanceList[i] <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distanceList[i] <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distanceList[i] <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distanceList[i] <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distanceList[i] <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distanceList[i] <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distanceList[i] <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distanceList[i] <= 20000)
                            {
                                temp.Priority = 1;
                            }
                            else
                            {
                                temp.Priority = 0;
                            }
                        }
                        distancePriority.Add(temp);
                    }
                }
                //có 3 list
                // distancePriority * 0.3
                //promotionsPriority 0.2
                //reservationsPriority 0.5
                //gom lai 1 list di
                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Bs_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Bs_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Bs_MostPromoted").Value);
                foreach (var resP in reservationsPriority)
                {
                    result.Add(new PriorityModel {
                        Id = resP.Id,
                        Priority = resP.Priority * mostBooked
                    });                    
                    
                }

                foreach (var proP in promotionsPriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * mostDiscount;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * mostDiscount
                        });

                    }
                }
                foreach (var proP in distancePriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * nearest;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * nearest
                        });

                    }
                }

                result = result.OrderByDescending(p => p.Priority).ToList();

                var recommendStadiums = new List<RecommendStadium>();

                foreach (var item in result)
                {
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    recommendStadiums.Add(temp);
                }

                return recommendStadiums;
            }
            else
            {
                return null;
            }                   
        }

        //find appropriate stadium
        public List<RecommendStadium> FindAppropriateStadiums(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // Most reservations
                ReservationDAO resDAO = new ReservationDAO();
                var reservations = resDAO.GetAllReservationsOfUser(userName);
                var notCancelReservations = reservations.Where(r => !r.Status.ToLower().Equals("canceled")).ToList();
                var nearestReservations = notCancelReservations.Where(r => r.Date.CompareTo(DateTime.Now.AddMonths(-3)) >= 0).ToList();
                var mostReservationStadiums = nearestReservations.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var reservationsPriority = new List<PriorityModel>();
                if (mostReservationStadiums != null && mostReservationStadiums.Count() != 0)
                {
                    // list<int stadiumId, count>
                    var dif = mostReservationStadiums.First().Count - mostReservationStadiums.Last().Count;
                    var e = 0;
                    if (dif != 0)
                    {
                        e = 10 / dif;
                    }

                    var max = mostReservationStadiums.First().Count;



                    for (int i = 0; i < mostReservationStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = mostReservationStadiums[i].StadiumId; //luu stadiumId vao PriorityId thi stadium nam trong priority do
                        temp.Priority = 10 - ((max - mostReservationStadiums[i].Count) * e);
                        reservationsPriority.Add(temp);
                    }
                }

                // Most promotions
                PromotionDAO proDAO = new PromotionDAO();
                var promotions = proDAO.GetAllActivePromotions();
                var promotionStadiums = promotions.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var promotionsPriority = new List<PriorityModel>();
                if (promotionStadiums != null && promotionStadiums.Count() != 0)
                {
                    promotions = promotions.OrderByDescending(p => p.Discount).ToList();

                    double difPro = promotions.First().Discount - promotions.Last().Discount;
                    double e = 0;
                    if (difPro != 0)
                    {
                        e = 10 / difPro;
                    }
                    var maxPro = promotions.First().Discount;


                    for (int i = 0; i < promotionStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = promotionStadiums[i].StadiumId;
                        var maxProOfSta = promotions.Where(p => p.Field.Stadium.Id == promotionStadiums[i].StadiumId).OrderByDescending(p => p.Discount).FirstOrDefault();
                        temp.Priority = 10 - ((maxPro - maxProOfSta.Discount) * e);
                        promotionsPriority.Add(temp);
                    }
                }



                // Nearest stadium

                UserDAO userDAO = new UserDAO();
                var userAddress = userDAO.GetUserByUserName(userName).Address;
                StadiumDAO stadiumDAO = new StadiumDAO();
                var stadiums = stadiumDAO.GetAllStadiums();
                var distancePriority = new List<PriorityModel>();
                if (stadiums != null && stadiums.Count() != 0)
                {
                    var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                    DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                    addresses.origin = userAddress;
                    addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();
                    var distanceList = MapBO.SinglePointDistanceMatrix(addresses);

                    //stadium - 1 record: id, address | distancelist: 1record tuong ung voi 1 stadium address
                    // ex: stadium id 1, address A, index: 0, distance[0]
                    for (int i = 0; i < distanceList.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = stadiumAddresses[i].StadiumId;
                        if (distanceList[i] != null)
                        {
                            if (distanceList[i] <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distanceList[i] <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distanceList[i] <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distanceList[i] <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distanceList[i] <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distanceList[i] <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distanceList[i] <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distanceList[i] <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distanceList[i] <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distanceList[i] <= 20000)
                            {
                                temp.Priority = 1;
                            }
                            else
                            {
                                temp.Priority = 0;
                            }
                        }
                        distancePriority.Add(temp);
                    }
                }
                //có 3 list
                // distancePriority * 0.3
                //promotionsPriority 0.2
                //reservationsPriority 0.5
                //gom lai 1 list di
                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Ap_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Ap_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Ap_MostPromoted").Value);
                foreach (var resP in reservationsPriority)
                {
                    result.Add(new PriorityModel
                    {
                        Id = resP.Id,
                        Priority = resP.Priority * mostBooked
                    });

                }

                foreach (var proP in promotionsPriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * mostDiscount;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * mostDiscount
                        });

                    }
                }
                foreach (var proP in distancePriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * nearest;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * nearest
                        });

                    }
                }

                result = result.OrderByDescending(p => p.Priority).ToList();

                var recommendStadiums = new List<RecommendStadium>();

                foreach (var item in result)
                {
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    recommendStadiums.Add(temp);
                }

                return recommendStadiums;
            }
            else
            {
                return null;
            }
        }

        //find promoted stadium
        public List<RecommendStadium> FindPromotedStadiums(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // Most promotions
                PromotionDAO proDAO = new PromotionDAO();
                var promotions = proDAO.GetAllActivePromotions();
                var promotionStadiums = promotions.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var promotionsPriority = new List<PriorityModel>();
                if (promotionStadiums != null && promotionStadiums.Count() != 0)
                {
                    promotions = promotions.OrderByDescending(p => p.Discount).ToList();

                    double difPro = promotions.First().Discount - promotions.Last().Discount;
                    double e = 0;
                    if (difPro != 0)
                    {
                        e = 10 / difPro;
                    }
                    var maxPro = promotions.First().Discount;


                    for (int i = 0; i < promotionStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = promotionStadiums[i].StadiumId;
                        var maxProOfSta = promotions.Where(p => p.Field.Stadium.Id == promotionStadiums[i].StadiumId).OrderByDescending(p => p.Discount).FirstOrDefault();
                        temp.Priority = 10 - ((maxPro - maxProOfSta.Discount) * e);
                        promotionsPriority.Add(temp);
                    }
                }
                else
                {
                    return new List<RecommendStadium>();
                }
                var listPSIds = promotionStadiums.Select(p => p.StadiumId).ToList();
                // Most reservations
                ReservationDAO resDAO = new ReservationDAO();
                var reservations = resDAO.GetAllReservationsOfUser(userName);
                reservations = reservations.Where(r => listPSIds.Contains(r.Field.StadiumId)).ToList();
                var notCancelReservations = reservations.Where(r => !r.Status.ToLower().Equals("canceled")).ToList();
                var nearestReservations = notCancelReservations.Where(r => r.Date.CompareTo(DateTime.Now.AddMonths(-3)) >= 0).ToList();
                var mostReservationStadiums = nearestReservations.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var reservationsPriority = new List<PriorityModel>();
                if (mostReservationStadiums != null && mostReservationStadiums.Count() != 0)
                {
                    // list<int stadiumId, count>
                    var dif = mostReservationStadiums.First().Count - mostReservationStadiums.Last().Count;
                    var e = 0;
                    if (dif != 0)
                    {
                        e = 10 / dif;
                    }

                    var max = mostReservationStadiums.First().Count;



                    for (int i = 0; i < mostReservationStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = mostReservationStadiums[i].StadiumId; //luu stadiumId vao PriorityId thi stadium nam trong priority do
                        temp.Priority = 10 - ((max - mostReservationStadiums[i].Count) * e);
                        reservationsPriority.Add(temp);
                    }
                }              
                
                // Nearest stadium

                UserDAO userDAO = new UserDAO();
                var userAddress = userDAO.GetUserByUserName(userName).Address;
                StadiumDAO stadiumDAO = new StadiumDAO();
                var stadiums = stadiumDAO.GetAllStadiums();
                stadiums = stadiums.Where(s => listPSIds.Contains(s.Id)).ToList();
                var distancePriority = new List<PriorityModel>();
                if (stadiums != null && stadiums.Count() != 0)
                {
                    var stadiumAddresses = stadiums.Select(s => new { StadiumId = s.Id, Address = (s.Street + ", " + s.Ward + ", " + s.District).Replace(" ", "+") }).ToList();
                    DistanceMatrixResponse addresses = new DistanceMatrixResponse();
                    addresses.origin = userAddress;
                    addresses.destinations = stadiumAddresses.Select(s => s.Address).ToArray();
                    var distanceList = MapBO.SinglePointDistanceMatrix(addresses);

                    //stadium - 1 record: id, address | distancelist: 1record tuong ung voi 1 stadium address
                    // ex: stadium id 1, address A, index: 0, distance[0]
                    for (int i = 0; i < distanceList.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = stadiumAddresses[i].StadiumId;
                        if (distanceList[i] != null)
                        {
                            if (distanceList[i] <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distanceList[i] <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distanceList[i] <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distanceList[i] <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distanceList[i] <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distanceList[i] <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distanceList[i] <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distanceList[i] <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distanceList[i] <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distanceList[i] <= 20000)
                            {
                                temp.Priority = 1;
                            }
                            else
                            {
                                temp.Priority = 0;
                            }
                        }
                        distancePriority.Add(temp);
                    }
                }
                //có 3 list
                // distancePriority * 0.3
                //promotionsPriority 0.2
                //reservationsPriority 0.5
                //gom lai 1 list di
                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Pr_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Pr_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Pr_MostPromoted").Value);
                foreach (var resP in reservationsPriority)
                {
                    result.Add(new PriorityModel
                    {
                        Id = resP.Id,
                        Priority = resP.Priority * mostBooked
                    });

                }

                foreach (var proP in promotionsPriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * mostDiscount;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * mostDiscount
                        });

                    }
                }
                foreach (var proP in distancePriority)
                {
                    var exist = result.Where(p => p.Id == proP.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Priority += proP.Priority * nearest;
                    }
                    else
                    {
                        result.Add(new PriorityModel
                        {
                            Id = proP.Id,
                            Priority = proP.Priority * nearest
                        });

                    }
                }

                result = result.OrderByDescending(p => p.Priority).ToList();

                var recommendStadiums = new List<RecommendStadium>();

                foreach (var item in result)
                {
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    recommendStadiums.Add(temp);
                }

                return recommendStadiums;
            }
            else
            {
                return null;
            }
        }

        public RecommendationPriorityModel ViewPriority()
        {
            RecommendationDAO recDAO = new RecommendationDAO();
            RecommendationPriorityModel result = new RecommendationPriorityModel();
            //result.BestStadiums = recDAO.GetPriorityDetailsByPriorityName("BestStadium");
            //result.NearestStadiums = recDAO.GetPriorityDetailsByPriorityName("MaybeKnownStadium");
            //result.PromotionStadiums = recDAO.GetPriorityDetailsByPriorityName("PromotedStadium");
            result.BestStadiumsMostBooked = recDAO.GetPriorityByConfigName("Bs_MostBooked");
            result.BestStadiumsNearest = recDAO.GetPriorityByConfigName("Bs_Nearest");
            result.BestStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Bs_MostPromoted");

            result.AppropriateStadiumsMostBooked = recDAO.GetPriorityByConfigName("Ap_MostBooked");
            result.AppropriateStadiumsNearest = recDAO.GetPriorityByConfigName("Ap_Nearest");
            result.AppropriateStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Ap_MostPromoted");

            result.PromotionStadiumsMostBooked = recDAO.GetPriorityByConfigName("Pr_MostBooked");
            result.PromotionStadiumsNearest = recDAO.GetPriorityByConfigName("Pr_Nearest");
            result.PromotionStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Pr_MostPromoted");
            return result;            
        }


        public int EditPriority(int PriorityID, int MostBooked, int MostNearest, int MostDiscount)
        {
            RecommendationDAO recDAO = new RecommendationDAO();
            
            return 1;
        }

        public int UpdatePriorityConfig(List<Configuration> configs)
        {
            double total = 0;
            foreach (var item in configs)
            {
                total += double.Parse(item.Value);
                
            }
            if (total != 100)
            {
                return -2;
            }
            else
            {
                ConfigurationDAO conDAO = new ConfigurationDAO();
                return conDAO.UpdateConfigurations(configs);
            }

        }
    }

    
}