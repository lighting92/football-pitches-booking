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
    }
}