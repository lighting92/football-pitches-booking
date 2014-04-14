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
        


    }
}