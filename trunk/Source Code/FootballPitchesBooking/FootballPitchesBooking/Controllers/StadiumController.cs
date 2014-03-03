using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumModels;
using FootballPitchesBooking.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                StadiumAddress = form["StadiumAddress"],
                Note = form["Note"]
            };

           if (string.IsNullOrEmpty(jsm.FullName) || string.IsNullOrEmpty(jsm.Address) || string.IsNullOrEmpty(jsm.Phone) ||
               string.IsNullOrEmpty(jsm.Email) || string.IsNullOrEmpty(jsm.StadiumName) || string.IsNullOrEmpty(jsm.StadiumAddress))
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
                   StadiumAddress = jsm.StadiumAddress,
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
