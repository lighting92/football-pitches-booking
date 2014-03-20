using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class FieldPriceDAO
    {
        public List<FieldPrice> GetAllFieldPriceOfStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.FieldPrices.Where(f => f.StadiumId == stadiumId).ToList();
        }

        public int CreateFieldPrice(FieldPrice fp)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.FieldPrices.InsertOnSubmit(fp);
                db.SubmitChanges();
                return fp.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateFieldPrice(FieldPrice fp)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var fieldPrice = db.FieldPrices.Where(f => f.Id == fp.Id).FirstOrDefault();
                fieldPrice.FieldPriceName = fp.FieldPriceName;
                fieldPrice.FieldPriceDescription = fp.FieldPriceDescription;
                db.PriceTables.DeleteAllOnSubmit(fieldPrice.PriceTables);
                fieldPrice.PriceTables = fp.PriceTables;
                db.SubmitChanges();
                return fieldPrice.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<PriceTable> GetAllPriceTableOfFieldPrice(int fieldPriceId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.PriceTables.Where(p => p.FieldPriceId == fieldPriceId).ToList();
        }

        public FieldPrice GetFieldPriceById(int fieldPriceId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.FieldPrices.Where(f => f.Id == fieldPriceId).FirstOrDefault();
        }
    }
}