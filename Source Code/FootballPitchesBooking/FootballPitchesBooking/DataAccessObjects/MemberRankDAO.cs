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

        //public MemberRank GetMemberRankByPoint(int point)
        //{
        //    FPBDataContext db = new FPBDataContext();
        //    return db.MemberRanks.Where(m => m.Point <= point).OrderByDescending(m => m.Point).FirstOrDefault();            
        //}

        public MemberRank GetMemberRankByPoint(int point)
        {
            FPBDataContext db = new FPBDataContext();
            return db.MemberRanks.Where(m => m.Point == point).FirstOrDefault();
        }

        public MemberRank GetMemberRankByName(string rankName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.MemberRanks.Where(m => m.RankName.ToLower().Equals(rankName.ToLower())).FirstOrDefault();
        }

        /// <summary>
        /// Select all Rank in db
        /// </summary>
        /// <returns>List Ranks</returns>

        public int CreateMemberRank(MemberRank rank)
        {
            FPBDataContext db = new FPBDataContext();
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
            FPBDataContext db = new FPBDataContext();
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