using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.RecommendationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class RecommendationDAO
    {
        public List<RecommendationTypeModel> ViewPriority()
        {
            FPBDataContext db = new FPBDataContext();
            RecommendationTypeModel recModel = new RecommendationTypeModel();
            List<RecommendationTypeModel> listRecModel = new List<RecommendationTypeModel>();
            var querryResult = db.PriorityDetails.ToList();
            
            foreach (var item in querryResult)
            {
                recModel.PriorityId = item.PriorityId;
                recModel.PriorityType = item.PriorityType;
                recModel.PriorityPercent = item.PriorityPercent;
                listRecModel.Add(recModel);
            }

            return listRecModel;
        }
        public int UpdatePriority(int PriorityID, int MostBooked, int MostNearest, int MostDiscount)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var querryResult = db.PriorityDetails.Where(s => s.PriorityId == PriorityID).ToList();
                foreach (var item in querryResult)
                {
                    if (item.PriorityType == "MostBooked")
                    {
                        item.PriorityPercent = MostBooked;
                    }
                    if (item.PriorityType == "MostNearest")
                    {
                        item.PriorityPercent = MostNearest;
                    }
                    if (item.PriorityType == "MostDiscount")
                    {
                        item.PriorityPercent = MostDiscount;
                    }
                }
                db.SubmitChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}