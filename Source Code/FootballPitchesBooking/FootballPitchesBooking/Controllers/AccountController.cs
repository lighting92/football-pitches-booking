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

namespace FootballPitchesBooking.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(FormCollection form, string returnUrl)
        {
            LoginModel log = new LoginModel();
            log.UserName = form["UserName"];
            log.Password = form["Password"];
            string strRemember = form["Remember"];
            bool remember = (strRemember != null) ? true : false;

            if (string.IsNullOrEmpty(log.UserName) || string.IsNullOrEmpty(log.Password))
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
                else
                {
                    string strResult = "";
                    if (result == -1)
                    {
                        strResult = Resources.Login_IncorrectUserName;
                    }
                    else if (result == -2)
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

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            RegisterModel reg = new RegisterModel();
            reg.UserName = form["UserName"];
            reg.Password = form["Password"];
            reg.ConfirmPassword = form["ConfirmPassword"];
            reg.Email = form["Email"];
            reg.FullName = form["FullName"];
            reg.Address = form["Province"] + form["District"] + form["Street"] ;
            reg.PhoneNumber = form["PhoneNumber"];

            reg.ErrorMessages = new List<string>();

            if (string.IsNullOrEmpty(reg.UserName) || string.IsNullOrEmpty(reg.Password) || string.IsNullOrEmpty(reg.ConfirmPassword)
                || string.IsNullOrEmpty(reg.Email) || string.IsNullOrEmpty(reg.FullName) || string.IsNullOrEmpty(reg.Address)
                || string.IsNullOrEmpty(reg.PhoneNumber))
            {
                reg.ErrorMessages.Add(Resources.Form_EmptyFields);
            }

            if (!reg.Password.Equals(reg.ConfirmPassword))
            {
                reg.ErrorMessages.Add(Resources.Password_NotMatchWithConfirm);
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
                    PhoneNumber = reg.PhoneNumber
                };

                List<int> result = userBO.CreateUser(newUser);

                if (result.Count == 1 && result[0] > 0)
                {
                    FormsAuthentication.SetAuthCookie(reg.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result)
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
    }
}
