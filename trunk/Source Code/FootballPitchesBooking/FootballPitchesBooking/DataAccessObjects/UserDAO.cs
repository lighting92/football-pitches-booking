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


        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }


        public List<User> GetUsers(string keyword)
        {
            return db.Users.Where(u => u.UserName.Contains(keyword) || u.FullName.Contains(keyword)).OrderBy(u => u.Id).ToList();
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
            MemberRank prevRank = db.MemberRanks.Where(m => m.Point < rank.Point).OrderByDescending(m => m.Point).FirstOrDefault();
            MemberRank nextRank = db.MemberRanks.Where(m => m.Point > rank.Point).OrderBy(m => m.Point).FirstOrDefault();

            try
            {
                List<User> users = db.Users.Where(u => u.RankId == rank.Id).ToList();
                if (users != null && users.Count > 0)
                {
                    users.ForEach(u => u.RankId = db.MemberRanks.Where(m => m.Point <= u.Point).OrderByDescending(m => m.Point).FirstOrDefault().Id);
                }

                if (prevRank != null)
                {
                    db.Users.Where(u => u.Point < rank.Point && u.Point >= prevRank.Point).ToList().ForEach(u => u.RankId = prevRank.Id);
                }
                if (nextRank != null)
                {
                    db.Users.Where(u => u.Point >= rank.Point && u.Point < nextRank.Point).ToList().ForEach(u => u.RankId = rank.Id);
                }
                else
                {
                    db.Users.Where(u => u.Point >= rank.Point).ToList().ForEach(u => u.RankId = rank.Id);
                }
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateUserProfiles(User user)
        {
            try
            {
                var olduser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                olduser.Email = user.Email;
                olduser.FullName = user.FullName;
                olduser.Address = user.Address;
                olduser.PhoneNumber = user.PhoneNumber;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ChangePassword(User user)
        {
            try
            {
                var olduser = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                olduser.Password = user.Password;
                db.SubmitChanges();
                return olduser.Id;
            }
            catch (Exception)
            {
                return 0;
            } 
        }


        public int UpdateUser(User user)
        {
            try
            {
                var curUser = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                curUser.Password = user.Password;
                curUser.Email = user.Email;
                curUser.Point = user.Point;
                curUser.RankId = user.RankId;
                curUser.IsActive = user.IsActive;
                curUser.RoleId = user.RoleId;
                db.SubmitChanges();
                return user.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int ActiveUser(int Id)
        {
            try
            {
                var curUser = db.Users.Where(u => u.Id == Id).FirstOrDefault();
                curUser.IsActive = true;
                db.SubmitChanges();
                return Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}