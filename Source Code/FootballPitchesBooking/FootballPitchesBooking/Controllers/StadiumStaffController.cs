using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumStaffModels;
using FootballPitchesBooking.Properties;
using FootballPitchesBooking.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    [Authorize(Roles = "StadiumStaff, StadiumOwner")]
    public class StadiumStaffController : Controller
    {
        //
        // GET: /StadiumStaff/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            StadiumBO stadiumBO = new StadiumBO();
            var model = stadiumBO.GetAllNotificationsOfStadiumsForUser(User.Identity.Name);
            model = model.OrderByDescending(n => n.Id).ToList();
            return View(model);
        }

        [Authorize]
        public ActionResult UpdateNotificationsStatus(FormCollection form)
        {
            var strIds = form["ids[]"];
            var action = form["action"];
            if (!string.IsNullOrEmpty(strIds) && !string.IsNullOrEmpty(action))
            {
                string[] sids = strIds.Split(',');
                List<int> ids = new List<int>();
                foreach (var item in sids)
                {
                    ids.Add(int.Parse(item));
                }
                action = action.Trim().ToLower();
                if (action.Equals("read") || action.Equals("unread") || action.Equals("delete"))
                {
                    StadiumBO stadiumBO = new StadiumBO();
                    var result = stadiumBO.UpdateNotifications(User.Identity.Name, ids, action);
                    if (result == -2)
                    {
                        return RedirectToAction("Index");
                    }
                    else if (result == 0)
                    {
                        TempData["Error"] = "Máy chủ đang bận, xin thử lại sau.";
                    }
                }
            }
            return RedirectToAction("Notifications");
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetCountOfUnreadNotifications()
        {
            StadiumBO stadiumBO = new StadiumBO();
            var count = stadiumBO.GetCountOfUnreadNotifications(User.Identity.Name);
            return Json(count);
        }


        #region STADIUMS MANAGEMENT


        [Authorize(Roles = "StadiumOwner")]
        public ActionResult Stadiums()
        {
            StadiumBO stadiumBO = new StadiumBO();
            var model = stadiumBO.GetStadiumsOfOwner(User.Identity.Name);
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult EditStadium(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium stadium = stadiumBO.GetAuthorizeStadium(id, User.Identity.Name);

            if (stadium != null)
            {

                List<string> listImages = new List<string>();
                List<string> imageIds = new List<string>();

                foreach (var img in stadium.StadiumImages)
                {
                    listImages.Add(img.Path);
                    imageIds.Add(img.Id.ToString());

                }

                var staffs = stadiumBO.GetAllStaffOfStadium(id);

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
                    Staffs = staffs,
                    OpenTime = openTime,
                    CloseTime = closeTime,
                    ExpiredDate = stadium.ExpiredDate.ToShortDateString()
                };

                model.IsMainOwner = staffs.Any(s => s.UserName.ToLower().Equals(User.Identity.Name.ToLower()) && s.Role.Equals("Main Owner"));
                return View(model);
            }
            else
            {
                var errMsg = new List<string>();
                errMsg.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
                EditStadiumModel model = new EditStadiumModel
                {
                    ErrorMessage = errMsg
                };
                return View(model);
            }
        }

        [Authorize(Roles = "StadiumOwner")]
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

            var curStaffs = form.AllKeys.Where(k => k.Contains("User_Name_")).ToList();
            var curRoles = form.AllKeys.Where(k => k.Contains("User_Role_")).ToList();
            List<string> changeStaffNames = new List<string>();
            List<string> changeStaffRoles = new List<string>();
            foreach (var item in curStaffs)
            {
                string count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                if (curRoles.Any(r => r.Equals("User_Role_" + count)))
                {
                    changeStaffNames.Add(form["User_Name_" + count]);
                    changeStaffRoles.Add(form["User_Role_" + count]);
                }
            }

            var newStaffs = form.AllKeys.Where(k => k.Contains("New_Staff_Name_")).ToList();
            var newRoles = form.AllKeys.Where(k => k.Contains("New_Staff_Role_")).ToList();
            List<string> newStaffNames = new List<string>();
            List<string> newStaffRoles = new List<string>();
            foreach (var item in newStaffs)
            {
                string count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                if (newRoles.Any(r => r.Equals("New_Staff_Role_" + count)))
                {
                    if (!form["New_Staff_Role_" + count].Trim().ToLower().Equals("remove"))
                    {
                        newStaffNames.Add(form["New_Staff_Name_" + count]);
                        newStaffRoles.Add(form["New_Staff_Role_" + count]);
                    }
                }
            }

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

            if (parseOH && parseOM && parseCH && parseCM)
            {
                openTime = openHour + (openMinute / 60.0);
                closeTime = closeHour + (closeMinute / 60.0);
            }
            else
            {
                model.ErrorMessage.Add("Bạn hãy dùng mẫu bên dưới để chỉnh sửa thông tin của sân");
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
                    Id = id,
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Ward = model.Ward,
                    District = model.District,
                    IsActive = model.IsActive,
                    OpenTime = openTime,
                    CloseTime = closeTime
                };

                string serverPath = Server.MapPath("~/Content/images/");
                int result = stadiumBO.OwnerUpdateStadium(stadium, listFiles, serverPath, changeStaffNames, changeStaffRoles, newStaffNames, newStaffRoles);
                if (result == 0)
                {
                    model.ErrorMessage.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessage.Add(Resources.StadiumStaff_UpdateStadiumUserNotFound);
                }
                else if (result == -2)
                {
                    model.ErrorMessage.Add(Resources.StadiumStaff_UpdateStadiumNewStaffConflict);
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

            var staffs = stadiumBO.GetAllStaffOfStadium(id);

            model.Images = listImages;
            model.ImageIds = imageIds;

            model.Staffs = staffs;

            model.IsMainOwner = staffs.Any(s => s.UserName.ToLower().Equals(User.Identity.Name.ToLower()) && s.Role.Equals("Main Owner"));

            return View(model);

        }

        [Authorize(Roles = "StadiumOwner")]
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

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult FieldPrices(int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            var fieldPrices = stadiumBO.GetAuthorizeStadiumFieldPrices(stadium, User.Identity.Name);
            FieldPricesModel model = new FieldPricesModel();
            if (fieldPrices == null)
            {
                model.HavePermission = false;
                model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
            }
            else
            {
                model.HavePermission = true;
                var s = stadiumBO.GetStadiumById(stadium);
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.PriceTables = new List<FieldPriceModel>();
                foreach (var item in fieldPrices)
                {
                    FieldPriceModel f = new FieldPriceModel();
                    f.Id = item.Id;
                    f.Name = item.FieldPriceName;
                    f.Description = item.FieldPriceDescription;
                    model.PriceTables.Add(f);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult AddFieldPrices(int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium s = stadiumBO.GetAuthorizeStadium(stadium, User.Identity.Name);
            AddFieldPricesModel model = new AddFieldPricesModel();
            if (s != null)
            {
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldPrice = new FieldPriceModel();
                model.DefaultPriceTables = new List<PriceTableModel>();
                model.DefaultPriceTables.Add(new PriceTableModel { StartTime = "0:00", EndTime = "0:00" });
                model.MondayPriceTables = new List<PriceTableModel>();
                model.TuesdayPriceTables = new List<PriceTableModel>();
                model.WednesdayPriceTables = new List<PriceTableModel>();
                model.ThurdayPriceTables = new List<PriceTableModel>();
                model.FridayPriceTables = new List<PriceTableModel>();
                model.SaturdayPriceTables = new List<PriceTableModel>();
                model.SundayPriceTables = new List<PriceTableModel>();
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "StadiumOwner")]
        public ActionResult AddFieldPrices(FormCollection form, int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium s = stadiumBO.GetAuthorizeStadium(stadium, User.Identity.Name);
            AddFieldPricesModel model = new AddFieldPricesModel();
            if (s != null)
            {
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldPrice = new FieldPriceModel();
                model.FieldPrice.Name = form["FieldPriceName"];
                model.FieldPrice.Description = form["FieldPriceDescription"];
                model.DefaultPriceTables = new List<PriceTableModel>();
                model.MondayPriceTables = new List<PriceTableModel>();
                model.TuesdayPriceTables = new List<PriceTableModel>();
                model.WednesdayPriceTables = new List<PriceTableModel>();
                model.ThurdayPriceTables = new List<PriceTableModel>();
                model.FridayPriceTables = new List<PriceTableModel>();
                model.SaturdayPriceTables = new List<PriceTableModel>();
                model.SundayPriceTables = new List<PriceTableModel>();
                model.ErrorMessages = new List<string>();

                var defaultStartTimeKeys = form.AllKeys.Where(k => k.Contains("defaultstarttime_")).ToList();
                var monStartTimeKeys = form.AllKeys.Where(k => k.Contains("monstarttime_")).ToList();
                var tueStartTimeKeys = form.AllKeys.Where(k => k.Contains("tuestarttime_")).ToList();
                var wedStartTimeKeys = form.AllKeys.Where(k => k.Contains("wedstarttime_")).ToList();
                var thuStartTimeKeys = form.AllKeys.Where(k => k.Contains("thustarttime_")).ToList();
                var friStartTimeKeys = form.AllKeys.Where(k => k.Contains("fristarttime_")).ToList();
                var satStartTimeKeys = form.AllKeys.Where(k => k.Contains("satstarttime_")).ToList();
                var sunStartTimeKeys = form.AllKeys.Where(k => k.Contains("sunstarttime_")).ToList();

                var fp = new FieldPrice();
                fp.FieldPriceName = model.FieldPrice.Name;
                fp.FieldPriceDescription = model.FieldPrice.Description;
                fp.StadiumId = s.Id;
                fp.PriceTables = new System.Data.Linq.EntitySet<PriceTable>();


                #region first validate data type

                var fperror = false;
                if (string.IsNullOrWhiteSpace(model.FieldPrice.Name) || string.IsNullOrWhiteSpace(model.FieldPrice.Description))
                {
                    fperror = true;
                    model.ErrorMessages.Add(Resources.Form_EmptyFields);
                }

                var derror = false;
                foreach (var item in defaultStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["defaultendtime_" + count];
                    var tempPrice = form["defaultprice_" + count];
                    model.DefaultPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!derror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá mặc định không hợp lệ");
                        derror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 0;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!derror)
                        {
                            model.ErrorMessages.Add("Bảng giá mặc định không hợp lệ");
                            derror = true;
                        }
                    }
                }

                var merror = false;
                foreach (var item in monStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["monendtime_" + count];
                    var tempPrice = form["monprice_" + count];
                    model.MondayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!merror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 2 không hợp lệ");
                        merror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 1;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!merror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 2 không hợp lệ");
                            merror = true;
                        }
                    }
                }

                var tuerror = false;
                foreach (var item in tueStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["tueendtime_" + count];
                    var tempPrice = form["tueprice_" + count];
                    model.TuesdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!tuerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 3 không hợp lệ");
                        tuerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 2;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!tuerror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 3 không hợp lệ");
                            tuerror = true;
                        }
                    }
                }

                var werror = false;
                foreach (var item in wedStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["wedendtime_" + count];
                    var tempPrice = form["wedprice_" + count];
                    model.WednesdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!werror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 4 không hợp lệ");
                        werror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 3;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!werror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 4 không hợp lệ");
                            werror = true;
                        }
                    }
                }

                var therror = false;
                foreach (var item in thuStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["thuendtime_" + count];
                    var tempPrice = form["thuprice_" + count];
                    model.ThurdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!therror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 5 không hợp lệ");
                        therror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 4;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!therror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 5 không hợp lệ");
                            therror = true;
                        }
                    }
                }

                var ferror = false;
                foreach (var item in friStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["friendtime_" + count];
                    var tempPrice = form["friprice_" + count];
                    model.FridayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!ferror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 6 không hợp lệ");
                        ferror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 5;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!ferror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 6 không hợp lệ");
                            ferror = true;
                        }
                    }
                }

                var saerror = false;
                foreach (var item in satStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["satendtime_" + count];
                    var tempPrice = form["satprice_" + count];
                    model.SaturdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!saerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 7 không hợp lệ");
                        saerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 6;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!saerror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 7 không hợp lệ");
                            saerror = true;
                        }
                    }
                }

                var suerror = false;
                foreach (var item in sunStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["sunendtime_" + count];
                    var tempPrice = form["sunprice_" + count];
                    model.SundayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!suerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá chủ nhật không hợp lệ");
                        suerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 7;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!suerror)
                        {
                            model.ErrorMessages.Add("Bảng giá chủ nhật không hợp lệ");
                            suerror = true;
                        }
                    }
                }


                #endregion first validate data type

                if (fperror || merror || tuerror || werror || therror || ferror || saerror || suerror)
                {
                    return View(model);
                }
                else
                {
                    var results = stadiumBO.CreateStadiumFieldPrice(fp);

                    if (results.FirstOrDefault() > 0)
                    {
                        return Redirect("/StadiumStaff/FieldPrices?Stadium=" + s.Id);
                    }
                    else
                    {
                        foreach (var item in results)
                        {
                            switch (item)
                            {
                                case 0:
                                    model.ErrorMessages.Add(Resources.DB_Exception);
                                    break;
                                case -1:
                                    model.ErrorMessages.Add("Bảng giá mặc định có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -2:
                                    model.ErrorMessages.Add("Bảng giá mặc định bắt buộc phải có 1 khung giá mặc định");
                                    break;
                                case -3:
                                    model.ErrorMessages.Add("Bảng giá mặc định có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -4:
                                    model.ErrorMessages.Add("Bảng giá mặc định có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -5:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -6:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -7:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -8:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -9:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -10:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -11:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -12:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -13:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -14:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -15:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -16:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -17:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -18:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -19:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -20:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -21:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -22:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -23:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -24:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -25:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult EditFieldPrices(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            FieldPrice fieldPrice = stadiumBO.GetAuthorizeFieldPrice(id, User.Identity.Name);


            AddFieldPricesModel model = new AddFieldPricesModel();
            if (fieldPrice != null)
            {
                Stadium s = stadiumBO.GetStadiumById(fieldPrice.StadiumId);
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldPrice = new FieldPriceModel();
                model.FieldPrice.Id = fieldPrice.Id;
                model.FieldPrice.Name = fieldPrice.FieldPriceName;
                model.FieldPrice.Description = fieldPrice.FieldPriceDescription;

                var priceTables = stadiumBO.GetAllPriceTablesOfFieldPrice(id);
                priceTables = priceTables.OrderBy(p => p.TimeFrom).ThenBy(p => p.TimeTo).ToList();

                model.DefaultPriceTables = new List<PriceTableModel>();
                model.MondayPriceTables = new List<PriceTableModel>();
                model.TuesdayPriceTables = new List<PriceTableModel>();
                model.WednesdayPriceTables = new List<PriceTableModel>();
                model.ThurdayPriceTables = new List<PriceTableModel>();
                model.FridayPriceTables = new List<PriceTableModel>();
                model.SaturdayPriceTables = new List<PriceTableModel>();
                model.SundayPriceTables = new List<PriceTableModel>();
                foreach (var item in priceTables)
                {
                    switch (item.Day)
                    {
                        case 0:
                            model.DefaultPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 1:
                            model.MondayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 2:
                            model.TuesdayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 3:
                            model.WednesdayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 4:
                            model.ThurdayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 5:
                            model.FridayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 6:
                            model.SaturdayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        case 7:
                            model.SundayPriceTables.Add(new PriceTableModel
                            {
                                StartTime = (int)item.TimeFrom + ":" + (item.TimeFrom - (int)item.TimeFrom),
                                EndTime = (int)item.TimeTo + ":" + (item.TimeTo - (int)item.TimeTo),
                                Price = item.Price.ToString()
                            });
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "StadiumOwner")]
        public ActionResult EditFieldPrices(FormCollection form, int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            FieldPrice fieldPrice = stadiumBO.GetAuthorizeFieldPrice(id, User.Identity.Name);
            AddFieldPricesModel model = new AddFieldPricesModel();
            if (fieldPrice != null)
            {
                Stadium s = stadiumBO.GetAuthorizeStadium(fieldPrice.StadiumId, User.Identity.Name);
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldPrice = new FieldPriceModel();
                model.FieldPrice.Name = form["FieldPriceName"];
                model.FieldPrice.Description = form["FieldPriceDescription"];
                model.DefaultPriceTables = new List<PriceTableModel>();
                model.MondayPriceTables = new List<PriceTableModel>();
                model.TuesdayPriceTables = new List<PriceTableModel>();
                model.WednesdayPriceTables = new List<PriceTableModel>();
                model.ThurdayPriceTables = new List<PriceTableModel>();
                model.FridayPriceTables = new List<PriceTableModel>();
                model.SaturdayPriceTables = new List<PriceTableModel>();
                model.SundayPriceTables = new List<PriceTableModel>();
                model.ErrorMessages = new List<string>();

                var defaultStartTimeKeys = form.AllKeys.Where(k => k.Contains("defaultstarttime_")).ToList();
                var monStartTimeKeys = form.AllKeys.Where(k => k.Contains("monstarttime_")).ToList();
                var tueStartTimeKeys = form.AllKeys.Where(k => k.Contains("tuestarttime_")).ToList();
                var wedStartTimeKeys = form.AllKeys.Where(k => k.Contains("wedstarttime_")).ToList();
                var thuStartTimeKeys = form.AllKeys.Where(k => k.Contains("thustarttime_")).ToList();
                var friStartTimeKeys = form.AllKeys.Where(k => k.Contains("fristarttime_")).ToList();
                var satStartTimeKeys = form.AllKeys.Where(k => k.Contains("satstarttime_")).ToList();
                var sunStartTimeKeys = form.AllKeys.Where(k => k.Contains("sunstarttime_")).ToList();

                var fp = new FieldPrice();
                fp.Id = id;
                fp.FieldPriceName = model.FieldPrice.Name;
                fp.FieldPriceDescription = model.FieldPrice.Description;
                fp.StadiumId = s.Id;
                fp.PriceTables = new System.Data.Linq.EntitySet<PriceTable>();


                #region first validate data type

                var fperror = false;
                if (string.IsNullOrWhiteSpace(model.FieldPrice.Name) || string.IsNullOrWhiteSpace(model.FieldPrice.Description))
                {
                    fperror = true;
                    model.ErrorMessages.Add(Resources.Form_EmptyFields);
                }

                var derror = false;
                foreach (var item in defaultStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["defaultendtime_" + count];
                    var tempPrice = form["defaultprice_" + count];
                    model.DefaultPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!derror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá mặc định không hợp lệ");
                        derror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 0;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!derror)
                        {
                            model.ErrorMessages.Add("Bảng giá mặc định không hợp lệ");
                            derror = true;
                        }
                    }
                }

                var merror = false;
                foreach (var item in monStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["monendtime_" + count];
                    var tempPrice = form["monprice_" + count];
                    model.MondayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!merror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 2 không hợp lệ");
                        merror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 1;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!merror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 2 không hợp lệ");
                            merror = true;
                        }
                    }
                }

                var tuerror = false;
                foreach (var item in tueStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["tueendtime_" + count];
                    var tempPrice = form["tueprice_" + count];
                    model.TuesdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!tuerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 3 không hợp lệ");
                        tuerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 2;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!tuerror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 3 không hợp lệ");
                            tuerror = true;
                        }
                    }
                }

                var werror = false;
                foreach (var item in wedStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["wedendtime_" + count];
                    var tempPrice = form["wedprice_" + count];
                    model.WednesdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!werror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 4 không hợp lệ");
                        werror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 3;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!werror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 4 không hợp lệ");
                            werror = true;
                        }
                    }
                }

                var therror = false;
                foreach (var item in thuStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["thuendtime_" + count];
                    var tempPrice = form["thuprice_" + count];
                    model.ThurdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!therror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 5 không hợp lệ");
                        therror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 4;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!therror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 5 không hợp lệ");
                            therror = true;
                        }
                    }
                }

                var ferror = false;
                foreach (var item in friStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["friendtime_" + count];
                    var tempPrice = form["friprice_" + count];
                    model.FridayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!ferror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 6 không hợp lệ");
                        ferror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 5;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!ferror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 6 không hợp lệ");
                            ferror = true;
                        }
                    }
                }

                var saerror = false;
                foreach (var item in satStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["satendtime_" + count];
                    var tempPrice = form["satprice_" + count];
                    model.SaturdayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!saerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá thứ 7 không hợp lệ");
                        saerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 6;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!saerror)
                        {
                            model.ErrorMessages.Add("Bảng giá thứ 7 không hợp lệ");
                            saerror = true;
                        }
                    }
                }

                var suerror = false;
                foreach (var item in sunStartTimeKeys)
                {
                    var count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                    var tempStart = form[item];
                    var tempEnd = form["sunendtime_" + count];
                    var tempPrice = form["sunprice_" + count];
                    model.SundayPriceTables.Add(new PriceTableModel
                    {
                        StartTime = tempStart,
                        EndTime = tempEnd,
                        Price = tempPrice
                    });
                    if (!suerror && (string.IsNullOrWhiteSpace(tempStart) || string.IsNullOrWhiteSpace(tempEnd) || string.IsNullOrWhiteSpace(tempPrice)))
                    {
                        model.ErrorMessages.Add("Bảng giá chủ nhật không hợp lệ");
                        suerror = true;
                    }
                    else
                    {
                        int hourStart;
                        bool parseHS = int.TryParse(tempStart.Substring(0, tempStart.LastIndexOf(":")).Trim(), out hourStart);
                        int minuteStart;
                        bool parseMS = int.TryParse(tempStart.Substring(tempStart.LastIndexOf(":") + 1, tempStart.Length - tempStart.LastIndexOf(":") - 1), out minuteStart);
                        int hourEnd;
                        bool parseHE = int.TryParse(tempEnd.Substring(0, tempEnd.LastIndexOf(":")).Trim(), out hourEnd);
                        int minuteEnd;
                        bool parseME = int.TryParse(tempEnd.Substring(tempEnd.LastIndexOf(":") + 1, tempEnd.Length - tempEnd.LastIndexOf(":") - 1), out minuteEnd);
                        double truePrice;
                        bool parseTP = double.TryParse(tempPrice.Replace(",", ""), out truePrice);
                        if (parseHS && parseMS && parseHE && parseME && parseTP)
                        {
                            var tempPB = new PriceTable();
                            tempPB.Day = 7;
                            tempPB.TimeFrom = hourStart + (minuteStart / 60.0);
                            tempPB.TimeTo = hourEnd + (minuteEnd / 60.0);
                            tempPB.Price = truePrice;
                            fp.PriceTables.Add(tempPB);
                        }
                        else if (!suerror)
                        {
                            model.ErrorMessages.Add("Bảng giá chủ nhật không hợp lệ");
                            suerror = true;
                        }
                    }
                }


                #endregion first validate data type

                if (fperror || merror || tuerror || werror || therror || ferror || saerror || suerror)
                {
                    return View(model);
                }
                else
                {
                    var results = stadiumBO.UpdateStadiumFieldPrice(fp);

                    if (results.FirstOrDefault() > 0)
                    {
                        model.SuccessMessage = Resources.Update_Success;
                    }
                    else
                    {
                        foreach (var item in results)
                        {
                            switch (item)
                            {
                                case 0:
                                    model.ErrorMessages.Add(Resources.DB_Exception);
                                    break;
                                case -1:
                                    model.ErrorMessages.Add("Bảng giá mặc định có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -2:
                                    model.ErrorMessages.Add("Bảng giá mặc định bắt buộc phải có 1 khung giá mặc định");
                                    break;
                                case -3:
                                    model.ErrorMessages.Add("Bảng giá mặc định có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -4:
                                    model.ErrorMessages.Add("Bảng giá mặc định có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -5:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -6:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -7:
                                    model.ErrorMessages.Add("Bảng giá thứ 2 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -8:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -9:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -10:
                                    model.ErrorMessages.Add("Bảng giá thứ 3 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -11:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -12:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -13:
                                    model.ErrorMessages.Add("Bảng giá thứ 4 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -14:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -15:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -16:
                                    model.ErrorMessages.Add("Bảng giá thứ 5 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -17:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -18:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -19:
                                    model.ErrorMessages.Add("Bảng giá thứ 6 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -20:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -21:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -22:
                                    model.ErrorMessages.Add("Bảng giá thứ 7 có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                case -23:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có nhiều hơn 1 khung giá mặc định");
                                    break;
                                case -24:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có khung giờ không hợp lệ, giờ bắt đầu phải nhỏ hơn giờ kết thúc ngoại trừ khung giờ mặc định");
                                    break;
                                case -25:
                                    model.ErrorMessages.Add("Bảng giá chủ nhật có khung giờ trùng nhau, khung giờ này đè lên khung giờ khác ngoại trừ khung giờ mặc định");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult Fields(int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            var fields = stadiumBO.GetAuthorizeStadiumFields(stadium, User.Identity.Name);
            FieldsModel model = new FieldsModel();
            if (fields == null)
            {
                model.HavePermission = false;
                model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
            }
            else
            {
                model.HavePermission = true;
                var s = stadiumBO.GetStadiumById(stadium);
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.Fields = new List<FieldModel>();
                foreach (var item in fields)
                {
                    FieldModel f = new FieldModel();
                    f.Id = item.Id;
                    f.Number = item.Number;
                    f.FieldType = item.FieldType;
                    if (item.ParentField != null)
                    {
                        f.Parent = item.Field1.Number;
                    }
                    f.IsActive = item.IsActive;
                    model.Fields.Add(f);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult AddField(int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium s = stadiumBO.GetAuthorizeStadium(stadium, User.Identity.Name);
            AddFieldModel model = new AddFieldModel();
            if (s != null)
            {
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.ChosenParent = null;
                model.Parent = stadiumBO.GetAllFields7And11(s.Id);
                model.Prices = stadiumBO.GetAllFieldPriceOfStadium(s.Id);
                if (model.Prices != null && model.Prices.Count > 0)
                {
                    model.ChosenPrice = model.Prices.FirstOrDefault().Id;
                }
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        [HttpPost]
        public ActionResult AddField(FormCollection form, int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium s = stadiumBO.GetAuthorizeStadium(stadium, User.Identity.Name);
            AddFieldModel model = new AddFieldModel();
            if (s != null)
            {
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.Parent = stadiumBO.GetAllFields7And11(s.Id);
                model.Prices = stadiumBO.GetAllFieldPriceOfStadium(s.Id);
                if (model.Prices != null && model.Prices.Count > 0)
                {
                    model.ChosenPrice = model.Prices.FirstOrDefault().Id;
                }
                model.ErrorMessages = new List<string>();

                model.Number = form["Number"];
                bool isActive;
                bool parseStatus = bool.TryParse(form["IsActive"], out isActive);
                model.IsActive = isActive;
                int fieldType;
                bool parseType = int.TryParse(form["FieldType"], out fieldType);
                model.FieldType = fieldType;
                string strParent = form["Parent"];

                bool parseParent = true;
                if (strParent.Trim().ToLower().Equals("null"))
                {
                    model.ChosenParent = null; ;
                }
                else
                {
                    int parent;
                    parseParent = int.TryParse(strParent, out parent);
                    model.ChosenParent = parent;
                }


                int prices;
                bool parsePrices = int.TryParse(form["FieldPrice"], out prices);
                model.ChosenPrice = prices;
                //save state - validate - create

                if (string.IsNullOrWhiteSpace(model.Number) || !parseStatus || !parseType || !parseParent || !parsePrices)
                {
                    model.ErrorMessages.Add("Bạn cần sử dụng mẫu nhập bên dưới để thêm đầy đủ thông tin vào sân bóng");
                }
                else
                {
                    Field field = new Field();
                    field.StadiumId = s.Id;
                    field.Number = model.Number;
                    field.IsActive = model.IsActive;
                    field.FieldType = model.FieldType;
                    field.PriceId = model.ChosenPrice;
                    field.ParentField = model.ChosenParent;
                    var result = stadiumBO.CreateField(field);

                    if (result == 0)
                    {
                        model.ErrorMessages.Add(Resources.DB_Exception);
                    }
                    else if (result == -1)
                    {
                        model.ErrorMessages.Add("Sân tách đã không thể tách ra thêm");
                    }
                    else if (result == -2)
                    {
                        model.ErrorMessages.Add("Không thể tách sân ra thành sân bằng hoặc lớn hơn");
                    }
                    else if (result == -3)
                    {
                        model.ErrorMessages.Add("Không thể tách sân không có trong hệ thống");
                    }
                    else if (result > 0)
                    {
                        return Redirect("/StadiumStaff/Fields?Stadium=" + s.Id);
                    }
                }

            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult EditField(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();

            Field f = stadiumBO.GetAuthorizeField(id, User.Identity.Name);
            EditFieldModel model = new EditFieldModel();
            if (f != null)
            {
                var s = stadiumBO.GetStadiumById(f.StadiumId);
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldId = f.Id;
                model.Parent = stadiumBO.GetAllFields7And11(s.Id);
                var remove = model.Parent.Where(p => p.Id == f.Id).FirstOrDefault();
                if (remove != null)
                {
                    model.Parent.Remove(remove);
                }
                model.Prices = stadiumBO.GetAllFieldPriceOfStadium(s.Id);
                model.ChosenParent = f.ParentField;
                model.ChosenPrice = f.PriceId;
                model.IsActive = f.IsActive;
                model.Number = f.Number;
                model.FieldType = f.FieldType;
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        [HttpPost]
        public ActionResult EditField(FormCollection form, int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Field f = stadiumBO.GetAuthorizeField(id, User.Identity.Name);
            EditFieldModel model = new EditFieldModel();
            if (f != null)
            {
                var s = stadiumBO.GetStadiumById(f.StadiumId);
                model.HavePermission = true;
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.FieldId = f.Id;
                model.Parent = stadiumBO.GetAllFields7And11(s.Id);
                var remove = model.Parent.Where(p => p.Id == f.Id).FirstOrDefault();
                if (remove != null)
                {
                    model.Parent.Remove(remove);
                }
                model.Prices = stadiumBO.GetAllFieldPriceOfStadium(s.Id);
                if (model.Prices != null && model.Prices.Count > 0)
                {
                    model.ChosenPrice = model.Prices.FirstOrDefault().Id;
                }
                model.ErrorMessages = new List<string>();

                model.Number = form["Number"];
                bool isActive;
                bool parseStatus = bool.TryParse(form["IsActive"], out isActive);
                model.IsActive = isActive;
                int fieldType;
                bool parseType = int.TryParse(form["FieldType"], out fieldType);
                model.FieldType = fieldType;
                string strParent = form["Parent"];

                bool parseParent = true;
                if (strParent.Trim().ToLower().Equals("null"))
                {
                    model.ChosenParent = null; ;
                }
                else
                {
                    int parent;
                    parseParent = int.TryParse(strParent, out parent);
                    model.ChosenParent = parent;
                }


                int prices;
                bool parsePrices = int.TryParse(form["FieldPrice"], out prices);
                model.ChosenPrice = prices;
                //save state - validate - create

                if (string.IsNullOrWhiteSpace(model.Number) || !parseStatus || !parseType || !parseParent || !parsePrices)
                {
                    model.ErrorMessages.Add("Bạn cần sử dụng mẫu nhập bên dưới để cập nhật đầy đủ thông tin vào sân bóng");
                }
                else
                {
                    f.Number = model.Number;
                    f.IsActive = model.IsActive;
                    f.FieldType = model.FieldType;
                    f.PriceId = model.ChosenPrice;
                    f.ParentField = model.ChosenParent;
                    var result = stadiumBO.UpdateField(f);

                    if (result == 0)
                    {
                        model.ErrorMessages.Add(Resources.DB_Exception);
                    }
                    else if (result == -1)
                    {
                        model.ErrorMessages.Add("Sân tách đã không thể tách ra thêm");
                    }
                    else if (result == -2)
                    {
                        model.ErrorMessages.Add("Không thể tách sân ra thành sân bằng hoặc lớn hơn");
                    }
                    else if (result == -3)
                    {
                        model.ErrorMessages.Add("Không thể tách sân không có trong hệ thống");
                    }
                    else if (result == -4)
                    {
                        model.ErrorMessages.Add("Không thể tách sân từ chính sân đang cập nhật");
                    }
                    else if (result > 0)
                    {
                        model.SuccessMessage = Resources.Update_Success;
                        return View(model);
                    }
                }

            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessages = new List<string>();
                model.ErrorMessages.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
            }
            return View(model);
        }

        #endregion STADIUMS MANAGEMENT

        #region RESERVATION MANAGEMENT

        public ActionResult Reservations(int? id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<Stadium> stadiums = stadiumBO.GetStadiumsByStaff(User.Identity.Name);
            Stadium std = new Stadium();
            ReservationsModel model = new ReservationsModel();
            try
            {
                int stadiumId = Convert.ToInt32(id);
                if (stadiums.Count > 0)
                {
                    std = stadiums[0];
                    foreach (var item in stadiums)
                    {
                        if (item.Id == stadiumId)
                        {
                            std = item;
                        }
                    }

                    ReservationBO resvBO = new ReservationBO();

                    model.HavePermission = true;
                    model.Stadium = std;
                    model.FieldCount = std.Fields.Count;
                    model.Stadiums = stadiums;
                    model.Reservations = resvBO.GetReservationsOfStadium(std.Id);
                }
                else
                {
                    model.HavePermission = false;
                    model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
                }
            }
            catch (Exception)
            {
                model.HavePermission = false;
                model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
            }
            model.Reservations = model.Reservations.OrderByDescending(r => r.Id).ToList();
            return View(model);
        }


        public ActionResult AddReservation(int? stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            try
            {
                ReservationModel model = new ReservationModel();

                model.Fields = stadiumBO.GetFieldsByStadiumId((int)stadium);

                if (model.Fields != null && model.Fields.Count > 0)
                {
                    Stadium std = stadiumBO.GetStadiumById((int)stadium);
                    model.StadiumName = std.Name;
                    model.StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Reservations/" + stadium, "StadiumStaff");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Reservations", "StadiumStaff");
            }

        }


        [HttpPost]
        public ActionResult AddReservation(FormCollection form, int stadium)
        {
            ReservationModel model = new ReservationModel();
            StadiumBO stadiumBO = new StadiumBO();
            model.ErrorMessages = new List<string>();

            bool checkParseError = false;

            try
            {
                model.FieldId = Int32.Parse(form["Fields"]);
                model.Fields = stadiumBO.GetFieldsByStadiumId(stadium);
                model.FullName = form["FullName"];
                model.PhoneNumber = form["PhoneNumber"];
                model.Email = form["Email"];
                model.Date = DateTime.Parse(form["Date"], new CultureInfo("vi-VN"));
                Utils utils = new Utils();
                model.StartTime = utils.TimeStringToDouble(form["StartTime"]);
                model.Duration = Double.Parse(form["Duration"]);
                model.Status = form["Status"];
                model.NeedRival = Boolean.Parse(form["NeedRival"]);
                if (model.NeedRival)
                {
                    model.RivalName = form["RivalName"];
                    model.RivalPhone = form["RivalPhone"];
                    model.RivalEmail = form["RivalEmail"];
                    model.RivalStatus = form["RivalStatus"];
                }
            }
            catch (Exception)
            {
                checkParseError = true;
            }

            if (checkParseError || string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.PhoneNumber) ||
                 (!model.RivalStatus.Equals("Waiting") && (string.IsNullOrWhiteSpace(model.RivalName) || string.IsNullOrWhiteSpace(model.RivalPhone))))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                UserBO userBO = new UserBO();
                User staff = userBO.GetUserByUserName(User.Identity.Name);
                Promotion promotion = stadiumBO.GetPromotionByField(model.FieldId, model.Date);
                var price = stadiumBO.CalculatePrice(stadiumBO.GetFieldById(model.FieldId), model.Date, model.StartTime, model.Duration);
                Guid g = Guid.NewGuid();
                string verCode = Convert.ToBase64String(g.ToByteArray());
                verCode = verCode.Replace("=", "");
                verCode = verCode.Replace("+", "");
                verCode = verCode.ToUpper();
                while (verCode.Length > 6)
                {
                    Random r = new Random((int)DateTime.Now.Ticks);
                    verCode = verCode.Remove(r.Next(verCode.Length), 1);
                }
                Reservation reservation = new Reservation()
                {
                    FieldId = model.FieldId,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Date = model.Date,
                    StartTime = model.StartTime,
                    Duration = model.Duration,
                    Price = price[0],
                    Discount = price[1],
                    VerifyCode = verCode,
                    CreatedDate = DateTime.Now.Date,
                    Approver = staff.Id,
                    Status = model.Status,
                    NeedRival = model.NeedRival
                };

                if (reservation.NeedRival)
                {
                    if (!string.IsNullOrWhiteSpace(model.RivalName) && !string.IsNullOrWhiteSpace(model.RivalPhone))
                    {
                        reservation.RivalName = model.RivalName;
                        reservation.RivalPhone = model.RivalPhone;
                        reservation.RivalEmail = model.RivalEmail;
                        reservation.RivalFinder = staff.Id;
                    }
                    reservation.RivalStatus = model.RivalStatus;
                }

                ReservationBO resvBO = new ReservationBO();

                int result = resvBO.CreateReservation(reservation);

                if (result > 0)
                {
                    return RedirectToAction("Reservations/" + stadium, "StadiumStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Resv_TimeOver);
                }
                else if (result == -2)
                {
                    model.ErrorMessages.Add(Resources.Resv_DurationTimeOutOfRange);
                }
                else if (result == -3)
                {
                    model.ErrorMessages.Add(Resources.Resv_NotInOpenTime);
                }
                else if (result == -4)
                {
                    model.ErrorMessages.Add(Resources.Resv_NeedRivalException);
                }
                else if (result == -5)
                {
                    model.ErrorMessages.Add(Resources.Resv_FieldIsNotAvailable);
                }
            }
            Stadium std = stadiumBO.GetStadiumById(stadium);
            model.StadiumName = std.Name;
            model.StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District);
            return View(model);
        }


        public ActionResult ViewReservation(int? id)
        {
            ReservationBO resvBO = new ReservationBO();

            try
            {
                Reservation resv = resvBO.GetReservationById((int)id);

                if (resv == null)
                {
                    return RedirectToAction("Reservations", "StadiumStaff");
                }

                Stadium std = resv.Field.Stadium;
                ReservationModel model = new ReservationModel()
                {
                    Id = resv.Id,
                    FieldId = resv.FieldId,
                    Fields = resv.Field.Stadium.Fields.ToList(),
                    UserId = resv.UserId == null ? 0 : (int)resv.UserId,
                    Customer = resv.UserId == null ? "" : resv.User.UserName,
                    FullName = resv.FullName,
                    PhoneNumber = resv.PhoneNumber,
                    Email = resv.Email,
                    Date = resv.Date,
                    StartTime = resv.StartTime,
                    Duration = resv.Duration,
                    Price = resv.Price,
                    Discount = resv.Discount == null ? 0 : (int)resv.Discount,
                    CreatedDate = resv.CreatedDate.Date,
                    Approver = resv.User1 == null ? "" : resv.User1.UserName,
                    Status = resv.Status,
                    NeedRival = resv.NeedRival,
                    RivalUser = resv.RivalId == null ? "" : resv.User2.UserName,
                    RivalName = resv.RivalName,
                    RivalPhone = resv.RivalPhone,
                    RivalEmail = resv.RivalEmail,
                    RivalFinder = resv.RivalFinder == null ? "" : resv.User3.UserName,
                    RivalStatus = resv.RivalStatus == null ? "Waiting" : resv.RivalStatus,
                    StadiumName = std.Name,
                    StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District)
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Reservations", "StadiumStaff");
            }

        }


        public ActionResult EditReservation(int? id)
        {
            ReservationBO resvBO = new ReservationBO();

            try
            {
                Reservation resv = resvBO.GetReservationById((int)id);

                if (resv == null)
                {
                    return RedirectToAction("Reservations", "StadiumStaff");
                }

                Stadium std = resv.Field.Stadium;
                ReservationModel model = new ReservationModel()
                {
                    Id = resv.Id,
                    FieldId = resv.FieldId,
                    Fields = resv.Field.Stadium.Fields.ToList(),
                    UserId = resv.UserId == null ? 0 : (int)resv.UserId,
                    Customer = resv.UserId == null ? "" : resv.User.UserName,
                    FullName = resv.FullName,
                    PhoneNumber = resv.PhoneNumber,
                    Email = resv.Email,
                    Date = resv.Date,
                    StartTime = resv.StartTime,
                    Duration = resv.Duration,
                    Price = resv.Price,
                    Discount = resv.Discount == null ? 0 : (int)resv.Discount,
                    CreatedDate = resv.CreatedDate.Date,
                    Approver = resv.User1 == null ? "" : resv.User1.UserName,
                    Status = resv.Status,
                    NeedRival = resv.NeedRival,
                    RivalUser = resv.RivalId == null ? "" : resv.User2.UserName,
                    RivalName = resv.RivalName,
                    RivalPhone = resv.RivalPhone,
                    RivalEmail = resv.RivalEmail,
                    RivalFinder = resv.RivalFinder == null ? "" : resv.User3.UserName,
                    RivalStatus = resv.RivalStatus == null ? "Waiting" : resv.RivalStatus,
                    StadiumName = std.Name,
                    StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District)
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Reservations", "StadiumStaff");
            }

        }


        [HttpPost]
        public ActionResult EditReservation(FormCollection form, int id)
        {
            ReservationModel model = new ReservationModel();

            model.ErrorMessages = new List<string>();

            bool checkParseError = false;
            ReservationBO resvBO = new ReservationBO();
            Reservation resv = resvBO.GetReservationById(id);
            try
            {
                model.Id = id;
                model.FieldId = Int32.Parse(form["Fields"]);
                model.FullName = form["FullName"];
                model.PhoneNumber = form["PhoneNumber"];
                model.Email = form["Email"];
                model.Date = DateTime.Parse(form["Date"], new CultureInfo("vi-VN"));
                Utils utils = new Utils();
                model.StartTime = utils.TimeStringToDouble(form["StartTime"]);
                model.Duration = Double.Parse(form["Duration"]);
                model.Status = form["Status"];
                model.NeedRival = Boolean.Parse(form["NeedRival"]);
                if (model.NeedRival)
                {
                    model.RivalName = form["RivalName"];
                    model.RivalPhone = form["RivalPhone"];
                    model.RivalEmail = form["RivalEmail"];
                    model.RivalStatus = form["RivalStatus"];
                }
            }
            catch (Exception)
            {
                checkParseError = true;
            }

            if (checkParseError || string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.PhoneNumber) ||
                (model.RivalStatus != "Waiting" && (string.IsNullOrWhiteSpace(model.RivalName) || string.IsNullOrWhiteSpace(model.RivalPhone))))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();
                UserBO userBO = new UserBO();
                User staff = userBO.GetUserByUserName(User.Identity.Name);
                int stadium = stadiumBO.GetFieldById(model.FieldId).StadiumId;

                Reservation reservation = new Reservation()
                {
                    Id = id,
                    FieldId = model.FieldId,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Date = model.Date,
                    StartTime = model.StartTime,
                    Duration = model.Duration,
                    Price = stadiumBO.CalculatePrice(stadiumBO.GetFieldById(model.FieldId), model.Date, model.StartTime, model.Duration)[0],
                    Approver = staff.Id,
                    Status = model.Status,
                    NeedRival = model.NeedRival
                };

                if (reservation.NeedRival)
                {
                    if (!string.IsNullOrWhiteSpace(model.RivalName) && !string.IsNullOrWhiteSpace(model.RivalPhone))
                    {
                        reservation.RivalName = model.RivalName;
                        reservation.RivalPhone = model.RivalPhone;
                        reservation.RivalEmail = model.RivalEmail;
                        reservation.RivalFinder = staff.Id;
                    }
                    reservation.RivalStatus = model.RivalStatus;
                }


                int result = resvBO.UpdateReservation(reservation);

                if (result > 0)
                {
                    return RedirectToAction("ViewReservation/" + id, "StadiumStaff");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Resv_TimeOver);
                }
                else if (result == -2)
                {
                    model.ErrorMessages.Add(Resources.Resv_DurationTimeOutOfRange);
                }
                else if (result == -3)
                {
                    model.ErrorMessages.Add(Resources.Resv_NotInOpenTime);
                }
                else if (result == -4)
                {
                    model.ErrorMessages.Add(Resources.Resv_NeedRivalException);
                }
                else if (result == -5)
                {
                    model.ErrorMessages.Add(Resources.Resv_FieldIsNotAvailable);
                }
            }
            Stadium std = resv.Field.Stadium;
            model.RivalStatus = model.RivalStatus == null ? "Waiting" : model.RivalStatus;
            model.StadiumName = std.Name;
            model.StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District);
            return View(model);
        }


        public ActionResult ApproveReservation(int id)
        {
            ReservationBO resvBO = new ReservationBO();
            UserBO userBO = new UserBO();
            User staff = userBO.GetUserByUserName(User.Identity.Name);
            int stadium = resvBO.GetReservationById(id).Field.StadiumId;
            int result = resvBO.UpdateReservationStatus(id, "Approved", staff.Id);
            if (result > 0)
            {
                return RedirectToAction("Reservations/" + stadium, "StadiumStaff");
            }
            else if (result == -1)
            {
                ViewData["fieldNA"] = "true";
                return RedirectToAction("ViewReservation/" + id, "StadiumStaff");
            }
            else
            {
                ViewData["dbExcp"] = "true";
                return RedirectToAction("ViewReservation/" + id, "StadiumStaff");
            }
        }


        public ActionResult DenyReservation(int id)
        {
            ReservationBO resvBO = new ReservationBO();
            UserBO userBO = new UserBO();
            User staff = userBO.GetUserByUserName(User.Identity.Name);
            int result = resvBO.UpdateReservationStatus(id, "Denied", staff.Id);
            return RedirectToAction("Reservations", "StadiumStaff");
        }


        #endregion RESERVATION MANAGEMENT

        #region PROMOTION MANAGEMENT


        public ActionResult Promotions(int? id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<Stadium> stadiums = stadiumBO.GetStadiumsByStaff(User.Identity.Name);
            Stadium std = new Stadium();
            PromotionsModel model = new PromotionsModel();
            try
            {
                int stadiumId = Convert.ToInt32(id);
                if (stadiums.Count > 0)
                {
                    std = stadiums[0];
                    foreach (var item in stadiums)
                    {
                        if (item.Id == stadiumId)
                        {
                            std = item;
                        }
                    }

                    model.HavePermission = true;
                    model.Stadium = std;
                    model.FieldCount = std.Fields.Count;
                    model.Stadiums = stadiums;
                    model.Promotions = stadiumBO.GetAllPromotionsByStadium(std.Id);
                }
                else
                {
                    model.HavePermission = false;
                    model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
                }
            }
            catch (Exception)
            {
                model.HavePermission = false;
                model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
            }

            return View(model);
        }


        public ActionResult AddPromotion(int? stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();
            try
            {
                PromotionModel model = new PromotionModel();
                model.Fields = stadiumBO.GetFieldsByStadiumId((int)stadium);
                if (model.Fields != null && model.Fields.Count > 0)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Promotions", "StadiumStaff");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Promotions", "StadiumStaff");
            }
        }


        [HttpPost]
        public ActionResult AddPromotion(FormCollection form, int stadium)
        {
            PromotionModel model = new PromotionModel();

            model.ErrorMessages = new List<string>();

            try
            {
                model.FieldId = Int32.Parse(form["Fields"]);
                model.PromotionFrom = DateTime.Parse(form["PromotionFrom"], new CultureInfo("vi-VN"));
                model.PromotionTo = DateTime.Parse(form["PromotionTo"], new CultureInfo("vi-VN"));
                model.Discount = Double.Parse(form["Discount"]);
            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();
                UserBO userBO = new UserBO();

                User creator = userBO.GetUserByUserName(User.Identity.Name);

                Promotion promotion = new Promotion
                {
                    FieldId = model.FieldId,
                    PromotionFrom = model.PromotionFrom,
                    PromotionTo = model.PromotionTo,
                    Discount = model.Discount,
                    Creator = creator.Id,
                    IsActive = true
                };

                int result = stadiumBO.CreatePromotion(promotion);

                if (result > 0)
                {
                    return RedirectToAction("Promotions/" + stadium, "StadiumStaff");
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


        public ActionResult EditPromotion(int? id)
        {
            StadiumBO stadiumBO = new StadiumBO();

            try
            {
                Promotion promotion = stadiumBO.GetPromotionById((int)id);

                if (promotion == null)
                {
                    return RedirectToAction("Promotions", "StadiumStaff");
                }

                PromotionModel model = new PromotionModel()
                {
                    FieldId = promotion.FieldId,
                    Fields = promotion.Field.Stadium.Fields.ToList(),
                    PromotionFrom = promotion.PromotionFrom,
                    PromotionTo = promotion.PromotionTo,
                    Discount = promotion.Discount,
                    IsActive = promotion.IsActive,
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Promotions", "StadiumStaff");
            }
        }


        [HttpPost]
        public ActionResult EditPromotion(FormCollection form, int id)
        {
            PromotionModel model = new PromotionModel();

            model.ErrorMessages = new List<string>();

            try
            {
                model.FieldId = Int32.Parse(form["Fields"]);
                model.PromotionFrom = DateTime.Parse(form["PromotionFrom"], new CultureInfo("vi-VN"));
                model.PromotionTo = DateTime.Parse(form["PromotionTo"], new CultureInfo("vi-VN"));
                model.Discount = Double.Parse(form["Discount"]);
                model.IsActive = Boolean.Parse(form["IsActive"]);
            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();
                int stadium = stadiumBO.GetFieldById(model.FieldId).StadiumId;
                Promotion promotion = new Promotion
                {
                    Id = id,
                    FieldId = model.FieldId,
                    PromotionFrom = model.PromotionFrom,
                    PromotionTo = model.PromotionTo,
                    Discount = model.Discount,
                    IsActive = model.IsActive,
                };

                int result = stadiumBO.UpdatePromotion(promotion);

                if (result > 0)
                {
                    return RedirectToAction("Promotions/" + stadium, "StadiumStaff");
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

        #endregion PROMOTION MANAGEMENT

    }
}
