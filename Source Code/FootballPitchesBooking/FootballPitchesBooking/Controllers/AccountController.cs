using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models.AccountModels;
using FootballPitchesBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FootballPitchesBooking.Properties;
using System.Text.RegularExpressions;
using System.IO;
using FootballPitchesBooking.Utilities;

namespace FootballPitchesBooking.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [Authorize]
        public ActionResult Index()
        {
            UserBO userBO = new UserBO();
            User user = new User();
            try
            {
                user = userBO.GetUserByUserName(User.Identity.Name);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AccountModel model = new AccountModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Address = user.Address,
                RankName = user.MemberRank.RankName,
                Point = user.Point,
                RoleName = user.Role.Role1,                
                JoinDate = user.JoinDate,
                ErrorMessages = new List<string>()
            };
            return View("Profiles", model);
        }

        //
        // GET: /Account/BookingHistory

        [Authorize]
        public ActionResult BookingHistory()
        {
            ReservationBO resBO = new ReservationBO();
            var res = resBO.GetReservationsOfUser(User.Identity.Name);
            var model = new List<BookingHistoryModel>();
            foreach (var item in res)
            {
                var temp = new BookingHistoryModel();
                temp.Id = item.Id;
                temp.StartDate = item.Date.ToShortDateString();
                var hour = (int)item.StartTime + "";
                var min = ((item.StartTime - (int)item.StartTime) * 60) + "";
                hour = (hour.Length == 1) ? ("0" + hour) : hour;
                min = (min.Length == 1) ? ("0" + min) : min;
                temp.StartTime = hour + ":" + min;
                temp.Duration = (item.Duration * 60).ToString();
                temp.StadiumName = item.Field.Stadium.Name;
                temp.StadiumAddress = item.Field.Stadium.Street + "," + item.Field.Stadium.Ward + "," + item.Field.Stadium.District;
                temp.Price = item.Price.ToString();
                temp.CreateDate = item.CreatedDate.ToShortDateString();
                temp.Status = item.Status;
                temp.RivalStatus = item.RivalStatus;
                model.Add(temp);
            }
            model = model.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }


        [Authorize]
        public ActionResult RivalHistory()
        {
            ReservationBO resBO = new ReservationBO();
            var res = resBO.GetReservationsOfRival(User.Identity.Name);
            return View(res);
        }


        [Authorize]
        public ActionResult CancelRival(int? id)
        {
            ReservationBO resvBO = new ReservationBO();

            try
            {
                Reservation resv = resvBO.GetReservationById((int)id);
                if (resv.Date > DateTime.Now || (resv.Date == DateTime.Now && resv.StartTime > (DateTime.Now.Hour + 1 + DateTime.Now.Minute / 60)))
                {
                    int result = resvBO.UpdateReservationRival((int)id, null);
                }
                return RedirectToAction("RivalHistory", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("RivalHistory", "Account");
            }
        }


        [Authorize]
        public ActionResult EditRival(int? id)
        {
            ReservationBO resvBO = new ReservationBO();

            try
            {
                Reservation resv = resvBO.GetReservationById((int)id);

                if (resv == null || !resv.User2.UserName.Equals(User.Identity.Name))
                {
                    return RedirectToAction("RivalHistory", "Account");
                }

                return View(resv);
            }
            catch (Exception)
            {
                return RedirectToAction("RivalHistory", "Account");
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditRival(FormCollection form, int id)
        {
            ReservationBO resvBO = new ReservationBO();
            string fullName = form["FullName"];
            string phone = form["PhoneNumber"];
            string email = form["Email"];

            UserBO userBO = new UserBO();
            User user = new User()
            {
                Id = userBO.GetUserByUserName(User.Identity.Name).Id,
                FullName = fullName,
                PhoneNumber = phone,
                Email = email
            };

            try
            {
                int result = resvBO.UpdateReservationRival(id, user);

                return RedirectToAction("RivalHistory", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("RivalHistory", "Account");
            }
        }


        //
        // GET: /Account/EditReservation

        [Authorize]
        public ActionResult EditReservation(int id)
        {
            ReservationBO resBO = new ReservationBO();
            var res = resBO.GetReservationById(id);
            if (res == null)
            {
                return RedirectToAction("BookingHistory");
            }
            var model = new EditReservationModel();
            if (res.User.UserName.ToLower().Equals(User.Identity.Name.ToLower()))
            {
                model.HavePermission = true;
                model.CanModify = true;
                var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
                bool expired = now.CompareTo(res.Date.Date.AddHours(res.StartTime)) >= 0;
                if (res.Status.ToLower().Equals("canceled") || expired)
                {
                    model.CanModify = false;
                }
                model.Id = res.Id;
                model.FullName = res.FullName;
                model.PhoneNumber = res.PhoneNumber;
                model.Email = res.Email;
                model.StartDate = res.Date.ToShortDateString();
                var hour = (int)res.StartTime + "";
                var min = ((res.StartTime - (int)res.StartTime) * 60) + "";
                hour = (hour.Length == 1) ? ("0" + hour) : hour;
                min = (min.Length == 1) ? ("0" + min) : min;
                model.StartTime = hour + ":" + min;
                model.Duration = (res.Duration * 60).ToString();
                model.StadiumName = res.Field.Stadium.Name;
                model.StadiumAddress = res.Field.Stadium.Street + "," + res.Field.Stadium.Ward + "," + res.Field.Stadium.District;
                model.FieldNumber = res.Field.Number;
                model.FieldType = res.Field.FieldType.ToString();
                model.Price = res.Price.ToString();
                model.Discount = res.Discount.HasValue ? res.Discount.ToString() : "0";
                model.VerifyCode = res.VerifyCode;
                model.CreateDate = res.CreatedDate.ToShortDateString();
                model.Status = res.Status;
                model.NeedRival = res.NeedRival;
                model.RivalName = res.RivalName;
                model.RivalPhone = res.RivalPhone;
                model.RivalEmail = res.RivalEmail;
                model.RivalStatus = res.RivalStatus;
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessage = "Bạn không đủ quyền hạn để chỉnh sửa thông tin đặt sân này";
            }
            return View(model);
        }

        //
        // POST: /Account/EditReservation

        [Authorize]
        [HttpPost]
        public ActionResult EditReservation(FormCollection form, int id)
        {
            ReservationBO resBO = new ReservationBO();
            var res = resBO.GetReservationById(id);
            if (res == null)
            {
                return RedirectToAction("BookingHistory");
            }
            var model = new EditReservationModel();
            if (res.User.UserName.ToLower().Equals(User.Identity.Name.ToLower()))
            {
                model.HavePermission = true;
                model.CanModify = true;
                var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
                bool expired = now.CompareTo(res.Date.Date.AddHours(res.StartTime)) >= 0;
                if (res.Status.ToLower().Equals("canceled") || expired)
                {
                    model.CanModify = false;
                }
                model.Id = res.Id;
                if (model.CanModify)
                {
                    model.FullName = form["FullName"];
                    model.PhoneNumber = form["PhoneNumber"];
                    model.Email = form["Email"];

                    bool needRival = !string.IsNullOrEmpty(form["NeedRival"]);
                    model.NeedRival = needRival;
                    model.RivalStatus = form["RivalStatus"];

                    res.FullName = model.FullName;
                    res.PhoneNumber = model.PhoneNumber;
                    res.Email = model.Email;
                    res.NeedRival = model.NeedRival;
                    res.RivalStatus = model.RivalStatus;

                    if (resBO.UserUpdateReservation(res) > 0)
                    {
                        model.SuccessMessage = Resources.Update_Success;
                    }
                    else
                    {
                        model.ErrorMessage = "Cập nhật không thành công";
                    }
                }
                else
                {
                    model.FullName = res.FullName;
                    model.PhoneNumber = res.PhoneNumber;
                    model.Email = res.Email;
                    model.NeedRival = res.NeedRival;
                    model.RivalStatus = res.RivalStatus;
                }
                model.StartDate = res.Date.ToShortDateString();
                var hour = (int)res.StartTime + "";
                var min = ((res.StartTime - (int)res.StartTime) * 60) + "";
                hour = (hour.Length == 1) ? ("0" + hour) : hour;
                min = (min.Length == 1) ? ("0" + min) : min;
                model.StartTime = hour + ":" + min;
                model.Duration = (res.Duration * 60).ToString();
                model.StadiumName = res.Field.Stadium.Name;
                model.StadiumAddress = res.Field.Stadium.Street + "," + res.Field.Stadium.Ward + "," + res.Field.Stadium.District;
                model.FieldNumber = res.Field.Number;
                model.FieldType = res.Field.FieldType.ToString();
                model.Price = res.Price.ToString();
                model.Discount = res.Discount.HasValue ? res.Discount.ToString() : "0";
                model.VerifyCode = res.VerifyCode;
                model.CreateDate = res.CreatedDate.ToShortDateString();
                model.Status = res.Status;
                model.RivalName = res.RivalName;
                model.RivalPhone = res.RivalPhone;
                model.RivalEmail = res.RivalEmail;
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessage = "Bạn không đủ quyền hạn để chỉnh sửa thông tin đặt sân này";
            }
            return View(model);
        }

        //
        // GET: /Account/CancelReservation
        [Authorize]
        public ActionResult CancelReservation(int id)
        {
            ReservationBO resBO = new ReservationBO();
            var res = resBO.GetReservationById(id);
            var model = new EditReservationModel();
            if (res.User.UserName.ToLower().Equals(User.Identity.Name.ToLower()))
            {
                model.HavePermission = true;
                model.CanModify = true;
                var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
                bool expired = now.CompareTo(res.Date.Date.AddHours(res.StartTime)) >= 0;
                if (res.Status.ToLower().Equals("canceled") || expired)
                {
                    model.CanModify = false;
                }
                if (model.CanModify)
                {
                    if (resBO.UserCancelBooking(id) > 0)
                    {
                        res.Status = "Canceled";
                        model.SuccessMessage = "Hủy đặt sân thành công";
                        model.CanModify = false;
                    }
                    else
                    {
                        model.ErrorMessage = "Hủy đặt sân không thành công";
                    }
                }
                else
                {
                    model.ErrorMessage = "Không thể hủy đơn đặt sân này";
                }
                model.Id = res.Id;
                model.FullName = res.FullName;
                model.PhoneNumber = res.PhoneNumber;
                model.Email = res.Email;
                model.StartDate = res.Date.ToShortDateString();
                var hour = (int)res.StartTime + "";
                var min = ((res.StartTime - (int)res.StartTime) * 60) + "";
                hour = (hour.Length == 1) ? ("0" + hour) : hour;
                min = (min.Length == 1) ? ("0" + min) : min;
                model.StartTime = hour + ":" + min;
                model.Duration = (res.Duration * 60).ToString();
                model.StadiumName = res.Field.Stadium.Name;
                model.StadiumAddress = res.Field.Stadium.Street + "," + res.Field.Stadium.Ward + "," + res.Field.Stadium.District;
                model.FieldNumber = res.Field.Number;
                model.FieldType = res.Field.FieldType.ToString();
                model.Price = res.Price.ToString();
                model.Discount = res.Discount.HasValue ? res.Discount.ToString() : "0";
                model.VerifyCode = res.VerifyCode;
                model.CreateDate = res.CreatedDate.ToShortDateString();
                model.Status = res.Status;
                model.NeedRival = res.NeedRival;
                model.RivalName = res.RivalName;
                model.RivalPhone = res.RivalPhone;
                model.RivalEmail = res.RivalEmail;
                model.RivalStatus = res.RivalStatus;
            }
            else
            {
                model.HavePermission = false;
                model.ErrorMessage = "Bạn không đủ quyền hạn để chỉnh sửa thông tin đặt sân này";
            }
            return View("EditReservation", model);
        }


        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profiles", "Account");
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(FormCollection form, string returnUrl)
        {
            if (Request.QueryString.AllKeys.Count() != 1)
            {
                returnUrl = "";
                for (int i = 0; i < Request.QueryString.AllKeys.Count(); i++)
                {
                    if (Request.QueryString.Keys[i].ToLower().Equals("returnurl"))
                    {
                        returnUrl += Request.QueryString[i];
                    }
                    else
                    {
                        returnUrl += "&" + Request.QueryString.Keys[i] + "=" + Request.QueryString[i];
                    }
                }
            }

            LoginModel log = new LoginModel();
            log.UserName = form["UserName"];
            log.Password = form["Password"];
            string strRemember = form["Remember"];
            bool remember = (strRemember != null) ? true : false;

            if (string.IsNullOrWhiteSpace(log.UserName) || string.IsNullOrWhiteSpace(log.Password))
            {
                log.ErrorString = Resources.Form_EmptyFields;
            }
            else
            {
                UserBO userBO = new UserBO();
                User loginUser = new User
                {
                    UserName = log.UserName,
                    Password = log.Password
                };
                int result = userBO.Authenticate(loginUser);

                if (result == 1)
                {

                    FormsAuthentication.SetAuthCookie(log.UserName, log.Remember);

                    if (userBO.GetUserByUserName(loginUser.UserName).Role.Role1.Equals("BannedMember"))
                    {
                        return RedirectToAction("Block", "Account");
                    }
                    else
                    {
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    string strResult = "";
                    if (result == -1)
                    {
                        strResult = Resources.Login_IncorrectUserName;
                    }
                    else if (result == -2)
                    {
                        strResult = string.Concat(Resources.Login_InactiveUser, 
                            "<br /><label style='color: black;'>Nếu chưa nhận được email kích hoạt, nhấn vào <a href='/Account/Activate'>đây</a>.</label>");
                    }
                    else if (result == -3)
                    {
                        strResult = Resources.Login_IncorrectPassword;
                    }
                    log.ErrorString = strResult;
                }
            }
            return View(log);
        }

        //
        // GET: /Account/Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Block()
        {
            try
            {
                UserBO userBO = new UserBO();
                PunishMember punish = userBO.GetPunishMemberByUserName(User.Identity.Name);

                BlockUserModel model = null;

                if (punish != null)
                {
                    model = new BlockUserModel();
                    model.ErrorMessage = Resources.Login_BlockUser;
                    model.UserName = User.Identity.Name;
                    model.StartDate = Convert.ToDateTime(punish.Date);
                    model.ExpiredDate = punish.ExpiredDate != null ? Convert.ToDateTime(punish.ExpiredDate) : new DateTime(0, 0, 0, 0, 0, 0);
                    model.IsForever = punish.IsForever != null ? (bool)punish.IsForever : false;
                    model.Reason = punish.Reason;
                    model.Staff = punish.User1.UserName;
                }

                if (User.IsInRole("BannedMember"))
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profiles", "Account");
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            AccountModel reg = new AccountModel();
            Regex alphanumeric = new Regex(@"^[a-z|A-Z|0-9]*$");
            Regex emailFormat = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            reg.UserName = form["UserName"];
            reg.Password = form["Password"];
            reg.ConfirmPassword = form["ConfirmPassword"];
            reg.Email = form["Email"];
            reg.FullName = form["FullName"];
            reg.Address = form["Street"] + ", " + form["District"] + ", " + form["Province"];
            reg.PhoneNumber = form["PhoneNumber"];
            reg.Result = 0;
            reg.ErrorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(reg.UserName) || string.IsNullOrWhiteSpace(reg.Password) || string.IsNullOrWhiteSpace(reg.ConfirmPassword)
                || string.IsNullOrWhiteSpace(reg.Email) || string.IsNullOrWhiteSpace(reg.FullName) || string.IsNullOrWhiteSpace(reg.Address)
                || string.IsNullOrWhiteSpace(reg.PhoneNumber))
            {
                reg.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (!reg.Password.Equals(reg.ConfirmPassword))
            {
                reg.ErrorMessages.Add(Resources.Password_NotMatchWithConfirm);
            }

            if (reg.UserName.Length < 6 || reg.UserName.Length > 32)
            {
                reg.ErrorMessages.Add(Resources.Reg_UserNameOutOfLength);
            }

            if (reg.Password.Length < 6 || reg.Password.Length > 32)
            {
                reg.ErrorMessages.Add(Resources.Reg_PasswordNotInLenght);
            }

            if (reg.FullName.Length <= 0 || reg.FullName.Length > 50)
            {
                reg.ErrorMessages.Add(Resources.Reg_FullnameNotInLenght);
            }

            if (reg.PhoneNumber.Length < 6 || reg.PhoneNumber.Length > 20)
            {
                reg.ErrorMessages.Add(Resources.Reg_PhoneNumberNotInLenght);
            }

            if (!alphanumeric.IsMatch(reg.UserName))
            {
                reg.ErrorMessages.Add(Resources.Reg_UserNamealphanumeric);
            }

            if (!emailFormat.IsMatch(reg.Email))
            {
                reg.ErrorMessages.Add(Resources.Reg_EmailWrongFormat);
            }


            if (reg.ErrorMessages.Count == 0)
            {

                UserBO userBO = new UserBO();

                User newUser = new User
                {
                    UserName = reg.UserName,
                    Password = reg.Password,
                    Email = reg.Email,
                    FullName = reg.FullName,
                    Address = reg.Address,
                    PhoneNumber = reg.PhoneNumber,
                    JoinDate = DateTime.Now.Date
                };

                List<int> results = userBO.CreateUser(newUser);

                if (results.Count == 1 && results[0] > 0)
                {
                    Utils utils = new Utils();
                    string verify = utils.ToBase64(string.Concat(newUser.UserName, "|", newUser.JoinDate.ToShortDateString()));

                    StreamReader sr = new StreamReader(Server.MapPath("~/Content/Activate.html"));
                    sr = System.IO.File.OpenText(Server.MapPath("~/Content/Activate.html"));
                    string content = sr.ReadToEnd();
                    content = content.Replace("[FullName]", newUser.FullName);
                    content = content.Replace("[VerifyLink]", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Activate/" + verify);
                    content = content.Replace("[LoginLink]", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Login");
                    content = content.Replace("[Signature]", Request.Url.GetLeftPart(UriPartial.Authority));

                    string subject = "Kích hoạt tài khoản của bạn tại hệ thống đặt sân bóng đá FPB";

                    WebsiteBO websiteBO = new WebsiteBO();
                    int result = websiteBO.SendEmail(newUser.Email, content, subject, true);
                    return View("Activate", result);
                }
                else
                {
                    foreach (var error in results)
                    {
                        if (error == 0)
                        {
                            reg.ErrorMessages.Add(Resources.DB_Exception);
                        }
                        else if (error == -1)
                        {
                            reg.ErrorMessages.Add(Resources.Reg_UserNameNotAvailable);
                        }
                        else if (error == -2)
                        {
                            reg.ErrorMessages.Add(Resources.Reg_EmailNotAvailable);
                        }
                    }
                }
            }
            return View(reg);
        }


        // GET: /Account/View Account Profile           
        //public ActionResult ViewAccountProfile(int id)
        //{
        //    UserBO userBO = new UserBO();
        //    User user = userBO.ViewAccountProfile(id);
        //    ViewBag.user = user;
        //    ViewBag.role = userBO.getRoleName(Int32.Parse(user.RoleId.ToString()));
        //    return View(userBO.getAllRole()); 
        //}

        //POST: Update Role
        [HttpPost]
        public ActionResult UpdateUser(FormCollection form, int userId)
        {
            int roleId = 0;
            int.TryParse(form["Role"], out roleId);
            UserBO userBO = new UserBO();
            if (userBO.UpdateRole(userId, roleId) > 0)
            {
                return RedirectToAction("Users", "WebsiteStaff");
            }
            else
            {
                return RedirectToAction("ViewAccountProfile", "AccountController", userId);
            }

        }

        [HttpPost]
        public JsonResult GetAllUserName()
        {
            UserBO userBO = new UserBO();
            var userNames = userBO.GetAllUserName();
            return Json(userNames);
        }
        [Authorize]
        public ActionResult Profiles(int? id)
        {
            UserBO userBO = new UserBO();
            User user = new User();
            try
            {
                user = userBO.GetUserById((int)id);
            }
            catch (Exception)
            {
                user = userBO.GetUserByUserName(User.Identity.Name);
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AccountModel model = new AccountModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Address = user.Address,
                RankName = user.MemberRank.RankName,
                Point = user.Point,
                RoleName = user.Role.Role1,
                JoinDate = user.JoinDate,
                ErrorMessages = new List<string>()
            };
            return View(model);
        }
        [Authorize]
        public ActionResult Edit()
        {
            UserBO userBO = new UserBO();
            User user = new User();
            try
            {
                user = userBO.GetUserByUserName(User.Identity.Name);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AccountModel model = new AccountModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Address = user.Address,
                RankName = user.MemberRank.RankName,
                Point = user.Point,
                RoleName = user.Role.Role1,
                JoinDate = user.JoinDate,
                ErrorMessages = new List<string>()
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            UserBO userBO = new UserBO();
            AccountModel model = new AccountModel();
            Regex alphanumeric = new Regex(@"^[a-z|A-Z|0-9]*$");
            Regex emailFormat = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            model.UserName = User.Identity.Name;
            model.Password = form["ConfirmPassword"];
            model.Email = form["Email"];
            model.FullName = form["FullName"];
            model.Address = form["Address"];
            model.PhoneNumber = form["PhoneNumber"];
            model.ErrorMessages = new List<string>();


            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.FullName) || string.IsNullOrWhiteSpace(model.Address)
                || string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.FullName.Length <= 0 || model.FullName.Length > 50)
            {
                model.ErrorMessages.Add(Resources.Reg_FullnameNotInLenght);
            }

            if (model.PhoneNumber.Length < 6 || model.PhoneNumber.Length > 20)
            {
                model.ErrorMessages.Add(Resources.Reg_PhoneNumberNotInLenght);
            }

            if (!emailFormat.IsMatch(model.Email))
            {
                model.ErrorMessages.Add(Resources.Reg_EmailWrongFormat);
            }

            try
            {

            }
            catch (Exception)
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                model.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (model.ErrorMessages.Count == 0)
            {
                User user = new User()
                {
                    Id = userBO.GetUserByUserName(model.UserName).Id,
                    Password = model.Password,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };


                int result = userBO.UpdateUserProfiles(user);

                if (result > 0)
                {
                    return RedirectToAction("Profiles", "Account");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Login_IncorrectPassword);
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            UserBO userBO = new UserBO();
            AccountModel model = new AccountModel();
            model.Password = form["NewPassword"];
            model.ConfirmPassword = form["ConfirmNewPassword"];
            model.ErrorMessages = new List<string>();
            string oldPassword = form["OldPassword"];
            User curUser = userBO.GetUserByUserName(User.Identity.Name);

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                model.ErrorMessages.Add(Resources.Password_NotMatchWithConfirm);
            }

            if (!oldPassword.Equals(curUser.Password))
            {
                model.ErrorMessages.Add(Resources.Login_IncorrectPassword);
            }

            if (oldPassword.Equals(model.Password))
            {
                model.ErrorMessages.Add(""); //add Resources Password moi trung password cu
            }

            if (model.ErrorMessages.Count == 0)
            {
                User user = new User()
                    {
                        Id = curUser.Id,
                        Password = model.Password
                    };

                int result = userBO.ChangePassword(user);

                if (result > 0)
                {
                    return RedirectToAction("Profiles", "Account");
                }
                else if (result == 0)
                {
                    model.ErrorMessages.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessages.Add(Resources.Login_IncorrectPassword);
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Notifications()
        {
            UserBO userBO = new UserBO();
            var model = userBO.GetAllNotificationsOfUser(User.Identity.Name);
            model = model.OrderByDescending(n => n.Id).ToList();
            return View(model);
        }

        [Authorize]
        public ActionResult UpdateNotificationsStatus(FormCollection form)
        {
            var strIds = form["ids[]"];
            var action = form["action"];
            if (!string.IsNullOrEmpty(strIds) && !string.IsNullOrEmpty(action))
            {
                string[] sids = strIds.Split(',');
                List<int> ids = new List<int>();
                foreach (var item in sids)
                {
                    ids.Add(int.Parse(item));
                }
                action = action.Trim().ToLower();
                if (action.Equals("read") || action.Equals("unread") || action.Equals("delete"))
                {
                    UserBO userBO = new UserBO();
                    var result = userBO.UpdateNotifications(User.Identity.Name, ids, action);
                    if (result == -2)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                        TempData["Error"] = "Máy chủ đang bận, xin thử lại sau.";
                    }
                }
            }
            return RedirectToAction("Notifications");
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetCountOfUnreadNotifications()
        {
            UserBO userBO = new UserBO();
            var count = userBO.GetCountOfUnreadNotifications(User.Identity.Name);
            return Json(count);
        }


        public ActionResult Recovery()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(0);
            }
            return RedirectToAction("Profiles", "Account");
        }


        [HttpPost]
        public ActionResult Recovery(FormCollection form)
        {
            try
            {
                string email = form["Email"];
                UserBO userBO = new UserBO();
                User user = userBO.GetUserByEmail(email);

                if (user == null)
                {
                    return View(-2);
                }
                else
                {
                    StreamReader sr = new StreamReader(Server.MapPath("~/Content/Reset.html"));
                    sr = System.IO.File.OpenText(Server.MapPath("~/Content/Reset.html"));
                    string content = sr.ReadToEnd();
                    content = content.Replace("[FullName]", user.FullName);
                    content = content.Replace("[UserName]", user.UserName);
                    content = content.Replace("[Password]", user.Password);
                    content = content.Replace("[LoginLink]", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Login");
                    content = content.Replace("[Signature]", Request.Url.GetLeftPart(UriPartial.Authority));

                    string subject = "Thông tin tài khoản của bạn tại hệ thống đặt sân bóng đá FPB";

                    WebsiteBO websiteBO = new WebsiteBO();
                    int result = websiteBO.SendEmail(email, content, subject, true);
                    return View(result);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult Activate(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profiles", "Account");
            }
            else
            {
                if (!string.IsNullOrEmpty(id))
                {
                    try
                    {
                        Utils utils = new Utils();
                        string verify = utils.FromBase64(id);
                        int i = verify.IndexOf("|");
                        string userName = verify.Substring(0, i);

                        UserBO userBO = new UserBO();
                        User user = userBO.GetUserByUserName(userName);

                        if (user.JoinDate.ToShortDateString().Equals(verify.Substring(i + 1, verify.Length - (i + 1))))
                        {
                            if (userBO.ActiveUser(user.Id) > 0)
                            {
                                return View(2);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return View(0);
            }
        }


        [HttpPost]
        public ActionResult Activate(FormCollection form)
        {
            try
            {
                string email = form["Email"];
                UserBO userBO = new UserBO();
                User user = userBO.GetUserByEmail(email);

                if (user == null)
                {
                    return View(-2);
                }
                else if (user.IsActive)
                {
                    return View(-3);
                }
                else
                {
                    Utils utils = new Utils();
                    string verify = utils.ToBase64(string.Concat(user.UserName, "|", user.JoinDate.ToShortDateString()));

                    StreamReader sr = new StreamReader(Server.MapPath("~/Content/Activate.html"));
                    sr = System.IO.File.OpenText(Server.MapPath("~/Content/Activate.html"));
                    string content = sr.ReadToEnd();
                    content = content.Replace("[FullName]", user.FullName);
                    content = content.Replace("[VerifyLink]", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Activate/" + verify);
                    content = content.Replace("[LoginLink]", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Login");
                    content = content.Replace("[Signature]", Request.Url.GetLeftPart(UriPartial.Authority));

                    string subject = "Kích hoạt tài khoản của bạn tại hệ thống đặt sân bóng đá FPB";

                    WebsiteBO websiteBO = new WebsiteBO();
                    int result = websiteBO.SendEmail(user.Email, content, subject, true);
                    return View(result);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
