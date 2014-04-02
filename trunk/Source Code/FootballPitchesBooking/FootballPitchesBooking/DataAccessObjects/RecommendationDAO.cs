using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.RecommendationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class RecommendationDAO
    {

        public Configuration GetPriorityByConfigName(String configName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Configurations.Where(c => c.Name.ToLower().Equals(configName.ToLower())).FirstOrDefault();
        }
    }
}