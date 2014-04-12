using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class PunishMemberDAO
    {
        public List<PunishMember> GetAllPunishingMember()
        {
            FPBDataContext db = new FPBDataContext();
            return db.PunishMembers.Where(p => p.ExpiredDate > DateTime.Now).OrderBy(p => p.Date).ToList();
        }


        public PunishMember GetPunishMemberById(int id)
        {
            FPBDataContext db = new FPBDataContext();
            return db.PunishMembers.Where(p => p.Id == id).FirstOrDefault();
        }


        public List<PunishMember> GetPunishsByUserId(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.PunishMembers.Where(p => p.UserId == userId).ToList();
        }


        public PunishMember GetPunishMemberByUserId(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.PunishMembers.Where(p => p.UserId == userId).OrderByDescending(p => p.IsForever).ThenByDescending(p => p.ExpiredDate).FirstOrDefault();
        }


        public PunishMember GetPunishMemberByUserName(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.PunishMembers.Where(p => p.User.UserName == userName).OrderByDescending(p => p.IsForever).ThenByDescending(p => p.ExpiredDate).FirstOrDefault();
        }
    }
}