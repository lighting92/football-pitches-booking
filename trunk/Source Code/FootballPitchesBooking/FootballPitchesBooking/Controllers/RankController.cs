using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Properties;
using PagedList;

namespace FootballPitchesBooking.Controllers
{
    public class RankController : Controller
    {
        //
        // GET: /Rank/

        UserBO userBO = new UserBO();

        public ActionResult Index(int? page, string keyWord = "", string column = "", string sort = "")
        {
            try
            {
                var noList = new List<NoModel>();
                var ranks = userBO.ToListMR(ref noList, page, keyWord, column, sort);

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
                    ? (ActionResult)PartialView("_List")
                    : View();
            }
            catch (Exception)
            {
                // Wrtite to log file.
                return RedirectToAction("Users", "Error", new { Area = "" });
            }
        }
    }
}
