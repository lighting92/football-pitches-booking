using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
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
    }
}
