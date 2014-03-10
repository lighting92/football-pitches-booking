﻿using FootballPitchesBooking.Models;
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

        public List<Stadium> GetStadiumsByOwner(int ownerId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.Where(s => s.MainOwner == ownerId).ToList();
        }

        public List<Stadium> GetAllStadiums()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Stadiums.ToList();
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

        public int UpdateStadium(Stadium stadium)
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
            currentStadium.MainOwner = stadium.MainOwner;

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