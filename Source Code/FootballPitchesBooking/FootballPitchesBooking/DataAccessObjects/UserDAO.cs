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

        public int UpdateUserRank(MemberRank rank)// dai vl @@ ko biet userDAO ma dung ham cua rankDAO thi sao ko =)) t con chua hieu cai doan m vua viet no la gi kia
        {
            MemberRank prevRank = db.MemberRanks.OrderByDescending(m => m.Point < rank.Point).FirstOrDefault();
            MemberRank nextRank = db.MemberRanks.OrderBy(m => m.Point > rank.Point).FirstOrDefault();
            db.Users.Where(u => u.Point < rank.Point && u.Point >= prevRank.Point).ToList().ForEach(u => u.RankId = prevRank.Id);

            try
            {
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

            db.Users.Where(u => u.Point >= rank.Point && u.Point < nextRank.Point).ToList().ForEach(u => u.RankId = rank.Id);

            try
            {
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            //List users bang cach lay user tu user point < point rank cho den user point >= point rank cua rank co point lon' nhat' trong cac point be' hon no'
            //vi du: rank có [0, 500, 1000]
            //insert vo rank 750, thi co phai cac rank be' hon rank 750 point la 0 voi 500 ko, lay'  cai lien` ke` 750 tuc la lat cai lon' nhat trong tat ca nhung cai be' hon 750
            //dai loai nhu la` tao tuong tac voi table memberRank o userDAO ma` dang' ra phai la tuong tac voi memberRank o rankDAO @@
            //db.MemberRanks.OrderByDescending(m => m.Point < point).FirstOrDefault()).Point
            //db.MemberRanks.OrderByDescending(m => m.Point < point) => sắp xếp các rank theo thứ tự giảm dần với điều kiện các rank có point < point truyền vào
            //sau khi sắp xếp thì đc 1 list giảm dần, vậy cái đầu tiên là cái lớn nhất, dùng hàm .FirstOrDefault() để lấy cái đầu tiên
            //=> cái đó là cái bé liền kề point truyền vào
            // mà cái đó là obj, .Point để lấy point thôi, vậy lấy đc ppoint bé liền kề rồi
            //ý nghĩa của cả đoạn đó là lấy các user có point nằm ở khoảng [point bé liền kề; point truyền vào), thế thôi @@
        }
    }
}