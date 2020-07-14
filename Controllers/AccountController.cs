using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using CMSProject.Models.EntityManager;
using CMSProject.Models.ViewModel;
using CMSProject.Models.DB;

namespace CMSProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public JsonResult doesUserNameExist(string UserName)
        {
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                bool isValid = !db.Users.ToList().Exists(p => p.Username.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
                return Json(isValid);
            }
        }

        [HttpPost]
        public JsonResult doesEmailExist(string Email)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                bool isValid = !db.UserProfiles.ToList().Exists(p => p.Email.Equals(Email, StringComparison.CurrentCultureIgnoreCase));
                return Json(isValid);
            }
        }

        [HttpPost]
        public JsonResult CheckUsername(string username, string email, string id)
        {
            int userid = Convert.ToInt32(id);
            int isValid;
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                List<User> userlist = db.Users.ToList();
                List<UserProfile> profilelist = db.UserProfiles.ToList();
                var check  = userlist.Where(o => o.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                var checke = profilelist.Where(o => o.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
                if (check.Any())
                {
                    if (check.FirstOrDefault().UserID == userid)
                    {
                        if (checke.Any())
                        {
                            if (checke.FirstOrDefault().UserID == userid)
                            {
                                isValid = 1;
                            }
                            else
                            {
                                isValid = 3;
                            }
                        }
                        else
                        {
                            isValid = 1;
                        }
                    }
                    else
                    {
                        isValid = 2;
                    }
                }
                else
                {
                    if (checke.Any())
                    {
                        if (checke.FirstOrDefault().UserID == userid)
                        {
                            isValid = 1;
                        }
                        else
                        {
                            isValid = 3;
                        }
                    }
                    else
                    {
                        isValid = 1;
                    }
                }
            }
            return Json(isValid);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpView USV)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                if (!UM.IsUserNameExist(USV.Username) && !UM.IsEmailExist(USV.Email))
                {
                    UM.AddUserAccount(USV);
                    FormsAuthentication.SetAuthCookie(USV.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (UM.IsUserNameExist(USV.Username))
                    {
                        ModelState.AddModelError("", "Username already taken.");
                    }
                    else
                        ModelState.AddModelError("", "Email already registered.");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginView ULV, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                string password = UM.GetUserPassword(ULV.LoginName);
                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (ULV.Password.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(ULV.LoginName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(ULV);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult UProfile()
        {
            UserManager UM = new UserManager();
            ProfileView PV = new ProfileView();
            PV = UM.ViewProfile();
            return View(PV);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UProfile(ProfileView PV)
        {
            UserManager UM = new UserManager();
            UM.UpdateProfile(PV);
            return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            UserManager UM = new UserManager();
            ProfileView PV = new ProfileView();
            PV = UM.ViewProfile();
            return View(PV);
        }
    }
}