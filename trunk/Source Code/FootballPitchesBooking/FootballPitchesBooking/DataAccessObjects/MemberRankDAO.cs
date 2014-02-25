using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class MemberRankDAO
    {
        private FPBDataContext db;

        public MemberRankDAO()
        {
            db = new FPBDataContext();
        }

        public MemberRank GetMemberRankByPoint(int point)
        {
            MemberRank rank = db.MemberRanks.Where(m => m.Point <= point).OrderByDescending(m => m.Point).FirstOrDefault();
            return rank;
        }
    }
}