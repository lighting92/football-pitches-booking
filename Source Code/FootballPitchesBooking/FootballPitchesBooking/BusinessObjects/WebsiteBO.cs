﻿using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class WebsiteBO
    {
        public List<Advertisement> GetAllAds()
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.GetAllAds();
        }


        public List<Advertisement> GetActiveAds()
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            List<Advertisement> adsList = new List<Advertisement>();
            adsList.Add(adsDAO.GetAdvertisement(0, "Banner1"));
            adsList.Add(adsDAO.GetAdvertisement(0, "Banner2"));
            adsList.Add(adsDAO.GetAdvertisement(0, "Banner3"));
            return adsList;
        }


        public Advertisement GetAdvertisementById(int adsId)
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.GetAdvertisementById(adsId);
        }


        public int CreateAdvertisement(Advertisement ads)
        {
            if (ads.ExpiredDate < DateTime.Now)
            {
                return -1;
            }

            AdvertisementDAO adsDAO = new AdvertisementDAO();

            if (ads.Status == "Active")
            {
                if(adsDAO.GetAdvertisement(0, ads.Position) != null)
                {
                    return -2;
                }
            }

            int result = adsDAO.CreateAdvertisement(ads);

            return result;
        }

        public int UpdateAdvertisement(Advertisement ads)
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();

            if (ads.ExpiredDate < DateTime.Now)
            {
                if (ads.Status == "Deactive")
                {
                    return adsDAO.UpdateAdvertisementStatus(ads.Id, "Deactive");
                }
                else
                {
                    return -1;
                }
            }

            if (ads.Status == "Active")
            {
                if (adsDAO.GetAdvertisement(ads.Id, ads.Position) != null)
                {
                    return -2;
                }
            }

            int result = adsDAO.UpdateAdvertisement(ads);

            return result;
        }


        public int DeleteAdvertisement(int adsId)
        {
            AdvertisementDAO adsDAO = new AdvertisementDAO();
            return adsDAO.DeleteAdvertisement(adsId);
        }

        public FootballPitchesBooking.Models.Configuration GetConfigByName(string configName)
        {
            ConfigurationDAO configDAO = new ConfigurationDAO();
            return configDAO.GetConfigByName(configName);
        }

        public int SendEmail(string toEmail, string content, string subject, bool isHTML)
        {
            SmtpSection cfg = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(cfg.Network.UserName, "Đặt Sân Bóng FPB");
                    if (toEmail != null)
                    {
                        mail.To.Add(toEmail);
                    }
                    else
                    {
                        mail.To.Add(cfg.Network.UserName);
                    }
                    mail.Subject = subject;
                    mail.Body = content;
                    mail.IsBodyHtml = isHTML;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(cfg.Network.UserName, cfg.Network.Password);
                    client.Host = cfg.Network.Host;
                    client.Port = cfg.Network.Port;
                    client.EnableSsl = cfg.Network.EnableSsl;
                    client.Send(mail);
                }
            }
            catch (SmtpException)
            {
                return -1;
            }
            return 1;
        }

        public int UpdateConfigNumberValueWithMinMax(FootballPitchesBooking.Models.Configuration config, double min, double max)
        {
            double value = double.Parse(config.Value);
            if (value < min && value > max)
            {
                return -2;
            }
            else
            {
                ConfigurationDAO conDAO = new ConfigurationDAO();
                return conDAO.UpdateConfiguration(config);
            }
        }

        public FootballPitchesBooking.Models.Configuration GetConfigurationByName(string confName)
        {
            ConfigurationDAO conDAO = new ConfigurationDAO();
            return conDAO.GetConfigByName(confName);
        }
    }
}