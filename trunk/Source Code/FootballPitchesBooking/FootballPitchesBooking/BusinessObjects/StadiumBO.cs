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

        public Stadium GetStadiumById(int id)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.GetStadiumById(id);
        }

        public int CreateStadium(Stadium stadium, string owner, List<HttpPostedFileBase> images)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            UserDAO userDAO = new UserDAO();

            User stadiumOwner = userDAO.GetUserByUserName(owner);

            if (stadiumOwner == null)
            {
                return -1;
            }

            stadium.IsActive = true;

            int result = stadiumDAO.CreateStadium(stadium);

            List<string> imagesName = new List<string>();
            for (int i = 0; i < images.Count(); i++)
            {
                var image = images[i];
                var fileName = DateTime.Now.Ticks;
                int lastDot = image.FileName.LastIndexOf('.');
                var fileType = image.FileName.Substring(lastDot + 1);
                var fullFileName = i + "_" + fileName + "." + fileType;
                imagesName.Add(fullFileName);
            }
            if (result > 0)
            {
                StadiumStaff stadiumStaff = new StadiumStaff
                {
                    UserId = stadiumOwner.Id,
                    StadiumId = result,
                    IsOwner = true
                };
                StadiumStaffDAO ssDAO = new StadiumStaffDAO();
                if (ssDAO.CreateStadiumStaff(stadiumStaff) > 0)
                {
                    return result;
                }
                else
                {
                    if (stadiumDAO.DeleteStadium(result) > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            else
            {
                return result;
            }
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