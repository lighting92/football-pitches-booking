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
                    promotionStadiums = promotionStadiums.Take(3).ToList();
                }

                
                model.BestStadiums = bestStadiums;
                model.BestStadiumsImages = new List<Models.StadiumImage>();
                foreach (var item in model.BestStadiums)
                {
                    var image = item.Stadium.StadiumImages.FirstOrDefault();
                    if (image == null)
                    {
                        image = new StadiumImage();
                        image.Path = "stadium.jpg";
                    }
                    model.BestStadiumsImages.Add(image);
                }

                model.NearestStadiums = nearestStadiums;
                model.NearestStadiumsImages = new List<Models.StadiumImage>();
                foreach (var item in model.NearestStadiums)
                {
                    var image = item.Stadium.StadiumImages.FirstOrDefault();
                    if (image == null)
                    {
                        image = new StadiumImage();
                        image.Path = "stadium.jpg";
                    }
                    model.NearestStadiumsImages.Add(image);
                }

                model.PromotionStadiums = promotionStadiums;
                model.PromotionStadiumsImages = new List<Models.StadiumImage>();
                foreach (var item in model.PromotionStadiums)
                {
                    var image = item.Stadium.StadiumImages.FirstOrDefault();
                    if (image == null)
                    {
                        image = new StadiumImage();
                        image.Path = "stadium.jpg";
                    }
                    model.PromotionStadiumsImages.Add(image);
                }
            }         
            return View(model);
        }

    }
}
