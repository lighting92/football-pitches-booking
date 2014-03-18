using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumStaffModels;
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

        public List<Stadium> GetStadiumsOfOwner(string ownerName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();

            return stadiumDAO.GetStadiumsByOwnerName(ownerName);
        }

        public Stadium GetStadiumById(int id)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.GetStadiumById(id);
        }


        public Stadium GetStadiumByStaffAndId(string staffName, int stadiumId)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();

            return stadiumDAO.GetStadiumByStaffAndId(staffName, stadiumId);
        }


        public Stadium GetAuthorizeStadium(int stadiumId, string userName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();
            UserDAO userDAO = new UserDAO();

            var user = userDAO.GetUserByUserName(userName);

            var stadium = stadiumDAO.GetStadiumById(stadiumId);

            if (stadium.MainOwner == user.Id)
            {
                return stadium;
            }
            else
            {
                StadiumStaff ss = ssDAO.GetStadiumStaffByUserAndStadium(user.Id, stadiumId);
                if (ss != null && ss.IsOwner)
                {
                    return stadium;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Field> GetAuthorizeStadiumFields(int stadiumId, string userName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();
            UserDAO userDAO = new UserDAO();

            var user = userDAO.GetUserByUserName(userName);

            var stadium = stadiumDAO.GetStadiumById(stadiumId);

            bool isAuthorized = false;

            if (stadium.MainOwner == user.Id)
            {
                isAuthorized = true;
            }
            else
            {
                StadiumStaff ss = ssDAO.GetStadiumStaffByUserAndStadium(user.Id, stadiumId);
                if (ss != null && ss.IsOwner)
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized)
            {
                FieldDAO fieldDAO = new FieldDAO();
                var result = fieldDAO.GetFieldsByStadiumId(stadiumId);
                if (result == null)
                {
                    result = new List<Field>();
                }
                return result;
            }
            else
            {
                return null;
            }
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

        public int OwnerUpdateStadium(Stadium stadium, List<HttpPostedFileBase> images, string serverPath,
            List<string> changeStaffNames, List<string> changeStaffRoles, List<string> newStaffNames, List<string> newStaffoles)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();

            UserBO userBO = new UserBO();
            List<User> changeStaffUsers = null;
            if (changeStaffNames.Count > 0)
            {
                changeStaffUsers = userBO.CheckAndGetAllUserInListName(changeStaffNames.ToList());
            }
            List<User> newStaffUsers = null;
            if (newStaffNames.Count > 0)
            {
                newStaffUsers = userBO.CheckAndGetAllUserInListName(newStaffNames);
            }

            if ((newStaffUsers == null && newStaffNames.Count != 0) || (changeStaffUsers == null && changeStaffNames.Count != 0))
            {
                return -1;
            }
            if (newStaffUsers != null)
            {
                foreach (var item in newStaffNames.Select(u => u.Trim().ToLower()))
                {
                    if (changeStaffNames.Select(u => u.Trim().ToLower()).Contains(item))
                    {
                        return -2;
                    }
                }
            }

            var oldStadium = stadiumDAO.GetStadiumById(stadium.Id);

            int result = stadiumDAO.UpdateStadium(stadium, false);

            if (result > 0)
            {
                StadiumStaffDAO ssDAO = new StadiumStaffDAO();
                UserDAO userDAO = new UserDAO();
                RoleDAO roleDAO = new RoleDAO();
                var sOwnerRole = roleDAO.GetRoleByRoleName("StadiumOwner");
                var sStaffRole = roleDAO.GetRoleByRoleName("StadiumStaff");
                var memberRole = roleDAO.GetRoleByRoleName("Member");
                for (int i = 0; i < changeStaffNames.Count; i++)
                {
                    var user = changeStaffUsers.Where(u => u.UserName.ToLower().Equals(changeStaffNames[i].Trim().ToLower())).FirstOrDefault();

                    if (changeStaffRoles[i].Trim().ToLower().Equals("remove"))
                    {
                        ssDAO.DeleteStadiumStaffByUserAndStadium(user.Id, stadium.Id);
                        var listS = stadiumDAO.GetStadiumsByMainOwnerId(user.Id);
                        if (listS == null || listS.Count == 0)
                        {
                            var listss = ssDAO.GetStadiumStaffByUser(user.Id);
                            if (listss != null && listss.Count != 0)
                            {
                                var isOwner = listss.Any(s => s.IsOwner);
                                userDAO.UpdateUserRole(user.Id, isOwner ? sOwnerRole.Id : sStaffRole.Id);
                            }
                            else
                            {
                                userDAO.UpdateUserRole(user.Id, memberRole.Id);
                            }
                        }
                    }
                    else
                    {
                        var ss = ssDAO.GetStadiumStaffByUserAndStadium(user.Id, stadium.Id);
                        if (ss.IsOwner != changeStaffRoles[i].Trim().ToLower().Equals("owner"))
                        {
                            ss.IsOwner = !ss.IsOwner;
                        }
                        ssDAO.UpdateStadiumStaff(ss);
                        var listS = stadiumDAO.GetStadiumsByMainOwnerId(user.Id);
                        if (listS == null || listS.Count == 0)
                        {
                            var listss = ssDAO.GetStadiumStaffByUser(user.Id);
                            var isOwner = listss.Any(s => s.IsOwner);
                            userDAO.UpdateUserRole(user.Id, isOwner ? sOwnerRole.Id : sStaffRole.Id);
                        }
                    }
                }

                if (newStaffNames.Count() > 0)
                {
                    for (int i = 0; i < newStaffNames.Count(); i++)
                    {
                        var user = newStaffUsers.Where(u => u.UserName.ToLower().Equals(newStaffNames[i].Trim().ToLower())).FirstOrDefault();
                        StadiumStaff ss = new StadiumStaff();
                        ss.IsOwner = newStaffoles[i].Trim().ToLower().Equals("owner");
                        ss.UserId = user.Id;
                        ss.StadiumId = stadium.Id;
                        ssDAO.CreateStadiumStaff(ss);
                        var listss = ssDAO.GetStadiumStaffByUser(user.Id);
                        var isOwner = listss.Any(s => s.IsOwner);
                        userDAO.UpdateUserRole(user.Id, isOwner ? sOwnerRole.Id : sStaffRole.Id);
                    }
                }

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

        public List<StadiumStaffModel> GetAllStaffOfStadium(int stadiumId)
        {
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();

            var staffs = ssDAO.GetStaffByStadiumId(stadiumId);

            var mainOwner = ssDAO.GetMainOwnerOfStadium(stadiumId);

            List<StadiumStaffModel> result = new List<StadiumStaffModel>();

            result.Add(new StadiumStaffModel { UserId = mainOwner.Id, UserName = mainOwner.UserName, Role = "Main Owner" });

            if (staffs != null)
            {

                UserDAO userDAO = new UserDAO();

                foreach (var item in staffs)
                {
                    var user = userDAO.GetUserByUserId(item.UserId);
                    result.Add(new StadiumStaffModel { UserId = item.UserId, UserName = user.UserName, Role = item.IsOwner ? "Owner" : "Staff" });
                }

            }
            return result;
        }


        public List<Promotion> GetAllPromotions()
        {
            PromotionDAO promotionDAO = new PromotionDAO();
            return promotionDAO.GetAllPromotions();
        }


        public List<Promotion> GetAllPromotionsByStadium(int stadiumId)
        {
            PromotionDAO promotionDAO = new PromotionDAO();
            return promotionDAO.GetAllPromotionsOfStadium(stadiumId);
        }


        public Promotion GetPromotionById(int promotionId)
        {
            PromotionDAO promotionDAO = new PromotionDAO();
            return promotionDAO.GetPromotionById(promotionId);
        }


        public int CreatePromotion(Promotion promotion)
        {
            if (promotion.PromotionTo < DateTime.Now)
            {
                return -1;
            }

            if (promotion.PromotionTo < promotion.PromotionFrom)
            {
                return -2;
            }

            PromotionDAO promotionDAO = new PromotionDAO();
            int result = promotionDAO.CreatePromotion(promotion);

            return result;
        }

        public int UpdatePromotion(Promotion promotion)
        {
            if (promotion.PromotionTo <= DateTime.Now)
            {
                return -1;
            }

            if (promotion.PromotionTo <= promotion.PromotionFrom)
            {
                return -2;
            }

            PromotionDAO promotionDAO = new PromotionDAO();
            int result = promotionDAO.UpdatePromotion(promotion);

            return result;
        }


        public int DeletePromotion(int promotionId)
        {
            PromotionDAO promotionDAO = new PromotionDAO();
            int result = promotionDAO.DeletePromotion(promotionId);

            if (result > 0)
            {
                ReservationDAO reservationDAO = new ReservationDAO();
                if (reservationDAO.UpdateReservationByPromotionChanged(promotionId) > 0)
                {
                    return result;
                }                
            }
            return 0;
        }


        public List<Field> GetFieldsByStadiumId(int stadiumId)
        {
            FieldDAO fieldDAO = new FieldDAO();

            return fieldDAO.GetFieldsByStadiumId(stadiumId);
        }
    }
}