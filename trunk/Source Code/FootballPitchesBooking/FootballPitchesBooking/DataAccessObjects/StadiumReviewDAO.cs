using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumReviewDAO
    {
        public List<StadiumReview> GetReviewByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumReviews.Where(r => r.StadiumId == stadiumId).ToList();
        }

        public bool CreateStadiumReview(StadiumReview review)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.StadiumReviews.InsertOnSubmit(review);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeReviewStatus(int reviewId, bool isApproved, int approver)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                StadiumReview review = db.StadiumReviews.Where(r => r.Id == reviewId).FirstOrDefault();
                review.IsApproved = isApproved;
                review.Approver = approver;
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