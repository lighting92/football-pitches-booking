using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumRatingDAO
    {
        public List<StadiumRating> GetRatingByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumRatings.Where(r => r.StadiumId == stadiumId).ToList();
        }

        public bool UpdateStadiumRating(StadiumRating rating)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                StadiumRating curRating = db.StadiumRatings.Where(r => r.UserId == rating.UserId && r.StadiumId == rating.StadiumId).FirstOrDefault();
                if (curRating != null)
                {
                    curRating.Rate = rating.Rate;
                }
                else
                {
                    db.StadiumRatings.InsertOnSubmit(rating);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}