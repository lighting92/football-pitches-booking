using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class MemberRankDAO
    {
        FPBDataContext db = new FPBDataContext();

        public MemberRank GetMemberRankById(int rankId)
        {
            return db.MemberRanks.Where(r => r.Id == rankId).FirstOrDefault();
        }

        public MemberRank GetMemberRankByPoint(int point)
        {
            return db.MemberRanks.Where(m => m.Point <= point).OrderByDescending(m => m.Point).FirstOrDefault();            
        }
        /// <summary>
        /// Select all Rank in db
        /// </summary>
        /// <returns>List Ranks</returns>

        public int CreateMemberRank(MemberRank rank)
        {
           
            db.MemberRanks.InsertOnSubmit(rank);
            try
            {
                db.SubmitChanges();
                return rank.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<MemberRank> Select()
        {
            try
            {
                var ranks = db.MemberRanks.ToList();
                return ranks;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}