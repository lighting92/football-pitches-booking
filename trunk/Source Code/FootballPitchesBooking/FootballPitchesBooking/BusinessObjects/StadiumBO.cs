using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public int CreateStadium(Stadium stadium, string owner, List<HttpPostedFileBase> images, string serverPath)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            UserDAO userDAO = new UserDAO();

            User stadiumOwner = userDAO.GetUserByUserName(owner);


            if (stadiumOwner == null)
            {
                return -1;
            }

            stadium.MainOwner = stadiumOwner.Id;
            int result = stadiumDAO.CreateStadium(stadium);

            if (result > 0)
            {

                List<string> imagesName = new List<string>();
                List<StadiumImage> sis = new List<StadiumImage>();
                for (int i = 0; i < images.Count(); i++)
                {
                    var image = images[i];
                    var fileName = DateTime.Now.Ticks;
                    int lastDot = image.FileName.LastIndexOf('.');
                    var fileType = image.FileName.Substring(lastDot + 1);
                    var fullFileName = result + "_" + i + "_" + fileName + "." + fileType;
                    imagesName.Add(fullFileName);
                    StadiumImage si = new StadiumImage
                    {
                        StadiumId = result,
                        Path = fullFileName
                    };
                    sis.Add(si);
                }

                StadiumImageDAO siDAO = new StadiumImageDAO();

                if (siDAO.InsertListStadiumImages(sis) > 0)
                {
                    for (int i = 0; i < images.Count(); i++)
                    {
                        images[i].SaveAs(serverPath + imagesName[i]);
                    }
                }

                RoleDAO roleDAO = new RoleDAO();
                var role = roleDAO.GetRoleByRoleName("StadiumOwner");
                userDAO.UpdateUserRole(stadiumOwner.Id, role.Id);

                return result;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateStadium(Stadium stadium, string owner, List<HttpPostedFileBase> images, string serverPath)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            UserDAO userDAO = new UserDAO();

            User stadiumOwner = userDAO.GetUserByUserName(owner);

            if (stadiumOwner == null)
            {
                return -1;
            }
            var oldStadium = stadiumDAO.GetStadiumById(stadium.Id);
            bool changedOwner = !(oldStadium.MainOwner == stadiumOwner.Id);

            stadium.MainOwner = stadiumOwner.Id;
            int result = stadiumDAO.UpdateStadium(stadium);

            if (result > 0)
            {
                List<string> imagesName = new List<string>();
                List<StadiumImage> sis = new List<StadiumImage>();
                for (int i = 0; i < images.Count(); i++)
                {
                    var image = images[i];
                    var fileName = DateTime.Now.Ticks;
                    int lastDot = image.FileName.LastIndexOf('.');
                    var fileType = image.FileName.Substring(lastDot + 1);
                    var fullFileName = result + "_" + i + "_" + fileName + "." + fileType;
                    imagesName.Add(fullFileName);
                    StadiumImage si = new StadiumImage
                    {
                        StadiumId = result,
                        Path = fullFileName
                    };
                    sis.Add(si);
                }

                StadiumImageDAO siDAO = new StadiumImageDAO();

                if (siDAO.InsertListStadiumImages(sis) > 0)
                {
                    for (int i = 0; i < images.Count(); i++)
                    {
                        images[i].SaveAs(serverPath + imagesName[i]);
                    }
                }

                if (changedOwner)
                {
                    RoleDAO roleDAO = new RoleDAO();
                    var soRole = roleDAO.GetRoleByRoleName("StadiumOwner");
                    userDAO.UpdateUserRole(stadiumOwner.Id, soRole.Id);
                    var memberRole = roleDAO.GetRoleByRoleName("Member");
                    userDAO.UpdateUserRole(oldStadium.MainOwner, memberRole.Id);
                }
            }

            return result;
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

        public int DeleteStadiumImage(int imgId, string serverPath)
        {
            StadiumImageDAO siDAO = new StadiumImageDAO();

            string result = siDAO.DeleteStadiumImage(imgId);

            if (result.Equals("0"))
            {
                return 0;
            }
            else
            {
                File.Delete(serverPath + result);
                return 1;
            }
        }

        public List<StadiumImage> GetAllImageOfStadium(int stadiumId)
        {
            StadiumImageDAO siDAO = new StadiumImageDAO();
            return siDAO.GetAllImageOfStadium(stadiumId);
        }
    }
}