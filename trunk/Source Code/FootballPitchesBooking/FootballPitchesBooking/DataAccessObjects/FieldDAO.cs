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
    }
}