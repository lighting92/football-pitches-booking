using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumImageDAO
    {
        public int InsertListStadiumImages(List<StadiumImage> sis)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.StadiumImages.InsertAllOnSubmit(sis);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteAllImageOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            var imgs = db.StadiumImages.Where(si => si.StadiumId == stadiumId).ToList();
            try
            {
                db.StadiumImages.DeleteAllOnSubmit(imgs);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public string DeleteStadiumImage(int imgId)
        {
            FPBDataContext db = new FPBDataContext();
            var img = db.StadiumImages.Where(si => si.Id == imgId).FirstOrDefault();
            var path = img.Path;
            try
            {
                db.StadiumImages.DeleteOnSubmit(img);
                db.SubmitChanges();
                return path;
            }
            catch (Exception)
            {
                return "0";
            }   
        }

        public List<StadiumImage> GetAllImageOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumImages.Where(si => si.StadiumId == stadiumId).ToList();
        }
    }
}