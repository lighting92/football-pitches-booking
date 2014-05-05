using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class ReservationDAO
    {
        public List<Reservation> GetAllReservationsOfUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.User.UserName.ToLower().Equals(userName.ToLower())).ToList();
        }


        public List<Reservation> GetAllReservationsOfRival(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.User2.UserName.ToLower().Equals(userName.ToLower()))
                                  .OrderByDescending(r => r.Date).ThenByDescending(r => r.StartTime).ToList();
        }


        public List<Reservation> GetAllReservations()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.ToList();
        }

        public List<Reservation> GetAllPendingReservationOfStadiumsForUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            var user = db.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();

            var staIds = user.Stadiums.Select(s => s.Id).ToList();
            var stastaff = user.StadiumStaffs.Select(ss => ss.StadiumId).ToList();
            foreach (var item in stastaff)
            {
                staIds.Add(item);
            }
            staIds = staIds.Distinct().ToList();

            double nowT = DateTime.Now.Hour + (DateTime.Now.Minute / 60.0);
            var result = db.Reservations.Where(r => staIds.Contains(r.Field.StadiumId) &&
                r.Status.ToLower().Equals("pending") &&
                (r.Date > DateTime.Now.Date ||
                (r.Date == DateTime.Now.Date && r.StartTime > nowT))
                ).ToList();
            return result;
        }

        public List<Reservation> GetReservationsNeedRival()
        {
            FPBDataContext db = new FPBDataContext();
            double time = DateTime.Now.Hour + (DateTime.Now.Minute / 60.0);
            return db.Reservations.Where(r => r.NeedRival == true && r.RivalId == null && r.RivalName.Equals(null) &&
                ((r.Date.Date.CompareTo(DateTime.Now.Date) > 0) || (r.Date.Date.CompareTo(DateTime.Now.Date) == 0
                 && r.StartTime > time)) && r.RivalStatus.Equals("Waiting") && (r.Status.Equals("Approved") || r.Status.Equals("Pending"))
                   ).OrderBy(r => r.Date).ThenBy(r => r.StartTime).ToList();
        }


        public Reservation GetReservationNeedRivalById(int id)
        {
            FPBDataContext db = new FPBDataContext();
            double time = DateTime.Now.Hour + (DateTime.Now.Minute / 60.0);
            return db.Reservations.Where(r => r.Id == id && r.NeedRival == true && r.RivalId == null && r.RivalName.Equals(null)
                && ((r.Date.Date.CompareTo(DateTime.Now.Date) > 0) || (r.Date.Date.CompareTo(DateTime.Now.Date) == 0
                 && r.StartTime > time)) && r.RivalStatus.Equals("Waiting")
                   ).FirstOrDefault();
        }


        public List<Reservation> GetReservationsNeedRival(string user, DateTime date, int type)
        {
            FPBDataContext db = new FPBDataContext();
            double time = DateTime.Now.Hour + (DateTime.Now.Minute / 60.0);
            List<Reservation> allRivals = db.Reservations.Where(r => r.NeedRival == true && r.RivalId == null && r.RivalName.Equals(null) &&
                ((r.Date.Date.CompareTo(DateTime.Now.Date) > 0) || (r.Date.Date.CompareTo(DateTime.Now.Date) == 0
                 && r.StartTime > time)) && r.RivalStatus.Equals("Waiting")
                   ).OrderBy(r => r.Date).ThenBy(r => r.StartTime).ToList();

            if (allRivals.Count == 0)
            {
                return allRivals;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(user))
                {
                    allRivals = allRivals.Where(r => r.FullName == user || r.User.UserName == user).ToList();
                }
                if (date > DateTime.Now)
                {
                    allRivals = allRivals.Where(r => r.Date.Date == date.Date).ToList();
                }
                if (type != 0)
                {
                    allRivals = allRivals.Where(r => r.Field.FieldType == type).ToList();
                }
                return allRivals;
            }
        }


        public List<Reservation> GetReservationsOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.Field.StadiumId == stadiumId).ToList();
        }


        public List<Reservation> GetReservationsOfField(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.FieldId == fieldId).ToList();
        }


        public Reservation GetReservationById(int reservationId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.Id == reservationId).FirstOrDefault();
        }


        public int CreateReservation(Reservation reservation)
        {
            FPBDataContext db = new FPBDataContext();
            db.Reservations.InsertOnSubmit(reservation);

            try
            {
                db.SubmitChanges();
                return reservation.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservation(Reservation reservation)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == reservation.Id).FirstOrDefault();
            curRev.FullName = reservation.FullName;
            curRev.PhoneNumber = reservation.PhoneNumber;
            curRev.Email = reservation.Email;
            curRev.Date = reservation.Date;
            curRev.StartTime = reservation.StartTime;
            curRev.Duration = reservation.Duration;
            curRev.Price = reservation.Price;
            curRev.Status = reservation.Status;
            curRev.NeedRival = reservation.NeedRival;
            curRev.RivalId = reservation.RivalId;
            curRev.RivalName = reservation.RivalName;
            curRev.RivalPhone = reservation.RivalPhone;
            curRev.RivalEmail = reservation.RivalEmail;
            curRev.RivalStatus = reservation.RivalStatus;

            try
            {
                db.SubmitChanges();
                return reservation.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservationRival(int reservationId, User user)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == reservationId).FirstOrDefault();
            if (user != null)
            {
                curRev.RivalId = user.Id;
                curRev.RivalName = user.FullName;
                curRev.RivalPhone = user.PhoneNumber;
                curRev.RivalEmail = user.Email;
                curRev.RivalStatus = "Pending";

                Notification newMsg = new Notification();
                UserDAO userDAO = new UserDAO();
                newMsg.Message = "Bạn nhận yêu cầu giao hữu từ [" + user.UserName + "] <a href='/Account/EditReservation?Id="
                    + curRev.Id + "'>Chi tiết</a>";
                newMsg.Status = "Unread";
                newMsg.CreateDate = DateTime.Now;
                if (curRev.UserId != null)
                {
                    newMsg.UserId = curRev.UserId;
                }
                else
                {
                    newMsg.StadiumId = curRev.Field.StadiumId;
                }

                NotificationDAO unDAO = new NotificationDAO();
                unDAO.CreateMessage(newMsg);
            }
            else
            {
                curRev.RivalId = null;
                curRev.RivalName = null;
                curRev.RivalPhone = null;
                curRev.RivalEmail = null;
                curRev.RivalFinder = null;
                curRev.RivalStatus = "Waiting";

                Notification newMsg = new Notification();
                UserDAO userDAO = new UserDAO();
                newMsg.Message = "Đối thủ giao hữu với bạn đã huỷ bỏ yêu cầu giao hữu <a href='/Account/EditReservation?Id="
                    + curRev.Id + "'>Chi tiết</a>";
                newMsg.Status = "Unread";
                newMsg.CreateDate = DateTime.Now;
                if (curRev.UserId != null)
                {
                    newMsg.UserId = curRev.UserId;
                }
                else
                {
                    newMsg.StadiumId = curRev.Field.StadiumId;
                }

                NotificationDAO unDAO = new NotificationDAO();
                unDAO.CreateMessage(newMsg);
            }

            try
            {
                db.SubmitChanges();
                return reservationId;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservationStatus(int reservationId, string status, int approver)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == reservationId).FirstOrDefault();
            curRev.Status = status;
            if (approver > 0)
            {
                curRev.Approver = approver;
            }

            try
            {
                db.SubmitChanges();
                return reservationId;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        public int UserCancelReservation(int resId)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var res = db.Reservations.Where(r => r.Id == resId).FirstOrDefault();
                if (res != null)
                {
                    res.Status = "Canceled";
                    db.SubmitChanges();
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UserUpdateReservation(Reservation res)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var cur = db.Reservations.Where(r => r.Id == res.Id).FirstOrDefault();
                if (cur != null)
                {
                    int result = 1;
                    if (!string.IsNullOrEmpty(cur.RivalStatus) && !string.IsNullOrEmpty(res.RivalStatus) &&
                        !cur.RivalStatus.ToLower().Equals(res.RivalStatus.ToLower()) && res.RivalStatus.ToLower().Equals("approve"))
                    {
                        result = 2;
                    }
                    cur.FullName = res.FullName;
                    cur.Email = res.Email;
                    cur.PhoneNumber = res.PhoneNumber;
                    cur.NeedRival = res.NeedRival;
                    cur.RivalId = res.RivalId;
                    cur.RivalName = res.RivalName;
                    cur.RivalPhone = res.RivalPhone;
                    cur.RivalEmail = res.RivalEmail;
                    cur.RivalStatus = res.RivalStatus;
                    db.SubmitChanges();
                    return result;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}