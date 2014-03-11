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

        public Field GetFieldByNumber(string fieldNumber, int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.Where(f => f.Number == fieldNumber && f.StadiumId == stadiumId).FirstOrDefault();
        }

        public List<Field> GetFieldsByStadium(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.Where(f => f.StadiumId == stadiumId).ToList();
        }

        public List<Field> GetAllFields()
        {
            FPBDataContext db = new FPBDataContext();
            return db.Fields.ToList();
        }

        public int CreateField(Field field)
        {
            FPBDataContext db = new FPBDataContext();
            db.Fields.InsertOnSubmit(field);
            try
            {
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
            Field currentField = db.Fields.Where(f => f.Id == field.Id).FirstOrDefault();
            currentField.Number = field.Number;
            currentField.ParentField = field.ParentField;
            currentField.FieldType = field.FieldType;
            currentField.IsActive = field.IsActive;

            try
            {
                db.SubmitChanges();
                return currentField.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateFieldStatus(int fieldId, bool fieldStatus)
        {
            FPBDataContext db = new FPBDataContext();
            Field field = db.Fields.Where(f => f.Id == fieldId).FirstOrDefault();
            field.IsActive = fieldStatus;

            try
            {
                db.SubmitChanges();
                return field.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteField(int fieldId)
        {
            FPBDataContext db = new FPBDataContext();
            Field field = db.Fields.Where(s => s.Id == fieldId).FirstOrDefault();
            try
            {
                db.Fields.DeleteOnSubmit(field);
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