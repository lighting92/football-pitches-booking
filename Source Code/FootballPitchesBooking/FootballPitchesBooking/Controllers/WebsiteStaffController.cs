using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    [Authorize(Roles="WebsiteStaff, WebsiteMaster")]
    public class WebsiteStaffController : Controller
    {
        //
        // GET: /WebsiteStaff/

        public ActionResult Index()
        {
            return View();
        }

    }
}
