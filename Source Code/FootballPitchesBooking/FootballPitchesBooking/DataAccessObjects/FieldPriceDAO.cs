using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class FieldPriceDAO
    {
        public FieldPrice GetFieldPriceById(int fieldPriceId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.FieldPrices.Where(f => f.Id == fieldPriceId).FirstOrDefault();
        }

        public List<FieldPrice> GetFieldPricesByField(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.FieldPrices.Where(f => f.FieldId == fieldId).ToList();
        }

        public int InsertListFieldPrice(List<FieldPrice> fieldPrice)
        {
            FPBDataContext db = new FPBDataContext();
            
            try
            {
                db.FieldPrices.InsertAllOnSubmit(fieldPrice);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateFieldPrice(FieldPrice fieldPrice)
        {
            FPBDataContext db = new FPBDataContext();
            FieldPrice currentFieldPrice = db.FieldPrices.Where(f => f.Id == fieldPrice.Id).FirstOrDefault();
            currentFieldPrice.Price = fieldPrice.Price;
            currentFieldPrice.TimeFrom = fieldPrice.TimeFrom;
            currentFieldPrice.TimeTo = fieldPrice.TimeTo;
            currentFieldPrice.Day = fieldPrice.Day;

            try
            {
                db.SubmitChanges();
                return currentFieldPrice.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteFieldPrice(int fieldPriceId)
        {
            FPBDataContext db = new FPBDataContext();
            FieldPrice fieldPrice = db.FieldPrices.Where(s => s.Id == fieldPriceId).FirstOrDefault();
            try
            {
                db.FieldPrices.DeleteOnSubmit(fieldPrice);
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