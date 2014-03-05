﻿using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class JoinSystemRequestDAO
    {
        public int CreateJoinSystemRequest(JoinSystemRequest jsr)
        {
            FPBDataContext db = new FPBDataContext();
            db.JoinSystemRequests.InsertOnSubmit(jsr);
            try
            {
                db.SubmitChanges();
                return jsr.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteJoinSystemRequest(int jsrId)
        {
            FPBDataContext db = new FPBDataContext();
            JoinSystemRequest jsrToDelete = db.JoinSystemRequests.Where(j => j.Id == jsrId).FirstOrDefault();
            try
            {
                db.JoinSystemRequests.DeleteOnSubmit(jsrToDelete);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateJoinSystemRequestStatus(JoinSystemRequest uJsr)
        {
            FPBDataContext db = new FPBDataContext();
            JoinSystemRequest jsr = db.JoinSystemRequests.Where(j => j.Id == uJsr.Id).FirstOrDefault();

            if (jsr != null)
            {
                try
                {
                    jsr.Status = uJsr.Status;
                    jsr.FullName = uJsr.FullName;
                    jsr.Email = uJsr.Email;
                    jsr.Phone = uJsr.Phone;
                    jsr.Address = uJsr.Address;
                    jsr.StadiumName = uJsr.StadiumName;
                    jsr.StadiumAddress = uJsr.StadiumAddress;
                    jsr.Note = uJsr.Note;
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

        public JoinSystemRequest GetJoinSystemRequestById(int jsrId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.JoinSystemRequests.Where(j => j.Id == jsrId).FirstOrDefault();
        }

        public List<JoinSystemRequest> GetAllJoinSystemRequest()
        {
            FPBDataContext db = new FPBDataContext();
            return db.JoinSystemRequests.ToList();
        }
    }
}