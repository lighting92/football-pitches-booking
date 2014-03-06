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
        public ActionResult ListStadiums()
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<Stadium> listStadiums = new List<Stadium>();
            listStadiums = stadiumBO.GetAllStadiums();
            return View(listStadiums);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddStadium()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStadium(FormCollection form)
        {
            StadiumModel std = new StadiumModel();
            std.Owner = form["Owner"];
            std.Name = form["StadiumName"];
            std.Street = form["Street"];
            std.Ward = form["Ward"];
            std.District = form["District"];
            std.City = form["City"];
            std.PhoneNumber = form["PhoneNumber"];
            std.Email = form["Email"];
            std.Description = form["Description"];
            std.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(std.Owner) || string.IsNullOrEmpty(std.Name) || string.IsNullOrEmpty(std.Street) ||
                string.IsNullOrEmpty(std.Ward) || string.IsNullOrEmpty(std.District) || string.IsNullOrEmpty(std.City) ||
                string.IsNullOrEmpty(std.PhoneNumber) || string.IsNullOrEmpty(std.Email) || string.IsNullOrEmpty(std.Description))
            {
                std.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (std.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();

                Stadium stadium = new Stadium
                {
                    Name = std.Name,
                    Street = std.Street,
                    Ward = std.Ward,
                    District = std.District,
                    City = std.City,
                    Phone = std.PhoneNumber,
                    Email = std.Email,
                    Description = std.Description
                };

                List<int> results = stadiumBO.CreateStadium(stadium, std.Owner);

                if (results.Count == 2 && results[1] > 0)
                {
                    return RedirectToAction("ListStadiums", "Stadium");
                }
                else
                {
                    foreach (var error in results)
                    {
                        if (error == 0) 
                        {
                            std.ErrorMessages.Add(Resources.DB_Exception);
                        }
                        else if (error == -1)
                        {
                            std.ErrorMessages.Add(Resources.User_UserNotMatched);
                        }
                    }
                }
            }
            return View(std);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStadium(int? s)
        {
            if (s == null)
            {
                return RedirectToAction("ListStadiums", "Stadium");
            }
            else
            {
                StadiumBO stadiumBO = new StadiumBO();
                Stadium stadium = stadiumBO.GetStadiumById((int)s);
                StadiumModel std = new StadiumModel 
                {
                    Id = stadium.Id,
                    Name = stadium.Name,
                    Street = stadium.Street,
                    Ward = stadium.Ward,
                    District = stadium.District,
                    City = stadium.City,
                    PhoneNumber = stadium.Phone,
                    Email = stadium.Email,
                    Description = stadium.Description,
                    IsActive = stadium.IsActive,
                    ErrorMessages = new List<string>()
                };
                return View(std);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditStadium(FormCollection form)
        {
            StadiumModel std = new StadiumModel();
            std.Id = int.Parse(form["StadiumId"]);
            std.Name = form["StadiumName"];
            std.Street = form["Street"];
            std.Ward = form["Ward"];
            std.District = form["District"];
            std.City = form["City"];
            std.PhoneNumber = form["PhoneNumber"];
            std.Email = form["Email"];
            std.Description = form["Description"];
            string aa = form["IsActive"];
            //std.IsActive = bool.Parse(form["IsActive"]);
            std.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(std.Name) || string.IsNullOrEmpty(std.Street) || string.IsNullOrEmpty(std.Ward) || 
                string.IsNullOrEmpty(std.District) || string.IsNullOrEmpty(std.City) || string.IsNullOrEmpty(std.PhoneNumber) || 
                string.IsNullOrEmpty(std.Email) || string.IsNullOrEmpty(std.Description))
            {
                std.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (std.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();

                Stadium stadium = new Stadium
                {
                    Id = std.Id,
                    Name = std.Name,
                    Street = std.Street,
                    Ward = std.Ward,
                    District = std.District,
                    City = std.City,
                    Phone = std.PhoneNumber,
                    Email = std.Email,
                    Description = std.Description,
                    //IsActive = std.IsActive
                };

                int results = stadiumBO.UpdateStadiumProfiles(stadium);

                if (results > 0)
                {
                    return RedirectToAction("ListStadiums", "Stadium");
                }
                else if (results == 0)
                {
                    std.ErrorMessages.Add(Resources.DB_Exception);
                }
            }
            return View(std);
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
                FullName =  form["FullName"],
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
