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


        public int CreateReservation(Reservation reservation)
        {
            Utils utils = new Utils();

            double curTime = DateTime.Now.Hour + utils.MinuteToDouble(DateTime.Now.Minute);

            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.Date < DateTime.Now.Date || (reservation.Date == DateTime.Now.Date && reservation.StartTime < (curTime + 1)))
            {
                return -1;
            }

            //Đặt thời gian đá ít hơn 1 tiếng hoặc nhiều hơn 3 tiếng
            if (reservation.Duration < 1 || reservation.Duration > 3)
            {
                return -2;
            }

            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (reservation.StartTime < reservation.Field.Stadium.OpenTime || reservation.StartTime + reservation.Duration > reservation.Field.Stadium.CloseTime)
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.HasRival && (string.IsNullOrEmpty(reservation.RivalName) ||
                string.IsNullOrEmpty(reservation.RivalPhone) || string.IsNullOrEmpty(reservation.RivalEmail)))
            {
                return -4;
            }

            FieldDAO fieldDAO = new FieldDAO();

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.Date, reservation.StartTime, reservation.Duration))
            {
                return -5;
            }

            ReservationDAO resvDAO = new ReservationDAO();

            int result = resvDAO.CreateReservation(reservation);

            return result;
        }


        public int UpdateReservation(Reservation reservation)
        {
            Utils utils = new Utils();

            double curTime = DateTime.Now.Hour + utils.MinuteToDouble(DateTime.Now.Minute);

            //Validate ngày đặt sân trước ngày hiện tại, hoặc đặt sân ngày hiện tại nhung giờ đặt đã qua hoặc quá sát với thời gian đặt (khoảng cách 1 tiếng)
            if (reservation.Date < DateTime.Now.Date || (reservation.Date == DateTime.Now.Date && reservation.StartTime < (curTime + 1)))
            {
                return -1;
            }

            //Đặt thời gian đá ít hơn 1 tiếng hoặc nhiều hơn 3 tiếng
            if (reservation.Duration < 1 || reservation.Duration > 3)
            {
                return -2;
            }

            //Đặt thời gian đá sớm hơn hoặc kéo dài quá thời gian phục vụ
            if (reservation.StartTime < reservation.Field.Stadium.OpenTime || reservation.StartTime + reservation.Duration > reservation.Field.Stadium.CloseTime)
            {
                return -3;
            }

            //Nếu có đối thủ thì phải điền thông tin
            if (reservation.HasRival && (string.IsNullOrEmpty(reservation.RivalName) ||
                string.IsNullOrEmpty(reservation.RivalPhone) || string.IsNullOrEmpty(reservation.RivalEmail)))
            {
                return -4;
            }

            FieldDAO fieldDAO = new FieldDAO();

            //Kiểm tra xem field có trống vào giờ đặt sân không
            if (!fieldDAO.CheckAvailableField(reservation.FieldId, reservation.Date, reservation.StartTime, reservation.Duration))
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
                reservation.RivalFinder = resv.RivalId;
            }

            ReservationDAO resvDAO = new ReservationDAO();

            int result = resvDAO.UpdateReservation(reservation);

            return result;
        }
    }
}