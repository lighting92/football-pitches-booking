using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumStaffModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        [HttpPost]
        public JsonResult Rival(int id)
        {
            UserBO userBO = new UserBO();
            var result = userBO.ReportRival(id, User.Identity.Name);
            var message = "";
            if (result > 0)
            {
                message = "SUCCESS::Bạn đã báo đối thủ không đến sân thành công.";
            }
            else if (result == 0)
            {
                message = "ERROR::Hệ thống đang bận, xin thử lại sau.";
            }
            else if (result == -1)
            {
                message = "ERROR::Bạn không có quyền để xử lí đơn này.";
            }
            else if (result == -2)
            {
                message = "ERROR::Thao tác không hợp lệ.";
            }
            else if (result == -3)
            {
                message = "ERROR::Bạn đã báo đối thủ này không đến sân rồi.";
            }
            return Json(message);
        }


        public ActionResult ViewReservation(int? id)
        {
            ReservationBO resvBO = new ReservationBO();

            try
            {
                ReservationModel model = new ReservationModel();
                model.Resv = resvBO.GetReservationById((int)id);

                if (model.Resv == null)
                {
                    return RedirectToAction("Reservations", "StadiumStaff");
                }

                model.Fields = model.Resv.Field.Stadium.Fields.ToList();
                Stadium std = model.Resv.Field.Stadium;
                model.StadiumName = std.Name;
                model.StadiumAddress = string.Concat(std.Street, ", ", std.Ward, ", ", std.District);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Reservations", "StadiumStaff");
            }

        }
    }
}
