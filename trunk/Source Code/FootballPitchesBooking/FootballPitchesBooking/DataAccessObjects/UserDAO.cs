using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class UserDAO
    {
        private FPBDataContext db = new FPBDataContext();

        public User GetUserByUserId(int userId)
        {
            return db.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public User GetUserByUserName(string userName)
        {
            return db.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
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

        /// <summary>
        /// Select all User in db
        /// </summary>
        /// <returns>List Users</returns>

        public List<User> Select()
        {
            try
            {
                var users = db.Users.ToList();
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }        

    }
}