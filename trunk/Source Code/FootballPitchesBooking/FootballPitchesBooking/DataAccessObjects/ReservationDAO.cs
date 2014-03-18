using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class ReservationDAO
    {
        public List<Reservation> GetAllReservations()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.ToList();
        }


        public List<Reservation> GetReservationsOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.Field.StadiumId == stadiumId).ToList();
        }


        public Reservation GetReservationById(int revId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Reservations.Where(r => r.Id == revId).FirstOrDefault();
        }


        public int CreateReservation(Reservation rev)
        {
            FPBDataContext db = new FPBDataContext();
            db.Reservations.InsertOnSubmit(rev);

            try
            {
                db.SubmitChanges();
                return rev.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservation(Reservation rev)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == rev.Id).FirstOrDefault();
            curRev.FieldId = rev.FieldId;
            curRev.UserId = rev.UserId;
            curRev.FullName = rev.FullName;
            curRev.PhoneNumber = rev.PhoneNumber;
            curRev.Email = rev.Email;
            curRev.Date = rev.Date;
            curRev.StartTime = rev.StartTime;
            curRev.Duration = rev.Duration;
            curRev.Price = rev.Price;
            curRev.Discount = rev.Discount;
            curRev.Status = rev.Status;
            curRev.HasRival = rev.HasRival;
            curRev.RivalId = rev.RivalId;
            curRev.RivalName = rev.RivalName;
            curRev.RivalPhone = rev.RivalPhone;
            curRev.RivalEmail = rev.RivalEmail;

            try
            {
                db.SubmitChanges();
                return rev.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservationRival(Reservation rev)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == rev.Id).FirstOrDefault();
            curRev.HasRival = rev.HasRival;
            curRev.RivalId = rev.RivalId;
            curRev.RivalName = rev.RivalName;
            curRev.RivalPhone = rev.RivalPhone;
            curRev.RivalEmail = rev.RivalEmail;
            curRev.RivalFinder = rev.RivalFinder;

            try
            {
                db.SubmitChanges();
                return rev.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateReservationStatus(int revId, string status)
        {
            FPBDataContext db = new FPBDataContext();
            Reservation curRev = db.Reservations.Where(r => r.Id == revId).FirstOrDefault();
            curRev.Status = status;

            try
            {
                db.SubmitChanges();
                return revId;
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