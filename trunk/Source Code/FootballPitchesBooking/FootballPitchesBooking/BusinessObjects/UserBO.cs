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
        UserDAO userDAO = new UserDAO();

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

            if (duplicatedNameUser != null)
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

        public User GetUserByUserName(string userName)
        {
            UserDAO userDAO = new UserDAO();
            return userDAO.GetUserByUserName(userName);
        }

        public List<User> ToList(ref List<NoModel> noList, int? page, string keyWord = "", string column = "", string sort = "")
        {
            // User list.
            var users = userDAO.Select();

            // Search by key word.
            if (keyWord.Equals(""))
            {
                users = users.OrderBy(u => u.Id).ToList();
            }
            else
            {
                users = users.Where(u => (u.FullName.ToLower().Contains(keyWord.ToLower()) || u.UserName.ToLower().Contains(keyWord.ToLower()))).OrderBy(u => u.Id).ToList();
            }

            // Sort by sort type and solumn name.
            switch (column + sort)
            {
                case "NoAsc":
                    users = users.OrderBy(u => u.Id).ToList();
                    break;
                case "NoDesc":
                    users = users.OrderByDescending(u => u.Id).ToList();
                    break;
                case "UserNameAsc":
                    users = users.OrderBy(u => u.UserName).ToList();
                    break;
                case "UserNameDesc":
                    users = users.OrderByDescending(u => u.UserName).ToList();
                    break;
                case "FullNameAsc":
                    users = users.OrderBy(u => u.FullName).ToList();
                    break;
                case "FullNameDesc":
                    users = users.OrderByDescending(u => u.FullName).ToList();
                    break;
                case "EmailAsc":
                    users = users.OrderBy(u => u.Email).ToList();
                    break;
                case "EmailDesc":
                    users = users.OrderByDescending(u => u.Email).ToList();
                    break;
                case "RoleAsc":
                    users = users.OrderBy(u => u.RoleId).ToList();
                    break;
                case "RoleDesc":
                    users = users.OrderByDescending(u => u.RoleId).ToList();
                    break;
                case "JoinDateAsc":
                    users = users.OrderBy(u => u.JoinDate).ToList();
                    break;
                case "JoinDateDesc":
                    users = users.OrderByDescending(u => u.JoinDate).ToList();
                    break;
                case "IsActiveAsc":
                    users = users.OrderBy(u => u.IsActive).ToList();
                    break;
                case "IsActiveDesc":
                    users = users.OrderByDescending(u => u.IsActive).ToList();
                    break;
            }

            // Generate no. List.
            foreach (var u in users)
            {
                noList.Add(new NoModel { Id = u.Id });
            }
            noList = noList.OrderBy(n => n.Id).ToList();
            for (int i = 0; i < noList.Count; i++)
            {
                noList[i].No = i + 1;
            }

            return users;
        }

    }
}