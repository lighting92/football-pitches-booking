using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.HomeModels;
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
                if (nearestStadiums == null)
                {
                    nearestStadiums = new List<RecommendStadium>();
                }
                if (nearestStadiums.Count() > 3)
                {
                    nearestStadiums = nearestStadiums.Take(3).ToList();
                }

                var promotionStadiums = recBO.FindPromotedStadiums(User.Identity.Name, distanceList);
                if (promotionStadiums == null)
                {
                    promotionStadiums = new List<RecommendStadium>();
                }
                if (promotionStadiums.Count() > 3)
                {
                    promotionStadiums = promotionStadiums.Take(3).ToList();
                }

                var mostBookedStadiums = recBO.FindMostBookedStadiums(User.Identity.Name, distanceList);
                if (mostBookedStadiums == null)
                {
                    mostBookedStadiums = new List<RecommendStadium>();
                }
                if (mostBookedStadiums.Count() > 3)
                {
                    mostBookedStadiums = mostBookedStadiums.Take(3).ToList();
                }

                var rivals = recBO.FindRivals(User.Identity.Name, distanceList);
                if (rivals == null)
                {
                    rivals = new List<RecommendRivalModel>();
                }
                if (rivals.Count() > 3)
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

        public ActionResult ListBestStadium()
        { 
        
            var model = new RecommendationModel();
            if (User.Identity.IsAuthenticated)
            {
                string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
                var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, xmlFolderPath);

                RecommendationBO recBO = new RecommendationBO();
                var bestStadiums = recBO.FindBestStadiums(User.Identity.Name, distanceList);
                if (bestStadiums.Count() != 0)
                {
                    bestStadiums = bestStadiums.ToList();
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

            }
            return View(model);
        }

        public ActionResult ListNearestStadiums()
        {

            var model = new RecommendationModel();
            if (User.Identity.IsAuthenticated)
            {
                string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
                var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, xmlFolderPath);

                RecommendationBO recBO = new RecommendationBO();
                var nearestStadiums = recBO.FindNearestStadiums(User.Identity.Name, distanceList);
                if (nearestStadiums.Count() != 0)
                {
                    nearestStadiums = nearestStadiums.ToList();
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

            }
            return View(model);
        }

        public ActionResult ListMostBookedStadiums()
        {

            var model = new RecommendationModel();
            if (User.Identity.IsAuthenticated)
            {
                string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
                var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, xmlFolderPath);

                RecommendationBO recBO = new RecommendationBO();
                var mostBookedStadiums = recBO.FindMostBookedStadiums(User.Identity.Name, distanceList);
                if (mostBookedStadiums.Count() != 0)
                {
                    mostBookedStadiums = mostBookedStadiums.ToList();
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

        public ActionResult ListPromotedStadiums()
        {

            var model = new RecommendationModel();
            if (User.Identity.IsAuthenticated)
            {
                string xmlFolderPath = Server.MapPath("/XMLUserDistance/");
                var distanceList = MapBO.GetStadiumDistanceFromUser(User.Identity.Name, xmlFolderPath);

                RecommendationBO recBO = new RecommendationBO();
                var mostPromotedStadiums = recBO.FindPromotedStadiums(User.Identity.Name, distanceList);
                if (mostPromotedStadiums.Count() != 0)
                {
                    mostPromotedStadiums = mostPromotedStadiums.ToList();
                }
                model.PromotionStadiums = mostPromotedStadiums;
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

        public ActionResult Advertise()
        {
            List<Advertisement> model = new List<Advertisement>();
            WebsiteBO webBO = new WebsiteBO();
            model = webBO.GetActiveAds();
            return PartialView(model);
        }
                

        public ActionResult AboutUs()
        {
            UserBO userBO = new UserBO();
            List<MemberRank> ranks = userBO.GetAllRanks();
            return View(ranks);
        }

        public ActionResult Feedback()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Feedback(FormCollection form)
        {
            string content = "";
            string subject = "";
            FeedbackModel model = new FeedbackModel();
            if (!User.Identity.IsAuthenticated)
            {
                model.FullName = form["FullName"];
                model.Email = form["Email"];
                content += "Khách viếng thăm\n";
                subject = string.Concat("[Góp ý] Ý kiến đóng góp xây dựng website từ khách viếng thăm");
            }
            else
            {
                UserBO userBO = new UserBO();
                User user = userBO.GetUserByUserName(User.Identity.Name);
                model.FullName = user.FullName;
                model.Email = user.Email;
                content += "Thành viên: " + user.UserName + "\n";
                subject = string.Concat("[Góp ý] Ý kiến đóng góp xây dựng website từ người dùng ", user.UserName);
            }

            model.Feedback = form["FeedbackContent"];
            
            content += "Tên đầy đủ: " + model.FullName + "\n";
            content += "Email: " + model.Email + "\n\n";
            content += "Nội dung:\n";
            content += model.Feedback;

            WebsiteBO websiteBO = new WebsiteBO();

            model.Result = websiteBO.SendEmail(null, content, subject, false);

            return View(model);
        }
    }
}
