using FootballPitchesBooking.DataAccessObjects;
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
        public List<RecommendStadium> FindAppropriateStadium(string userName, double mostReserve, double near, double promotion)
        {
            if (!string.IsNullOrEmpty(userName))
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
                var promotions = proDAO.GetAllPromotions();
                var promotionStadiums = promotions.GroupBy(r => r.Field.StadiumId).Select(r => new { StadiumId = r.Key, Count = r.Count() }).OrderByDescending(r => r.Count).ToList();
                var promotionsPriority = new List<PriorityModel>();
                if (promotionStadiums != null && promotionStadiums.Count() != 0)
                {
                    var difPro = promotionStadiums.First().Count - promotionStadiums.Last().Count;
                    var e = 0;
                    if (difPro != 0)
                    {
                        e = 10 / difPro;
                    }
                    var maxPro = promotionStadiums.First().Count;


                    for (int i = 0; i < promotionStadiums.Count(); i++)
                    {
                        PriorityModel temp = new PriorityModel();
                        temp.Id = promotionStadiums[i].StadiumId;
                        temp.Priority = 10 - ((maxPro - promotionStadiums[i].Count) * e);
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
                            if (distanceList[i] <= 2)
                            {
                                temp.Priority = 10;
                            }
                            else if (distanceList[i] <= 4)
                            {
                                temp.Priority = 9;
                            }
                            else if (distanceList[i] <= 6)
                            {
                                temp.Priority = 8;
                            }
                            else if (distanceList[i] <= 8)
                            {
                                temp.Priority = 7;
                            }
                            else if (distanceList[i] <= 10)
                            {
                                temp.Priority = 6;
                            }
                            else if (distanceList[i] <= 12)
                            {
                                temp.Priority = 5;
                            }
                            else if (distanceList[i] <= 14)
                            {
                                temp.Priority = 4;
                            }
                            else if (distanceList[i] <= 16)
                            {
                                temp.Priority = 3;
                            }
                            else if (distanceList[i] <= 18)
                            {
                                temp.Priority = 2;
                            }
                            else if (distanceList[i] <= 20)
                            {
                                temp.Priority = 1;
                            }
                        }
                        else
                        {
                            temp.Priority = 0;
                        }
                        distancePriority.Add(temp);
                    }



                }
                //có 3 list
                // distancePriority * 0.3
                //promotionsPriority 0.2
                //reservationsPriority 0.5
                //gom lai 1 list di


                foreach (var resP in reservationsPriority)
                {
                    //trong list reservation thi point = priority * 0.5 het roi
                    resP.Priority = resP.Priority * 0.5;

                    //kiem tra xem co phan tu nao trung id voi resP ko
                    foreach (var proP in promotionsPriority)
                    {
                        if (resP.Id == proP.Id)
                        {
                            resP.Priority = resP.Priority + proP.Priority * 0.2; //neu trung thi + diem vao
                            promotionsPriority.Remove(proP); //+ xong remove de loai bo Id trung
                        }
                    }
                }

                //con cac id ko trung nhau thi add vao
                foreach (var proP in promotionsPriority)
                {
                    reservationsPriority.Add(proP);
                }

                //lai kiem tra voi distance
                foreach (var resP in reservationsPriority)
                {
                    //kiem tra xem co phan tu nao trung id voi resP ko
                    foreach (var disP in distancePriority)
                    {
                        if (resP.Id == disP.Id)
                        {
                            resP.Priority = resP.Priority + disP.Priority * 0.3;
                            promotionsPriority.Remove(disP);
                        }
                    }
                }

                //cung loai bo cac phan tu luon roi add them vao, dam bao list reservation moi co day du phan tu cua ca 3 list + lai
                //ma tot nhat em nen copy ra list khac nen ko muon thay doi 3 list nay, con neu ko dung den 3 list nay nua
                //xem 3 list do vo dung sau khi gom lai thi gom truc tiep luon cho do~ ton' bo. nho'
                foreach (var disP in distancePriority)
                {
                    reservationsPriority.Add(disP);
                }

                var newList = reservationsPriority.OrderBy(p => p.Priority).ToList(); //em debug roi xem lai list sap xep dung chua
            
            }
            

            return null;
        }


    }
}