using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class MemberRankDAO
    {
        public MemberRank GetMemberRankById(int rankId)
        {
            FPBDataContext db = new FPBDataContext();
            return db.MemberRanks.Where(r => r.Id == rankId).FirstOrDefault();
        }

        public MemberRank GetMemberRankByPoint(int point)
        {
            FPBDataContext db = new FPBDataContext();
            return db.MemberRanks.Where(m => m.Point <= point).OrderByDescending(m => m.Point).FirstOrDefault();            
        }
    }
}