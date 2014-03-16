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

        public List<User> GetUsersInListNames(List<string> userNames)
        {
            for (int i = 0; i < userNames.Count(); i++)
            {
                userNames[i] = userNames[i].Trim().ToLower();
            }
            return db.Users.Where(u => userNames.Contains(u.UserName.ToLower())).ToList();
        }

        public int CreateUser(User user)
        {
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

        public List<User> GetAllUser()
        {
            return db.Users.ToList();
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

        public int UpdateUserRole(int userId, int roleId)
        {
            var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
            try
            {
                user.RoleId = roleId;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateUserRank(MemberRank rank)//  ko biet userDAO ma dung ham cua rankDAO thi sao ko =))
        {
            MemberRank prevRank = db.MemberRanks.OrderByDescending(m => m.Point < rank.Point).FirstOrDefault();
            MemberRank nextRank = db.MemberRanks.OrderBy(m => m.Point > rank.Point).FirstOrDefault();
            
            try
            {
                db.Users.Where(u => u.Point < rank.Point && u.Point >= prevRank.Point).ToList().ForEach(u => u.RankId = prevRank.Id);
                db.Users.Where(u => u.Point >= rank.Point && u.Point < nextRank.Point).ToList().ForEach(u => u.RankId = rank.Id);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateUser(User newuser)
        {
            try
            {
                var olduser = db.Users.Where(x => x.Id == newuser.Id).FirstOrDefault();
                olduser.FullName = newuser.FullName;
                olduser.Address = newuser.Address;
                olduser.PhoneNumber = newuser.PhoneNumber;
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