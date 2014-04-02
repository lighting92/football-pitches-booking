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
            string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
            //var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, "xmlFolderPath");
            var model = new RecommendationModel();            
            
            if (User.Identity.IsAuthenticated)
            {
                RecommendationBO recBO = new RecommendationBO();
                var bestStadiums = recBO.FindBestStadiums(User.Identity.Name);
                if (bestStadiums.Count() > 3)
                {
                    bestStadiums = bestStadiums.Take(3).ToList();
                }

                var nearestStadiums = recBO.FindAppropriateStadiums(User.Identity.Name);
                if (nearestStadiums.Count() > 3)
                {
                    nearestStadiums = nearestStadiums.Take(3).ToList();
                }

                var promotionStadiums = recBO.FindPromotedStadiums(User.Identity.Name);
                if (promotionStadiums != null && promotionStadiums.Count() > 3)
                {
                    nearestStadiums = promotionStadiums.Take(3).ToList();
                }

                model.Stadiums = bestStadiums;
                model.StadiumImages = new List<Models.StadiumImage>();
                foreach (var item in model.Stadiums)
                {
                    model.StadiumImages.Add(item.Stadium.StadiumImages.FirstOrDefault());
                }
                model.PromotionStadiums = promotionStadiums;
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
