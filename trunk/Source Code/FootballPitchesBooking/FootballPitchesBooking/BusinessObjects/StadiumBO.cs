using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class StadiumBO
    {
        public List<Stadium> GetAllStadiums()
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.GetAllStadiums();
        }

        public List<int> CreateStadium(Stadium stadium, string owner)
        {
            List<int> results = new List<int>();
            StadiumDAO stadiumDAO = new StadiumDAO();
            UserDAO userDAO = new UserDAO();

            User stadiumOwner = userDAO.GetUserByUserName(owner);

            if (stadiumOwner == null)
            {
                results.Add(-1);
            }

            if (results.Count == 0)
            {
                stadium.IsActive = true;
                results.Add(stadiumDAO.CreateStadium(stadium));

                if (results.Count == 1 && results[0] > 0)
                {
                    StadiumStaff stadiumStaff = new StadiumStaff
                    {
                        UserId = stadiumOwner.Id,
                        StadiumId = results[0],
                        IsOwner = true
                    };
                    results.Add(CreateStadiumStaff(stadiumStaff));
                }
            }

            return results;
        }

        public int CreateStadiumStaff(StadiumStaff stadiumStaff)
        {
            StadiumStaffDAO stadiumStaffDAO = new StadiumStaffDAO();
            return stadiumStaffDAO.CreateStadiumStaff(stadiumStaff);
        }

        public int UpdateStadium(Stadium stadium)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.UpdateStadium(stadium);
        }

        public int ActivateStadium(int stadiumId)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.UpdateStadiumStatus(stadiumId, true);
        }

        public int DeactivateStadium(int stadiumId)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.UpdateStadiumStatus(stadiumId, false);
        }

        public int RegisterNewStadium(JoinSystemRequest jsr)
        {
            jsr.Status = "MỚI";
            jsr.CreateDate = DateTime.Now;

            JoinSystemRequestDAO jsrDAO = new JoinSystemRequestDAO();

            return jsrDAO.CreateJoinSystemRequest(jsr);
        }

        public List<JoinSystemRequest> GetAllJoinSystemRequest()
        {
            JoinSystemRequestDAO jsrDAO = new JoinSystemRequestDAO();
            return jsrDAO.GetAllJoinSystemRequest();
        }

        public int DeleteJoinSystemRequest(int jsrId)
        {
            JoinSystemRequestDAO jsrDAO = new JoinSystemRequestDAO();
            return jsrDAO.DeleteJoinSystemRequest(jsrId);
        }

        public JoinSystemRequest GetJoinSystemRequestById(int jsrId)
        {
            JoinSystemRequestDAO jsrDAO = new JoinSystemRequestDAO();
            return jsrDAO.GetJoinSystemRequestById(jsrId);
        }

        public int UpdateJoinSystemRequest(JoinSystemRequest jsr)
        {
            JoinSystemRequestDAO jsrDAO = new JoinSystemRequestDAO();
            return jsrDAO.UpdateJoinSystemRequestStatus(jsr);
        }
    }
}