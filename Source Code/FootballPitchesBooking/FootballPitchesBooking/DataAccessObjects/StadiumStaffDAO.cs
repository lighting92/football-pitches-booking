using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumStaffDAO
    {
        public List<StadiumStaff> GetStaffByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumStaffs.Where(staff => staff.StadiumId == stadiumId).ToList();
        }

        public User GetMainOwnerOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(s => s.Id == stadiumId).FirstOrDefault().User;
        }

        public List<StadiumStaff> GetStadiumStaffByUser(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumStaffs.Where(ss => ss.UserId == userId).ToList();
        }

        public int CreateStadiumStaff(StadiumStaff stadiumStaff)
        {
            FPBDataContext db = new FPBDataContext();
            db.StadiumStaffs.InsertOnSubmit(stadiumStaff);
            try
            {
                db.SubmitChanges();
                return stadiumStaff.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateStadiumStaff(StadiumStaff stadiumStaff)
        {
            FPBDataContext db = new FPBDataContext();
            StadiumStaff currentStadiumStaff = db.StadiumStaffs.Where(s => s.Id == stadiumStaff.Id).FirstOrDefault();
            currentStadiumStaff.UserId = stadiumStaff.UserId;
            currentStadiumStaff.StadiumId = stadiumStaff.StadiumId;
            currentStadiumStaff.IsOwner = stadiumStaff.IsOwner;

            try
            {
                db.SubmitChanges();
                return currentStadiumStaff.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public StadiumStaff GetStadiumStaffByUserAndStadium(int userId, int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumStaffs.Where(ss => ss.UserId == userId &&  ss.StadiumId == stadiumId).FirstOrDefault();
        }

        public int DeleteStadiumStaff(int ssId)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var stadiumStaff = db.StadiumStaffs.Where(ss => ss.Id == ssId).FirstOrDefault();
                if (stadiumStaff != null)
                {
                    db.StadiumStaffs.DeleteOnSubmit(stadiumStaff);
                }
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteStadiumStaffByUserAndStadium(int userId, int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var stadiumStaff = db.StadiumStaffs.Where(ss => ss.UserId == userId && ss.StadiumId == stadiumId).FirstOrDefault();
                if (stadiumStaff != null)
                {
                    db.StadiumStaffs.DeleteOnSubmit(stadiumStaff);
                }
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