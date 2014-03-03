using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.BusinessObjects;
using PagedList;

namespace FootballPitchesBooking.Controllers
{
    //[Authorize(Roles="WebsiteStaff, WebsiteMaster")]
    public class WebsiteStaffController : Controller
    {
        UserBO userBO = new UserBO();

        //
        // GET: /WebsiteStaff/

        public ActionResult Index(int? page, string keyWord = "", string column = "", string sort = "")
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
                return RedirectToAction("Index", "Error", new { Area = "" });
            }
        }

    }
}
