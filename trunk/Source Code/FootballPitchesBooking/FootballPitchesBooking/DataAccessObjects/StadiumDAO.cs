using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumDAO
    {
        public Stadium GetStadiumById(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(s => s.Id == stadiumId).FirstOrDefault();
        }

        public List<Stadium> GetStadiumsByMainOwnerId(int mainOwnerId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(s => s.MainOwner == mainOwnerId).ToList();
        }

        public List<Stadium> GetStadiumsByOwnerName(string ownerName)
        {
            FPBDataContext db = new FPBDataContext();

            var result = db.StadiumStaffs.Where(ss => ss.User.UserName.ToLower().Equals(ownerName.ToLower())).Select(ss => ss.Stadium).ToList();
            var more = db.Stadiums.Where(s => s.User.UserName.ToLower().Equals(ownerName.ToLower())).ToList();

            foreach (var item in more)
            {
                result.Add(item);
            }
            return result.Distinct().ToList();
        }

        public List<Stadium> GetStadiumsByStaff(string staffName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(s => s.User.UserName.ToLower().Equals(staffName.ToLower())
                || s.StadiumStaffs.Any(ss => ss.User.UserName.ToLower().Equals(staffName.ToLower()))).ToList();
        }

        public List<Stadium> GetAllStadiums()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.ToList();
        }

        public List<Stadium> GetStadiums(string name, string address)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(p => p.Name.Contains(name) && string.Concat(p.Street, ", ", p.Ward, ", ", p.District).Contains(address)).ToList();
        }

        public int CreateStadium(Stadium stadium)
        {
            FPBDataContext db = new FPBDataContext();
            db.Stadiums.InsertOnSubmit(stadium);
            try
            {
                db.SubmitChanges();
                return stadium.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateStadium(Stadium stadium, bool updateOwner = true)
        {
            FPBDataContext db = new FPBDataContext();
            Stadium currentStadium = db.Stadiums.Where(s => s.Id == stadium.Id).FirstOrDefault();
            currentStadium.Name = stadium.Name;
            currentStadium.Street = stadium.Street;
            currentStadium.Ward = stadium.Ward;
            currentStadium.District = stadium.District;
            currentStadium.Phone = stadium.Phone;
            currentStadium.Email = stadium.Email;
            currentStadium.IsActive = stadium.IsActive;
            currentStadium.OpenTime = stadium.OpenTime;
            currentStadium.CloseTime = stadium.CloseTime;
            currentStadium.ExpiredDate = stadium.ExpiredDate;
            if (updateOwner)
            {
                currentStadium.MainOwner = stadium.MainOwner;
            }
            try
            {
                db.SubmitChanges();
                return currentStadium.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateStadiumStatus(int stadiumId, bool stadiumStatus)
        {
            FPBDataContext db = new FPBDataContext();
            Stadium stadium = db.Stadiums.Where(s => s.Id == stadiumId).FirstOrDefault();
            stadium.IsActive = stadiumStatus;

            try
            {
                db.SubmitChanges();
                return stadium.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            Stadium stadium = db.Stadiums.Where(s => s.Id == stadiumId).FirstOrDefault();
            try
            {
                db.Stadiums.DeleteOnSubmit(stadium);
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