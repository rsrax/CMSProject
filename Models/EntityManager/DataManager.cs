using CMSProject.Models.DB;
using CMSProject.Models.ViewModel;
using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;

namespace CMSProject.Models.EntityManager
{
    public class DataManager
    {
        public List<ComplaintView> GetComplaintViews()
        {
            List<ComplaintView> complaints = new List<ComplaintView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var comp = db.Complaints.ToList();
                foreach(Complaint c in comp)
                {
                    ComplaintView CV = new ComplaintView();
                    CV.ComplaintID = c.ComplaintID;
                    CV.CustomerID = c.CustomerID;
                    CV.EmployeeID = c.EmployeeID.GetValueOrDefault();
                    CV.Description = c.Description;
                    CV.ComplaintStatusID = c.ComplaintStatusID;
                    var compstat = db.ComplaintStatusLookups.Where(o => o.ComplaintStatusID.Equals(CV.ComplaintStatusID)).FirstOrDefault();
                    CV.ComplaintStatus = compstat.Status;
                    complaints.Add(CV);
                }
            }
            return complaints;
        }

        public List<ComplaintView> GetMyComplaintViews(int id)
        {
            List<ComplaintView> complaints = new List<ComplaintView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var comp = db.Complaints.ToList();
                foreach (Complaint c in comp)
                {
                    ComplaintView CV = new ComplaintView();
                    CV.ComplaintID = c.ComplaintID;
                    CV.CustomerID = c.CustomerID;
                    CV.EmployeeID = c.EmployeeID.GetValueOrDefault();
                    CV.Description = c.Description;
                    CV.ComplaintStatusID = c.ComplaintStatusID;
                    var compstat = db.ComplaintStatusLookups.Where(o => o.ComplaintStatusID.Equals(CV.ComplaintStatusID)).FirstOrDefault();
                    CV.ComplaintStatus = compstat.Status;
                    if(CV.CustomerID.Equals(id))
                    {
                        complaints.Add(CV);
                    }
                }
            }
            return complaints;
        }

        public List<OrdersView> GetOrdersViews()
        {
            List<OrdersView> OV = new List<OrdersView>();
            using(CMSProjectEntities db =new CMSProjectEntities())
            {
                var OrderList = db.CustomerOrders.ToList();
                foreach(CustomerOrder order in OrderList)
                {
                    OrdersView ordersView = new OrdersView();
                    ordersView.OrderID = order.OrderID;
                    ordersView.CustomerID = order.CustomerID;
                    ordersView.OrderDate = order.OrderDate.ToString("yyyy-MM-dd");
                    if (order.ShippedDate.HasValue)
                        ordersView.ShippedDate = order.ShippedDate.GetValueOrDefault().ToString("yyyy-MM-dd");
                    else
                        ordersView.ShippedDate = null;
                    ordersView.PickupAddressID = order.PickupAddressID;
                    ordersView.ShippingAddressID = order.ShippingAddressID;
                    ordersView.Weight = (float)order.Weight;
                    ordersView.OrderValue = (float)order.OrderValue;
                    var cust = db.CustomerUsers.Where(o => o.CustomerID.Equals(ordersView.CustomerID)).FirstOrDefault();
                    var userp = db.UserProfiles.Where(o => o.UserID.Equals(cust.UserID)).FirstOrDefault();
                    ordersView.CustomerName = userp.FirstName + " " + userp.LastName;
                    var paddr = db.Addresses.Where(o => o.AddressID.Equals(ordersView.PickupAddressID)).FirstOrDefault();
                    ordersView.PickupAddress = paddr.StreetAddress + ", " + paddr.City + ", " + paddr.State + " - " + paddr.Pincode;
                    var saddr = db.Addresses.Where(o => o.AddressID.Equals(ordersView.ShippingAddressID)).FirstOrDefault();
                    ordersView.ShippingAddress = saddr.StreetAddress + ", " + saddr.City + ", " + saddr.State + " - " + saddr.Pincode;
                    ordersView.TrackingID = db.TrackingTBLs.Where(o => o.OrderID.Equals(ordersView.OrderID)).FirstOrDefault().TrackingID;
                    ordersView.OrderStatusID = db.TrackingTBLs.Where(o => o.TrackingID.Equals(ordersView.TrackingID)).FirstOrDefault().OrderStatusID;
                    ordersView.OrderStatus = db.OrderStatusLookups.Where(o => o.OrderStatusID.Equals(ordersView.OrderStatusID)).FirstOrDefault().Status;
                    ordersView.PaymentID = db.PaymentTBLs.Where(o => o.OrderID.Equals(ordersView.OrderID)).FirstOrDefault().PaymentID;
                    ordersView.PaymentStatusID = db.PaymentTBLs.Where(o => o.PaymentID.Equals(ordersView.PaymentID)).FirstOrDefault().PaymentStatusID;
                    ordersView.PaymentStatus = db.PaymentStatusLookups.Where(o => o.PaymentStatusID.Equals(ordersView.PaymentStatusID)).FirstOrDefault().Status;
                    OV.Add(ordersView);
                }
                return OV;
            }
        }

        public List<OrdersView> GetMyOrders(int id)
        {
            List<OrdersView> orders = GetOrdersViews();
            List<OrdersView> myOrders = new List<OrdersView>();
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                var user = db.CustomerUsers.Where(o => o.UserID.Equals(id)).FirstOrDefault();
                foreach (OrdersView order in orders)
                {
                    if(order.CustomerID.Equals(user.CustomerID))
                    {
                        myOrders.Add(order);
                    }
                }
                return myOrders;
            }
        }

        public List<FeedbackView> GetFeedbackViews()
        {
            List<FeedbackView> feedbacks = new List<FeedbackView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var feed = db.Feedbacks.ToList();
                foreach (Feedback f in feed)
                {
                    FeedbackView FV = new FeedbackView();
                    FV.FeedbackID = f.FeedbackID;
                    FV.CustomerID = f.CustomerID;
                    FV.Description = f.Description;
                    var cuid = db.CustomerUsers.Where(o => o.CustomerID.Equals(FV.CustomerID)).FirstOrDefault();
                    var cname = db.UserProfiles.Where(o => o.UserID.Equals(cuid.UserID)).FirstOrDefault();
                    FV.CustomerName = cname.FirstName + " " + cname.LastName;
                    feedbacks.Add(FV);
                }
            }
            return feedbacks;
        }

        public List<PaymentDetailsView> GetPDetailsViews()
        {
            List<PaymentDetailsView> paymentDetails = new List<PaymentDetailsView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var payments = db.PaymentTBLs.ToList();
                foreach (PaymentTBL payment in payments)
                {
                    PaymentDetailsView PV = new PaymentDetailsView();
                    PV.PaymentID = payment.PaymentID;
                    PV.Value = payment.Value;
                    PV.PaymentDate = payment.PaymentDate.ToString();
                    PV.PaymentStatusID = payment.PaymentStatusID;
                    var pstatus = db.PaymentStatusLookups.Where(o => o.PaymentStatusID.Equals(PV.PaymentStatusID)).FirstOrDefault();
                    PV.PaymentStatus = pstatus.Status;
                    paymentDetails.Add(PV);
                }
                return paymentDetails;
            } 
        }
        public List<TrackingDetailsView> GetTDetailsViews()
        {
            List<TrackingDetailsView> trackingDetails = new List<TrackingDetailsView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var tracks = db.TrackingTBLs.ToList();
                foreach (TrackingTBL track in tracks)
                {
                    TrackingDetailsView TV = new TrackingDetailsView();
                    TV.TrackingID = track.TrackingID;
                    TV.PickupBranchID = track.PickupBranchID.GetValueOrDefault();
                    TV.ShippingBranchID = track.ShippingBranchID.GetValueOrDefault();
                    TV.OrderStatusID = track.OrderStatusID;
                    if (track.PickupBranchID.HasValue)
                    {
                        var paddr = db.BranchLists.Where(o => o.BranchID.Equals(TV.PickupBranchID)).FirstOrDefault();
                        TV.PickupBranch = paddr.StreetAddress + ", " + paddr.City + ", " + paddr.State + " - " + paddr.Pincode;
                    }
                    else
                        TV.PickupBranch = "Not Assigned";
                    if (track.ShippingBranchID.HasValue)
                    {
                        var saddr = db.BranchLists.Where(o => o.BranchID.Equals(TV.ShippingBranchID)).FirstOrDefault();
                        TV.ShippingBranch = saddr.StreetAddress + ", " + saddr.City + ", " + saddr.State + " - " + saddr.Pincode;
                    }
                    else
                        TV.ShippingBranch = "Not Assigned";
                    var tstatus = db.OrderStatusLookups.Where(o => o.OrderStatusID.Equals(TV.OrderStatusID)).FirstOrDefault();
                    TV.OrderStatus = tstatus.Status;
                    trackingDetails.Add(TV);
                }
            }
            return trackingDetails;
        }

        public List<BranchesView> GetBranchesViews()
        {
            List<BranchesView> BV = new List<BranchesView>();
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                var branchlist = db.BranchLists.ToList();
                foreach(BranchList BL in branchlist)
                {
                    BranchesView branchesView = new BranchesView();
                    branchesView.BranchID = BL.BranchID;
                    branchesView.StreetAddress = BL.StreetAddress;
                    branchesView.City = BL.City;
                    branchesView.State = BL.State;
                    branchesView.Pincode = BL.Pincode;
                    branchesView.Active = BL.Active;
                    BV.Add(branchesView);
                }
            }
            return (BV);
        }

        public BranchesView GetBranchView(int id)
        {
            BranchesView branch = new BranchesView();
            branch = GetBranchesViews().Where(o => o.BranchID.Equals(id)).FirstOrDefault();
            return branch;
        }

        public void AddBranch(BranchesView BV)
        {
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                BranchList branch = new BranchList();
                branch.StreetAddress = BV.StreetAddress;
                branch.City = BV.City;
                branch.State = BV.State;
                branch.Pincode = BV.Pincode;
                branch.Active = true;
                db.BranchLists.Add(branch);
                db.SaveChanges();
            }
        }

        public void UpdateBranch(BranchesView BV)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                BranchList branch = new BranchList();
                branch.BranchID = BV.BranchID;
                branch.StreetAddress = BV.StreetAddress;
                branch.City = BV.City;
                branch.State = BV.State;
                branch.Pincode = BV.Pincode;
                branch.Active = (bool)BV.Active;
                db.Entry(branch).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void PlaceOrder(PlaceOrderView POV)
        {
            Address paddr = new Address();
            Address saddr = new Address();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                paddr.StreetAddress = POV.PickupAddressSA;
                paddr.City = POV.PickupAddressCity;
                paddr.State = POV.PickupAddressState;
                paddr.Pincode = POV.PickupAddressPincode;
                saddr.StreetAddress = POV.ShippingAddressSA;
                saddr.City = POV.ShippingAddressCity;
                saddr.State = POV.ShippingAddressState;
                saddr.Pincode = POV.ShippingAddressPincode;
                db.Addresses.Add(paddr);
                db.SaveChanges();
                db.Addresses.Add(saddr);
                db.SaveChanges();
                User user = db.Users.Where(o => o.Username.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault();
                CustomerUser CU = db.CustomerUsers.Where(o => o.UserID.Equals(user.UserID)).FirstOrDefault();
                CustomerOrder CO = new CustomerOrder();
                CO.CustomerID = CU.CustomerID;
                CO.OrderDate = DateTime.Now;
                CO.ShippedDate = null;
                CO.PickupAddressID = db.Addresses.Where(o => o.StreetAddress.Equals(paddr.StreetAddress)).FirstOrDefault().AddressID;
                CO.ShippingAddressID = db.Addresses.Where(o => o.StreetAddress.Equals(saddr.StreetAddress)).FirstOrDefault().AddressID;
                CO.Weight = POV.Weight;
                CO.OrderValue = POV.OrderValue;
                db.CustomerOrders.Add(CO);
                db.SaveChanges();

                TrackingTBL tracking = new TrackingTBL();
                tracking.OrderStatusID = 0;
                tracking.OrderID = CO.OrderID;
                db.TrackingTBLs.Add(tracking);
                db.SaveChanges();

                PaymentTBL pay = new PaymentTBL();
                pay.Value = CO.OrderValue;
                pay.PaymentDate = null;
                pay.PaymentStatusID = 1;
                pay.OrderID = CO.OrderID;
                db.PaymentTBLs.Add(pay);
                db.SaveChanges();
            }
        }

        public Complaint AddOrUpdateComplaint(ComplaintView CV,int i)
        {
            Complaint comp = new Complaint();
            using (CMSProjectEntities db = new CMSProjectEntities())
            { 
                if (i == 1)
                {
                    comp.Description = CV.Description;
                    var user = db.Users.Where(o => o.Username.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault();
                    int cid = db.CustomerUsers.Where(o => o.UserID.Equals(user.UserID)).FirstOrDefault().CustomerID;
                    comp.CustomerID = cid;
                    comp.ComplaintStatusID = 1;
                }
                return comp;
            }
        }

        public OrderUpdateView CreateOUView(int id)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var OrderList = db.CustomerOrders.ToList();
                CustomerOrder order = db.CustomerOrders.Where(o => o.OrderID.Equals(id)).FirstOrDefault();
                
                OrderUpdateView orders = new OrderUpdateView();
                //Orders
                orders.OrderID = order.OrderID;
                orders.CustomerID = order.CustomerID;
                orders.OrderDate = order.OrderDate;
                if (order.ShippedDate.HasValue)
                    orders.ShippedDate = order.ShippedDate.GetValueOrDefault();
                else
                    orders.ShippedDate = null;
                orders.PickupAddressID = order.PickupAddressID;
                orders.ShippingAddressID = order.ShippingAddressID;
                orders.Weight = order.Weight;
                orders.OrderValue = order.OrderValue;

                //Tracking
                orders.TrackingID = db.TrackingTBLs.Where(o => o.OrderID.Equals(orders.OrderID)).FirstOrDefault().TrackingID;
                orders.PickupBranchID = db.TrackingTBLs.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().PickupBranchID.GetValueOrDefault();
                orders.ShippingBranchID = db.TrackingTBLs.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().ShippingBranchID.GetValueOrDefault();
                orders.OrderStatusID = db.TrackingTBLs.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().OrderStatusID;

                //Payment
                orders.PaymentID = db.PaymentTBLs.Where(o => o.OrderID.Equals(orders.OrderID)).FirstOrDefault().PaymentID;
                if (db.PaymentTBLs.Where(o => o.PaymentID.Equals(orders.PaymentID)).FirstOrDefault().PaymentDate.HasValue)
                    orders.PaymentDate = db.PaymentTBLs.Where(o => o.PaymentID.Equals(orders.PaymentID)).FirstOrDefault().PaymentDate.GetValueOrDefault();
                else
                    orders.PaymentDate = null;
                orders.PaymentStatusID = db.PaymentTBLs.Where(o => o.PaymentID.Equals(orders.PaymentID)).FirstOrDefault().PaymentStatusID;

                //Extras
                var paddr = db.Addresses.Where(o => o.AddressID.Equals(orders.PickupAddressID)).FirstOrDefault();
                orders.PickupAddress = paddr.StreetAddress + ", " + paddr.City + ", " + paddr.State + " - " + paddr.Pincode;
                var saddr = db.Addresses.Where(o => o.AddressID.Equals(orders.ShippingAddressID)).FirstOrDefault();
                orders.ShippingAddress = saddr.StreetAddress + ", " + saddr.City + ", " + saddr.State + " - " + saddr.Pincode;
                orders.Branches = GetActiveBranchesViews();

                return orders;
            }
        }

        public void UpdateOrder(OrderUpdateView OUV)
        {
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                CustomerOrder CO = new CustomerOrder();
                CO.OrderID = OUV.OrderID;
                CO.CustomerID = OUV.CustomerID;
                CO.OrderDate = OUV.OrderDate;
                if(OUV.ShippedDate.HasValue)
                {
                    CO.ShippedDate = OUV.ShippedDate;
                }
                else
                {
                    CO.ShippedDate = null;
                }
                CO.PickupAddressID = OUV.PickupAddressID;
                CO.ShippingAddressID = OUV.ShippingAddressID;
                CO.Weight = OUV.Weight;
                CO.OrderValue = OUV.OrderValue;
                db.Entry(CO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                PaymentTBL pay = new PaymentTBL();
                pay.PaymentID = OUV.PaymentID;
                pay.Value = CO.OrderValue;
                if (OUV.PaymentDate.HasValue)
                {
                    pay.PaymentDate = OUV.PaymentDate;
                }
                else
                {
                    pay.PaymentDate = null;
                }
                pay.PaymentStatusID = OUV.PaymentStatusID;
                pay.OrderID = CO.OrderID;
                db.Entry(pay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                TrackingTBL tracking = new TrackingTBL();
                tracking.TrackingID = OUV.TrackingID;
                tracking.PickupBranchID = OUV.PickupBranchID;
                tracking.ShippingBranchID = OUV.ShippingBranchID;
                tracking.OrderStatusID = OUV.OrderStatusID;
                tracking.OrderID = CO.OrderID;
                db.Entry(tracking).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ComplaintView ViewComplaint(int id)
        {
            List<ComplaintView> complaints = GetComplaintViews();
            ComplaintView CV = complaints.Where(o => o.ComplaintID.Equals(id)).FirstOrDefault();
            return CV;
        }

        public List<BranchesView> GetActiveBranchesViews()
        {
            List<BranchesView> BV = new List<BranchesView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var branchlist = db.BranchLists.ToList();
                foreach (BranchList BL in branchlist)
                {
                    BranchesView branchesView = new BranchesView();
                    branchesView.BranchID = BL.BranchID;
                    branchesView.StreetAddress = BL.StreetAddress;
                    branchesView.City = BL.City;
                    branchesView.State = BL.State;
                    branchesView.Pincode = BL.Pincode;
                    branchesView.Active = BL.Active;
                    if (branchesView.Active == true)
                    { 
                        BV.Add(branchesView);
                    }
                }
            }
            return (BV);
        }

        public void UpdateComplaint(ComplaintView CV)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var emp = db.Users.Where(o => o.Username.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault();
                Complaint currComplaint = new Complaint();
                currComplaint.ComplaintID = CV.ComplaintID;
                currComplaint.CustomerID = CV.CustomerID;
                currComplaint.Description = CV.Description;
                currComplaint.ComplaintStatusID = CV.ComplaintStatusID;
                currComplaint.EmployeeID = emp.UserID;
                db.Entry(currComplaint).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}