using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using FootballPitchesBooking.Models.WebsiteStaffModels;
using System.Text.RegularExpressions;
using System.Globalization;

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

            var ci = new CultureInfo("vi-VN");
            DateTime expiredDate;
            bool parseED = DateTime.TryParse(model.ExpiredDate, ci, DateTimeStyles.AssumeLocal, out expiredDate);

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

            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.MainOwner) || string.IsNullOrWhiteSpace(model.Phone) ||
                string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Street) || string.IsNullOrWhiteSpace(model.Ward) ||
                string.IsNullOrWhiteSpace(model.District))
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

            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.MainOwner) || string.IsNullOrWhiteSpace(model.Phone) ||
                string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Street) || string.IsNullOrWhiteSpace(model.Ward) ||
                string.IsNullOrWhiteSpace(model.District))
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

        public ActionResult Users()
        {
            List<User> users = new List<User>();
            string keyword = null;
            try
            {
                keyword = Request.QueryString["Search"];
            }
            catch (Exception)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }
            if (string.IsNullOrWhiteSpace(keyword))
            {
                users = userBO.GetAllUsers();
            }
            else
            {
                users = userBO.GetUsers(keyword);
            }
            return View(users);
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
                IsActive = user.IsActive,
                RoleId = (int)user.RoleId,
                Roles = userBO.GetAllRoles(),
                ErrorMessages = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(FormCollection form, int id)
        {
            UserModel model = new UserModel();
            Regex emailFormat = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            model.Password = form["Password"];
            model.ConfirmPassword = form["ConfirmPassword"];
            model.Email = form["Email"];
            model.ErrorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (model.Password.Length < 6 || model.Password.Length > 32)
                {
                    model.ErrorMessages.Add(Resources.Reg_PasswordNotInLenght);
                }
            }

            if (!emailFormat.IsMatch(model.Email))
            {
                model.ErrorMessages.Add(Resources.Reg_EmailWrongFormat);
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                model.ErrorMessages.Add(Resources.Password_NotMatchWithConfirm);
            }

            User usr = null;
            try
            {
                usr = userBO.GetUserById((int)id); //convert từ int? về int rồi mới gọi hàm
            }
            catch (Exception)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }
            if (usr == null)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }


            try
            {
                model.IsActive = Boolean.Parse(form["IsActive"]);
            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
                model.IsActive = usr.IsActive;
            }

            try
            {
                model.RoleId = Int32.Parse(form["RoleId"]);
            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
                model.RoleId = (int)usr.RoleId;
            }

            if (string.IsNullOrWhiteSpace(model.Email))
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
                    IsActive = model.IsActive,
                    RoleId = model.RoleId
                };


                int result = userBO.UpdateUser(user);

                if (result > 0)
                {
                    return RedirectToAction("Users", "WebsiteStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
            }

            model.UserName = usr.UserName;
            model.PhoneNumber = usr.PhoneNumber;
            model.FullName = usr.FullName;
            model.Address = usr.Address;
            model.Roles = userBO.GetAllRoles();

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

            if (string.IsNullOrWhiteSpace(ejm.FullName) || string.IsNullOrWhiteSpace(ejm.Address) || string.IsNullOrWhiteSpace(ejm.Phone) ||
                string.IsNullOrWhiteSpace(ejm.Email) || string.IsNullOrWhiteSpace(ejm.StadiumName) || string.IsNullOrWhiteSpace(ejm.StadiumStreet) ||
                string.IsNullOrWhiteSpace(ejm.StadiumWard) || string.IsNullOrWhiteSpace(ejm.StadiumDistrict) || string.IsNullOrWhiteSpace(ejm.Status))
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


        #region ADVERTISEMENT MANAGEMENT


        public ActionResult Advertisements(int? stadium)
        {
            WebsiteBO webBO = new WebsiteBO();
            List<Advertisement> adsList = webBO.GetAllAds();
            return View(adsList);
        }


        public ActionResult AddAdvertisement()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddAdvertisement(FormCollection form)
        {
            AdvertisementModel model = new AdvertisementModel();

            model.ErrorMessages = new List<string>();
            model.Position = form["Position"];
            model.AdvertiseDetail = form["AdvertiseDetail"];
            model.Status = form["Status"];

            bool checkParseError = false;

            try
            {
                model.ExpiredDate = DateTime.Parse(form["ExpiredDate"], new CultureInfo("vi-VN"));
            }
            catch (Exception)
            {
                checkParseError = true;
            }

            if (checkParseError || string.IsNullOrWhiteSpace(model.Position) || string.IsNullOrWhiteSpace(model.AdvertiseDetail) || string.IsNullOrWhiteSpace(model.Status))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                UserBO userBO = new UserBO();

                User creator = userBO.GetUserByUserName(User.Identity.Name);

                Advertisement ads = new Advertisement()
                {
                    Position = model.Position,
                    AdvertiseDetail = model.AdvertiseDetail,
                    CreateDate = DateTime.Now.Date,
                    ExpiredDate = model.ExpiredDate,
                    Status = model.Status,
                    Creator = creator.Id
                };

                WebsiteBO webBO = new WebsiteBO();

                int result = webBO.CreateAdvertisement(ads);

                if (result > 0)
                {
                    return RedirectToAction("Ads", "WebsiteStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Ads_ExpiredTimeOver);
                }
                else if (result == -2)
                {
                    model.ErrorMessages.Add(Resources.Ads_ExistingAdvertisement);
                }
            }

            return View(model);
        }


        public ActionResult EditAdvertisement(int? id)
        {
            WebsiteBO webBO = new WebsiteBO();

            try
            {
                Advertisement ads = webBO.GetAdvertisementById((int)id);

                if (ads == null)
                {
                    return RedirectToAction("Ads", "WebsiteStaff");
                }

                AdvertisementModel model = new AdvertisementModel()
                {
                    Position = ads.Position,
                    AdvertiseDetail = ads.AdvertiseDetail,
                    CreateDate = ads.CreateDate,
                    ExpiredDate = ads.ExpiredDate,
                    Status = ads.Status,
                    Creator = ads.User.UserName
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Ads", "WebsiteStaff");
            }
        }


        [HttpPost]
        public ActionResult EditAdvertisement(FormCollection form, int id)
        {
            AdvertisementModel model = new AdvertisementModel();

            model.ErrorMessages = new List<string>();
            model.Position = form["Position"];
            model.AdvertiseDetail = form["AdvertiseDetail"];
            model.Status = form["Status"];

            bool checkParseError = false;

            try
            {
                model.ExpiredDate = DateTime.Parse(form["ExpiredDate"], new CultureInfo("vi-VN"));
            }
            catch (Exception)
            {
                checkParseError = true;
            }

            if (checkParseError || string.IsNullOrWhiteSpace(model.Position) || string.IsNullOrWhiteSpace(model.AdvertiseDetail) || string.IsNullOrWhiteSpace(model.Status))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                Advertisement ads = new Advertisement()
                {
                    Id = id,
                    Position = model.Position,
                    AdvertiseDetail = model.AdvertiseDetail,
                    ExpiredDate = model.ExpiredDate,
                    Status = model.Status,
                };

                WebsiteBO webBO = new WebsiteBO();

                int result = webBO.UpdateAdvertisement(ads);

                if (result > 0)
                {
                    return RedirectToAction("Ads", "WebsiteStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Ads_ExpiredTimeOver);
                }
                else if (result == -2)
                {
                    model.ErrorMessages.Add(Resources.Ads_ExistingAdvertisement);
                }
            }

            return View(model);
        }

        #endregion ADVERTISEMENT MANAGEMENT


        #region PRIORITY MANAGEMENT
        [Authorize(Roles = "WebsiteMaster")]
        public ActionResult EditPriority()
        {
            RecommendationBO recBO = new RecommendationBO();
            return View(recBO.ViewPriority());
        }

        [Authorize(Roles = "WebsiteMaster")]
        [HttpPost]
        public JsonResult EditPriority(FormCollection form)
        {
            string json = form[0];
            json = json.Replace("{", "");
            json = json.Replace("}", "");
            json = json.Replace("\"", "");
            string[] list = json.Split(',');
            var configs = new List<Configuration>();
            foreach (var item in list)
            {
                string[] kv = item.Split(':');
                configs.Add(new Configuration
                {
                    Name = kv[0].Trim(),
                    Value = kv[1].Trim()
                });
            }

            bool valid = true;
            foreach (var item in configs)
            {
                double temp = 0;
                valid = double.TryParse(item.Value, out temp);
                if (!valid)
                {
                    break;
                }
            }
            string message = "";
            if (valid)
            {
                RecommendationBO recBO = new RecommendationBO();
                var result = recBO.UpdatePriorityConfig(configs);
                switch (result)
                {
                    case 1:
                        message = "Cập nhật thành công";
                        break;
                    case 0:
                        message = Resources.DB_Exception;
                        break;
                    case -1:
                        message = "Không tìm thấy tên cấu hình";
                        break;
                    case -2:
                        message = "Cập nhật không thành công";
                        break;
                    default:
                        break;
                }
            }
            return Json(message);
        }

        #endregion


        #region STADIUM REVIEW


        public ActionResult Reviews()
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<StadiumReview> reviews = stadiumBO.GetAllReviews();
            return View(reviews);
        }


        public ActionResult Review(int? id)
        {
            try
            {
                StadiumBO stadiumBO = new StadiumBO();
                StadiumReview review = stadiumBO.GetReviewById((int)id);
                return View(review);
            }
            catch (Exception)
            {
                return RedirectToAction("Reviews", "WebsiteStaff");
            }
        }


        public ActionResult ApproveReview(int? id)
        {
            try
            {
                StadiumBO stadiumBO = new StadiumBO();
                bool result = stadiumBO.ChangeReviewStatus((int)id, true, userBO.GetUserByUserName(User.Identity.Name).Id);
                return RedirectToAction("Reviews", "WebsiteStaff");
            }
            catch (Exception)
            {
                return RedirectToAction("Reviews", "WebsiteStaff");
            }
        }


        public ActionResult DeleteReview(int? id)
        {
            try
            {
                int user;
                try
                {
                    user = Int32.Parse(Request.QueryString["User"]);
                }
                catch (Exception)
                {
                    user = 0;
                }
                StadiumBO stadiumBO = new StadiumBO();
                bool result = stadiumBO.DeleteReview((int)id);
                if (user == 0)
                {
                    return RedirectToAction("Reviews", "WebsiteStaff");
                }
                else
                {
                    return RedirectToAction("Punish/" + user, "WebsiteStaff");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Reviews", "WebsiteStaff");
            }
        }


        #endregion STADIUM REVIEW

    }

}
