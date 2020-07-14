using CMSProject.Models.DB;
using CMSProject.Models.EntityManager;
using CMSProject.Models.ViewModel;
using CMSProject.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace CMSProject.Controllers
{
    [AuthorizeRoles("Customer")]
    public class CUserController : Controller
    {
        // GET: CUser
        public ActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(PlaceOrderView POV)
        {
            if (ModelState.IsValid)
            {
                DataManager DM = new DataManager();
                DM.PlaceOrder(POV);
                return RedirectToAction("MyOrders", "CUser");
            }
            return View();
        }

        public ActionResult GetMYOrders()
        {
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                CMSProject.Models.DB.User user = db.Users.Where(o => o.Username.Equals(username)).FirstOrDefault();
                DataManager DM = new DataManager();
                List<OrdersView> orders = new List<OrdersView>();
                orders = DM.GetMyOrders(user.UserID);
                return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMYComplaints()
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                CMSProject.Models.DB.User user = db.Users.Where(o => o.Username.Equals(username)).FirstOrDefault();
                DataManager DM = new DataManager();
                List<ComplaintView> orders = new List<ComplaintView>();
                var cuser = db.CustomerUsers.Where(o => o.UserID.Equals(user.UserID)).FirstOrDefault();
                orders = DM.GetMyComplaintViews(cuser.CustomerID);
                return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MakeComplaint()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(FeedbackView FV)
        {
            using(CMSProjectEntities db =new CMSProjectEntities())
            {
                CMSProject.Models.DB.Feedback FB = new Feedback();
                FB.Description = FV.Description;
                UserManager UM = new UserManager();
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                CMSProject.Models.DB.User user = db.Users.Where(o => o.Username.Equals(username)).FirstOrDefault();
                FB.CustomerID = db.CustomerUsers.Where(o => o.UserID.Equals(user.UserID)).FirstOrDefault().CustomerID;
                db.Feedbacks.Add(FB);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Complaint()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeComplaint(ComplaintView CV)
        {
            DataManager DM = new DataManager();
            Complaint C = new Complaint();
            C = DM.AddOrUpdateComplaint(CV, 1);
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                db.Complaints.Add(C);
                db.SaveChanges();
            }
            return Json(new { success = true, message = "Added Successfully!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Track(int id=0)
        {
            if (id==0)
            {
                return HttpNotFound();
            }
            else
            {
                List<OrdersView> MyOrders = new List<OrdersView>();
                DataManager DM = new DataManager();
                MyOrders = DM.GetOrdersViews();
                OrdersView order = MyOrders.Where(o => o.OrderID.Equals(id)).FirstOrDefault();
                return View(order);
            }
        }
    }
}