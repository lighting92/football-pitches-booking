using FootballPitchesBooking.DataAccessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.BusinessObjects
{
    public class ReservationBO
    {
        public Reservation GetReservationById(int resvId)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationById(resvId);
        }


        public List<Reservation> GetReservationsOfStadium(int stadiumId)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationsOfStadium(stadiumId);
        }


        public List<Reservation> GetReservationsOfField(int fieldId)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationsOfField(fieldId);
        }


        public List<Reservation> GetReservationsOfUser(string userName)
        {
            ReservationDAO resDAO = new ReservationDAO();
            return resDAO.GetAllReservationsOfUser(userName);
        }


        public List<Reservation> GetReservationsOfRival(string userName)
        {
            ReservationDAO resDAO = new ReservationDAO();
            return resDAO.GetAllReservationsOfRival(userName);
        }


        public int CreateReservation(Reservation reservation)
        {
            Utils utils = new Utils();
            ConfigurationDAO configDAO = new ConfigurationDAO();
            double min = 0;
            try
            {
                min = Double.Parse(configDAO.GetConfigByName("MinTimeBooking").Value);
            }
            catch (Exception)
            { }
            double expTime = utils.TimeToDouble(DateTime.Now.AddMinutes(min));

            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.Date < DateTime.Now.Date || (reservation.Date == DateTime.Now.Date && reservation.StartTime <= (expTime)))
            {
                return -1;
            }

            //Đặt thời gian đá ít hơn 1 tiếng hoặc nhiều hơn 3 tiếng
            if (reservation.Duration < 1 || reservation.Duration > 3)
            {
                return -2;
            }

            FieldDAO fieldDAO = new FieldDAO();

            Stadium std = fieldDAO.GetFieldById(reservation.FieldId).Stadium;
            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (!((std.OpenTime < std.CloseTime
                    && (reservation.StartTime < std.OpenTime || reservation.StartTime + reservation.Duration > std.CloseTime)) ||
                (std.OpenTime > std.CloseTime
                    && (reservation.StartTime > std.OpenTime && reservation.StartTime + reservation.Duration < std.CloseTime + 24)) ||
                (std.OpenTime > std.CloseTime
                    && (reservation.StartTime < std.OpenTime && (reservation.StartTime + reservation.Duration < std.CloseTime)))))
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.NeedRival && (!string.IsNullOrEmpty(reservation.RivalName) && string.IsNullOrEmpty(reservation.RivalPhone)) ||
                                         (string.IsNullOrEmpty(reservation.RivalName) && !string.IsNullOrEmpty(reservation.RivalPhone)))
            {
                return -4;
            }

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.Date, reservation.StartTime, reservation.Duration, reservation.Id))
            {
                return -5;
            }

            //xoá thông tin đối thủ nếu không có
            if (!reservation.NeedRival)
            {
                reservation.RivalId = null;
                reservation.RivalName = null;
                reservation.RivalPhone = null;
                reservation.RivalEmail = null;
                reservation.RivalFinder = null;
            }

            ReservationDAO resvDAO = new ReservationDAO();

            int result = resvDAO.CreateReservation(reservation);

            return result;
        }


        public int UpdateReservation(Reservation reservation)
        {
            Utils utils = new Utils();
            ReservationDAO resvDAO = new ReservationDAO();
            ConfigurationDAO configDAO = new ConfigurationDAO();
            double min = 0;
            try
            {
                min = Double.Parse(configDAO.GetConfigByName("MinTimeBooking").Value);
            }
            catch (Exception)
            { }
            double expTime = utils.TimeToDouble(DateTime.Now.AddMinutes(-min));

            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.Date < DateTime.Now.Date || (reservation.Date == DateTime.Now.Date && reservation.StartTime <= (expTime)))
            {
                if (reservation.Status.Equals("Canceled"))
                {
                    return resvDAO.UpdateReservationStatus(reservation.Id, "Canceled", 0);
                }
                else
                {
                    return -1;
                }
            }

            //Đặt thời gian đá ít hơn 1 tiếng hoặc nhiều hơn 3 tiếng
            if (reservation.Duration < 1 || reservation.Duration > 3)
            {
                return -2;
            }

            FieldDAO fieldDAO = new FieldDAO();

            Stadium std = fieldDAO.GetFieldById(reservation.FieldId).Stadium;
            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (!((std.OpenTime < std.CloseTime
                    && (reservation.StartTime < std.OpenTime || reservation.StartTime + reservation.Duration > std.CloseTime)) ||
                (std.OpenTime > std.CloseTime
                    && (reservation.StartTime > std.OpenTime && reservation.StartTime + reservation.Duration < std.CloseTime + 24)) ||
                (std.OpenTime > std.CloseTime
                    && (reservation.StartTime < std.OpenTime && (reservation.StartTime + reservation.Duration < std.CloseTime)))))
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.NeedRival && (!string.IsNullOrEmpty(reservation.RivalName) && string.IsNullOrEmpty(reservation.RivalPhone)) ||
                                         (string.IsNullOrEmpty(reservation.RivalName) && !string.IsNullOrEmpty(reservation.RivalPhone)))
            {
                return -4;
            }

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.Date, reservation.StartTime, reservation.Duration, reservation.Id))
            {
                return -5;
            }


            Reservation resv = GetReservationById(reservation.Id);

            //Nếu không có thay đổi ở rival thì giữ nguyên Rival Finder
            if (!reservation.NeedRival)
            {
                reservation.RivalFinder = null;
            }

            if (resv.NeedRival == true && reservation.NeedRival == true)
            {
                reservation.RivalFinder = resv.RivalFinder;
            }

            if (resv.Status == reservation.Status)
            {
                reservation.Approver = resv.Approver;
            }

            int result = resvDAO.UpdateReservation(reservation);

            return result;
        }

        public int UserBooking(Reservation res)
        {
            var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddHours(0.5), "SE Asia Standard Time");
            if (res.Date.AddHours(res.StartTime).CompareTo(now) >= 0)
            {
                ReservationDAO resDAO = new ReservationDAO();
                return resDAO.CreateReservation(res);
            }
            else
            {
                return -1;
            }
        }

        public int UserCancelBooking(int resId)
        {
            ReservationDAO resDAO = new ReservationDAO();

            var res = resDAO.GetReservationById(resId);
            if (res != null)
            {
                Notification msg = new Notification();
                msg.StadiumId = res.Field.StadiumId;
                msg.Status = "Unread";
                msg.Message = "[" + res.User.UserName + "] đã hủy đặt sân do bạn quản lý <a href='/StadiumStaff/ViewReservation?Id=" + res.Id + "'>Chi tiết</a>";
                msg.CreateDate = msg.CreateDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
                var result = resDAO.UserCancelReservation(resId);
                if (result > 0)
                {
                    NotificationDAO notDAO = new NotificationDAO();
                    notDAO.CreateMessage(msg);
                }
                return result;
            }
            else
            {
                return -1;
            }
        }

        public int UserUpdateReservation(Reservation res)
        {
            Notification msg = null;
            if (!string.IsNullOrEmpty(res.RivalStatus) && res.RivalStatus.ToLower().Equals("deni") && res.NeedRival)
            {
                if (res.RivalId != null)
                {
                    msg = new Notification();
                    msg.UserId = res.RivalId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] không đồng ý giao hữu với bạn";
                }
                else if (!string.IsNullOrEmpty(res.RivalName))
                {
                    msg = new Notification();
                    msg.StadiumId = res.Field.StadiumId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] không chấp nhận giao hữu với [" + res.RivalName + " - "
                        + res.RivalPhone + "] <a href='/StadiumStaff/ViewReservation?Id=" + res.Id + "'>Chi tiết</a>";
                }
                res.RivalName = null;
                res.RivalPhone = null;
                res.RivalEmail = null;
                res.RivalStatus = "Waiting";
                res.RivalId = null;
            }
            else if (res.NeedRival && string.IsNullOrEmpty(res.RivalStatus))
            {
                res.RivalStatus = "Waiting";
            }
            else if (!res.NeedRival)
            {
                if (res.RivalId != null)
                {
                    msg = new Notification();
                    msg.UserId = res.RivalId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] không mời giao hữu nữa";
                }
                else if (!string.IsNullOrEmpty(res.RivalName))
                {
                    msg = new Notification();
                    msg.StadiumId = res.Field.StadiumId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] không mời giao hữu nữa. [Người được cáp: " + res.RivalName + " - " + res.RivalPhone + "] <a href='/StadiumStaff/ViewReservation?Id=" + res.Id + "'>Chi tiết</a>";
                }
                res.RivalName = null;
                res.RivalPhone = null;
                res.RivalEmail = null;
                res.RivalStatus = null;
                res.RivalId = null;
            }
            ReservationDAO resDAO = new ReservationDAO();
            int result = resDAO.UserUpdateReservation(res);
            if (result == 2)
            {
                if (res.RivalId != null)
                {
                    msg = new Notification();
                    msg.UserId = res.RivalId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] đồng ý giao hữu với bạn";
                }
                else if (!string.IsNullOrEmpty(res.RivalName))
                {
                    msg = new Notification();
                    msg.StadiumId = res.Field.StadiumId;
                    msg.Status = "Unread";
                    msg.Message = "[" + res.User.UserName + "] chấp nhận giao hữu với [" + res.RivalName + " - "
                        + res.RivalPhone + "] <a href='/StadiumStaff/ViewReservation?Id=" + res.Id + "'>Chi tiết</a>";
                }
            }
            if (result > 0 && msg != null)
            {
                msg.CreateDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
                NotificationDAO noDAO = new NotificationDAO();
                noDAO.CreateMessage(msg);
            }
            return result;
        }

        public int UpdateReservationStatus(int reservationId, string status, int approver)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            FieldDAO fieldDAO = new FieldDAO();
            Reservation reservation = resvDAO.GetReservationById(reservationId);
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.Date, reservation.StartTime, reservation.Duration, reservation.Id))
            {
                return -1;
            }
            return resvDAO.UpdateReservationStatus(reservationId, status, approver);
        }


        public int UpdateReservationRival(int reservationId, User user)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.UpdateReservationRival(reservationId, user);
        }


        public List<Reservation> GetReservationsNeedRival()
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationsNeedRival();
        }

        public List<Reservation> GetReservationsNeedRival(string user, DateTime date, int type)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationsNeedRival(user, date, type);
        }

        public Reservation GetReservationNeedRivalById(int id)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.GetReservationNeedRivalById(id);
        }
    }
}