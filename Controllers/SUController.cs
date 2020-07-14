using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CMSProject.Models.ViewModel;
using CMSProject.Models.EntityManager;
using CMSProject.Security;
using CMSProject.Models.DB;
using System.Reflection;
using System.Data.Entity;

namespace CMSProject.Controllers
{
    [AuthorizeRoles("Admin")]
    public class SUController : Controller
    {
        public ActionResult ManageEmployees()
        {
            return View();
        }

        public ActionResult ManageBranches()
        {
            return View();
        }

        public ActionResult ViewComplaints()
        {
            return View();
        }

        public ActionResult ViewCustomers()
        {
            return View();
        }

        public ActionResult ViewFeedback()
        {
            return View();
        }

        public ActionResult ViewOrders()
        {
            return View();
        }

        public ActionResult ViewPaymentDetails()
        {
            return View();
        }

        public ActionResult ViewTrackingDetails()
        {
            return View();
        }

        public ActionResult GetAllUserProfiles()
        {
            List<UserProfileView> profiles = new List<UserProfileView>();
            UserManager UM = new UserManager();
            profiles = UM.GetAllUserProfiles();
            return Json(new { data = profiles }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCustomerProfiles()
        {
            List<CustomerProfileView> profiles = new List<CustomerProfileView>();
            UserManager UM = new UserManager();
            profiles = UM.GetAllCustomerProfiles();
            return Json(new { data = profiles }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if(id==0)
            {
                return View(new UserProfileView());
            }
            else
            {
                UserManager UM = new UserManager();
                return View(UM.GetUserProfile(id));
            }            
        }

        [HttpPost]
        public ActionResult AddOrEdit(UserProfileView UPV)
        {
            UserManager UM = new UserManager();
            if (UPV.UserProfileID == 0)
            {
                UM.AddEmployeeAccount(UPV);
                return Json(new { success = true, message = "Added Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                UM.UpdateUserAccount(UPV);
                return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UserManager UM = new UserManager();
            UserProfileView UPV = new UserProfileView();
            UPV = UM.GetAllUserProfiles().Where(o => o.UserProfileID.Equals(id)).FirstOrDefault();
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                UserProfile userProfile = new UserProfile();
                userProfile.UserProfileID = UPV.UserProfileID;
                userProfile.UserID = UPV.UserID;
                userProfile.FirstName = UPV.FirstName;
                userProfile.LastName = UPV.LastName;
                userProfile.Gender = UPV.Gender;
                userProfile.BirthDate = Convert.ToDateTime(UPV.BirthDate);
                userProfile.Email = UPV.Email;
                userProfile.Mobile = UPV.Mobile;
                userProfile.RoleID = UPV.RoleID;
                db.Entry(userProfile).State = EntityState.Deleted;
                db.SaveChanges();
                CMSProject.Models.DB.User user = new User();
                user.UserID = userProfile.UserID;
                user.Username = UPV.Username;
                user.Password = UPV.Password;
                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteB(int id)
        {
            DataManager DM = new DataManager();
            BranchesView BV = new BranchesView();
            BV = DM.GetBranchView(id);
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                BranchList branch = new BranchList();
                branch.BranchID = BV.BranchID;
                branch.StreetAddress = BV.StreetAddress;
                branch.City = BV.City;
                branch.State = BV.State;
                branch.Pincode = BV.Pincode;
                branch.Active = (bool)BV.Active;
                db.Entry(branch).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetAllComplaints()
        {
            DataManager DM = new DataManager();
            List<ComplaintView> cv = new List<ComplaintView>();
            cv = DM.GetComplaintViews();
            return Json(new { data = cv }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllOrders()
        {
            DataManager DM = new DataManager();
            List<OrdersView> ov = new List<OrdersView>();
            ov = DM.GetOrdersViews();
            return Json(new { data = ov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllFeedbacks()
        {
            DataManager DM = new DataManager();
            List<FeedbackView> fv = new List<FeedbackView>();
            fv = DM.GetFeedbackViews();
            return Json(new { data = fv }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllPaymentDetails()
        {
            DataManager DM = new DataManager();
            List<PaymentDetailsView> pv = new List<PaymentDetailsView>();
            pv = DM.GetPDetailsViews();
            return Json(new { data = pv }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllTrackingDetails()
        {
            DataManager DM = new DataManager();
            List<TrackingDetailsView> tv = new List<TrackingDetailsView>();
            tv = DM.GetTDetailsViews();
            return Json(new { data = tv }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllBranchDetails()
        {
            DataManager DM = new DataManager();
            List<BranchesView> BL = new List<BranchesView>();
            BL = DM.GetBranchesViews();
            return Json(new { data = BL }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEditB(int id = 0)
        {
            if (id == 0)
            {
                return View(new BranchesView());
            }
            else
            {
                DataManager DM = new DataManager();
                return View(DM.GetBranchView(id));
            }
        }

        [HttpPost]
        public ActionResult AddOrEditB(BranchesView BV)
        {
            DataManager DM = new DataManager();
            if (BV.BranchID == 0)
            {
                DM.AddBranch(BV);
                return Json(new { success = true, message = "Added Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DM.UpdateBranch(BV);
                return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}