using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumModels;
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
        public QuickSearchResultModel GetAvailableFieldsOfStadium(int stadiumId, int fieldType, DateTime startDate, double startTime, double duration)
        {
            FieldDAO fieldDAO = new FieldDAO();
            var availableFields = fieldDAO.GetAvailableFieldsOfStadium(stadiumId, fieldType, startDate, startTime, duration);
            if (availableFields != null && availableFields.Count != 0)
            {
                    QuickSearchResultModel result = new QuickSearchResultModel();
                   
                    result.Fields = availableFields;

                    List<double> prices = new List<double>();

                    foreach (var f in result.Fields)
                    {
                        var price = CalculatePrice(f, startDate, startTime, duration);
                        prices.Add(price);
                    }

                    result.Prices = prices;

                    return result;
            }
            else
            {
                return null;
            }
        }

        public List<Stadium> GetAllStadiums()
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.GetAllStadiums();
        }


        public List<Stadium> GetStadiums(string name, string address)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            return stadiumDAO.GetStadiums(name, address);
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


        public List<Stadium> GetStadiumsByStaff(string staffName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();

            return stadiumDAO.GetStadiumsByStaff(staffName);
        }


        public Stadium GetAuthorizeStadium(int stadiumId, string userName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();
            UserDAO userDAO = new UserDAO();

            var user = userDAO.GetUserByUserName(userName);

            var stadium = stadiumDAO.GetStadiumById(stadiumId);

            if (stadium != null && stadium.MainOwner == user.Id)
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

        public List<Field> GetAllFields7And11(int stadiumId)
        {
            FieldDAO fieldDAO = new FieldDAO();
            var fields = fieldDAO.GetFieldsByStadiumId(stadiumId);
            var results = fields.Where(f => f.FieldType == 7 || f.FieldType == 11).ToList();
            return results;
        }

        public Field GetAuthorizeField(int fieldId, string userName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            FieldDAO fieldDAO = new FieldDAO();
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();
            UserDAO userDAO = new UserDAO();

            var user = userDAO.GetUserByUserName(userName);
            var field = fieldDAO.GetFieldById(fieldId);
            if (field == null)
            {
                return null;
            }
            var stadium = stadiumDAO.GetStadiumById(field.StadiumId);

            bool isAuthorized = false;

            if (stadium.MainOwner == user.Id)
            {
                isAuthorized = true;
            }
            else
            {
                StadiumStaff ss = ssDAO.GetStadiumStaffByUserAndStadium(user.Id, stadium.Id);
                if (ss != null && ss.IsOwner)
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized)
            {
                return field;
            }
            else
            {
                return null;
            }
        }

        public List<FieldPrice> GetAuthorizeStadiumFieldPrices(int stadiumId, string userName)
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
                FieldPriceDAO fpDAO = new FieldPriceDAO();
                var result = fpDAO.GetAllFieldPriceOfStadium(stadiumId);
                if (result == null)
                {
                    result = new List<FieldPrice>();
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public FieldPrice GetAuthorizeFieldPrice(int fieldPriceId, string userName)
        {
            StadiumDAO stadiumDAO = new StadiumDAO();
            StadiumStaffDAO ssDAO = new StadiumStaffDAO();
            UserDAO userDAO = new UserDAO();
            FieldPriceDAO fpDAO = new FieldPriceDAO();

            var fieldPrice = fpDAO.GetFieldPriceById(fieldPriceId);

            if (fieldPrice == null)
            {
                return null;
            }

            var user = userDAO.GetUserByUserName(userName);

            var stadium = stadiumDAO.GetStadiumById(fieldPrice.StadiumId);

            bool isAuthorized = false;

            if (stadium.MainOwner == user.Id)
            {
                isAuthorized = true;
            }
            else
            {
                StadiumStaff ss = ssDAO.GetStadiumStaffByUserAndStadium(user.Id, stadium.Id);
                if (ss != null && ss.IsOwner)
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized)
            {

                return fieldPrice;
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
            stadium.ExpiredDate = oldStadium.ExpiredDate;
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

        public List<FieldPrice> GetAllFieldPriceOfStadium(int stadiumId)
        {
            FieldPriceDAO fpDAO = new FieldPriceDAO();
            return fpDAO.GetAllFieldPriceOfStadium(stadiumId);
        }

        public List<PriceTable> GetAllPriceTablesOfFieldPrice(int fieldPriceId)
        {
            FieldPriceDAO fpDAO = new FieldPriceDAO();
            return fpDAO.GetAllPriceTableOfFieldPrice(fieldPriceId);
        }

        /// <summary>
        /// Return 0 for database exception, positive integer for id of new field price
        /// Error:
        /// -1: There are at least 2 super default price (time from is 0 and time to is 0). 
        /// -2: Do not have super default price. 
        /// -3: Except default price, there are at least one price have time from equal or less than time to. 
        /// -4: Conflict timeline in default price table. 
        /// -5: There are at least 2 monday default price (0 - 0)
        /// -6: Except monday price, there are at least one price have time from equal or greater than time to
        /// -7: Conflict timeline in monday price table.
        /// --- And so on for tuesday, wednesday...
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public List<int> CreateStadiumFieldPrice(FieldPrice fp)
        {
            var result = new List<int>();

            //default price            
            var superDefault = fp.PriceTables.Where(p => p.Day == 0 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (superDefault.Count() > 1)
            {
                result.Add(-1);
            }
            if (superDefault == null || superDefault.Count() == 0)
            {
                result.Add(-2);
            }
            var defaultPB = fp.PriceTables.Where(p => p.Day == 0).ToList();
            if (defaultPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-3);
            }
            if (superDefault != null)
            {
                foreach (var item in superDefault)
                {
                    defaultPB.Remove(item);
                }
            }
            foreach (var item in defaultPB)
            {
                if (defaultPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-4);
                    break;
                }
            }

            //monday price

            var mondayDefault = fp.PriceTables.Where(p => p.Day == 1 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (mondayDefault.Count() > 1)
            {
                result.Add(-5);
            }
            var mondayPB = fp.PriceTables.Where(p => p.Day == 1).ToList();
            if (mondayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-6);
            }
            if (mondayDefault != null)
            {
                foreach (var item in mondayDefault)
                {
                    mondayPB.Remove(item);
                }
            }
            foreach (var item in mondayPB)
            {
                if (mondayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-7);
                    break;
                }
            }

            //tuesday price

            var tuesdayDefault = fp.PriceTables.Where(p => p.Day == 2 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (tuesdayDefault.Count() > 1)
            {
                result.Add(-8);
            }
            var tuesdayPB = fp.PriceTables.Where(p => p.Day == 2).ToList();
            if (tuesdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-9);
            }
            if (tuesdayDefault != null)
            {
                foreach (var item in tuesdayDefault)
                {
                    tuesdayPB.Remove(item);
                }
            }
            foreach (var item in tuesdayPB)
            {
                if (tuesdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-10);
                    break;
                }
            }

            //wednesday price

            var wednesdayDefault = fp.PriceTables.Where(p => p.Day == 3 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (wednesdayDefault.Count() > 1)
            {
                result.Add(-11);
            }
            var wednesdayPB = fp.PriceTables.Where(p => p.Day == 3).ToList();
            if (wednesdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-12);
            }
            if (wednesdayDefault != null)
            {
                foreach (var item in wednesdayDefault)
                {
                    wednesdayPB.Remove(item);
                }
            }
            foreach (var item in wednesdayPB)
            {
                if (wednesdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-13);
                    break;
                }
            }

            //thurday price

            var thurdayDefault = fp.PriceTables.Where(p => p.Day == 4 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (thurdayDefault.Count() > 1)
            {
                result.Add(-14);
            }
            var thurdayPB = fp.PriceTables.Where(p => p.Day == 4).ToList();
            if (thurdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-15);
            }
            if (thurdayDefault != null)
            {
                foreach (var item in thurdayDefault)
                {
                    thurdayPB.Remove(item);
                }
            }
            foreach (var item in thurdayPB)
            {
                if (thurdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-16);
                    break;
                }
            }

            //friday price

            var fridayDefault = fp.PriceTables.Where(p => p.Day == 5 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (fridayDefault.Count() > 1)
            {
                result.Add(-17);
            }
            var fridayPB = fp.PriceTables.Where(p => p.Day == 5).ToList();
            if (fridayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-18);
            }
            if (fridayDefault != null)
            {
                foreach (var item in fridayDefault)
                {
                    fridayPB.Remove(item);
                }
            }
            foreach (var item in fridayPB)
            {
                if (fridayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-19);
                    break;
                }
            }

            //saturday price

            var saturadayDefault = fp.PriceTables.Where(p => p.Day == 6 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (saturadayDefault.Count() > 1)
            {
                result.Add(-20);
            }
            var saturdayPB = fp.PriceTables.Where(p => p.Day == 6).ToList();
            if (saturdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-21);
            }
            if (saturadayDefault != null)
            {
                foreach (var item in saturadayDefault)
                {
                    saturdayPB.Remove(item);
                }
            }
            foreach (var item in saturdayPB)
            {
                if (saturdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-22);
                    break;
                }
            }

            //sunday price

            var sundayDefault = fp.PriceTables.Where(p => p.Day == 7 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (sundayDefault.Count() > 1)
            {
                result.Add(-23);
            }
            var sundayPB = fp.PriceTables.Where(p => p.Day == 7).ToList();
            if (sundayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-24);
            }
            if (sundayDefault != null)
            {
                foreach (var item in sundayDefault)
                {
                    sundayPB.Remove(item);
                }
            }
            foreach (var item in sundayPB)
            {
                if (sundayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-25);
                    break;
                }
            }

            if (result.Count == 0)
            {
                FieldPriceDAO fpDAO = new FieldPriceDAO();
                result.Add(fpDAO.CreateFieldPrice(fp));
            }

            return result;
        }

        public List<int> UpdateStadiumFieldPrice(FieldPrice fp)
        {

            var result = new List<int>();

            //default price            
            var superDefault = fp.PriceTables.Where(p => p.Day == 0 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (superDefault.Count() > 1)
            {
                result.Add(-1);
            }
            if (superDefault == null || superDefault.Count() == 0)
            {
                result.Add(-2);
            }
            var defaultPB = fp.PriceTables.Where(p => p.Day == 0).ToList();
            if (defaultPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-3);
            }
            if (superDefault != null)
            {
                foreach (var item in superDefault)
                {
                    defaultPB.Remove(item);
                }
            }
            foreach (var item in defaultPB)
            {
                if (defaultPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-4);
                    break;
                }
            }

            //monday price

            var mondayDefault = fp.PriceTables.Where(p => p.Day == 1 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (mondayDefault.Count() > 1)
            {
                result.Add(-5);
            }
            var mondayPB = fp.PriceTables.Where(p => p.Day == 1).ToList();
            if (mondayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-6);
            }
            if (mondayDefault != null)
            {
                foreach (var item in mondayDefault)
                {
                    mondayPB.Remove(item);
                }
            }
            foreach (var item in mondayPB)
            {
                if (mondayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-7);
                    break;
                }
            }

            //tuesday price

            var tuesdayDefault = fp.PriceTables.Where(p => p.Day == 2 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (tuesdayDefault.Count() > 1)
            {
                result.Add(-8);
            }
            var tuesdayPB = fp.PriceTables.Where(p => p.Day == 2).ToList();
            if (tuesdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-9);
            }
            if (tuesdayDefault != null)
            {
                foreach (var item in tuesdayDefault)
                {
                    tuesdayPB.Remove(item);
                }
            }
            foreach (var item in tuesdayPB)
            {
                if (tuesdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-10);
                    break;
                }
            }

            //wednesday price

            var wednesdayDefault = fp.PriceTables.Where(p => p.Day == 3 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (wednesdayDefault.Count() > 1)
            {
                result.Add(-11);
            }
            var wednesdayPB = fp.PriceTables.Where(p => p.Day == 3).ToList();
            if (wednesdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-12);
            }
            if (wednesdayDefault != null)
            {
                foreach (var item in wednesdayDefault)
                {
                    wednesdayPB.Remove(item);
                }
            }
            foreach (var item in wednesdayPB)
            {
                if (wednesdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-13);
                    break;
                }
            }

            //thurday price

            var thurdayDefault = fp.PriceTables.Where(p => p.Day == 4 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (thurdayDefault.Count() > 1)
            {
                result.Add(-14);
            }
            var thurdayPB = fp.PriceTables.Where(p => p.Day == 4).ToList();
            if (thurdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-15);
            }
            if (thurdayDefault != null)
            {
                foreach (var item in thurdayDefault)
                {
                    thurdayPB.Remove(item);
                }
            }
            foreach (var item in thurdayPB)
            {
                if (thurdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-16);
                    break;
                }
            }

            //friday price

            var fridayDefault = fp.PriceTables.Where(p => p.Day == 5 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (fridayDefault.Count() > 1)
            {
                result.Add(-17);
            }
            var fridayPB = fp.PriceTables.Where(p => p.Day == 5).ToList();
            if (fridayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-18);
            }
            if (fridayDefault != null)
            {
                foreach (var item in fridayDefault)
                {
                    fridayPB.Remove(item);
                }
            }
            foreach (var item in fridayPB)
            {
                if (fridayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-19);
                    break;
                }
            }

            //saturday price

            var saturadayDefault = fp.PriceTables.Where(p => p.Day == 6 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (saturadayDefault.Count() > 1)
            {
                result.Add(-20);
            }
            var saturdayPB = fp.PriceTables.Where(p => p.Day == 6).ToList();
            if (saturdayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-21);
            }
            if (saturadayDefault != null)
            {
                foreach (var item in saturadayDefault)
                {
                    saturdayPB.Remove(item);
                }
            }
            foreach (var item in saturdayPB)
            {
                if (saturdayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-22);
                    break;
                }
            }

            //sunday price

            var sundayDefault = fp.PriceTables.Where(p => p.Day == 7 && p.TimeFrom == 0 && p.TimeTo == 0).ToList();
            if (sundayDefault.Count() > 1)
            {
                result.Add(-23);
            }
            var sundayPB = fp.PriceTables.Where(p => p.Day == 7).ToList();
            if (sundayPB.Any(p => p.TimeFrom >= p.TimeTo && p.TimeFrom != 0))
            {
                result.Add(-24);
            }
            if (sundayDefault != null)
            {
                foreach (var item in sundayDefault)
                {
                    sundayPB.Remove(item);
                }
            }
            foreach (var item in sundayPB)
            {
                if (sundayPB.Any(p => p != item
                    && ((p.TimeFrom > item.TimeFrom) && (p.TimeFrom < item.TimeTo))
                    || (p.TimeTo > item.TimeFrom) && (p.TimeTo < item.TimeTo)))
                {
                    result.Add(-25);
                    break;
                }
            }

            if (result.Count == 0)
            {
                FieldPriceDAO fpDAO = new FieldPriceDAO();
                result.Add(fpDAO.UpdateFieldPrice(fp));
            }

            return result;
        }

        public int CreateField(Field field)
        {
            FieldDAO fieldDAO = new FieldDAO();
            if (field.ParentField != null)
            {
                var fields = GetAllFields7And11(field.StadiumId);
                var parent = fields.Where(f => f.Id == field.ParentField.Value).FirstOrDefault();
                if (parent != null)
                {
                    var childrens = fieldDAO.GetAllChildrenOfField(parent.Id);
                    if (parent.FieldType == 11)
                    {
                        var direct5 = childrens.Where(c => c.ParentField == parent.Id && c.FieldType == 5).ToList();
                        var direct7 = childrens.Where(c => c.FieldType == 7).ToList();
                        if (field.FieldType == 5)
                        {
                            if (((direct5.Count() / 2.0) + direct7.Count()) == 4)
                            {
                                return -1;
                            }
                        }
                        else if (field.FieldType == 7)
                        {
                            if (((direct5.Count() / 2.0) + direct7.Count()) > 3)
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else if (parent.FieldType == 7)
                    {
                        if (field.FieldType == 5)
                        {
                            var direct5 = childrens.Where(c => c.FieldType == 5).ToList();
                            if (direct5.Count() == 2)
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -3;
                }
            }
            return fieldDAO.CreateField(field);
        }

        public int UpdateField(Field field)
        {
            FieldDAO fieldDAO = new FieldDAO();
            if (field.ParentField != null)
            {
                if (field.ParentField == field.Id)
                {
                    return -4;
                }
                var fields = GetAllFields7And11(field.StadiumId);
                var parent = fields.Where(f => f.Id == field.ParentField.Value).FirstOrDefault();
                if (parent != null)
                {
                    var childrens = fieldDAO.GetAllChildrenOfField(parent.Id);
                    var old = childrens.Where(c => c.Id == field.Id).FirstOrDefault();
                    if (old != null)
                    {
                        childrens.Remove(old);
                    }
                    if (parent.FieldType == 11)
                    {
                        var direct5 = childrens.Where(c => c.ParentField == parent.Id && c.FieldType == 5).ToList();
                        var direct7 = childrens.Where(c => c.FieldType == 7).ToList();
                        if (field.FieldType == 5)
                        {
                            if (((direct5.Count() / 2.0) + direct7.Count()) == 4)
                            {
                                return -1;
                            }
                        }
                        else if (field.FieldType == 7)
                        {
                            if (((direct5.Count() / 2.0) + direct7.Count()) > 3)
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else if (parent.FieldType == 7)
                    {
                        if (field.FieldType == 5)
                        {
                            var direct5 = childrens.Where(c => c.FieldType == 5).ToList();
                            if (direct5.Count() == 2)
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -3;
                }
            }
            return fieldDAO.UpdateField(field);
        }

        public List<QuickSearchResultModel> FindAvailableStadium(int fieldType, DateTime startDate, double startTime, double duration, string city, string district)
        {
            FieldDAO fieldDAO = new FieldDAO();
            var availableFields = fieldDAO.GetAllAvailableFields(fieldType, startDate, startTime, duration, city, district);
            if (availableFields != null && availableFields.Count != 0)
            {
                List<QuickSearchResultModel> results = new List<QuickSearchResultModel>();
                List<int> stadiumIds = availableFields.Select(f => f.StadiumId).Distinct().ToList();
                foreach (var item in stadiumIds)
                {
                    QuickSearchResultModel temp = new QuickSearchResultModel();

                    Stadium s = GetStadiumById(item);
                    temp.Stadium = s;

                    List<Field> fs = availableFields.Where(f => f.StadiumId == item).ToList();
                    temp.Fields = fs;

                    List<double> prices = new List<double>();
                    foreach (var f in fs)
                    {
                        var price = CalculatePrice(f, startDate, startTime, duration);
                        prices.Add(price);
                    }
                    temp.Prices = prices;

                    results.Add(temp);
                }
                return results;
            }
            else
            {
                return null;
            }
        }

        public double CalculatePrice(Field field, DateTime startDate, double startTime, double duration)
        {
            FieldPriceDAO fpDAO = new FieldPriceDAO();
            double price = 0;
            var priceTables = fpDAO.GetAllPriceTableOfFieldPrice(field.PriceId);
            int day = 0;
            switch (startDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    day = 5;
                    break;
                case DayOfWeek.Monday:
                    day = 1;
                    break;
                case DayOfWeek.Saturday:
                    day = 6;
                    break;
                case DayOfWeek.Sunday:
                    day = 7;
                    break;
                case DayOfWeek.Thursday:
                    day = 4;
                    break;
                case DayOfWeek.Tuesday:
                    day = 2;
                    break;
                case DayOfWeek.Wednesday:
                    day = 3;
                    break;
                default:
                    break;
            }
            var superDefault = priceTables.Where(p => p.Day == 0 && p.TimeFrom == 0 && p.TimeTo == 0).FirstOrDefault();
            var pricesDefault = priceTables.Where(p => p.Day == 0 && p.TimeFrom != p.TimeTo &&
                ((p.TimeFrom >= startTime && p.TimeFrom < startTime + duration) ||
                 (p.TimeTo != 0 && p.TimeTo > startTime && p.TimeTo <= startTime + duration) ||
                 (p.TimeTo == 0 && 24 > startTime && 24 <= startTime + duration) ||
                 (p.TimeFrom <= startTime && p.TimeTo >= startTime + duration)))
                 .OrderBy(p => p.TimeFrom).ToList();
            var dayDefault = priceTables.Where(p => p.Day == day && p.TimeFrom == 0 && p.TimeTo == 0).FirstOrDefault();
            priceTables.Remove(superDefault);
            if (dayDefault != null)
            {
                priceTables.Remove(dayDefault);
            }
            var dayPrices = priceTables.Where(p => p.Day == day &&
                ((p.TimeFrom >= startTime && p.TimeFrom < startTime + duration) ||
                 (p.TimeTo != 0 && p.TimeTo > startTime && p.TimeTo <= startTime + duration) ||
                 (p.TimeTo == 0 && 24 > startTime && 24 <= startTime + duration) ||
                 (p.TimeFrom <= startTime && p.TimeTo >= startTime + duration)))
                 .OrderBy(p => p.TimeFrom).ToList();
            var finalPrices = new List<PriceTable>();
            bool complete = false;
            if (dayPrices == null)
            {
                dayPrices = new List<PriceTable>();
            }
            foreach (var item in pricesDefault)
            {
                if (item.TimeTo == 0)
                {
                    item.TimeTo = 24;
                }
            }
            foreach (var item in dayPrices)
            {
                if (item.TimeTo == 0)
                {
                    item.TimeTo = 24;
                }
            }
            int count = 0;
            double goToTime = startTime;
            while (!complete)
            {
                if (count < dayPrices.Count())
                {
                    if (dayPrices[count].TimeFrom <= goToTime)
                    {
                        PriceTable temp = new PriceTable();
                        temp.TimeFrom = goToTime;
                        if (temp.TimeTo > startTime + duration)
                        {
                            temp.TimeTo = dayPrices[count].TimeTo;                            
                        }
                        else
                        {
                            temp.TimeTo = startTime + duration;
                        }
                        temp.Price = dayPrices[count].Price;
                        finalPrices.Add(temp);
                        goToTime = temp.TimeTo;
                        count++;
                    }
                    else
                    {
                        PriceTable temp = new PriceTable();
                        temp.TimeFrom = goToTime;

                        if (dayDefault != null)
                        {
                            temp.TimeTo = dayPrices[count].TimeFrom;
                            temp.Price = dayDefault.Price;
                        }
                        else if (pricesDefault != null && pricesDefault.Count() > 0)
                        {
                            var pd = pricesDefault.Where(p => p.TimeFrom <= goToTime && p.TimeTo > goToTime).FirstOrDefault();
                            if (pd != null)
                            {
                                if (pd.TimeTo < dayPrices[count].TimeFrom)
                                {
                                    temp.TimeTo = pd.TimeTo;
                                    temp.Price = pd.Price;
                                }
                                else
                                {
                                    temp.TimeTo = dayPrices[count].TimeFrom;
                                    temp.Price = pd.Price;
                                }
                            }
                            else
                            {
                                temp.Price = superDefault.Price;
                                PriceTable nextPrice = null;
                                var npd = pricesDefault.Where(p => p.TimeFrom > goToTime).OrderBy(p => p.TimeFrom).FirstOrDefault();
                                if (npd != null)
                                {
                                    if (npd.TimeFrom < dayPrices[count].TimeFrom)
                                    {
                                        nextPrice = npd;
                                    }
                                    else
                                    {
                                        nextPrice = dayPrices[count];
                                    }
                                }
                                else
                                {
                                    nextPrice = dayPrices[count];
                                }
                                temp.TimeTo = nextPrice.TimeFrom;
                            }
                        }
                        else
                        {
                            temp.TimeTo = dayPrices[count].TimeFrom;
                            temp.Price = superDefault.Price;
                        }
                        goToTime = temp.TimeTo;
                        finalPrices.Add(temp);
                    }
                }
                else
                {
                    PriceTable temp = new PriceTable();
                    temp.TimeFrom = goToTime;

                    if (dayDefault != null)
                    {
                        if (startTime + duration > 24)
                        {
                            temp.TimeTo = 24;
                        }
                        else
                        {
                            temp.TimeTo = startTime + duration;
                        }
                        temp.Price = dayDefault.Price;
                    }
                    else if (pricesDefault != null && pricesDefault.Count() > 0)
                    {
                        var pd = pricesDefault.Where(p => p.TimeFrom <= goToTime && p.TimeTo > goToTime).FirstOrDefault();
                        if (pd != null)
                        {
                            temp.TimeTo = pd.TimeTo;
                            temp.Price = pd.Price;
                        }
                        else
                        {
                            temp.Price = superDefault.Price;
                            var npd = pricesDefault.Where(p => p.TimeFrom > goToTime).OrderBy(p => p.TimeFrom).FirstOrDefault();
                            if (npd != null)
                            {
                                temp.TimeTo = npd.TimeFrom;
                            }
                            else
                            {
                                if (startTime + duration > 24)
                                {
                                    temp.TimeTo = 24;
                                }
                                else
                                {
                                    temp.TimeTo = startTime + duration;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (startTime + duration > 24)
                        {
                            temp.TimeTo = 24;
                        }
                        else
                        {
                            temp.TimeTo = startTime + duration;
                        }
                        temp.Price = superDefault.Price;
                    }
                    goToTime = temp.TimeTo;
                    finalPrices.Add(temp);
                }
                if (goToTime == 24 || goToTime == startTime + duration)
                {
                    complete = true;
                }
            }
            foreach (var item in finalPrices)
            {
                price += (item.Price * (item.TimeTo - item.TimeFrom));
            }
            if (goToTime < startTime + duration)
            {
                price += CalculatePrice(field, startDate.AddDays(1).Date, 0, startTime + duration - goToTime);
            }
            return price;
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

        public Promotion GetPromotionByField(int fieldId, DateTime date)
        {
            PromotionDAO promotionDAO = new PromotionDAO();
            return promotionDAO.GetPromotionByField(fieldId, date);
        }

        public List<Field> GetFieldsByStadiumId(int stadiumId)
        {
            FieldDAO fieldDAO = new FieldDAO();

            return fieldDAO.GetFieldsByStadiumId(stadiumId);
        }

        public Field GetFieldById(int fieldId)
        {
            FieldDAO fieldDAO = new FieldDAO();
            return fieldDAO.GetFieldById(fieldId);
        }

        public bool UpdateStadiumRating(StadiumRating rating)
        {
            StadiumRatingDAO ratingDAO = new StadiumRatingDAO();
            return ratingDAO.UpdateStadiumRating(rating);
        }


        public List<StadiumReview> GetAllReviews()
        {
            StadiumReviewDAO reviewDAO = new StadiumReviewDAO();
            return reviewDAO.GetAllReviews();
        }

        public StadiumReview GetReviewById(int reviewId)
        {
            StadiumReviewDAO reviewDAO = new StadiumReviewDAO();
            return reviewDAO.GetReviewById(reviewId);
        }

        public bool CreateStadiumReview(StadiumReview review)
        {
            StadiumReviewDAO reviewDAO = new StadiumReviewDAO();
            return reviewDAO.CreateStadiumReview(review);
        }

        public bool ChangeReviewStatus(int reviewId, bool isApproved, int approver)
        {
            StadiumReviewDAO reviewDAO = new StadiumReviewDAO();
            return reviewDAO.ChangeReviewStatus(reviewId, isApproved, approver);
        }

        public bool DeleteReview(int reviewId)
        {
            StadiumReviewDAO reviewDAO = new StadiumReviewDAO();
            return reviewDAO.DeleteReview(reviewId);
        }
    }
}