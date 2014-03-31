using FootballPitchesBooking.Models;
using FootballPitchesBooking.Utilities;
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

        public List<Field> GetAllAvailableFields(int fieldType, DateTime startTime, double duration, string city, string district)
        {
            FPBDataContext db = new FPBDataContext();
            Utils utils = new Utils();
            double startHour = utils.TimeToDouble(startTime);
            var fields = db.Fields.Where(f => f.FieldType == fieldType && f.IsActive && f.Stadium.IsActive
                && f.Stadium.District.ToLower().Equals(district.ToLower().Trim())
                && ((f.Stadium.OpenTime == f.Stadium.CloseTime)
                  || (f.Stadium.OpenTime < f.Stadium.CloseTime && f.Stadium.OpenTime <= startHour && f.Stadium.CloseTime >= startHour + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime <= startHour && f.Stadium.CloseTime + 24 >= startHour + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime > startHour && f.Stadium.CloseTime >= startHour + duration)
                  )).ToList();

            var availableFields = fields.Where(f => f.Reservations.Where(r => !r.Status.Equals("Canceled") && (r.StartTime.CompareTo(startTime) == 0)
                || (r.StartTime.TimeOfDay < startTime.TimeOfDay && (r.StartTime.AddHours(r.Duration).TimeOfDay > startTime.TimeOfDay)
                || (startTime.TimeOfDay < r.StartTime.TimeOfDay) && startTime.AddHours(duration).TimeOfDay > r.StartTime.TimeOfDay)).Count() == 0).ToList();

            return availableFields;
        }

        public List<Field> GetAvailableFieldsOfStadium(int stadiumId, int fieldType, DateTime startTime, double duration)
        {
            FPBDataContext db = new FPBDataContext();
            Utils utils = new Utils();
            double startHour = utils.TimeToDouble(startTime);
            var fields = db.Fields.Where(f => f.StadiumId == stadiumId && f.Stadium.IsActive
                && f.FieldType == fieldType && f.IsActive 
                && ((f.Stadium.OpenTime == f.Stadium.CloseTime)
                  || (f.Stadium.OpenTime < f.Stadium.CloseTime && f.Stadium.OpenTime <= startHour && f.Stadium.CloseTime >= startHour + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime <= startHour && f.Stadium.CloseTime + 24 >= startHour + duration)
                  || (f.Stadium.OpenTime > f.Stadium.CloseTime && f.Stadium.OpenTime > startHour && f.Stadium.CloseTime >= startHour + duration)
                  )).ToList();

            var availableFields = fields.Where(f => f.Reservations.Where(r => !r.Status.Equals("Canceled") && (r.StartTime.CompareTo(startTime) == 0)
                || (r.StartTime.TimeOfDay < startTime.TimeOfDay && (r.StartTime.AddHours(r.Duration).TimeOfDay > startTime.TimeOfDay)
                || (startTime.TimeOfDay < r.StartTime.TimeOfDay) && startTime.AddHours(duration).TimeOfDay > r.StartTime.TimeOfDay)).Count() == 0).ToList();

            return availableFields;
        }

        public bool CheckAvailableField(int fieldId, DateTime startTime, double duration, int reservationId)
        {
            FPBDataContext db = new FPBDataContext();
            Field field = db.Reservations.Where(r => r.FieldId == fieldId && r.Id != reservationId && r.StartTime.Date == startTime.Date && 
                ((r.StartTime.TimeOfDay <= startTime.TimeOfDay && (r.StartTime.AddHours(duration)) >= startTime) || //start time của order nằm giữa start time của reservation và end time (starttime + duration)
                r.StartTime >= startTime && r.StartTime <= startTime.AddHours(duration))).Select(r => r.Field).FirstOrDefault();
            if (field == null)
            {
                return true;
            }
            return false;
        }
    }
}