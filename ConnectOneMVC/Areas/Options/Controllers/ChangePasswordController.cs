using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Areas.Options.Models;
using System.Data;
using Common_Lib;

namespace ConnectOneMVC.Areas.Options.Controllers
{
    public class ChangePasswordController : BaseController
    {
        // GET: Options/ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Frm_Chg_Password(string User_Name)
        {
            ChangePasswordModel model = new ChangePasswordModel();
            model.User_Name = User_Name == null? BASE._open_User_ID : User_Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Chg_Password(ChangePasswordModel model)
        {
            try
            {
                DataTable _user;
                string currPassword = "";

                if(BASE._open_ClientUser == "YES")
                {
                    _user = BASE._Chang_Password_DBOps.GetPassword_Role(model.User_Name);
                    if(_user == null)
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if(_user.Rows.Count > 0)
                    {
                        currPassword = _user.Rows[0]["USER_PWD"].ToString();
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Incorrect User...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper() || BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())
                    {
                        _user = BASE._Chang_Password_DBOps.GetPassword_Role(model.User_Name);
                    }
                    else
                    {
                        _user = BASE._Chang_Password_DBOps.GetPassword_Role(BASE._open_Cen_ID.ToString(),model.User_Name);
                    }

                    if (_user == null)
                    {
                        return Json(new
                        {
                            Message = Common_Lib.Messages.SomeError,
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (_user.Rows.Count > 0)
                    {
                        currPassword = _user.Rows[0]["USER_PWD"].ToString();
                    }
                    else
                    {
                        return Json(new
                        {
                            Message = "Incorrect User...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.Old_Password.ToUpper() == currPassword.ToUpper())
                {

                    if ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper() || BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()) && BASE._IsVolumeCenter == false)
                    {
                        BASE._Chang_Password_DBOps.ChangePassword(0, model.User_Name, model.New_Password);
                    }
                    else
                    {
                        BASE._Chang_Password_DBOps.ChangePassword(BASE._open_Cen_ID, model.User_Name, model.New_Password);
                    }

                    return Json(new
                    {
                        Message = "Password Changed Successfully...!<br/><br/>  Om Shanti..!",
                        result = true,
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Message = "Incorrect Old Password...!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Message = Common_Lib.Messages.SomeError,
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }
           
            
        }


        

    }
}