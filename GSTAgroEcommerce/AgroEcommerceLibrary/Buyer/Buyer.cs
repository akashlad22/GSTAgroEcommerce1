using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgroEcommerceLibrary.Buyer
{
    public class Buyer
    {  
        public List<Buyer> category { get; set; }
        public List<Buyer> products { get; set; }
        public List<Buyer> Wishlist { get; set; }
        public List<Buyer> CatProd { get; set; }

        ///  Login property/// 
        public string ReturnUrl { get; set; }
        public List<AuthenticationSchemes> ExternalLogin { get; set; }


        public int BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerFullName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage =" Enter Valid Email Required")]
        public string EmailId { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string Addresstype  { get; set; }
        public string AlternaterMobileNo { get; set; }
        [Display(Name = "City ")]
        public int CityId { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string AadhaarNo { get; set; }
        public string PanCardNo { get; set; }
        public string AadhaarPhoto { get; set; }
        public string PanCardPhoto { get; set; }
        public float Salary { get; set; }
        public DateTime RegistrationDate { get; set; }

        ////////// //tblOrder
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderCode { get; set; } 
      
        // public string BuyerCode { get; set; }
        public int MyProperty { get; set; }
        public string ProductCode { get; set; }
        public int ProductQuantity { get; set; }
        public int AddressId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime ProcessDate { get; set; }

        ///Address prop
        public string Home { get; set; }
        public string Office { get; set; }
        public string Other { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public int PinCode { get; set; }
        [Display(Name = "Country ")]
        public int CountryId { get; set; }
     
        public string CountryName { get; set; }
        [Display(Name = "State ")]
        public int StateId { get; set; }
      
        public string StateName { get; set; }
               
       public string CityName { get; set; }

        public int ShippingCharges { get; set; }
        public bool IsDelete { get; set; }
        public int ShareId { get; set; }
        public bool IsNotify { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string RejectedByUserCode { get; set; }
        public string RejectionReason { get; set; }

        /////tblBuyerPayment

        public int PaymentId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public int StatusId { get; set; }
        public string TransactionId { get; set; }
        public string InvoiceNo { get; set; }

        //////Wallet
        public int WalletId { get; set; }
        public int RewardId { get; set; }
        public int TotalRewardPoints { get; set; }
        public int UseedRewardPoints { get; set; }
        public string CouponCode { get; set; }

        ////RatingAndFeedback
        public int ReviewId { get; set; }
        public int RatingStarNo { get; set; }
        public string FeedbackOnProduct { get; set; }

      
        /// Product
       
        public int ProductId { get; set; }
        //public string ProductCode { get; set; }
        public int CategoryCode { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float MRP { get; set; }
        public string MainImage { get; set; }
        public string Image1 { get; set; }     
        public string ProductWeight  { get; set; }  
        public bool IsproductExpirable { get; set; }
        public bool IsProductReturnable { get; set; }
        public float Quantity { get; set; } 


        //Category

        //public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public string CategoryCode { get; set; }

        //public int StatusId { get; set; }
        public float Commission { get; set; }
        //public string RejectionReason { get; set; }

        ///SubCategory
        public int SubCategory1Id { get; set; }
        public string SubCategory1Name { get; set; }
        public string SubCategory1Code { get; set; }

        public int SubCategory2Id { get; set; }
        public string SubCategory2Name { get; set; }
        public string SubCategory2Code { get; set; }

        public int SubCategory3Id { get; set; }
        public string SubCategory3Name { get; set; }
        public string SubCategory3Code { get; set; }

        
    }
}
