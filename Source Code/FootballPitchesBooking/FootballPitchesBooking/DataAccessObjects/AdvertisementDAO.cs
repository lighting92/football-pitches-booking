using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class AdvertisementDAO
    {
        public Advertisement GetAdvertisementById(int adsId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Advertisements.Where(a => a.Id == adsId).FirstOrDefault();
        }


        public List<Advertisement> GetAllAds()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Advertisements.OrderByDescending(a => a.ExpiredDate).ToList();
        }


        public Advertisement GetAdvertisement(int id, string position)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Advertisements.Where(a => a.Id != id && a.Position == position &&
                                           a.ExpiredDate >= DateTime.Now && a.Status == "Active").FirstOrDefault();
        }


        public int CreateAdvertisement(Advertisement ads)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.Advertisements.InsertOnSubmit(ads);
                db.SubmitChanges();
                return ads.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateAdvertisement(Advertisement ads)
        {
            FPBDataContext db = new FPBDataContext();
            Advertisement currentAdvertisement = db.Advertisements.Where(a => a.Id == ads.Id).FirstOrDefault();
            currentAdvertisement.Position = ads.Position;
            currentAdvertisement.AdvertiseDetail = ads.AdvertiseDetail;
            currentAdvertisement.ExpiredDate = ads.ExpiredDate;
            currentAdvertisement.Status = ads.Status;

            try
            {
                db.SubmitChanges();
                return ads.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int UpdateAdvertisementStatus(int adsId, string status)
        {
            FPBDataContext db = new FPBDataContext();
            Advertisement currentAdvertisement = db.Advertisements.Where(a => a.Id == adsId).FirstOrDefault();
            currentAdvertisement.Status = status;

            try
            {
                db.SubmitChanges();
                return adsId;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int DeleteAdvertisement(int adsId)
        {
            FPBDataContext db = new FPBDataContext();
            Advertisement ads = db.Advertisements.Where(a => a.Id == adsId).FirstOrDefault();

            try
            {
                db.Advertisements.DeleteOnSubmit(ads);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}