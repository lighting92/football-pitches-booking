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
            return db.PunishMembers.Where(p => p.ExpiredDate > DateTime.Now || p.IsForever != null && p.IsForever.Value).OrderBy(p => p.Date).ToList();
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

        public int PunishMember(PunishMember pm)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.PunishMembers.InsertOnSubmit(pm);
                db.SubmitChanges();
                return pm.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int CancelPunishMembers(int[] ids)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var lpm = db.PunishMembers.Where(p => ids.Contains(p.Id)).ToList();
                db.PunishMembers.DeleteAllOnSubmit(lpm);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdatePunishMember(PunishMember pm)
        {
            FPBDataContext db = new FPBDataContext();
            var oldpm = db.PunishMembers.Where(p => p.Id == pm.Id).FirstOrDefault();
            try
            {
                oldpm.ExpiredDate = pm.ExpiredDate;
                oldpm.IsForever = pm.IsForever;
                oldpm.Reason = pm.Reason;
                db.SubmitChanges();
                return oldpm.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}