using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class ReservationDAO
    {
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