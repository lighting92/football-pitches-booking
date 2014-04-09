using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class WebsiteBO
    {
        public List<Advertisement> GetAllAds()
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.GetAllAds();
        }


        public Advertisement GetAdvertisementById(int adsId)
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.GetAdvertisementById(adsId);
        }


        public int CreateAdvertisement(Advertisement ads)
        {
            if (ads.ExpiredDate < DateTime.Now)
            {
                return -1;
            }

            AdvertisementDAO adsDAO = new AdvertisementDAO();

            if (ads.Status == "Active")
            {
                if(adsDAO.GetAdvertisement(0, ads.Position) != null)
                {
                    return -2;
                }
            }

            int result = adsDAO.CreateAdvertisement(ads);

            return result;
        }

        public int UpdateAdvertisement(Advertisement ads)
        {
            if (ads.ExpiredDate < DateTime.Now)
            {
                return -1;
            }

            AdvertisementDAO adsDAO = new AdvertisementDAO();

            if (ads.Status == "Active")
            {
                if (adsDAO.GetAdvertisement(ads.Id, ads.Position) != null)
                {
                    return -2;
                }
            }

            int result = adsDAO.UpdateAdvertisement(ads);

            return result;
        }


        public int DeleteAdvertisement(int adsId)
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.DeleteAdvertisement(adsId);
        }
    }
}