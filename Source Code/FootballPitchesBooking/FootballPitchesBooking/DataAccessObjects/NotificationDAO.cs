using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class NotificationDAO
    {
        public List<Notification> GetAllNotificationsOfUser(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Notifications.Where(n => n.User.UserName.ToLower().Equals(userName.ToLower())).ToList();
        }

        public List<Notification> GetCountOfUnreadNotifications(string userName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Notifications.Where(n => n.User.UserName.ToLower().Equals(userName.ToLower()) && n.Status.ToLower().Equals("unread")).ToList();
        }

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

        public int MarkAsUnread(List<int> ids)
        {
            FPBDataContext db = new FPBDataContext();
            var nots = db.Notifications.Where(n => ids.Contains(n.Id)).ToList();
            try
            {
                foreach (var item in nots)
                {
                    item.Status = "unread";
                }
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int MarkAsRead(List<int> ids)
        {
            FPBDataContext db = new FPBDataContext();
            var nots = db.Notifications.Where(n => ids.Contains(n.Id)).ToList();
            try
            {
                foreach (var item in nots)
                {
                    item.Status = "read";
                }
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateMesssagesStatus(List<int> ids, List<bool> values)
        {
            FPBDataContext db = new FPBDataContext();
            var nots = db.Notifications.Where(n => ids.Contains(n.Id)).ToList();
            if (nots != null && nots.Count() != 0)
            {
                try
                {
                    foreach (var item in nots)
                    {
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            if (ids[i] == item.Id)
                            {
                                item.Status = values[i] ? "read" : "unread";
                                break;
                            }
                        }
                    }
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