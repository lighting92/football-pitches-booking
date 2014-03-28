using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.RecommendationModels;
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
            
            var model = new RecommendationModel();            
            
            if (User.Identity.IsAuthenticated)
            {
                RecommendationBO recBO = new RecommendationBO();
                var a = recBO.FindAppropriateStadium(User.Identity.Name, 0.5, 0.3, 0.2);
                if (a.Count() > 2)
                {
                    a = a.Take(2).ToList();
                }                
                var b = recBO.FindAppropriateStadium(User.Identity.Name, 0.2, 0.3, 0.5);
                if (b.Count() > 2)
                {
                    b = b.Take(2).ToList();
                }
                model.Stadiums = a;
                model.StadiumImages = new List<Models.StadiumImage>();
                foreach (var item in model.Stadiums)
                {
                    model.StadiumImages.Add(item.Stadium.StadiumImages.FirstOrDefault());
                }
                model.PromotionStadiums = b;
                model.PromotionStadiumImages = new List<StadiumImage>();
                foreach (var item in model.PromotionStadiums)
                {
                    model.PromotionStadiumImages.Add(item.Stadium.StadiumImages.FirstOrDefault());
                }
            }         
            return View(model);
        }

    }
}
