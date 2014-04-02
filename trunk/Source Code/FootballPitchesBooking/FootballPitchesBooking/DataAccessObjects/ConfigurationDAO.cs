using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.DataAccessObjects
{
    public class ConfigurationDAO
    {
        public Configuration GetConfigByName(string configName)
        {
            FPBDataContext db = new FPBDataContext();
            return db.Configurations.Where(c => c.Name.ToLower().Equals(configName.ToLower())).FirstOrDefault();
        }

        public int UpdateConfigurations(List<Configuration> configs)
        {
            FPBDataContext db = new FPBDataContext();
            bool notFound = false;
            foreach (var item in configs)
            {
                var temp = db.Configurations.Where(c => c.Name.ToLower().Equals(item.Name.ToLower())).FirstOrDefault();
                if (temp != null)
                {
                    temp.Value = item.Value;
                }
                else
                {
                    notFound = true;
                    break;
                }
            }
            if (!notFound)
            {
                try
                {
                    db.SubmitChanges();
                    return 1;
                }
                catch (Exception)
                {
                    
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}