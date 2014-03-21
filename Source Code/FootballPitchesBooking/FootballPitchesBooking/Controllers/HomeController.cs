using FootballPitchesBooking.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            RecommendationBO recBO = new RecommendationBO();
            recBO.FindAppropriateStadium(User.Identity.Name, 5, 3, 2);
            return View();
        }

    }
}
