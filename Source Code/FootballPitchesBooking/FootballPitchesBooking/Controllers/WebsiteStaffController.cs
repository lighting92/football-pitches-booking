using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.RankModels;
using FootballPitchesBooking.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using FootballPitchesBooking.Models.WebsiteStaffModels;
using System.Text.RegularExpressions;

namespace FootballPitchesBooking.Controllers
{
    [Authorize(Roles = "WebsiteStaff, WebsiteMaster")]
    public class WebsiteStaffController : Controller
    {
        UserBO userBO = new UserBO();

        //
        // GET: /WebsiteStaff/

        public ActionResult Index()
        {
            return View();
        }


        #region STADIUM MANAGEMENT

        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult Stadiums()
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<Stadium> stadiums = stadiumBO.GetAllStadiums();
            return View(stadiums);
        }

        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult AddStadium()
        {
            EditStadiumModel model = new EditStadiumModel();
            model.OpenTime = "0:0";
            model.CloseTime = "0:0";
            return View(model);
        }

        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public ActionResult AddStadium(FormCollection form)
        {
            EditStadiumModel model = new EditStadiumModel();
            model.Name = form["Name"];
            model.MainOwner = form["MainOwner"];
            model.IsActive = bool.Parse(form["IsActive"]);
            model.Phone = form["Phone"];
            model.Email = form["Email"];
            model.Street = form["Street"];
            model.Ward = form["Ward"];
            model.District = form["District"];
            model.ErrorMessage = new List<string>();
            model.OpenTime = form["OpenTime"];
            model.CloseTime = form["CloseTime"];
            model.ExpiredDate = form["ExpiredDate"];

            double openTime = 0;
            double closeTime = 0;

            int openHour;
            int openMinute;
            int closeHour;
            int closeMinute;

            bool parseOH = int.TryParse(model.OpenTime.Substring(0, model.OpenTime.LastIndexOf(":")).Trim(), out openHour);
            bool parseOM = int.TryParse(model.OpenTime.Substring(model.OpenTime.LastIndexOf(":") + 1, model.OpenTime.Length - model.OpenTime.LastIndexOf(":") - 1).Trim(), out openMinute);
            bool parseCH = int.TryParse(model.CloseTime.Substring(0, model.CloseTime.LastIndexOf(":")).Trim(), out closeHour);
            bool parseCM = int.TryParse(model.CloseTime.Substring(model.CloseTime.LastIndexOf(":") + 1, model.CloseTime.Length - model.CloseTime.LastIndexOf(":") - 1).Trim(), out closeMinute);

            DateTime expiredDate;
            bool parseED = DateTime.TryParse(model.ExpiredDate, out expiredDate);

            List<String> images = new List<string>();
            List<HttpPostedFileBase> listFiles = new List<HttpPostedFileBase>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] != null)
                    {
                        var file = Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            listFiles.Add(file);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.MainOwner) || string.IsNullOrEmpty(model.Phone) ||
                string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Street) || string.IsNullOrEmpty(model.Ward) ||
                string.IsNullOrEmpty(model.District))
            {
                model.ErrorMessage.Add(Resources.Form_EmptyFields);
            }

            if (parseOH && parseOM && parseCH && parseCM && parseED)
            {
                openTime = openHour + (openMinute / 60.0);
                closeTime = closeHour + (closeMinute / 60.0);
            }
            else
            {
                model.ErrorMessage.Add("Bạn hãy dùng mẫu bên dưới để nhập thông tin cho sân mới");
            }

            foreach (var item in listFiles)
            {
                if (!item.ContentType.Contains("image"))
                {
                    model.ErrorMessage.Add(Resources.Upload_NotImage);
                    break;
                }
            }

            if (model.ErrorMessage.Count == 0)
            {
                Stadium stadium = new Stadium
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Ward = model.Ward,
                    District = model.District,
                    IsActive = model.IsActive,
                    OpenTime = openTime,
                    CloseTime = closeTime,
                    ExpiredDate = expiredDate

                };

                StadiumBO stadiumBO = new StadiumBO();
                string serverPath = Server.MapPath("~/Content/images/");
                int result = stadiumBO.CreateStadium(stadium, model.MainOwner, listFiles, serverPath);
                if (result == 0)
                {
                    model.ErrorMessage.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessage.Add(Resources.Webstaff_MainOwnerNotFound);
                }
                else
                {
                    return Redirect("/WebsiteStaff/EditStadium?id=" + result);
                }


                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult EditStadium(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium stadium = stadiumBO.GetStadiumById(id);

            List<string> listImages = new List<string>();
            List<string> imageIds = new List<string>();

            foreach (var img in stadium.StadiumImages)
            {
                listImages.Add(img.Path);
                imageIds.Add(img.Id.ToString());

            }

            string openTime = (int)stadium.OpenTime + ":" + ((stadium.OpenTime - (int)stadium.OpenTime) * 60);
            string closeTime = (int)stadium.CloseTime + ":" + ((stadium.CloseTime - (int)stadium.CloseTime) * 60);

            EditStadiumModel model = new EditStadiumModel
            {
                Name = stadium.Name,
                Street = stadium.Street,
                Ward = stadium.Ward,
                District = stadium.District,
                Phone = stadium.Phone,
                Email = stadium.Email,
                IsActive = stadium.IsActive,
                MainOwner = stadium.User.UserName,
                Images = listImages,
                ImageIds = imageIds,
                OpenTime = openTime,
                CloseTime = closeTime,
                ExpiredDate = stadium.ExpiredDate.ToShortDateString()
            };
            return View(model);
        }

        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public ActionResult EditStadium(FormCollection form, int id)
        {
            StadiumBO stadiumBO = new StadiumBO();

            EditStadiumModel model = new EditStadiumModel();
            model.Name = form["Name"];
            model.MainOwner = form["MainOwner"];
            model.IsActive = bool.Parse(form["IsActive"]);
            model.Phone = form["Phone"];
            model.Email = form["Email"];
            model.Street = form["Street"];
            model.Ward = form["Ward"];
            model.District = form["District"];
            model.ErrorMessage = new List<string>();
            model.OpenTime = form["OpenTime"];
            model.CloseTime = form["CloseTime"];
            model.ExpiredDate = form["ExpiredDate"];

            double openTime = 0;
            double closeTime = 0;

            int openHour;
            int openMinute;
            int closeHour;
            int closeMinute;

            bool parseOH = int.TryParse(model.OpenTime.Substring(0, model.OpenTime.LastIndexOf(":")).Trim(), out openHour);
            bool parseOM = int.TryParse(model.OpenTime.Substring(model.OpenTime.LastIndexOf(":") + 1, model.OpenTime.Length - model.OpenTime.LastIndexOf(":") - 1).Trim(), out openMinute);
            bool parseCH = int.TryParse(model.CloseTime.Substring(0, model.CloseTime.LastIndexOf(":")).Trim(), out closeHour);
            bool parseCM = int.TryParse(model.CloseTime.Substring(model.CloseTime.LastIndexOf(":") + 1, model.CloseTime.Length - model.CloseTime.LastIndexOf(":") - 1).Trim(), out closeMinute);

            DateTime expiredDate;
            bool parseED = DateTime.TryParse(model.ExpiredDate, out expiredDate);

            List<String> images = new List<string>();
            List<HttpPostedFileBase> listFiles = new List<HttpPostedFileBase>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] != null)
                    {
                        var file = Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            listFiles.Add(file);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.MainOwner) || string.IsNullOrEmpty(model.Phone) ||
                string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Street) || string.IsNullOrEmpty(model.Ward) ||
                string.IsNullOrEmpty(model.District))
            {
                model.ErrorMessage.Add(Resources.Form_EmptyFields);
            }

            foreach (var item in listFiles)
            {
                if (!item.ContentType.Contains("image"))
                {
                    model.ErrorMessage.Add(Resources.Upload_NotImage);
                    break;
                }
            }

            if (parseOH && parseOM && parseCH && parseCM && parseED)
            {
                openTime = openHour + (openMinute / 60.0);
                closeTime = closeHour + (closeMinute / 60.0);
            }
            else
            {
                model.ErrorMessage.Add("Bạn hãy dùng mẫu bên dưới để chỉnh sửa thông tin của sân");
            }

            if (model.ErrorMessage.Count == 0)
            {
                Stadium stadium = new Stadium
                {
                    Id = id,
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Ward = model.Ward,
                    District = model.District,
                    IsActive = model.IsActive,
                    OpenTime = openTime,
                    CloseTime = closeTime,
                    ExpiredDate = expiredDate
                };

                string serverPath = Server.MapPath("~/Content/images/");
                int result = stadiumBO.UpdateStadium(stadium, model.MainOwner, listFiles, serverPath);
                if (result == 0)
                {
                    model.ErrorMessage.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessage.Add(Resources.Webstaff_MainOwnerNotFound);
                }
                else
                {
                    model.SuccessMessage = Resources.Update_Success;
                }

            }

            List<string> listImages = new List<string>();
            List<string> imageIds = new List<string>();

            foreach (var img in stadiumBO.GetAllImageOfStadium(id))
            {
                listImages.Add(img.Path);
                imageIds.Add(img.Id.ToString());

            }

            model.Images = listImages;
            model.ImageIds = imageIds;

            return View(model);

        }


        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public JsonResult DeleteStadiumImage(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            string serverPath = Server.MapPath("~/Content/images/");
            int result = stadiumBO.DeleteStadiumImage(id, serverPath);
            if (result == 0)
            {
                return Json("ERROR::" + Resources.DB_Exception);
            }
            else
            {
                return Json("SUCCESS::" + Resources.Delete_Success);
            }
        }

        #endregion STADIUM MANAGEMENT

        #region USER MANAGEMENT
        //
        // GET: /WebsiteStaff/Users

        public ActionResult Users(int? page, string keyWord = "", string column = "", string sort = "")
        {
            try
            {
                // No. list.
                var noList = new List<NoModel>();

                // User list.
                List<User> users = userBO.ToList(ref noList, page, keyWord, column, sort);

                // Sort states.
                ViewBag.KeyWord = keyWord;
                ViewBag.Page = page;
                ViewBag.Column = column;
                ViewBag.Sort = sort;
                ViewBag.NoList = noList;

                // Return.
                var pageNumber = page ?? 1;
                var onePageOfUsers = users.ToPagedList(pageNumber, 10);
                ViewBag.onePageOfUsers = onePageOfUsers;
                return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_List")
                    : View();
            }
            catch (Exception)
            {
                // Wrtite to log file.
                return RedirectToAction("Users", "Error", new { Area = "" });
            }
        }

        public ActionResult EditUser(int? id)
        {
            User user = new User();
            try
            {
                user = userBO.GetUserById((int)id); //convert từ int? về int rồi mới gọi hàm
            }
            catch (Exception)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }
            if (user == null)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }
            UserModel model = new UserModel()
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Address = user.Address,
                RankName = user.MemberRank.RankName,
                Point = user.Point,
                IsActive = user.IsActive,
                RoleId = (int)user.RoleId,
                Roles = userBO.getAllRole(),
                ErrorMessages = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(FormCollection form, int id)
        {
            UserModel model = new UserModel();

            model.Password = form["Password"];
            model.ConfirmPassword = form["ConfirmPassword"];
            model.Email = form["Email"];
            model.ErrorMessages = new List<string>();

            try
            {
                model.Point = Int32.Parse(form["Point"]);
                model.IsActive = Boolean.Parse(form["IsActive"]);
                model.RoleId = Int32.Parse(form["RoleId"]);
            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }
            //cho nay sai business, neu ko cap nhat passworrd thi sao, anh phai de y trương hop nay
            //neu ko cap nhat password thi ko de password vao duoi entity user khi cap nhat
            //lam nhu vay khi nay a bam cap nhat ma password anh ko nhap thi no se cap nhat trong db password trong
            //a quen a da lam dieu do trong BO chua, neu lam roi thi e sr
            //um a lam roi nhung ma phai can than validate du lieu password email du thu, do dai, businesss khi cap nhat email nua, ko dc trung email

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                model.ErrorMessages.Add(Resources.Password_NotMatchWithConfirm);
            }

            if (model.ErrorMessages.Count == 0)
            {
                User user = new User()
                {
                    Id = id,
                    Password = model.Password,
                    Email = model.Email,
                    Point = model.Point,
                    IsActive = model.IsActive,
                    RoleId = model.RoleId
                };


                int result = userBO.UpdateUser(user);

                if (result > 0)
                {
                    return RedirectToAction("Promotions", "StadiumStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Promotion_TimeOver);
                }
                else if (result == -2)
                {
                    model.ErrorMessages.Add(Resources.Promotion_TimeFromOverTo);
                }
            }

            return View(model);
        }


        #endregion USER MANAGEMENT


        #region JOIN SYSTEM REQUEST MANAGEMENT

        //
        // GET: /WebsiteStaff/JoinRequests
        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult JoinRequests()
        {
            StadiumBO stadiumBO = new StadiumBO();
            return View(stadiumBO.GetAllJoinSystemRequest());
        }

        //
        // GET: /WebsiteStaff/DeleteJSR
        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult DeleteJSR(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            int result = stadiumBO.DeleteJoinSystemRequest(id);
            if (result == 0)
            {
                TempData["DeleteError"] = Resources.DB_Exception;
            }
            return RedirectToAction("JoinRequests");
        }

        //
        // GET: /WebsiteStaff/EditJSR
        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult EditJSR(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            JoinSystemRequest jsr = stadiumBO.GetJoinSystemRequestById(id);
            string[] stadiumAddress = jsr.StadiumAddress.Split(',');
            string street = stadiumAddress[0].Trim();
            string ward = stadiumAddress[1].Trim();
            string district = stadiumAddress[2].Trim();
            EditJSRModel ejm = new EditJSRModel
                {
                    UserName = jsr.User.UserName,
                    FullName = jsr.FullName,
                    Address = jsr.Phone,
                    Email = jsr.Email,
                    Phone = jsr.Phone,
                    StadiumStreet = street,
                    StadiumWard = ward,
                    StadiumDistrict = district,
                    StadiumName = jsr.StadiumName,
                    Status = jsr.Status,
                    Note = jsr.Note,
                    CreateDate = jsr.CreateDate.ToShortDateString()
                };
            return View(ejm);
        }

        //
        // POST: /WebsiteStaff/EditJSR
        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public ActionResult EditJSR(FormCollection form, int id)
        {
            EditJSRModel ejm = new EditJSRModel
            {
                UserName = form["UserName"],
                FullName = form["FullName"],
                Address = form["Address"],
                Phone = form["PhoneNumber"],
                Email = form["Email"],
                StadiumName = form["StadiumName"],
                StadiumStreet = form["StadiumStreet"],
                StadiumWard = form["StadiumWard"],
                StadiumDistrict = form["StadiumDistrict"],
                Status = form["Status"],
                Note = form["Note"],
                CreateDate = form["CreateDate"]
            };

            if (string.IsNullOrEmpty(ejm.FullName) || string.IsNullOrEmpty(ejm.Address) || string.IsNullOrEmpty(ejm.Phone) ||
                string.IsNullOrEmpty(ejm.Email) || string.IsNullOrEmpty(ejm.StadiumName) || string.IsNullOrEmpty(ejm.StadiumStreet) ||
                string.IsNullOrEmpty(ejm.StadiumWard) || string.IsNullOrEmpty(ejm.StadiumDistrict) || string.IsNullOrEmpty(ejm.Status))
            {
                ejm.ErrorMessage = Resources.Form_EmptyFields;
                return View(ejm);
            }
            else
            {
                UserBO userBO = new UserBO();
                User curUser = userBO.GetUserByUserName(User.Identity.Name);
                JoinSystemRequest jsr = new JoinSystemRequest
                {
                    FullName = ejm.FullName,
                    Address = ejm.Address,
                    Phone = ejm.Phone,
                    Email = ejm.Email,
                    StadiumName = ejm.StadiumName,
                    StadiumAddress = ejm.StadiumStreet + ", " + ejm.StadiumWard + ", " + ejm.StadiumDistrict,
                    Note = ejm.Note,
                    Id = id,
                    Status = ejm.Status
                };
                StadiumBO stadiumBO = new StadiumBO();
                int result = stadiumBO.UpdateJoinSystemRequest(jsr);

                if (result > 0)
                {
                    ejm.SuccessMessage = Resources.Update_Success;
                    return View(ejm);
                }
                else
                {
                    ejm.ErrorMessage = Resources.DB_Exception;
                    return View(ejm);
                }
            }
        }

        #endregion JOIN SYSTEM REQUEST MANAGEMENT

        #region MEMBER RANK MANAGEMENT

        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult MemberRanks(int? page, string keyWord = "", string column = "", string sort = "")
        {
            try
            {
                // No. list.
                var noList = new List<NoModel>();

                // User list.
                List<MemberRank> ranks = userBO.ToListMR(ref noList, page, keyWord, column, sort);

                // Sort states.
                ViewBag.KeyWord = keyWord;
                ViewBag.Page = page;
                ViewBag.Column = column;
                ViewBag.Sort = sort;
                ViewBag.NoList = noList;

                // Return.
                var pageNumber = page ?? 1;
                var onePageOfUsers = ranks.ToPagedList(pageNumber, 10);
                ViewBag.onePageOfUsers = onePageOfUsers;
                return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_List2")
                    : View();
            }
            catch (Exception)
            {
                // Wrtite to log file.
                return View("Error");
            }
        }

        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult AddMemberRank()
        {
            return View();
        }

        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public ActionResult AddMemberRank(FormCollection form)
        {
            RankModel rank = new RankModel();
            rank.RankName = form["RankName"];
            rank.Point = Int32.Parse(form["Point"]);
            rank.Promotion = form["Promotion"];
            rank.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(rank.RankName) || string.IsNullOrEmpty(rank.Promotion))
            {
                rank.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (rank.ErrorMessages.Count == 0)
            {
                UserBO userBO = new UserBO();

                MemberRank memberrank = new MemberRank
                {
                    RankName = rank.RankName,
                    Point = rank.Point,
                    Promotion = rank.Promotion,

                };

                List<int> results = userBO.CreateMemberRank(memberrank);

                if (results.Count == 1 && results[0] > 0)
                {
                    return RedirectToAction("Index", "Home"); //cai nay sau nay sua lai redirect den trang list rank hay gi day, khi nao add success thi no redirect, ko thi bao loi
                }
                else
                {
                    foreach (var error in results)
                    {
                        if (error == 0)
                        {
                            rank.ErrorMessages.Add(Resources.DB_Exception);
                        }
                        if (error == -1)
                        {
                            rank.ErrorMessages.Add(Resources.Rank_RankNameNotAvailable);
                        }
                        if (error == -2)
                        {
                            rank.ErrorMessages.Add(Resources.Rank_RankPointNotAvailable);
                        }
                    }
                }

            }
            return View(rank); //cai bao' loi~ mang tinh' tuong doi', chua biet requirement chinh xac sao nen chu check trc cho chac
        }

        //giao dien tinh' sau
        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult EditMemberRank(int? id) //truyen vao 1 id, int? id tuc la id nay co the null hoac kieu khac, vi du user co' tinh` gõ đường dẫn là EditMemberRank?id=abc thì sao, int? id để bắt các TH này
        {
            MemberRank memberRank = new MemberRank();
            memberRank = userBO.GetRankById((int)id); //convert từ int? về int rồi mới gọi hàm
            RankModel rank = new RankModel 
            {
                Id = memberRank.Id,
                RankName = memberRank.RankName,
                Point = (int)memberRank.Point,
                Promotion = memberRank.Promotion,
                ErrorMessages = new List<string>()
            };
            return View(rank); //trả về Rankmodel
        }

        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public ActionResult EditMemberRank(FormCollection form) //cái này là lấy form vào, các field của form tương tự ở dưới
        {
            RankModel rank = new RankModel();
            rank.Id = Int32.Parse(form["RankId"]);
            rank.RankName = form["RankName"];
            rank.Point = Int32.Parse(form["Point"]);
            rank.Promotion = form["Promotion"];
            rank.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(rank.RankName) || string.IsNullOrEmpty(rank.Promotion)) //check null
            {
                rank.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (rank.ErrorMessages.Count == 0) //nếu ko có lỗi thì gọi BO lên update
            {
                MemberRank memberrank = new MemberRank
                {
                    Id = rank.Id,
                    RankName = rank.RankName,
                    Point = rank.Point,
                    Promotion = rank.Promotion,

                };

                List<int> results = userBO.UpdateMemberRank(memberrank);

                if (results.Count == 2 && results[0] > 0 && results[1] > 0) //nếu update ko có lỗi thì redirect qua cái này
                {
                    return RedirectToAction("MemberRanks", "WebsiteStaff"); //cai nay sau nay sua lai redirect den trang list rank hay gi day, khi nao add success thi no redirect, ko thi bao loi
                }
                else //nếu update lỗi thì báo lỗi ra ngoài rồi kiu ng ta update lại
                {
                    foreach (var error in results)
                    {
                        if (error == 0)
                        {
                            rank.ErrorMessages.Add(Resources.DB_Exception);
                        }
                        if (error == -1)
                        {
                            rank.ErrorMessages.Add(Resources.Rank_RankNameNotAvailable);
                        }
                        if (error == -2)
                        {
                            rank.ErrorMessages.Add(Resources.Rank_RankPointNotAvailable);
                        }
                    }
                }

            }
            return View(rank); //cai bao' loi~ mang tinh' tuong doi', chua biet requirement chinh xac sao nen  check trc cho chac
        }


        #endregion MEMBER RANK MANAGEMENT
    }
}
