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

        public ActionResult AddMemberRank()
        {
            return View();
        }

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
    }
}
