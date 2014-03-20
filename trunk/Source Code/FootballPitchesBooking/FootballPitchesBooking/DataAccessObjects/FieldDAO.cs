using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class FieldDAO
    {
        public Field GetFieldById(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.Where(f => f.Id == fieldId).FirstOrDefault();
        }

        public List<Field> GetFieldsByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.Where(f => f.StadiumId == stadiumId).ToList();
        }

        public List<Field> GetAllChildrenOfField(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.Where(f => (f.ParentField != null && f.ParentField.Value == fieldId)
                || (f.Field1 != null && f.Field1.ParentField != null && f.Field1.ParentField.Value == fieldId)).ToList();
        }

        public int CreateField(Field field)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                db.Fields.InsertOnSubmit(field);
                db.SubmitChanges();
                return field.Id;
            }
            catch (Exception)
            {
                return 0;
            }            
        }

        public int UpdateField(Field field)
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                var f = db.Fields.Where(fi => fi.Id == field.Id).FirstOrDefault();
                f.Number = field.Number;
                f.IsActive = field.IsActive;
                f.FieldType = field.FieldType;
                f.ParentField = field.ParentField;
                f.PriceId = field.PriceId;
                db.SubmitChanges();
                return f.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Field> GetAllAvailableFields(int fieldType, DateTime startDate, double startTime, double duration, string city, string district)
        {
            FPBDataContext db = new FPBDataContext();
            var fields = db.Fields.Where(f => f.FieldType == fieldType && f.IsActive && f.Stadium.IsActive
                && f.Stadium.District.ToLower().Equals(district.ToLower().Trim())
                && ((f.Stadium.OpenTime == f.Stadium.CloseTime)
                  || (f.Stadium.OpenTime < f.Stadium.CloseTime && f.Stadium.OpenTime <= startTime && f.Stadium.CloseTime >= startTime + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime <= startTime && f.Stadium.CloseTime + 24 >= startTime + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime > startTime && f.Stadium.CloseTime >= startTime + duration)
                  )).ToList();

            var availableFields = fields.Where(f => f.Reservations.Where(r => !r.Status.Equals("Canceled") && (r.Date.Date.CompareTo(startDate.Date) == 0)
                && ((r.StartTime == startTime)
                || (r.StartTime < startTime && (r.StartTime + r.Duration) > startTime)
                || (startTime < r.StartTime) && (startTime + duration) > r.StartTime)).Count() == 0).ToList();

            return availableFields;
        }
    }
}