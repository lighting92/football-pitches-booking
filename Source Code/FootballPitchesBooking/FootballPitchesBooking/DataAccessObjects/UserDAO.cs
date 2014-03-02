using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class UserDAO
    {
        public User GetUserByUserId(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public User GetUserByUserName(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Users.Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();
        }

        public int CreateUser(User user)
        {
            FPBDataContext db = new FPBDataContext();
            db.Users.InsertOnSubmit(user);
            try
            {
                db.SubmitChanges();
                return user.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}