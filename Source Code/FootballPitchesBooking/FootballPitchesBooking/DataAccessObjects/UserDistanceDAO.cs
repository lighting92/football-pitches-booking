using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class UserDistanceDAO
    {
        public UserDistance GetUserDistanceByUserName(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.UserDistances.Where(u => u.User.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
        }

        public int CreateUserDistance(UserDistance ud)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.UserDistances.InsertOnSubmit(ud);
                db.SubmitChanges();
                return ud.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateUserDistance(UserDistance ud)
        {
            FPBDataContext db = new FPBDataContext();
            var oud = db.UserDistances.Where(u => u.Id == ud.Id).FirstOrDefault();
            oud.Path = ud.Path;
            oud.UpdateDate = ud.UpdateDate;
            oud.UserId = ud.UserId;
            try
            {
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