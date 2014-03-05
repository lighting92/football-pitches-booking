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

        public ActionResult ListStadiums()
        {
            StadiumBO stadiumBO = new StadiumBO();
            List<Stadium> listStadiums = new List<Stadium>();
            listStadiums = stadiumBO.GetAllStadiums();
            return View(listStadiums);
        }

        public ActionResult AddStadium()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStadium(FormCollection form)
        {
            AddStadiumModel add = new AddStadiumModel();
            add.Owner = form["Owner"];
            add.Name = form["StadiumName"];
            add.Street = form["Street"];
            add.Ward = form["Ward"];
            add.District = form["District"];
            add.City = form["City"];
            add.PhoneNumber = form["PhoneNumber"];
            add.Email = form["Email"];
            add.Description = form["Description"];
            add.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(add.Owner) || string.IsNullOrEmpty(add.Name) || string.IsNullOrEmpty(add.Street) ||
                string.IsNullOrEmpty(add.Ward) || string.IsNullOrEmpty(add.District) || string.IsNullOrEmpty(add.City) ||
                string.IsNullOrEmpty(add.PhoneNumber) || string.IsNullOrEmpty(add.Email) || string.IsNullOrEmpty(add.Description))
            {
                add.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (add.ErrorMessages.Count == 0)
            {
                StadiumBO stadiumBO = new StadiumBO();

                Stadium stadium = new Stadium
                {
                    Name = add.Name,
                    Street = add.Street,
                    Ward = add.Ward,
                    District = add.District,
                    City = add.City,
                    Phone = add.PhoneNumber,
                    Email = add.Email,
                    Description = add.Description
                };

                List<int> results = stadiumBO.CreateStadium(stadium, add.Owner);

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
                            add.ErrorMessages.Add(Resources.DB_Exception);
                        }
                        else if (error == -1)
                        {
                            add.ErrorMessages.Add(Resources.User_UserNotMatched);
                        }
                    }
                }
            }
            return View(add);
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
                   StadiumAddress = jsm.StadiumStreet + ", Phường " + jsm.StadiumWard + ", Quận " + jsm.StadiumDistrict,
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
