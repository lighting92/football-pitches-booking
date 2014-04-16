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

        public MemberRank GetMemberRankByUserPoint(int point) //ham nay la get rank by point theo diem? user
        {
            FPBDataContext db = new FPBDataContext();
            return db.MemberRanks.Where(m => m.Point <= point).OrderByDescending(m => m.Point).FirstOrDefault(); 
        }

        public MemberRank GetMemberRankByPoint(int point) //ham nay get rank by point cua rank nhap vao
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

        public int UpdateMemberRank(MemberRank rank, bool updateOwner = true)
        {
            FPBDataContext db = new FPBDataContext();
            MemberRank currentMemberRank = db.MemberRanks.Where(r => r.Id == rank.Id).FirstOrDefault(); //cai chu r nay ko co y nghia nhung ma nen dat theo ten entity (MemberRank)
            currentMemberRank.RankName = rank.RankName;
            currentMemberRank.Point = rank.Point;
            currentMemberRank.Promotion = rank.Promotion;
            try //cai nay de test xem co' update success ko, neu success (ko co' loi~) thi return id cua rank vua update, con neu ko thi return 0 (cai nay de so sanh' o phia' controller de hien thi loi~)
            {
                db.SubmitChanges();
                return currentMemberRank.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int DeleteMemberRank(MemberRank rank)
        {
            FPBDataContext db = new FPBDataContext();
            db.MemberRanks.DeleteOnSubmit(rank);
            try //cai nay de test xem co' update success ko, neu success (ko co' loi~) thi return id cua rank vua update, con neu ko thi return 0 (cai nay de so sanh' o phia' controller de hien thi loi~)
            {
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public List<MemberRank> GetAllRanks()
        {
            FPBDataContext db = new FPBDataContext();
            try
            {
                List<MemberRank> ranks = db.MemberRanks.OrderBy(r => r.Point).ToList();
                return ranks;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}