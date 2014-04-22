using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class ReportUserDAO
    {
        public List<ReportUser> GetAllReport()
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.ToList();
        }

        public List<ReportUser> GetAllReportOfUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.Where(r => r.User.UserName.ToLower().Equals(userName.ToLower())).ToList();
        }

        public List<ReportUser> GetAllReportOfUser(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.Where(r => r.UserId == userId).ToList();
        }

        public List<ReportUser> GetAllReportOfReportedUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.Where(r => r.User1.UserName.ToLower().Equals(userName.ToLower())).ToList();
        }

        public List<ReportUser> GetAllReportOfReportedUser(int userId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.Where(r => r.ReportUserId == userId).ToList();
        }

        public ReportUser GetReportByReservationId(int resId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.ReportUsers.Where(r => r.Reference == resId).FirstOrDefault();
        }

        public int CreateReportUser(ReportUser report)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.ReportUsers.InsertOnSubmit(report);
                db.SubmitChanges();
                return report.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}