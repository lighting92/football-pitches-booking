using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class NotificationDAO
    {
        public int CreateMessage(Notification message)
        {
            try
            {
                FPBDataContext db = new FPBDataContext();
                db.Notifications.InsertOnSubmit(message);
                db.SubmitChanges();
                return message.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteMessage(int msgId)
        {
            try
            {
                FPBDataContext db = new FPBDataContext();
                var delete = db.Notifications.Where(n => n.Id == msgId).FirstOrDefault();
                if (delete != null)
                {
                    db.Notifications.DeleteOnSubmit(delete);
                    db.SubmitChanges();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteMessages(List<int> msgIds)
        {
            try
            {
                FPBDataContext db = new FPBDataContext();
                var msgs = db.Notifications.Where(n => msgIds.Contains(n.Id)).ToList();
                db.Notifications.DeleteAllOnSubmit(msgs);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateMessageStatus(Notification msg)
        {
            FPBDataContext db = new FPBDataContext();
            var up = db.Notifications.Where(n => n.Id == msg.Id).FirstOrDefault();
            if (up != null)
            {
                try
                {
                    up.Status = msg.Status;
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