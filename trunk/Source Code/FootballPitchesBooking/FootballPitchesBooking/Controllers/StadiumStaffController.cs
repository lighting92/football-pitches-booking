using FootballPitchesBooking.BusinessObjects;
using FootballPitchesBooking.Models;
using FootballPitchesBooking.Models.StadiumStaffModels;
using FootballPitchesBooking.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPitchesBooking.Controllers
{
    [Authorize(Roles = "StadiumStaff, StadiumOwner")]
    public class StadiumStaffController : Controller
    {
        //
        // GET: /StadiumStaff/

        public ActionResult Index()
        {
            return View();
        }


        #region STADIUMS MANAGEMENT


        [Authorize(Roles="StadiumOwner")]
        public ActionResult Stadiums()
        {
            StadiumBO stadiumBO = new StadiumBO();
            var model = stadiumBO.GetStadiumsOfOwner(User.Identity.Name);
            return View(model);
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult EditStadium(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            Stadium stadium = stadiumBO.GetAuthorizeStadium(id, User.Identity.Name);

            if (stadium != null)
            {

                List<string> listImages = new List<string>();
                List<string> imageIds = new List<string>();

                foreach (var img in stadium.StadiumImages)
                {
                    listImages.Add(img.Path);
                    imageIds.Add(img.Id.ToString());

                }

                var staffs = stadiumBO.GetAllStaffOfStadium(id);                

                EditStadiumModel model = new EditStadiumModel
                {
                    Name = stadium.Name,
                    Street = stadium.Street,
                    Ward = stadium.Ward,
                    District = stadium.District,
                    Phone = stadium.Phone,
                    Email = stadium.Email,
                    IsActive = stadium.IsActive,
                    MainOwner = stadium.User.UserName,
                    Images = listImages,
                    ImageIds = imageIds,
                    Staffs = staffs
                };

                model.IsMainOwner = staffs.Any(s => s.UserName.ToLower().Equals(User.Identity.Name.ToLower()) && s.Role.Equals("Main Owner"));
                return View(model);
            }
            else
            {
                var errMsg = new List<string>();
                errMsg.Add(Resources.StadiumStaff_HaveNoPermissionTotAccessStadium);
                EditStadiumModel model = new EditStadiumModel
                {
                    ErrorMessage = errMsg
                };
                return View(model);
            }
        }

        [Authorize(Roles = "StadiumOwner")]
        [HttpPost]
        public ActionResult EditStadium(FormCollection form, int id)
        {
            StadiumBO stadiumBO = new StadiumBO();

            EditStadiumModel model = new EditStadiumModel();
            model.Name = form["Name"];
            model.MainOwner = form["MainOwner"];
            model.IsActive = bool.Parse(form["IsActive"]);
            model.Phone = form["Phone"];
            model.Email = form["Email"];
            model.Street = form["Street"];
            model.Ward = form["Ward"];
            model.District = form["District"];
            model.ErrorMessage = new List<string>();

            var curStaffs = form.AllKeys.Where(k => k.Contains("User_Name_")).ToList();
            var curRoles = form.AllKeys.Where(k => k.Contains("User_Role_")).ToList();
            List<string> changeStaffNames = new List<string>();
            List<string> changeStaffRoles = new List<string>();
            foreach (var item in curStaffs)
            {
                string count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                if (curRoles.Any(r => r.Equals("User_Role_" + count)))
                {
                    changeStaffNames.Add(form["User_Name_" + count]);
                    changeStaffRoles.Add(form["User_Role_" + count]);
                }
            }

            var newStaffs = form.AllKeys.Where(k => k.Contains("New_Staff_Name_")).ToList();
            var newRoles = form.AllKeys.Where(k => k.Contains("New_Staff_Role_")).ToList();
            List<string> newStaffNames = new List<string>();
            List<string> newStaffRoles = new List<string>();
            foreach (var item in newStaffs)
            {
                string count = item.Substring(item.LastIndexOf("_") + 1, item.Length - item.LastIndexOf("_") - 1);
                if (newRoles.Any(r => r.Equals("New_Staff_Role_" + count)))
                {
                    if (!form["New_Staff_Role_" + count].Trim().ToLower().Equals("remove"))
                    {
                        newStaffNames.Add(form["New_Staff_Name_" + count]);
                        newStaffRoles.Add(form["New_Staff_Role_" + count]);
                    }
                }
            }

            List<String> images = new List<string>();
            List<HttpPostedFileBase> listFiles = new List<HttpPostedFileBase>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] != null)
                    {
                        var file = Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            listFiles.Add(file);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.MainOwner) || string.IsNullOrEmpty(model.Phone) ||
                string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Street) || string.IsNullOrEmpty(model.Ward) ||
                string.IsNullOrEmpty(model.District))
            {
                model.ErrorMessage.Add(Resources.Form_EmptyFields);
            }

            foreach (var item in listFiles)
            {
                if (!item.ContentType.Contains("image"))
                {
                    model.ErrorMessage.Add(Resources.Upload_NotImage);
                    break;
                }
            }

            if (model.ErrorMessage.Count == 0)
            {
                Stadium stadium = new Stadium
                {
                    Id = id,
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Street = model.Street,
                    Ward = model.Ward,
                    District = model.District,
                    IsActive = model.IsActive
                };

                string serverPath = Server.MapPath("~/Content/images/");
                int result = stadiumBO.OwnerUpdateStadium(stadium, listFiles, serverPath, changeStaffNames, changeStaffRoles, newStaffNames, newStaffRoles);
                if (result == 0)
                {
                    model.ErrorMessage.Add(Resources.DB_Exception);
                }
                else if (result == -1)
                {
                    model.ErrorMessage.Add(Resources.StadiumStaff_UpdateStadiumUserNotFound);
                }
                else if (result == -2)
                {
                    model.ErrorMessage.Add(Resources.StadiumStaff_UpdateStadiumNewStaffConflict);
                }
                else
                {
                    model.SuccessMessage = Resources.Update_Success;
                }

            }

            List<string> listImages = new List<string>();
            List<string> imageIds = new List<string>();

            foreach (var img in stadiumBO.GetAllImageOfStadium(id))
            {
                listImages.Add(img.Path);
                imageIds.Add(img.Id.ToString());

            }

            var staffs = stadiumBO.GetAllStaffOfStadium(id);

            model.Images = listImages;
            model.ImageIds = imageIds;

            model.Staffs = staffs;

            model.IsMainOwner = staffs.Any(s => s.UserName.ToLower().Equals(User.Identity.Name.ToLower()) && s.Role.Equals("Main Owner"));

            return View(model);

        }

        [Authorize(Roles = "StadiumOwner")]
        [HttpPost]
        public JsonResult DeleteStadiumImage(int id)
        {
            StadiumBO stadiumBO = new StadiumBO();
            string serverPath = Server.MapPath("~/Content/images/");
            int result = stadiumBO.DeleteStadiumImage(id, serverPath);
            if (result == 0)
            {
                return Json("ERROR::" + Resources.DB_Exception);
            }
            else
            {
                return Json("SUCCESS::" + Resources.Delete_Success);
            }
        }

        [Authorize(Roles = "StadiumOwner")]
        public ActionResult Fields(int stadium)
        {
            StadiumBO stadiumBO = new StadiumBO();            
            var fields = stadiumBO.GetAuthorizeStadiumFields(stadium, User.Identity.Name);
            FieldsModel model = new FieldsModel();
            if (fields == null)
            {
                model.HavePermission = false;
                model.ErrorMessage = Resources.StadiumStaff_HaveNoPermissionTotAccessStadium;
            }
            else
            {
                model.HavePermission = true;
                var s = stadiumBO.GetStadiumById(stadium);
                model.StadiumId = s.Id;
                model.StadiumName = s.Name;
                model.StadiumAddress = s.Street + ", " + s.Ward + ", " + s.District;
                model.Fields = new List<FieldModel>();
                foreach (var item in fields)
                {
                    FieldModel f = new FieldModel();
                    f.Id = item.Id;
                    f.Number = item.Number;
                    f.FieldType = item.FieldType;
                    if (item.ParentField != null)
                    {
                        f.Parent = item.Field1.Number;
                    }
                    f.IsActive = item.IsActive;
                }
            }
            return View(model);
        }

        #endregion STADIUMS MANAGEMENT
        
        #region RESERVATION MANAGEMENT

        public ActionResult Reservations()
        {
            return View();
        }

        #endregion RESERVATION MANAGEMENT

        #region PROMOTION MANAGEMENT

        public ActionResult Promotions()
        {
            return View();
        }

        #endregion PROMOTION MANAGEMENT

    }
}
