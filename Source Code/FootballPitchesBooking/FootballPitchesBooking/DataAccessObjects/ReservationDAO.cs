using FootballPitchesBooking.Models;
using FootballPitchesBooking.Utilities;
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

        public List<Reservation> GetAllReservations()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.ToList();
        }


        public List<Reservation> GetReservationsNeedRival()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.HasRival == true && r.RivalId == null && r.RivalName.Equals(null) &&
                   r.RivalPhone.Equals(null) && r.RivalEmail.Equals(null) && (r.StartTime > DateTime.Now)).OrderByDescending(r => r.StartTime).ToList();
        }


        public Reservation GetReservationNeedRivalById(int id)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.Id == id && r.HasRival == true && r.RivalId == null && r.RivalName.Equals(null) &&
                   r.RivalPhone.Equals(null) && r.RivalEmail.Equals(null) && (r.StartTime > DateTime.Now)).OrderByDescending(r => r.StartTime).FirstOrDefault();
        }


        public List<Reservation> GetReservationsNeedRival(string user, DateTime date, int type)
        {
            FPBDataContext db = new FPBDataContext();
            List<Reservation> allRivals = db.Reservations.Where(r => r.HasRival == true && r.RivalId == null && r.RivalName.Equals(null) &&
                   r.RivalPhone.Equals(null) && r.RivalEmail.Equals(null) && (r.StartTime > DateTime.Now)).OrderByDescending(r => r.StartTime).ToList();

            if (allRivals.Count == 0)
            {
                return allRivals;
            }
            else
            {
                if (!string.IsNullOrEmpty(user))
                {
                    allRivals = allRivals.Where(r => r.FullName == user || r.User.UserName == user).ToList();
                }
                if (date > DateTime.Now)
                {
                    allRivals = allRivals.Where(r => r.StartTime.Date == date.Date).ToList();
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
            curRev.StartTime = reservation.StartTime;
            curRev.Duration = reservation.Duration;
            curRev.Price = reservation.Price;
            curRev.Status = reservation.Status;
            curRev.HasRival = reservation.HasRival;
            curRev.RivalId = reservation.RivalId;
            curRev.RivalName = reservation.RivalName;
            curRev.RivalPhone = reservation.RivalPhone;
            curRev.RivalEmail = reservation.RivalEmail;

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


        public int UpdateReservationRival(Reservation resveration)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == resveration.Id).FirstOrDefault();
            curRev.RivalId = resveration.RivalId;
            curRev.RivalName = resveration.RivalName;
            curRev.RivalPhone = resveration.RivalPhone;
            curRev.RivalEmail = resveration.RivalEmail;

            try
            {
                db.SubmitChanges();
                return resveration.Id;
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
            curRev.Approver = approver;

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


        public int UpdateReservationByPromotionChanged(int promotionId)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.Reservations.Where(r => r.PromotionId == promotionId).ToList().ForEach(r => r.PromotionId = null);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}