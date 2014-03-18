﻿using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models.StadiumModels;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FootballPitchesBooking.Properties;

namespace FootballPitchesBooking.Controllers
{
    public class StadiumController : Controller
    {
        //
        // GET: /Stadium/

        public ActionResult Index()
        {
            return View();
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
            model.City = form["City"];
            model.District = form["District"];

            if (string.IsNullOrEmpty(model.FieldType) || string.IsNullOrEmpty(model.StartTime) || string.IsNullOrEmpty(model.Date)
                || string.IsNullOrEmpty(model.Duration) || string.IsNullOrEmpty(model.City) || string.IsNullOrEmpty(model.District))
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

            if (string.IsNullOrEmpty(jsm.FullName) || string.IsNullOrEmpty(jsm.Address) || string.IsNullOrEmpty(jsm.Phone) ||
                string.IsNullOrEmpty(jsm.Email) || string.IsNullOrEmpty(jsm.StadiumName) || string.IsNullOrEmpty(jsm.StadiumStreet) ||
                string.IsNullOrEmpty(jsm.StadiumWard) || string.IsNullOrEmpty(jsm.StadiumDistrict))
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
    }
}
