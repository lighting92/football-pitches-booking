using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class StadiumStaffDAO
    {
        public List<StadiumStaff> GetStaffByStadiumId(int stadiumId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.StadiumStaffs.Where(staff => staff.StadiumId == stadiumId).ToList();
        }

        public int CreateStadiumStaff(StadiumStaff stadiumStaff)
        {
            FPBDataContext db = new FPBDataContext();
            db.StadiumStaffs.InsertOnSubmit(stadiumStaff);
            try
            {
                db.SubmitChanges();
                return stadiumStaff.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}