using System;
using System.Collections.Generic;
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


        #region STADIUMS MANAGEMENT

        public ActionResult Stadiums()
        {
            return View();
        }

        #endregion STADIUMS MANAGEMENT
        
        #region RESERVATION MANAGEMENT

        public ActionResult Reservations()
        {
            return View();
        }

        #endregion RESERVATION MANAGEMENT

        #region PROMOTION MANAGEMENT

        public ActionResult Promotions()
        {
            return View();
        }

        #endregion PROMOTION MANAGEMENT

    }
}
