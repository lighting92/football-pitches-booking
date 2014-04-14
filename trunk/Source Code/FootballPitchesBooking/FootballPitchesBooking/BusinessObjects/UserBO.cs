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
        UserDAO userDAO = new UserDAO(); // day dung ko, uh


        public List<User> GetAllUsers()
        {
            return userDAO.GetAllUsers();
        }


        public List<User> GetUsers(string keyword)
        {
            return userDAO.GetUsers(keyword);
        }


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
                RoleDAO roleDAO = new RoleDAO();
                Role r = roleDAO.GetRoleByRoleName("Member");
                newUser.IsActive = false;
                newUser.RoleId = r.Id;
                results.Add(userDAO.CreateUser(newUser));
            }

            return results;
        }

        public int UpdateUserProfiles(User user)
        {
            UserDAO userDAO = new UserDAO();
            if (user.Password != userDAO.GetUserByUserId(user.Id).Password)
            {
                return -1;
            }
            return userDAO.UpdateUserProfiles(user);
        }

        public int ChangePassword(User user)
        {
            UserDAO userDAO = new UserDAO();
            return userDAO.ChangePassword(user);
        }

        public int UpdateUser(User user)
        {
            UserDAO userDAO = new UserDAO();
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = userDAO.GetUserByUserId(user.Id).Password;
            }
            return userDAO.UpdateUser(user);
        }

        /// <summary>
        /// Validate for login information of user.
        /// Return 1 for valid info, -1 for user not available, -2 for inactive user, -3 for incorrect password.
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
                else if (!user.IsActive)
                {
                    return -2;
                }
                else
                {
                    return -3;
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


        public User GetUserByEmail(string email)
        {
            UserDAO userDAO = new UserDAO();
            return userDAO.GetUserByEmail(email);
        }


        //View account profile
        public User GetUserById(int userId)
        {
            UserDAO userDAO = new UserDAO();
            return userDAO.GetUserByUserId(userId);
        }


        //Get all role 
        public List<Role> GetAllRoles()
        {
            RoleDAO roleDAO = new RoleDAO();
            return roleDAO.GetAllRoles();
        }

        //Get role name
        public Role GetRoleName(int roleId)
        {
            RoleDAO roleDAO = new RoleDAO();
            return roleDAO.GetRoleNameById(roleId);
        }

        //Update role
        public int UpdateRole(int userID, int roleId)
        {
            RoleDAO roleDAO = new RoleDAO();
            return roleDAO.UpdateRole(roleId, roleId);
        } // thang nao viet vay @@ func thi phai viet hoa chu dau tien theo coding convention @@


        public int ActiveUser(int Id)
        {
            UserDAO userDAO = new UserDAO();
            return userDAO.ActiveUser(Id);
        }


        public string[] GetAllUserName()
        {
            UserDAO userDAO = new UserDAO();
            var userNames = userDAO.GetAllUsers().Select(u => u.UserName).ToArray();
            return userNames;
        }

        public List<User> CheckAndGetAllUserInListName(List<string> userNames)
        {
            UserDAO userDAO = new UserDAO();
            var listUsers = userDAO.GetUsersInListNames(userNames);
            if (listUsers.Count() != userNames.Count())
            {
                return null;
            }
            else
            {
                return listUsers;
            }
        }


        public List<PunishMember> GetAllPunishingMember()
        {
            PunishMemberDAO punishDAO = new PunishMemberDAO();
            return punishDAO.GetAllPunishingMember();
        }


        public PunishMember GetPunishMemberById(int id)
        {
            PunishMemberDAO punishDAO = new PunishMemberDAO();
            return punishDAO.GetPunishMemberById(id);
        }


        public List<PunishMember> GetPunishsByUserId(int userId)
        {
            PunishMemberDAO punishDAO = new PunishMemberDAO();
            return punishDAO.GetPunishsByUserId(userId);
        }


        public PunishMember GetPunishMemberByUserId(int userId)
        {
            PunishMemberDAO punishDAO = new PunishMemberDAO();
            return punishDAO.GetPunishMemberByUserId(userId);
        }


        public PunishMember GetPunishMemberByUserName(string userName)
        {
            PunishMemberDAO punishDAO = new PunishMemberDAO();
            return punishDAO.GetPunishMemberByUserName(userName);
        }
    }
}