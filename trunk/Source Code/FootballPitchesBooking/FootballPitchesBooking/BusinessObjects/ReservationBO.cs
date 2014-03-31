﻿using FootballPitchesBooking.DataAccessObjects;
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


        public int CreateReservation(Reservation reservation)
        {

            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.StartTime < DateTime.Now.AddHours(1))
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

            Utils utils = new Utils();
            double startHour = utils.TimeToDouble(reservation.StartTime);

            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (std.OpenTime < std.CloseTime && (startHour < std.OpenTime || startHour + reservation.Duration > std.CloseTime) ||
                std.OpenTime > std.CloseTime && (!(startHour + reservation.Duration < std.CloseTime + 24 && startHour > std.OpenTime) ||
                                                 !(startHour < std.OpenTime && startHour + reservation.Duration < std.CloseTime)))
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.HasRival && !(string.IsNullOrEmpty(reservation.RivalName) &&
                string.IsNullOrEmpty(reservation.RivalPhone) && string.IsNullOrEmpty(reservation.RivalEmail)))
            {
                return -4;
            }

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.StartTime, reservation.Duration, reservation.Id))
            {
                return -5;
            }

            //xoá thông tin đối thủ nếu không có
            if (!reservation.HasRival)
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
            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.StartTime < DateTime.Now.AddHours(1))
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

            Utils utils = new Utils();
            double startHour = utils.TimeToDouble(reservation.StartTime);

            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (std.OpenTime < std.CloseTime && (startHour < std.OpenTime || startHour + reservation.Duration > std.CloseTime) ||
                std.OpenTime > std.CloseTime && (!(startHour + reservation.Duration < std.CloseTime + 24 && startHour > std.OpenTime) ||
                                                 !(startHour < std.OpenTime && startHour + reservation.Duration < std.CloseTime)))
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.HasRival && !(string.IsNullOrEmpty(reservation.RivalName) &&
                string.IsNullOrEmpty(reservation.RivalPhone) && string.IsNullOrEmpty(reservation.RivalEmail)))
            {
                return -4;
            }

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.StartTime, reservation.Duration, reservation.Id))
            {
                return -5;
            }

            Reservation resv = GetReservationById(reservation.Id);

            //Nếu không có thay đổi ở rival thì giữ nguyên Rival Finder
            if (!reservation.HasRival)
            {
                reservation.RivalFinder = null;
            }

            if (resv.HasRival == true && reservation.HasRival == true)
            {
                reservation.RivalFinder = resv.RivalFinder;
            }

            if (resv.Status == reservation.Status)
            {
                reservation.Approver = resv.Approver;
            }

            ReservationDAO resvDAO = new ReservationDAO();

            int result = resvDAO.UpdateReservation(reservation);

            return result;
        }

        public int UserBooking(Reservation res)
        {
            var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.AddHours(2), "SE Asia Standard Time");
            if (res.StartTime.Date.CompareTo(now.Date) >= 0 && res.StartTime.TimeOfDay > DateTime.Now.TimeOfDay)
            {
                ReservationDAO resDAO = new ReservationDAO();
                return resDAO.CreateReservation(res);
            }
            else
            {
                return -1;                
            }
        }

        public int UpdateReservationRival(Reservation reservation)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.UpdateReservationRival(reservation);
        }


        public int UpdateReservationStatus(int reservationId, string status, int approver)
        {
            ReservationDAO resvDAO = new ReservationDAO();
            return resvDAO.UpdateReservationStatus(reservationId, status, approver);
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