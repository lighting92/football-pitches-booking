using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class RoleDAO
    {
        public Role GetRoleForUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Roles.Where(r => r.Id == r.Users.Where(u => u.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault().RoleId).FirstOrDefault();
        }
        public Role GetRoleByRoleName(String roleName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Roles.Where(r => r.Role1.ToLower().Equals(roleName.ToLower())).FirstOrDefault();
        }
        public List<Role> GetAllRoles()
        {
                FPBDataContext db = new FPBDataContext();
                return db.Roles.ToList();
        }
        public Role GetRoleNameById(int id)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Roles.Where(r => r.Id == id).FirstOrDefault();  
            
        }

        public int UpdateRole(int id, int roleId)
        {
            FPBDataContext db = new FPBDataContext();
            User user = db.Users.Where(u => u.Id == id).FirstOrDefault();  
            if (user != null)
            {
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
            else
            {
                return -1;
            }
        }
    }
}