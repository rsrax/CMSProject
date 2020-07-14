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
    [AuthorizeRoles("Employee")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult ViewFeedback()
        {
            return View();
        }
        public ActionResult ManageComplaints()
        {
            return View();
        }
        public ActionResult ManageOrders()
        {
            return View();
        }
        public ActionResult GetAllFeedbacks()
        {
            DataManager DM = new DataManager();
            List<FeedbackView> fv = new List<FeedbackView>();
            fv = DM.GetFeedbackViews();
            return Json(new { data = fv }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllOrders()
        {
            DataManager DM = new DataManager();
            List<OrdersView> ov = new List<OrdersView>();
            ov = DM.GetOrdersViews();
            return Json(new { data = ov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOrders(int id=0)
        {
            OrderUpdateView OUV = new OrderUpdateView();
            DataManager DM = new DataManager();
            OUV = DM.CreateOUView(id);
            return View(OUV);
        }
        [HttpPost]
        public ActionResult UpdateOrders(OrderUpdateView OUV)
        {
            DataManager DM = new DataManager();
            DM.UpdateOrder(OUV);
            return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComplaints()
        {
            DataManager DM = new DataManager();
            List<ComplaintView> cv = new List<ComplaintView>();
            cv = DM.GetComplaintViews();
            return Json(new { data = cv }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateComplaint(int id=0)
        {
            DataManager DM = new DataManager();
            ComplaintView CV = DM.ViewComplaint(id);
            return View(CV);
        }

        [HttpPost]
        public ActionResult UpdateComplaint(ComplaintView CV)
        {
            DataManager DM = new DataManager();
            DM.UpdateComplaint(CV);
            return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
        }
    }
}