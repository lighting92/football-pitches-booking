using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumReviewDAO
    {
        public List<StadiumReview> GetAllReviews()
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumReviews.OrderBy(r => r.IsApproved).ThenByDescending(r => r.CreateDate).ToList();
        }

        public List<StadiumReview> GetReviewsByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumReviews.Where(r => r.StadiumId == stadiumId).OrderByDescending(r => r.CreateDate).ToList();
        }

        public StadiumReview GetReviewById(int reviewId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumReviews.Where(r => r.Id == reviewId).FirstOrDefault();
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

        public bool DeleteReview(int reviewId)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                StadiumReview review = db.StadiumReviews.Where(r => r.Id == reviewId).FirstOrDefault();
                db.StadiumReviews.DeleteOnSubmit(review);
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