﻿﻿using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models.StadiumModels;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FootballPitchesBooking.Properties;
using System.Globalization;
using FootballPitchesBooking.Utilities;


namespace FootballPitchesBooking.Controllers
{
    public class StadiumController : Controller
    {
        //
        // GET: /Stadium/

        public ActionResult Index()
        {
            StadiumBO stadiumBO = new StadiumBO();
            StadiumsModel model = new StadiumsModel();
            model.Stadiums = stadiumBO.GetAllStadiums().Where(s => s.IsActive && s.Fields.Count() > 0).ToList();            
            model.Rate = new List<double>();
            model.Image = new List<string>();
            for (int i = 0; i < model.Stadiums.Count; i++)
            {
                if (model.Stadiums[i].StadiumRatings.Count > 0)
                {
                    model.Rate.Add(model.Stadiums[i].StadiumRatings.Select(sr => sr.Rate).Average());
                }
                else
                {
                    model.Rate.Add(0);
                }

                if (model.Stadiums[i].StadiumImages.Count > 0)
                {
                    model.Image.Add(model.Stadiums[i].StadiumImages.Select(si => si.Path).FirstOrDefault());
                }
                else
                {
                    model.Image.Add(null);
                }
            }
            if (model.Stadiums.Count == 0)
            {
                model = null;
            }
            return View(model);
        }


