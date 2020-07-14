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
                    ordersView.ShippedDate = order.ShippedDate.ToString();
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
                    ordersView.TrackingID = db.Trackings.Where(o => o.OrderID.Equals(ordersView.OrderID)).FirstOrDefault().TrackingID;
                    ordersView.OrderStatusID = db.Trackings.Where(o => o.TrackingID.Equals(ordersView.TrackingID)).FirstOrDefault().OrderStatusID;
                    ordersView.OrderStatus = db.OrderStatusLookups.Where(o => o.OrderStatusID.Equals(ordersView.OrderStatusID)).FirstOrDefault().Status;
                    ordersView.PaymentID = db.Payments.Where(o => o.OrderID.Equals(ordersView.OrderID)).FirstOrDefault().PaymentID;
                    ordersView.PaymentStatusID = db.Payments.Where(o => o.PaymentID.Equals(ordersView.PaymentID)).FirstOrDefault().PaymentStatusID;
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
                var payments = db.Payments.ToList();
                foreach (Payment payment in payments)
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
                var tracks = db.Trackings.ToList();
                foreach (Tracking track in tracks)
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

                Payment pay = new Payment();
                pay.Value = CO.OrderValue;
                pay.PaymentDate = null;
                pay.PaymentStatusID = 1;
                pay.OrderID = CO.OrderID;
                db.Payments.Add(pay);
                db.SaveChanges();

                Tracking tracking = new Tracking();
                tracking.OrderStatusID = 1;
                tracking.OrderID = CO.OrderID;
                db.Trackings.Add(tracking);
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
                orders.ShippedDate = order.ShippedDate;
                orders.PickupAddressID = order.PickupAddressID;
                orders.ShippingAddressID = order.ShippingAddressID;

                //Tracking
                orders.TrackingID = db.Trackings.Where(o => o.OrderID.Equals(orders.OrderID)).FirstOrDefault().TrackingID;
                orders.PickupBranchID = db.Trackings.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().PickupBranchID.GetValueOrDefault();
                orders.ShippingBranchID = db.Trackings.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().ShippingBranchID.GetValueOrDefault();
                orders.OrderStatusID = db.Trackings.Where(o => o.TrackingID.Equals(orders.TrackingID)).FirstOrDefault().OrderStatusID;

                //Payment
                orders.PaymentID = db.Payments.Where(o => o.OrderID.Equals(orders.OrderID)).FirstOrDefault().PaymentID;
                orders.PaymentDate = db.Payments.Where(o => o.PaymentID.Equals(orders.PaymentID)).FirstOrDefault().PaymentDate;
                orders.PaymentStatusID = db.Payments.Where(o => o.PaymentID.Equals(orders.PaymentID)).FirstOrDefault().PaymentStatusID;

                //Extras
                var paddr = db.Addresses.Where(o => o.AddressID.Equals(orders.PickupAddressID)).FirstOrDefault();
                orders.PickupAddress = paddr.StreetAddress + ", " + paddr.City + ", " + paddr.State + " - " + paddr.Pincode;
                var saddr = db.Addresses.Where(o => o.AddressID.Equals(orders.ShippingAddressID)).FirstOrDefault();
                orders.ShippingAddress = saddr.StreetAddress + ", " + saddr.City + ", " + saddr.State + " - " + saddr.Pincode;
                orders.Branches = GetBranchesViews();

                return orders;
            }
        }

        public void UpdateOrder(OrderUpdateView OUV)
        {
            using(CMSProjectEntities db = new CMSProjectEntities())
            {
                var Order = db.CustomerOrders.Where(o => o.OrderID.Equals(OUV.OrderID)).FirstOrDefault();
                CustomerOrder CO = new CustomerOrder();
                CO.OrderID = OUV.OrderID;
                CO.CustomerID = Order.CustomerID;
                CO.OrderDate = Order.OrderDate;
                CO.ShippedDate = OUV.ShippedDate;
                CO.PickupAddressID = Order.PickupAddressID;
                CO.ShippingAddressID = Order.ShippingAddressID;
                CO.Weight = Order.Weight;
                CO.OrderValue = Order.OrderValue;
                db.Entry(CO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Payment pay = new Payment();
                pay.PaymentID = OUV.PaymentID;
                pay.Value = CO.OrderValue;
                pay.PaymentDate = OUV.PaymentDate;
                pay.PaymentStatusID = OUV.PaymentStatusID;
                pay.OrderID = CO.OrderID;
                db.Entry(pay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Tracking tracking = new Tracking();
                tracking.TrackingID = OUV.TrackingID;
                tracking.PickupBranchID = OUV.PickupBranchID;
                tracking.ShippingBranchID = OUV.ShippingBranchID;
                tracking.OrderStatusID = OUV.OrderStatusID;
                tracking.OrderID = CO.OrderID;
                db.Entry(CO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}