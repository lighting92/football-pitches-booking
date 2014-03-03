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
    }
}