        public ActionResult PriceTable(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            var fp = stadiumBO.GetFieldPriceById(id);
            if (fp != null)
            {
                PriceTableModel model = new PriceTableModel();
                model.FieldPrice = fp;
                model.Stadium = fp.Stadium;
                var pts = fp.PriceTables.ToList();
                model.Monday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Monday, pts);
                model.MondayElse = model.Monday.Last();
                model.Monday.RemoveAt(model.Monday.Count() - 1);
                model.Tuesday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Tuesday, pts);
                model.TuesdayElse = model.Tuesday.Last();
                model.Tuesday.RemoveAt(model.Tuesday.Count() - 1);
                model.Wednesday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Wednesday, pts);
                model.WednesdayElse = model.Wednesday.Last();
                model.Wednesday.RemoveAt(model.Wednesday.Count() - 1);
                model.Thursday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Thursday, pts);
                model.ThursdayElse = model.Thursday.Last();
                model.Thursday.RemoveAt(model.Thursday.Count() - 1);
                model.Friday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Friday, pts);
                model.FridayElse = model.Friday.Last();
                model.Friday.RemoveAt(model.Friday.Count() - 1);
                model.Saturday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Saturday, pts);
                model.SaturdayElse = model.Saturday.Last();
                model.Saturday.RemoveAt(model.Saturday.Count() - 1);
                model.Sunday = stadiumBO.FilterPriceTablesOfDoW(DayOfWeek.Sunday, pts);
                model.SundayElse = model.Sunday.Last();
                model.Sunday.RemoveAt(model.Sunday.Count() - 1);
                model.Fields = fp.Fields.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }


        public ActionResult Details(int? id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            try
            {
                StadiumModel model = new StadiumModel();
                model.Stadium = stadiumBO.GetStadiumById((int)id);

                if (!model.Stadium.IsActive || !(model.Stadium.Fields.Count() > 0))
                {
                    return RedirectToAction("Index");
                }

                if (model.Stadium != null)
                {
                    if (model.Stadium.StadiumRatings.Count > 0)
                    {
                        model.Rate = model.Stadium.StadiumRatings.Select(sr => sr.Rate).Average();
                        model.RateCount = model.Stadium.StadiumRatings.Count;
                    }
                    else
                    {
                        model.Rate = 0;
                        model.RateCount = 0;
                    }

                    if (model.Stadium.StadiumImages.Count > 0)
                    {
                        model.Images = model.Stadium.StadiumImages.ToList();
                    }

                    if (model.Stadium.FieldPrices.Count > 0)
                    {
                        model.Prices = model.Stadium.FieldPrices.ToList();
                    }

                    if (model.Stadium.StadiumReviews.Where(r => r.IsApproved == true).ToList().Count > 0)
                    {
                        model.Reviews = model.Stadium.StadiumReviews.Where(r => r.IsApproved == true).OrderByDescending(sr => sr.CreateDate).ToList();
                    }

                    model.Promotions = stadiumBO.GetAllPromotionsByStadium((int)id).Where(p => p.PromotionTo >= DateTime.Now).OrderBy(p => p.PromotionTo).ToList();

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Stadium");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Stadium");
            }
        }

        [Authorize]
        public ActionResult Rate()
        {
            try
            {
                UserBO userBO = new UserBO();

                StadiumRating rating = new StadiumRating()
                {
                    UserId = userBO.GetUserByUserName(User.Identity.Name).Id,
                    StadiumId = Int32.Parse(Request.QueryString["Stadium"]),
                    Rate = Int32.Parse(Request.QueryString["Point"])
                };

                StadiumBO stadiumBO = new StadiumBO();

                bool result = stadiumBO.UpdateStadiumRating(rating);

                return RedirectToAction("Details/" + rating.StadiumId, "Stadium");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Stadium");
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult Review(FormCollection form, int stadium)
        {
            try
            {
                string content = form["UserReviewContent"];

                if (!string.IsNullOrWhiteSpace(content))
                {
                    UserBO userBO = new UserBO();
                    bool isAdmin = false;
                    if (User.IsInRole("WebsiteMaster") || User.IsInRole("WebsiteStaff"))
                    {
                        isAdmin = true;
                    }
                    StadiumReview review = new StadiumReview()
                    {
                        UserId = userBO.GetUserByUserName(User.Identity.Name).Id,
                        StadiumId = stadium,
                        ReviewContent = content,
                        IsApproved = isAdmin,
                        CreateDate = DateTime.Now
                    };

                    StadiumBO stadiumBO = new StadiumBO();
                    bool result = stadiumBO.CreateStadiumReview(review);
                }
                TempData["reviewStatus"] = "Success";
                return Redirect("~/Stadium/Details/" + stadium + "#reviews");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Stadium");
            }
        }


        [Authorize]
        public ActionResult Book()
        {
            string strStadium = Request.QueryString["Stadium"];
            string strDate = Request.QueryString["StartDate"];
            string strTime = Request.QueryString["StartTime"];
            string strDuration = Request.QueryString["Duration"];
            string strType = Request.QueryString["FieldType"];
            int stadiumId;
            BookModel model = new BookModel();
            if (int.TryParse(strStadium, out stadiumId))
            {
                StadiumBO stadiumBO = new StadiumBO();
                model.Stadium = stadiumBO.GetStadiumById(stadiumId);
                if (model.Stadium != null)
                {
                    if (model.Stadium.IsActive && model.Stadium.Fields.Count() > 0)
                    {
                        model.UserInfo = new BookingUserInfo();
                        model.Options = new BookingOptions();

                        UserBO userBO = new UserBO();
                        var user = userBO.GetUserByUserName(User.Identity.Name);

                        model.UserInfo.FullName = user.FullName;
                        model.UserInfo.Address = user.Address;
                        model.UserInfo.Phone = user.PhoneNumber;
                        model.UserInfo.Email = user.Email;

                        if (!string.IsNullOrWhiteSpace(strDate) && !string.IsNullOrWhiteSpace(strTime) && !string.IsNullOrWhiteSpace(strDuration)
                            && !string.IsNullOrWhiteSpace(strType))
                        {
                            CultureInfo ci = new CultureInfo("vi-VN");
                            DateTime startDate;
                            double startTime = 0;
                            double duration;
                            int fieldType;
                            bool parseTime = false;
                            string[] times = strTime.Split(':');
                            if (times.Count() == 2)
                            {
                                int startHour;
                                int startMin;
                                if (int.TryParse(times[0].Trim(), out startHour) && int.TryParse(times[1].Trim(), out startMin))
                                {
                                    startTime = startHour + (startMin / 60.0);
                                    if (startTime >= 0 && startTime < 24)
                                    {
                                        parseTime = true;
                                    }
                                }
                            }
                            if (DateTime.TryParse(strDate, ci, DateTimeStyles.AssumeLocal, out startDate)
                                && parseTime && double.TryParse(strDuration, out duration)
                                && int.TryParse(strType, out fieldType))
                            {
                                var avails = stadiumBO.GetAvailableFieldsOfStadium(stadiumId, fieldType, startDate, startTime, duration);
                                model.Fields = avails.Fields;
                                model.Prices = avails.Prices;
                                model.Discounts = avails.Discounts;
                                model.Options.StartDate = strDate;
                                model.Options.StartTime = strTime;
                                model.Options.Duration = strDuration;
                                model.Options.FieldType = strType;
                            }
                            else
                            {
                                return Redirect("/Stadium/Book?Stadium=" + stadiumId);
                            }
                        }
                        else
                        {
                            model.Options.Duration = "1";
                            var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddHours(2), "SE Asia Standard Time");
                            model.Options.StartTime = now.Hour.ToString() + ":0";
                            model.Options.StartDate = now.ToShortDateString();
                            model.Options.FieldType = "5";
                            model.Options.ChosenField = "0";
                        }
                        return View(model);
                    }
                    else
                    {
                        model.Stadium.IsActive = false;
                        model.ErrorMessage = "Sân này đang không hoạt động. Xin chọn sân khác.";
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Book(FormCollection form)
        {

            string strStadium = form["StadiumId"];
            string strDate = form["StartDate"];
            string strTime = form["StartTime"];
            string strDuration = form["Duration"];
            string strType = form["FieldType"];
            string strField = form["Field"];
            string strNeedRival = form["NeedRival"];
            bool needRival = !string.IsNullOrEmpty(strNeedRival);
            int stadiumId;
            BookModel model = new BookModel();
            if (int.TryParse(strStadium, out stadiumId))
            {
                StadiumBO stadiumBO = new StadiumBO();
                model.Stadium = stadiumBO.GetStadiumById(stadiumId);
                if (model.Stadium != null)
                {
                    if (model.Stadium.IsActive)
                    {
                        model.UserInfo = new BookingUserInfo();
                        model.Options = new BookingOptions();

                        model.Options.NeedRival = needRival;

                        UserBO userBO = new UserBO();
                        var user = userBO.GetUserByUserName(User.Identity.Name);

                        model.UserInfo.FullName = form["FullName"];
                        model.UserInfo.Address = form["Address"];
                        model.UserInfo.Phone = form["PhoneNumber"];
                        model.UserInfo.Email = form["Email"];

                        if (!string.IsNullOrWhiteSpace(strDate) && !string.IsNullOrWhiteSpace(strTime) && !string.IsNullOrWhiteSpace(strDuration)
                            && !string.IsNullOrWhiteSpace(strType))
                        {
                            CultureInfo ci = new CultureInfo("vi-VN");
                            DateTime startDate;
                            double startTime = 0;
                            double duration;
                            int fieldType;
                            int field;
                            bool parseTime = false;
                            string[] times = strTime.Split(':');
                            if (times.Count() == 2)
                            {
                                int startHour;
                                int startMin;
                                if (int.TryParse(times[0].Trim(), out startHour) && int.TryParse(times[1].Trim(), out startMin))
                                {
                                    startTime = startHour + (startMin / 60.0);
                                    if (startTime >= 0 && startTime < 24)
                                    {
                                        parseTime = true;
                                    }
                                }
                            }
                            if (DateTime.TryParse(strDate, ci, DateTimeStyles.AssumeLocal, out startDate)
                                && parseTime && double.TryParse(strDuration, out duration)
                                && int.TryParse(strType, out fieldType) && int.TryParse(strField, out field))
                            {
                                model.Options.StartDate = strDate;
                                model.Options.StartTime = strTime;
                                model.Options.Duration = strDuration;
                                model.Options.FieldType = strType;
                                model.Options.ChosenField = field + "";
                                var avails = stadiumBO.GetAvailableFieldsOfStadium(stadiumId, fieldType, startDate, startTime, duration);
                                if (avails != null && avails.Fields.Count() != 0)
                                {
                                    model.Fields = avails.Fields;
                                    model.Prices = avails.Prices;
                                    model.Discounts = avails.Discounts;

                                    double price = 0;
                                    Field fa = null;
                                    double discount = 0;
                                    for (int i = 0; i < avails.Fields.Count(); i++)
                                    {
                                        if (avails.Fields[i].Id == field)
                                        {
                                            fa = avails.Fields[i];
                                            price = avails.Prices[i];
                                            discount = avails.Discounts[i];
                                        }
                                    }
                                    if (fa != null)
                                    {
                                        ReservationBO resBO = new ReservationBO();
                                        Reservation res = new Reservation();
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

                                        res.FieldId = field;
                                        res.UserId = user.Id;
                                        res.FullName = model.UserInfo.FullName;
                                        res.PhoneNumber = model.UserInfo.Phone;
                                        res.Email = model.UserInfo.Email;
                                        res.Date = startDate.Date;
                                        res.StartTime = startTime;
                                        res.Duration = duration;
                                        res.Price = price;
                                        res.Discount = discount;
                                        res.VerifyCode = verCode;
                                        res.CreatedDate = DateTime.Now;
                                        res.Status = "Pending";
                                        res.NeedRival = needRival;
                                        if (needRival)
                                        {
                                            res.RivalStatus = "Waiting";
                                        }

                                        int result = resBO.UserBooking(res);
                                        if (result == 0)
                                        {
                                            model.ErrorMessage = Resources.DB_Exception;
                                        }
                                        else if (result == -1)
                                        {
                                            model.ErrorMessage = "Không thể đặt sân ở thời điểm sớm hơn thời điểm hiện tại";
                                        }
                                        else if (result > 0)
                                        {
                                            model.SuccessMessage = "Bạn đã đặt sân thành công. Mã số xác nhận của bạn là: " + res.VerifyCode;
                                            return View("BookSuccess", model);
                                        }

                                    }
                                    else
                                    {
                                        model.ErrorMessage = "Sân bóng bạn chọn hiện không còn trống. Xin chọn sân khác.";
                                    }
                                }
                                else
                                {
                                    model.ErrorMessage = "Sân bóng bạn chọn hiện không còn trống. Xin chọn sân khác.";
                                }
                            }
                            else
                            {
                                model.Options.Duration = "1";
                                var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddHours(2), "SE Asia Standard Time");
                                model.Options.StartTime = now.Hour.ToString() + ":0";
                                model.Options.StartDate = now.ToShortDateString();
                                model.Options.FieldType = "5";
                                model.Options.ChosenField = "0";
                                model.ErrorMessage = "Bạn phải dùng mẫu bên dưới để đặt sân.";
                            }
                        }
                        else
                        {
                            model.Options.Duration = "1";
                            var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddHours(2), "SE Asia Standard Time");
                            model.Options.StartTime = now.Hour.ToString() + ":0";
                            model.Options.StartDate = now.ToShortDateString();
                            model.Options.FieldType = "5";
                            model.Options.ChosenField = "0";
                            model.ErrorMessage = "Bạn phải dùng mẫu bên dưới để đặt sân.";
                        }
                        return View(model);
                    }
                    else
                    {
                        model.ErrorMessage = "Sân này đang không hoạt động. Xin chọn sân khác.";
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult FindAvailableOfStadium(FormCollection form, int stadium)
        {
            if (User.Identity.IsAuthenticated)
            {
                string strType = form["FieldType"];
                string strDate = form["StartDate"];
                string strTime = form["StartTime"];
                string strDuration = form["Duration"];

                if (!string.IsNullOrWhiteSpace(strDate) && !string.IsNullOrWhiteSpace(strTime) && !string.IsNullOrWhiteSpace(strDuration)
                            && !string.IsNullOrWhiteSpace(strType))
                {
                    StadiumBO stadiumBO = new StadiumBO();
                    CultureInfo ci = new CultureInfo("vi-VN");
                    DateTime startDate;
                    double startTime = 0;
                    double duration;
                    int fieldType;
                    bool parseTime = false;
                    string[] times = strTime.Split(':');
                    if (times.Count() == 2)
                    {
                        int startHour;
                        int startMin;
                        if (int.TryParse(times[0].Trim(), out startHour) && int.TryParse(times[1].Trim(), out startMin))
                        {
                            startTime = startHour + (startMin / 60.0);
                            if (startTime >= 0 && startTime < 24)
                            {
                                parseTime = true;
                            }
                        }
                    }
                    if (DateTime.TryParse(strDate, ci, DateTimeStyles.AssumeLocal, out startDate)
                        && parseTime && double.TryParse(strDuration, out duration)
                        && int.TryParse(strType, out fieldType))
                    {
                        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddMinutes(2), "SE Asia Standard Time");
                        double time = now.Hour + (now.Minute / 60.0);
                        if (startDate.Date.CompareTo(now.Date) > 0 || ( startDate.Date.CompareTo(now.Date) == 0 && startTime > time))
                        {
                            var avails = stadiumBO.GetAvailableFieldsOfStadium(stadium, fieldType, startDate, startTime, duration);
                            if (avails != null && avails.Fields.Count() != 0)
                            {
                                var result = new
                                {
                                    Fields = avails.Fields.Select(f => new { Id = f.Id, Number = f.Number }),
                                    Prices = avails.Prices,
                                    Discounts = avails.Discounts
                                };
                                return Json(result);
                            }
                            else
                            {
                                return Json("NOTFOUND::Không tìm thấy sân trống");
                            }
                        }
                        else
                        {
                            return Json("ERROR::Không thể tìm sân trống ở thời điểm sớm hơn thời điểm hiện tại");
                        }
                    }
                    else
                    {
                        return Json("ERROR::Bạn phải dùng mẫu trong trang đặt sân để sử dụng chức năng này");
                    }
                }
                else
                {
                    return Json("ERROR::Bạn phải dùng mẫu trong trang đặt sân để sử dụng chức năng này");
                }
            }
            else
            {
                return Json("ERROR::Bạn phải đăng nhập để sử dụng chức năng này");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult QuickSearch()
        {
            QuickSearchModel model = new QuickSearchModel();
            model.FieldType = "5";
            model.StartTime = DateTime.Now.AddHours(2).Hour + ":" + "00";
            model.Date = DateTime.Now.ToShortDateString();
            return View(model);
        }

        [HttpPost]
        public ActionResult QuickSearch(FormCollection form)
        {
            QuickSearchModel model = new QuickSearchModel();
            model.FieldType = form["FieldType"];
            model.StartTime = form["StartTime"];
            model.Date = form["StartDate"];
            model.Duration = form["Duration"];
            model.District = form["District"];

            if (string.IsNullOrWhiteSpace(model.FieldType) || string.IsNullOrWhiteSpace(model.StartTime) || string.IsNullOrWhiteSpace(model.Date)
                || string.IsNullOrWhiteSpace(model.Duration) || string.IsNullOrWhiteSpace(model.District))
            {
                model.ErrorMessage = "Bạn hãy sử dụng mẫu bên dưới để tìm kiếm nhanh sân còn trống";
            }
            else
            {
                int fieldType;
                bool trueFieldType = int.TryParse(model.FieldType, out fieldType);
                if (trueFieldType)
                {
                    if (fieldType != 5 && fieldType != 7 && fieldType != 11)
                    {
                        trueFieldType = false;
                    }
                }

                DateTime startDate;
                bool trueDate = DateTime.TryParse(model.Date, out startDate);
                if (trueDate)
                {
                    if (startDate.CompareTo(DateTime.Now.Date) < 0)
                    {
                        trueDate = false;
                    }
                }

                bool isPast = false;
                double startTime = 0;
                bool trueStartTime = false;
                bool trueStartHour;
                int startHour;
                trueStartHour = int.TryParse(model.StartTime.Substring(0, model.StartTime.LastIndexOf(":")).Trim(), out startHour);
                int startMinute;
                bool trueStartMinute;
                trueStartMinute = int.TryParse(model.StartTime.Substring(model.StartTime.LastIndexOf(":") + 1).Trim(), out startMinute);
                if (trueStartHour && trueStartMinute)
                {
                    if (startDate.CompareTo(DateTime.Now.Date) == 0)
                    {
                        if (startHour < DateTime.Now.Hour)
                        {
                            isPast = true;
                        }
                        else if (startHour == DateTime.Now.Hour && startMinute <= DateTime.Now.Minute)
                        {
                            isPast = true;
                        }
                    }
                    startTime = startHour + (startMinute / 60.0);
                    if (startTime >= 0 && startTime < 24)
                    {
                        if ((startMinute / 60.0) != 0 && (startMinute / 60.0) != 0.5)
                        {
                            trueStartTime = false;
                        }
                        else
                        {
                            trueStartTime = true;
                        }
                    }
                    else
                    {
                        trueStartTime = false;
                    }
                }
                double duration;
                bool trueDuration;
                trueDuration = double.TryParse(model.Duration, out duration);
                if (trueDuration)
                {
                    if (duration >= 1 && duration <= 3)
                    {
                        double fr = duration - (int)duration;
                        if (fr != 0 && fr != 0.5)
                        {
                            trueDuration = false;
                        }
                        else
                        {
                            trueDuration = true;
                        }
                    }
                    else
                    {
                        trueDuration = false;
                    }
                }

                if (trueFieldType && trueStartTime && trueDate && trueDuration && !isPast)
                {
                    StadiumBO stadiumBO = new StadiumBO();
                    model.Results = stadiumBO.FindAvailableStadium(fieldType, startDate, startTime, duration, model.City, model.District);
                    if (model.Results == null || model.Results.Count == 0)
                    {
                        model.SearchResultString = "Không tìm thấy sân bóng còn trống theo thông tin bạn yêu cầu";
                    }
                }
                else if (!trueFieldType || !trueStartTime || !trueDate || !trueDuration)
                {
                    model.ErrorMessage = "Bạn hãy sử dụng mẫu bên dưới để tìm kiếm nhanh sân còn trống";
                }
                else if (isPast)
                {
                    model.ErrorMessage = "Không thể đặt sân ở thời điểm trước thời điểm hiện tại";
                }
            }

            return View(model);
        }
        //
        // GET: /Stadium/JoinUs
        [Authorize]
        public ActionResult JoinUs()
        {
            UserBO userBO = new UserBO();
            User curUser = userBO.GetUserByUserName(User.Identity.Name);
            JoinUsModel rs = new JoinUsModel
            {
                FullName = curUser.FullName,
                Address = curUser.Address,
                Phone = curUser.PhoneNumber,
                Email = curUser.Email
            };
            return View(rs);
        }

        //
        // POST: /Stadium/JoinUs
        [Authorize]
        [HttpPost]
        public ActionResult JoinUs(FormCollection form)
        {
            JoinUsModel jsm = new JoinUsModel
            {
                FullName = form["FullName"],
                Address = form["Address"],
                Phone = form["PhoneNumber"],
                Email = form["Email"],
                StadiumName = form["StadiumName"],
                StadiumStreet = form["StadiumStreet"],
                StadiumWard = form["StadiumWard"],
                StadiumDistrict = form["StadiumDistrict"],
                Note = form["Note"]
            };

            if (string.IsNullOrWhiteSpace(jsm.FullName) || string.IsNullOrWhiteSpace(jsm.Address) || string.IsNullOrWhiteSpace(jsm.Phone) ||
                string.IsNullOrWhiteSpace(jsm.Email) || string.IsNullOrWhiteSpace(jsm.StadiumName) || string.IsNullOrWhiteSpace(jsm.StadiumStreet) ||
                string.IsNullOrWhiteSpace(jsm.StadiumWard) || string.IsNullOrWhiteSpace(jsm.StadiumDistrict))
            {
                jsm.ErrorMessage = Resources.Form_EmptyFields;
                return View(jsm);
            }
            else
            {
                UserBO userBO = new UserBO();
                User curUser = userBO.GetUserByUserName(User.Identity.Name);
                JoinSystemRequest jsr = new JoinSystemRequest
                {
                    FullName = jsm.FullName,
                    Address = jsm.Address,
                    Phone = jsm.Phone,
                    Email = jsm.Email,
                    StadiumName = jsm.StadiumName,
                    StadiumAddress = jsm.StadiumStreet + ", " + jsm.StadiumWard + ", " + jsm.StadiumDistrict,
                    Note = jsm.Note,
                    UserId = curUser.Id
                };
                StadiumBO stadiumBO = new StadiumBO();
                int result = stadiumBO.RegisterNewStadium(jsr);

                if (result > 0)
                {
                    return View("JoinUsSuccess");
                }
                else
                {
                    jsm.ErrorMessage = Resources.DB_Exception;
                    return View(jsm);
                }
            }
        }


        public ActionResult Rivals()
        {
            ReservationBO resvBO = new ReservationBO();

            List<Reservation> rivalList = resvBO.GetReservationsNeedRival();

            if (rivalList.Count == 0)
            {
                rivalList = null;
            }

            return View(rivalList);
        }

        [HttpPost]
        public ActionResult Rivals(FormCollection form)
        {
            DateTime date = new DateTime(1, 1, 1, 0, 0, 0);
            try
            {
                date = DateTime.Parse(form["Date"], new CultureInfo("vi-VN"));
            }
            catch (Exception)
            {
            }

            string userInfo = form["UserInfo"];
            int fieldType;
            try
            {
                fieldType = Int32.Parse(form["FieldType"]);
            }
            catch (Exception)
            {
                fieldType = 0;
            }

            ReservationBO resvBO = new ReservationBO();

            List<Reservation> rivalList = resvBO.GetReservationsNeedRival(userInfo, date, fieldType);

            return View(rivalList);
        }

        public ActionResult JoinRival(int? id)
        {
            try
            {
                ReservationBO resvBO = new ReservationBO();
                UserBO userBO = new UserBO();
                StadiumBO stadiumBO = new StadiumBO();

                JoinRivalModel model = new JoinRivalModel();
                model.Resv = resvBO.GetReservationNeedRivalById((int)id);
                model.UserInfo = userBO.GetUserByUserName(User.Identity.Name);
                model.ErrorMessages = new List<string>();
                if (model.Resv != null)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Rivals", "Stadium");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Rivals", "Stadium");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult JoinRival(FormCollection form, int id)
        {
            try
            {
                ReservationBO resvBO = new ReservationBO();
                JoinRivalModel model = new JoinRivalModel();
                model.ErrorMessages = new List<string>();
                model.Resv = resvBO.GetReservationNeedRivalById(id);

                UserBO userBO = new UserBO();
                model.UserInfo = userBO.GetUserByUserName(User.Identity.Name);
                int userId = model.UserInfo.Id;
                string fullName = form["RivalName"];
                string phone = form["RivalPhone"];
                string email = form["RivalEmail"];

                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
                {
                    model.ErrorMessages.Add(Resources.Form_EmptyFields);
                }

                if (model.ErrorMessages.Count == 0)
                {
                    User user = new User()
                    {
                        Id = userId,
                        UserName = User.Identity.Name,
                        FullName = fullName,
                        PhoneNumber = phone,
                        Email = email
                    };

                    int result = resvBO.UpdateReservationRival(id, user);

                    model.Resv.RivalId = userId;
                    model.Resv.RivalName = fullName;
                    model.Resv.RivalPhone = phone;
                    model.Resv.RivalEmail = email;

                    if (result > 0)
                    {
                        return RedirectToAction("Rivals", "Stadium");
                    }
                    else if (result == 0)
                    {
                        model.ErrorMessages.Add(Resources.DB_Exception);
                    }
                    else if (result == -1)
                    {
                        model.ErrorMessages.Add(Resources.Resv_RivalAvailable);
                    }
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Rivals", "Stadium");
            }
        }
    }
}
