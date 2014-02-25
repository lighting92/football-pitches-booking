using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class UserDAO
    {
        private FPBDataContext db;

        public UserDAO()
        {
            db = new FPBDataContext();
        }

        public User GetUserByUserName(string userName)
        {
            User user = db.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = db.Users.Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();
            return user;
        }

        public int CreateUser(User newUser)
        {
            try
            {
                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
                return newUser.Id;
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public int UpdateUser(int userId, string email, string password, string fullName, string address, string phoneNumber, 
            int? point, int? rankId, bool? isReceiveReward, bool? isActive)
        {
            try
            {
                User user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = email;
                }
                if (!string.IsNullOrEmpty(password))
                {
                    user.Password = password;
                }
                if (!string.IsNullOrEmpty(fullName))
                {
                    user.FullName = fullName;
                }
                if (!string.IsNullOrEmpty(address))
                {
                    user.Address = address;
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    user.PhoneNumber = phoneNumber;
                }
                if (point != null)
                {
                    user.Point = point.Value;
                }
                if (rankId != null)
                {
                    user.RankId = rankId.Value;
                }
                if (isReceiveReward != null)
                {
                    user.IsReceivedReward = isReceiveReward.Value;
                }
                if (isActive != null)
                {
                    user.IsActive = isActive.Value;
                }
                
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