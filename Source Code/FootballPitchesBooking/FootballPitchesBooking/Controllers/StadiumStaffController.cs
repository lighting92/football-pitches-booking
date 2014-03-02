using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    [Authorize(Roles="StadiumStaff, StadiumMaster")]
    public class StadiumStaffController : Controller
    {
        //
        // GET: /StadiumStaff/

        public ActionResult Index()
        {
            return View();
        }

    }
}
