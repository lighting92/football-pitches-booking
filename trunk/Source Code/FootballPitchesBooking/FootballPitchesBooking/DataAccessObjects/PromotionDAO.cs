using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class PromotionDAO
    {
        public List<Promotion> GetHighestPromotionOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.Id == stadiumId).ToList();
        }
        public List<Promotion> GetAllActivePromotions()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.IsActive && p.Field.IsActive && p.Field.Stadium.IsActive &&
                p.PromotionFrom.Date.CompareTo(DateTime.Now) <= 0 && p.PromotionTo.Date.CompareTo(DateTime.Now) >= 0
                ).ToList();
        }
        public Promotion GetPromotionById(int promotionId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.Id == promotionId).FirstOrDefault();
        }


        public List<Promotion> GetAllPromotions()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.OrderByDescending(p => p.PromotionTo).ToList();
        }


        public List<Promotion> GetAllPromotionsOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.Field.StadiumId == stadiumId).OrderByDescending(p => p.PromotionTo).ToList();
        }


        public List<Promotion> GetAllPromotionsOfField(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.FieldId == fieldId).OrderByDescending(p => p.PromotionTo).ToList();
        }


        public Promotion GetPromotionByField(int fieldId, DateTime date)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Promotions.Where(p => p.FieldId == fieldId && p.PromotionFrom <= date && p.PromotionTo >= date).FirstOrDefault();
        }


        public int CreatePromotion(Promotion promotion)
        {
            FPBDataContext db = new FPBDataContext();
            db.Promotions.InsertOnSubmit(promotion);
            try
            {
                db.SubmitChanges();
                return promotion.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdatePromotion(Promotion promotion)
        {
            FPBDataContext db = new FPBDataContext();
            Promotion currentPromotion = db.Promotions.Where(p => p.Id == promotion.Id).FirstOrDefault();
            currentPromotion.FieldId = promotion.FieldId;
            currentPromotion.PromotionFrom = promotion.PromotionFrom;
            currentPromotion.PromotionTo = promotion.PromotionTo;
            currentPromotion.Discount = promotion.Discount;
            currentPromotion.IsActive = promotion.IsActive;

            try
            {
                db.SubmitChanges();
                return promotion.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdatePromotionStatus(int promotionId, bool status)
        {
            FPBDataContext db = new FPBDataContext();
            Promotion currentPromotion = db.Promotions.Where(p => p.Id == promotionId).FirstOrDefault();
            currentPromotion.IsActive = status;

            try
            {
                db.SubmitChanges();
                return promotionId;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int DeletePromotion(int promotionId)
        {
            FPBDataContext db = new FPBDataContext();
            Promotion promotion = db.Promotions.Where(p => p.Id == promotionId).FirstOrDefault();

            try
            {
                db.Promotions.DeleteOnSubmit(promotion);
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