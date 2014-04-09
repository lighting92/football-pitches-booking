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
                string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
                var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, xmlFolderPath);

                RecommendationBO recBO = new RecommendationBO();
                var bestStadiums = recBO.FindBestStadiums(User.Identity.Name, distanceList);
                if (bestStadiums.Count() > 3)
                {
                    bestStadiums = bestStadiums.Take(3).ToList();
                }

                var nearestStadiums = recBO.FindNearestStadiums(User.Identity.Name, distanceList);
                if (nearestStadiums.Count() > 3)
                {
                    nearestStadiums = nearestStadiums.Take(3).ToList();
                }

                var promotionStadiums = recBO.FindPromotedStadiums(User.Identity.Name, distanceList);
                if (promotionStadiums != null && promotionStadiums.Count() > 3)
                {
                    promotionStadiums = promotionStadiums.Take(3).ToList();
                }

                var mostBookedStadiums = recBO.FindMostBookedStadiums(User.Identity.Name, distanceList);
                if (mostBookedStadiums != null && mostBookedStadiums.Count() > 3)
                {
                    mostBookedStadiums = mostBookedStadiums.Take(3).ToList();
                }

                var rivals = recBO.FindRivals(User.Identity.Name, distanceList);
                if (rivals != null && rivals.Count() > 3)
                {
                    rivals = rivals.Take(3).ToList();
                }
                model.Rivals = rivals;
                

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

                model.MostBookedStadiums = mostBookedStadiums;
                model.MostBookedStadiumsImages = new List<Models.StadiumImage>();
                foreach (var item in model.MostBookedStadiums)
                {
                    var image = item.Stadium.StadiumImages.FirstOrDefault();
                    if (image == null)
                    {
                        image = new StadiumImage();
                        image.Path = "stadium.jpg";
                    }
                    model.MostBookedStadiumsImages.Add(image);
                }
            }         
            return View(model);
        }

        public ActionResult Advertise()
        {
            List<Advertisement> model = new List<Advertisement>();
            WebsiteBO webBO = new WebsiteBO();
            model = webBO.GetActiveAds();
            return PartialView(model);
        }
    }
}
