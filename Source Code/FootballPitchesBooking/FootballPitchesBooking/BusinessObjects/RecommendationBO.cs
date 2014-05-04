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

        public List<RecommendRivalModel> FindRivals(string userName, List<StadiumDistance> distances)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // Most reservations
                ReservationDAO resDAO = new ReservationDAO();
                var reservationsNeedRival = resDAO.GetReservationsNeedRival();
                var listStadiumIds = reservationsNeedRival.Select(r => r.Field.StadiumId).Distinct().ToList();
                var reservations = resDAO.GetAllReservationsOfUser(userName);
                reservations = reservations.Where(r => listStadiumIds.Contains(r.Field.StadiumId)).ToList();
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

                // Nearest rival stadium
                UserDAO userDAO = new UserDAO();
                StadiumDAO stadiumDAO = new StadiumDAO();
                var distancePriority = new List<PriorityModel>();

                if (distances != null && distances.Count() != 0)
                {
                    distances = distances.Where(d => listStadiumIds.Contains(d.StadiumId)).ToList();
                    for (int i = 0; i < distances.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = distances[i].StadiumId;
                        if (distances[i].Distance != null)
                        {
                            if (distances[i].Distance <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distances[i].Distance <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distances[i].Distance <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distances[i].Distance <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distances[i].Distance <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distances[i].Distance <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distances[i].Distance <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distances[i].Distance <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distances[i].Distance <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distances[i].Distance <= 20000)
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

                // Experied Rival
                var nearExpiredPriority = new List<PriorityModel>();
                var reservationNearExpried = reservationsNeedRival.OrderBy(r => r.Date).ThenBy(r => r.StartTime).ToList();
                if (reservationNearExpried != null && reservationNearExpried.Count() > 0)
                {
                    var soon = reservationNearExpried.First().Date.AddHours(reservationNearExpried.First().StartTime);
                    var late = reservationNearExpried.Last().Date.AddHours(reservationNearExpried.Last().StartTime);
                    var span = late - soon;


                    double timePoint = 0;

                    if (span.TotalDays != 0)
                    {
                        timePoint = 10 / span.TotalDays;
                    }

                    if (reservationsNeedRival != null && reservationsNeedRival.Count() != 0)
                    {
                        for (int i = 0; i < reservationsNeedRival.Count(); i++)
                        {
                            PriorityModel temp = new PriorityModel();
                            var resTemp = reservationsNeedRival[i];

                            temp.Id = resTemp.Id;

                            var resSpan = resTemp.Date.AddHours(resTemp.StartTime) - soon;
                            if (resSpan.TotalDays == 0)
                            {
                                temp.Priority = 10;
                            }
                            else
                            {
                                temp.Priority = timePoint * (resSpan.TotalDays);
                            }
                            nearExpiredPriority.Add(temp);
                        }
                    }
                }
                else
                {
                    return null;
                }


                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Rv_Mostbooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Rv_Nearest").Value);
                var nearExpired = double.Parse(confDAO.GetConfigByName("Rv_Expired").Value);

                foreach (var near in nearExpiredPriority)
                {

                    var temp = resDAO.GetReservationById(near.Id);
                    var resP = reservationsPriority.Where(r => r.Id == temp.Field.StadiumId).FirstOrDefault();
                    double resPPriority = 0;
                    if (resP != null)
                    {
                        resPPriority = resP.Priority * mostBooked;
                    }
                    var nearP = distancePriority.Where(r => r.Id == temp.Field.StadiumId).FirstOrDefault();
                    double nearPriority = 0;
                    if (nearP != null)
                    {
                        nearPriority = nearP.Priority * nearest;
                    }
                    result.Add(new PriorityModel
                        {
                            Id = near.Id,
                            Priority = near.Priority * nearExpired + resPPriority + nearPriority
                        });

                }

                result = result.OrderByDescending(p => p.Priority).ToList();
                var recommendRival = new List<RecommendRivalModel>();

                foreach (var item in result)
                {
                    var temp = new RecommendRivalModel();
                    temp.Reservation = resDAO.GetReservationById(item.Id);
                    recommendRival.Add(temp);
                }
                return recommendRival;
            }
            else
            {
                return null;
            }
        }

        // Find best stadium
        public List<RecommendStadium> FindBestStadiums(string userName, List<StadiumDistance> distances)
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
                StadiumDAO stadiumDAO = new StadiumDAO();
                var distancePriority = new List<PriorityModel>();
                if (distances != null && distances.Count() != 0)
                {
                    for (int i = 0; i < distances.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = distances[i].StadiumId;
                        if (distances[i].Distance != null)
                        {
                            if (distances[i].Distance <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distances[i].Distance <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distances[i].Distance <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distances[i].Distance <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distances[i].Distance <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distances[i].Distance <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distances[i].Distance <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distances[i].Distance <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distances[i].Distance <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distances[i].Distance <= 20000)
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
                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Bs_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Bs_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Bs_MostPromoted").Value);

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
                PromotionDAO highestPromotion = new PromotionDAO();
                foreach (var item in result)
                {
                    //var temp = new RecommendStadium();
                    //temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    //recommendStadiums.Add(temp);
                    //
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    var prom = highestPromotion.GetHighestPromotionOfStadium(item.Id);
                    temp.HighestPromotion = prom;
                    recommendStadiums.Add(temp);
                }

                return recommendStadiums;
            }
            else
            {
                return null;
            }
        }

        //find nearest stadium
        public List<RecommendStadium> FindNearestStadiums(string userName, List<StadiumDistance> distances)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                // Most reservations
                if (distances == null)
                {
                    return null;
                }
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
                StadiumDAO stadiumDAO = new StadiumDAO();
                var distancePriority = new List<PriorityModel>();
                if (distances != null && distances.Count() != 0)
                {
                    for (int i = 0; i < distances.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = distances[i].StadiumId;
                        if (distances[i].Distance != null)
                        {
                            if (distances[i].Distance <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distances[i].Distance <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distances[i].Distance <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distances[i].Distance <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distances[i].Distance <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distances[i].Distance <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distances[i].Distance <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distances[i].Distance <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distances[i].Distance <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distances[i].Distance <= 20000)
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

                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Nr_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Nr_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Nr_MostPromoted").Value);
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
                PromotionDAO highestPromotion = new PromotionDAO();

                foreach (var item in result)
                {
                    //var temp = new RecommendStadium();
                    //temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    //recommendStadiums.Add(temp);
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    var prom = highestPromotion.GetHighestPromotionOfStadium(item.Id);
                    temp.HighestPromotion = prom;
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
        public List<RecommendStadium> FindPromotedStadiums(string userName, List<StadiumDistance> distances)
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
                StadiumDAO stadiumDAO = new StadiumDAO();
                var distancePriority = new List<PriorityModel>();

                if (distances != null && distances.Count() != 0)
                {
                    distances = distances.Where(d => listPSIds.Contains(d.StadiumId)).ToList();
                    for (int i = 0; i < distances.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = distances[i].StadiumId;
                        if (distances[i].Distance != null)
                        {
                            if (distances[i].Distance <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distances[i].Distance <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distances[i].Distance <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distances[i].Distance <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distances[i].Distance <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distances[i].Distance <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distances[i].Distance <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distances[i].Distance <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distances[i].Distance <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distances[i].Distance <= 20000)
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
                PromotionDAO highestPromotion = new PromotionDAO();
                foreach (var item in result)
                {
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    var prom = highestPromotion.GetHighestPromotionOfStadium(item.Id);
                    temp.HighestPromotion = prom;
                    recommendStadiums.Add(temp);
                }

                return recommendStadiums;
            }
            else
            {
                return null;
            }
        }

        //find mostbook stadium
        public List<RecommendStadium> FindMostBookedStadiums(string userName, List<StadiumDistance> distances)
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

                var listMostIds = mostReservationStadiums.Select(s => s.StadiumId).ToList();

                // Most promotions
                PromotionDAO proDAO = new PromotionDAO();
                var promotions = proDAO.GetAllActivePromotions();
                var promotionStadiums = promotions.Where(p => listMostIds.Contains(p.Field.StadiumId)).GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
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
                StadiumDAO stadiumDAO = new StadiumDAO();
                var distancePriority = new List<PriorityModel>();

                if (distances != null && distances.Count() != 0)
                {
                    distances = distances.Where(d => listMostIds.Contains(d.StadiumId)).ToList();
                    for (int i = 0; i < distances.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = distances[i].StadiumId;
                        if (distances[i].Distance != null)
                        {
                            if (distances[i].Distance <= 2000)
                            {
                                temp.Priority = 10;
                            }
                            else if (distances[i].Distance <= 4000)
                            {
                                temp.Priority = 9;
                            }
                            else if (distances[i].Distance <= 6000)
                            {
                                temp.Priority = 8;
                            }
                            else if (distances[i].Distance <= 8000)
                            {
                                temp.Priority = 7;
                            }
                            else if (distances[i].Distance <= 10000)
                            {
                                temp.Priority = 6;
                            }
                            else if (distances[i].Distance <= 12000)
                            {
                                temp.Priority = 5;
                            }
                            else if (distances[i].Distance <= 14000)
                            {
                                temp.Priority = 4;
                            }
                            else if (distances[i].Distance <= 16000)
                            {
                                temp.Priority = 3;
                            }
                            else if (distances[i].Distance <= 18000)
                            {
                                temp.Priority = 2;
                            }
                            else if (distances[i].Distance <= 20000)
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

                List<PriorityModel> result = new List<PriorityModel>();
                ConfigurationDAO confDAO = new ConfigurationDAO();
                var mostBooked = double.Parse(confDAO.GetConfigByName("Mb_MostBooked").Value);
                var nearest = double.Parse(confDAO.GetConfigByName("Mb_Nearest").Value);
                var mostDiscount = double.Parse(confDAO.GetConfigByName("Mb_MostPromoted").Value);
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
                PromotionDAO highestPromotion = new PromotionDAO();
                foreach (var item in result)
                {
                    //var temp = new RecommendStadium();
                    //temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    //recommendStadiums.Add(temp);
                    var temp = new RecommendStadium();
                    temp.Stadium = stadiumDAO.GetStadiumById(item.Id);
                    var prom = highestPromotion.GetHighestPromotionOfStadium(item.Id);
                    temp.HighestPromotion = prom;
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

            result.BestStadiumsMostBooked = recDAO.GetPriorityByConfigName("Bs_MostBooked");
            result.BestStadiumsNearest = recDAO.GetPriorityByConfigName("Bs_Nearest");
            result.BestStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Bs_MostPromoted");

            result.NearestStadiumsMostBooked = recDAO.GetPriorityByConfigName("Nr_MostBooked");
            result.NearestStadiumsNearest = recDAO.GetPriorityByConfigName("Nr_Nearest");
            result.NearestStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Nr_MostPromoted");

            result.PromotionStadiumsMostBooked = recDAO.GetPriorityByConfigName("Pr_MostBooked");
            result.PromotionStadiumsNearest = recDAO.GetPriorityByConfigName("Pr_Nearest");
            result.PromotionStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Pr_MostPromoted");

            result.MostBookedStadiumsMostBooked = recDAO.GetPriorityByConfigName("Mb_MostBooked");
            result.MostBookedStadiumsNearest = recDAO.GetPriorityByConfigName("Mb_Nearest");
            result.MostBookedStadiumsMostDiscount = recDAO.GetPriorityByConfigName("Mb_MostPromoted");

            result.RivalAtStadiumMostBooked = recDAO.GetPriorityByConfigName("Rv_Mostbooked");
            result.RivalAtStadiumNearest = recDAO.GetPriorityByConfigName("Rv_Nearest");
            result.RivalExpired = recDAO.GetPriorityByConfigName("Rv_Expired");

            result.MinTimeBooking = recDAO.GetPriorityByConfigName("MinTimeBooking");

            result.MinTimeCancel = recDAO.GetPriorityByConfigName("MinTimeCancel");

            result.GgPeriodic = recDAO.GetPriorityByConfigName("Gg_Periodic");
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