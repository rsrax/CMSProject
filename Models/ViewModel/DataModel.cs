using CMSProject.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMSProject.Models.ViewModel
{
    public class ComplaintView
    {
        [Key]
        public int ComplaintID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        [Required(ErrorMessage ="Please enter the description of your complaint!")]
        public string Description { get; set; }
        public int ComplaintStatusID { get; set; }
        public string ComplaintStatus { get; set; }
    }

    public class FeedbackView
    {
        [Key]
        public int FeedbackID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
    }

    public class OrdersView
    {
        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public int PickupAddressID { get; set; }
        public string PickupAddress { get; set; }
        public int ShippingAddressID { get; set; }
        public string ShippingAddress { get; set; }
        public float Weight { get; set; }
        public float OrderValue { get; set; }
        public int PaymentID { get; set; }
        public int PaymentStatusID { get; set; }
        public string PaymentStatus { get; set; }
        public int TrackingID { get; set; }
        public int OrderStatusID { get; set; }
        public string OrderStatus { get; set; }
    }

    public class PlaceOrderView
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
        public int PickupAddressID { get; set; }
        [Display(Name = "Pickup Address:")]
        [Required(ErrorMessage = "Pickup Address is required.")]
        public string PickupAddressSA { get; set; }
        [Display(Name = "City:")]
        [Required(ErrorMessage = "City is required.")]
        public string PickupAddressCity { get; set; }
        [Display(Name = "State:")]
        [Required(ErrorMessage = "State is required.")]
        public string PickupAddressState { get; set; }
        [Display(Name = "Pincode:")]
        [Required(ErrorMessage = "Pincode is required.")]
        public string PickupAddressPincode { get; set; }
        public int ShippingAddressID { get; set; }
        [Display(Name = "Shipping Address:")]
        [Required(ErrorMessage = "Shipping Address is required.")]
        public string ShippingAddressSA { get; set; }
        [Display(Name = "City:")]
        [Required(ErrorMessage = "City is required.")]
        public string ShippingAddressCity { get; set; }
        [Display(Name = "State:")]
        [Required(ErrorMessage = "State is required.")]
        public string ShippingAddressState { get; set; }
        [Display(Name = "Pincode:")]
        [Required(ErrorMessage = "Pincode is required.")]
        public string ShippingAddressPincode { get; set; }
        [Display(Name = "Order Weight:")]
        [Required(ErrorMessage = "Order weight is required.")]
        public double Weight { get; set; }
        [Display(Name = "Order Price:")]
        public double OrderValue { get; set; }
        public int PaymentID { get; set; }
        public double PaymentValue { get; set; }
        public string PaymentDate { get; set; }
        public int PaymentStatusID { get; set; }
        public int TrackingID { get; set; }
        public int OrderStatusID { get; set; }
    }

    public class TrackingDetailsView
    {
        [Key]
        public int TrackingID { get; set; }
        public int PickupBranchID { get; set; }
        public int ShippingBranchID { get; set; }
        public string PickupBranch { get; set; }
        public string ShippingBranch { get; set; }
        public int OrderStatusID { get; set; }
        public string OrderStatus { get; set; }
    }

    public class PaymentDetailsView
    {
        [Key]
        public int PaymentID { get; set; }
        public double Value { get; set; }
        public string PaymentDate { get; set; }
        public int PaymentStatusID { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class BranchesView
    {
        [Key]
        public int BranchID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public bool? Active { get; set; }
    }

    public class OrderUpdateView
    {
        //Orders
        public int OrderID { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public int PickupAddressID { get; set; }
        public int ShippingAddressID { get; set; }

        //Tracking
        public int TrackingID { get; set; }
        public int PickupBranchID { get; set; }
        public int ShippingBranchID { get; set; }
        public int OrderStatusID { get; set; }

        //Payment
        public int PaymentID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public int PaymentStatusID { get; set; }

        //Extras
        public string PickupAddress { get; set; }
        public string ShippingAddress { get; set; }
        public List<BranchesView> Branches { get; set; }
    }
}