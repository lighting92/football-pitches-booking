﻿using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class WebsiteBO
    {
        public int CreateMemberRank(MemberRank rank)
        {
            MemberRankDAO memberrankDAO = new MemberRankDAO();

            return memberrankDAO.CreateMemberRank(rank);
        }

    }
}