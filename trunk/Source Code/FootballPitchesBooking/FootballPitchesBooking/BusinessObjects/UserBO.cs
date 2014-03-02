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
        /// <summary>
        /// Create user. Return a list result for validating.
        /// Return a positive integer (id of new user) for success proccess, 0 for exception, 
        /// -1 for duplicate user name, -2 for duplicate email
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public List<int> CreateUser(User newUser)
        {
            List<int> results = new List<int>();
            UserDAO userDAO = new UserDAO();

            User duplicatedNameUser = userDAO.GetUserByUserName(newUser.UserName);
            User duplicatedEmailUser = userDAO.GetUserByEmail(newUser.Email);

            if (duplicatedEmailUser != null)
            {
                results.Add(-1);
            }
            if (duplicatedEmailUser != null)
            {
                results.Add(-2);
            }

            if (results.Count == 0)
            {
                MemberRankDAO mrDAO = new MemberRankDAO();
                MemberRank mr = mrDAO.GetMemberRankByPoint(0);
                RoleDAO roleDAO = new RoleDAO();
                Role r = roleDAO.GetRoleByRoleName("Member");
                newUser.Point = 0;
                newUser.RankId = mr.Id;
                newUser.IsReceivedReward = true;
                newUser.IsActive = true;
                newUser.JoinDate = DateTime.Now;
                newUser.RoleId = r.Id;
                results.Add(userDAO.CreateUser(newUser));
            }            

            return results;
        }


        /// <summary>
        /// Validate for login information of user.
        /// Return 1 for valid info, -1 for user not available, -2 for incorrect password.
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public int Authenticate(User loginUser)
        {
            UserDAO userDAO = new UserDAO();
            User user = userDAO.GetUserByUserName(loginUser.UserName);
            if (user != null)
            {
                if (user.Password.Equals(loginUser.Password))
                {
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}