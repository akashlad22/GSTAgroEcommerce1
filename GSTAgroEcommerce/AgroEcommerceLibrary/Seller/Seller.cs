using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Seller
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string SellerCode { get; set; }
        public string SellerFullName { get; set; }
        public string BusinessName { get; set; }
        public int BusinessPinCode { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public int PinCode { get; set; }
        public string RejectionReasonFromAdmin { get; set; }
        public DateTime DOB { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string AlternateMobileNo { get; set; }
        public int StatusId { get; set; }

        public DateTime SellerApprovalDate { get; set; }

        /////tblSellerPayment
        public int SellerPaymentId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaidAmount { get; set; }
        public string TransactionId { get; set; }
        public string PaymentDoneFile { get; set; }
        public bool IsDelete { get; set; }
        ///tblProduct
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double CurrentStock { get; set; }
        public double MinRangeDiscount { get; set; }
        public double MRP { get; set; }
        public double ProductWeight { get; set; }

        public double Discount { get; set; }
        public bool IsProductReturnable { get; set; }
        public bool IsproductExpirable { get; set; }
        public string PrductExpiryDuration { get; set; }
        public bool IsProductSeasonable { get; set; }
        public string SeasonName { get; set; }
        // public string ProductWeightRange { get; set; }
        public DateTime ProductRegistrationDate { get; set; }
        public DateTime ProductApprovalDate { get; set; }
        public string ManufacturerName { get; set; }
        public double MaximumStock { get; set; }
        public double MinimumStock { get; set; }
        public string RejectionReasonfromAdmin { get; set; }

        ////tblTransactionSeller

        public int SellerTransactionId { get; set; }
        public double TotalAmount { get; set; }
        public double PartialPayment { get; set; }
        public int TotalOrder { get; set; }

        ///tblSellerDocument

        public int SellerDocumentId { get; set; }
        public string AadharNo { get; set; }
        public string PanCardNo { get; set; }
        public string AadharImage { get; set; }

        public string PanImage { get; set; }
        public string GSTIN { get; set; }
        public string BusinessAadharNo { get; set; }
        public string BusinessPanNo { get; set; }
        public string BusinessAdharImage { get; set; }
        public string BusinessPanImage { get; set; }
    }
}
