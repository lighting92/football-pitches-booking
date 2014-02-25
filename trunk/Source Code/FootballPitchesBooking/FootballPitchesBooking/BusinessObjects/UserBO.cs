using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class UserBO
    {
        private UserDAO userDAO;

        public UserBO()
        {
            userDAO = new UserDAO();
        }

        /// <summary>
        /// Return a list roles of user.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<String> GetRolesOfUser(string userName)
        {
            List<String> result = new List<string>();

            User user = userDAO.GetUserByUserName(userName);

            if (user.IsActive)
            {
                result.Add("ActiveUser");

                WebsiteStaff ws = user.WebsiteStaff;
                List<StadiumStaff> ss = user.StadiumStaffs.ToList();

                if (ws != null)
                {
                    if (ws.IsMaster)
                    {
                        result.Add("WebsiteMaster");
                    }
                    else
                    {
                        result.Add("WebsiteStaff");
                    }
                }
                bool isOwner = false;
                bool isStaff = false;
                if (ss != null)
                {
                    isStaff = true;
                    foreach (var staff in ss)
                    {
                        if (staff.IsOwner)
                        {
                            result.Add("StadiumOwner");
                            isOwner = true;
                            break;
                        }
                    }
                }

                if (isStaff && !isOwner)
                {
                    result.Add("StadiumStaff");
                }
            }
            else
            {
                result.Add("InactiveUser");
            }

            return result;
        }

        /// <summary>
        /// Create user. Return a list result for validating.
        /// Return a positive integer (id of new user) for success proccess, 0 for exception, -1 for null or empty fields, 
        /// -2 for duplicate user name, -3 for duplicate email, -4 for password and confirm are not match
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public List<int> CreateUser(string userName, string password, string confirmPassword, string email,
            string fullName, string address, string phoneNumber)
        {
            List<int> result = new List<int>();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)
                || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(address)
                || string.IsNullOrEmpty(phoneNumber))
            {
                result.Add(-1);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                if (IsDuplicateUserName(userName))
                {
                    result.Add(-2);
                }
            }

            if (!string.IsNullOrEmpty(email))
            {
                if (IsDuplicateEmail(email))
                {
                    result.Add(-3);
                }
            }

            if (!password.Equals(confirmPassword))
            {
                result.Add(-4);
            }

            if (result.Count() == 0)
            {
                MemberRankDAO memberRankDAO = new MemberRankDAO();
                MemberRank rank = memberRankDAO.GetMemberRankByPoint(0);
                User user = new User
                {
                    UserName = userName,
                    Password = password,
                    Email = email,
                    FullName = fullName,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Point = 0,
                    RankId = rank.Id,
                    IsReceivedReward = true,
                    JoinDate = DateTime.Now,
                    IsActive = true,
                };

                result.Add(userDAO.CreateUser(user));
            }

            return result;
        }

        /// <summary>
        /// Checking available of user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsDuplicateUserName(string userName)
        {
            User dupUserName = userDAO.GetUserByUserName(userName);
            if (dupUserName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checking available of email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsDuplicateEmail(string email)
        {
            User dupEmail = userDAO.GetUserByEmail(email);
            if (dupEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validate for login information of user.
        /// Return 1 for valid info, -1 for null or empty fields, -2 for user not available, -3 for incorrect password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ValidateUserLogin(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return -1;
            }
            else
            {
                User user = userDAO.GetUserByUserName(userName);
                if (user != null)
                {
                    if (user.Password.Equals(password))
                    {
                        return 1;
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    return -2;
                }
            }
        }
    }
}