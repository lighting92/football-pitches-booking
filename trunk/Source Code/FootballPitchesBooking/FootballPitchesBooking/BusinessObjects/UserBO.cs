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
        MemberRankDAO rankDAO = new MemberRankDAO();

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
                MemberRankDAO mrDAO = new MemberRankDAO();
                MemberRank mr = mrDAO.GetMemberRankByPoint(0);
                RoleDAO roleDAO = new RoleDAO();
                Role r = roleDAO.GetRoleByRoleName("Member");
                newUser.Point = 0;
                newUser.RankId = mr.Id;
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
            MemberRankDAO rankDAO = new MemberRankDAO();
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = userDAO.GetUserByUserId(user.Id).Password;
            }
            user.RankId = rankDAO.GetMemberRankByUserPoint(user.Point).Id;
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
                    if (user.IsActive)
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

        public List<MemberRank> GetAllRanks()
        {
            return rankDAO.GetAllRanks();
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

        public MemberRank GetRankById(int rankId)
        {
            return rankDAO.GetMemberRankById(rankId);
        }

        public List<int> CreateMemberRank(MemberRank rank)
        {
            List<int> results = new List<int>();
            MemberRank existedMemberRankName = rankDAO.GetMemberRankByName(rank.RankName);
            MemberRank existedMemberRankPoint = rankDAO.GetMemberRankByPoint((int)rank.Point);

            if (existedMemberRankName != null) //check xem co trung ten voi rank nao ko
            {
                results.Add(-1);
            }

            if (existedMemberRankPoint != null) //check xem co cung point voi rank nao ko
            {
                results.Add(-2);
            }

            if (results.Count == 0) //neu ko co loi~
            {
                results.Add(rankDAO.CreateMemberRank(rank));
            }

            if (results.Count == 1 && results[0] > 0)
            {
                results.Add(userDAO.UpdateUserRank(rank));
            }

            return results;
        }

        public List<int> UpdateMemberRank(MemberRank rank)
        {
            List<int> results = new List<int>();
            MemberRank existedMemberRankName = rankDAO.GetMemberRankByName(rank.RankName);
            MemberRank existedMemberRankPoint = rankDAO.GetMemberRankByPoint((int)rank.Point);

            if (existedMemberRankName != null && existedMemberRankName.Id != rank.Id) //check xem co trung ten voi rank nao ko
            {
                results.Add(-1);
            }

            if (existedMemberRankPoint != null && existedMemberRankPoint.Id != rank.Id) //check xem co cung point voi rank nao ko
            {
                results.Add(-2);
            }

            if (results.Count == 0) //neu ko co loi~
            {
                results.Add(rankDAO.UpdateMemberRank(rank));
            } // doan validate nay y chang create

            if (results.Count == 1 && results[0] > 0)
            {
                results.Add(userDAO.UpdateUserRank(rank));
                //hoàn thành việc update rank, nhưng còn có đk gì nữa ko? validate?
                //rank có 2 chỗ cần validate, là point với name có trùng với dữ liệu đã có trong db ko, để tránh lỗi logic
                //ví dụ: nếu có 2 rank cùng point thì user sẽ lấy rank nào? conflict
                //hoặc nếu 2 rank cùng tên thì ng ngoài nhìn vào sẽ ko thấy thay đổi gì thì sao? cũng conflict về lỗi logic
                //vậy nên check 2 cái này trc rồi mới update
                //chỗ này để update toàn bộ user dựa vào point đã thay đổi
                //cai nay dung den user nen phai khai bao userDAO vao
            }

            return results;
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

        public List<Notification> GetAllNotificationsOfUser(string userName)
        {
            NotificationDAO notDAO = new NotificationDAO();
            return notDAO.GetAllNotificationsOfUser(userName);
        }

        public int GetCountOfUnreadNotifications(string userName)
        {
            NotificationDAO notDAO = new NotificationDAO();
            var unread = notDAO.GetCountOfUnreadNotifications(userName);
            return unread.Count();
        }

        public int UpdateNotifications(string userName, List<int> ids, string action)
        {
            NotificationDAO notDAO = new NotificationDAO();
            var allNotifications = notDAO.GetAllNotificationsOfUser(userName);
            foreach (var item in ids)
            {
                if (allNotifications.Where(n => n.Id == item).FirstOrDefault() == null)
                {
                    return -2;
                }
            }
            if (action.Equals("read"))
            {
                return notDAO.MarkAsRead(ids);
            }
            else if (action.Equals("unread"))
            {
                return notDAO.MarkAsUnread(ids);
            }
            else
            {
                return notDAO.DeleteMessages(ids);
            }
        }
    }
}