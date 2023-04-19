using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.WebPages;
using AgroEcommerceLibrary.Buyer;
using System.Web.Security;
using System.Threading.Tasks;
using System.Net;
using GoogleAuthentication.Services;
using System.Web.Script.Serialization;

namespace GSTAgroEcommerce.Controllers
{
    public class BuyerController : Controller
    {
        BALBuyer obj = new BALBuyer();
        string Code;
        // GET: Buyer
        [HttpGet]
        public ActionResult Login()
        {
            var clientId = "737816805057-5ks6319c89hhsh7k5mffdq8qn3gq1l1p.apps.googleusercontent.com\r\n\r\n";
            var url = "http://localhost:53017/Buyer/Googlelogin";
            var response = GoogleAuth.GetAuthUrl(clientId, url);
            ViewBag.response = response;
            return View();
        }

        [HttpPost]

        [AllowAnonymous]
        [Authorize]
        public ActionResult Login(string emailid, string password)///////Local LogIn///
        {
            Buyer obj1 = new Buyer();
            obj1.EmailId = emailid;
            obj1.Password = password;
            SqlDataReader dr;
            dr = obj.LogIn(obj1.EmailId, obj1.Password);
            if (dr.Read())
            {
                FormsAuthentication.SetAuthCookie(emailid, true);
                obj1.BuyerCode = dr["BuyerCode"].ToString();
                obj1.EmailId = dr["EmailId"].ToString();
                obj1.Password = dr["Password"].ToString();
                obj1.BuyerFullName = dr["BuyerFullName"].ToString();

                Session["BuyerCode"] = obj1.BuyerCode.ToString();
                Session["EmailId"] = obj1.EmailId.ToString();
                Session["Password"] = obj1.Password.ToString();
                Session["BuyerFullName"] = obj1.BuyerFullName.ToString();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LogInErrorMessage = "Incorrect Login Details";

            }


            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Googlelogin(string code, string emailid)////Extranal Google Login///
        {
            Buyer obj1 = new Buyer();
            var clientId = "737816805057-5ks6319c89hhsh7k5mffdq8qn3gq1l1p.apps.googleusercontent.com";
            var url = "http://localhost:53017/Buyer/Googlelogin";
            var clientsecret = "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";
            var token = await GoogleAuth.GetAuthAccessToken(code, clientId, clientsecret, url);
            var buyerprofile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());

            string[] profile = buyerprofile.Split(':', ',');
            Buyer exbuyer = new Buyer();
            for (int i = 0; i < profile.Length; i++)
            {

                string id = profile[1].ToString();
                exbuyer.BuyerCode = id.Replace("\"", "").TrimStart();

                string email = profile[3].ToString();
                exbuyer.EmailId = email.Replace("\"", "").TrimStart();

                string name = profile[7].ToString();
                exbuyer.BuyerFullName = name.Replace("\"", "").TrimStart();
            }

            SqlDataReader dr;
            dr = obj.ExtrnalLogIn(exbuyer.EmailId);///////Check Already Extranal LogedIn or Not 
            if (dr.Read())
            {
                FormsAuthentication.SetAuthCookie(emailid, true);
                obj1.BuyerCode = dr["BuyerCode"].ToString();
                obj1.EmailId = dr["EmailId"].ToString();
                obj1.BuyerFullName = dr["BuyerFullName"].ToString();

                Session["BuyerCode"] = obj1.BuyerCode.ToString();
                Session["BuyerFullName"] = obj1.BuyerFullName.ToString();

                return RedirectToAction("Index");
            }
            dr.Close();

            Buyer objU = new Buyer();
            obj1.RegistrationDate = DateTime.Now;
            obj.ResisterExtrnalLogIn(exbuyer.EmailId, exbuyer.BuyerCode, exbuyer.BuyerFullName, obj1.RegistrationDate);///Extrnal Login Registration

            Session["BuyerCode"] = exbuyer.BuyerCode.ToString();
            Session["BuyerFullName"] = exbuyer.BuyerFullName.ToString();

            return RedirectToAction("Index");

            //return View();
        }
        //Prathamesh Start//
        public ActionResult Index()
        {                                                   // summerProducts
            Buyer objU = new Buyer();
            int Months = Convert.ToInt32(DateTime.Now.Month.ToString());
            if (Months >= 2 && Months <= 5)
            {
                BALBuyer objBa = new BALBuyer();
                DataSet DS1 = new DataSet();
                DS1 = objBa.GetSUmmerProducts();

                List<Buyer> listBuy1 = new List<Buyer>();
                for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                {
                    Buyer ObjB = new Buyer();
                    ObjB.ProductName = DS1.Tables[0].Rows[i]["ProductName"].ToString();
                    ObjB.MRP = Convert.ToInt32(DS1.Tables[0].Rows[i]["MRP"].ToString());
                    string Image = DS1.Tables[0].Rows[i]["MainImage"].ToString();
                    string path = "/Content/Images/Product/";
                    ObjB.MainImage = string.Concat(path, Image);
                    listBuy1.Add(ObjB);
                }
                objU.Season = listBuy1;
                //return View(objU);

            }
            else if (Months >= 6 && Months <= 9)
            {                                              //WinterProducts
                BALBuyer objBa = new BALBuyer();
                DataSet DS1 = new DataSet();
                DS1 = objBa.GetWinterProducts();
                // Buyer objU = new Buyer();
                List<Buyer> listBuy1 = new List<Buyer>();
                for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                {
                    Buyer ObjB = new Buyer();
                    ObjB.ProductName = DS1.Tables[0].Rows[i]["ProductName"].ToString();
                    ObjB.MRP = Convert.ToInt32(DS1.Tables[0].Rows[i]["MRP"].ToString());
                    string Img = DS1.Tables[0].Rows[i]["MainImage"].ToString();
                    string path = "/Content/Images/Product/";
                    ObjB.MainImage = string.Concat(path, Img);
                    listBuy1.Add(ObjB);
                }
                objU.Season = listBuy1;
                // return View(objU);

            }
            else if (Months >= 10 && Months <= 1)
            {                                                  //RainyProducts
                BALBuyer objBa = new BALBuyer();
                DataSet DS1 = new DataSet();
                DS1 = objBa.GetRainyProducts();
                //  Buyer objU = new Buyer();
                List<Buyer> listBuy1 = new List<Buyer>();
                for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                {
                    Buyer ObjB = new Buyer();
                    ObjB.ProductName = DS1.Tables[0].Rows[i]["ProductName"].ToString();
                    ObjB.MRP = Convert.ToInt32(DS1.Tables[0].Rows[i]["MRP"].ToString());
                    string Image = DS1.Tables[0].Rows[i]["MainImage"].ToString();
                    string path = "/Content/Images/Product/";
                    ObjB.MainImage = string.Concat(path, Image);
                    listBuy1.Add(ObjB);
                }
                objU.Season = listBuy1;
                // return View(objU);

            }
            //ProductsDetails
            BALBuyer OBjA = new BALBuyer();
            DataSet DSB = new DataSet();
            DSB = OBjA.GetProductDetails();
            // Buyer objBUY = new Buyer();
            List<Buyer> listBuy = new List<Buyer>();
            for (int i = 0; i < DSB.Tables[0].Rows.Count; i++)
            {
                Buyer ObjB = new Buyer();
                ObjB.ProductName = DSB.Tables[0].Rows[i]["ProductName"].ToString();
                ObjB.MRP = Convert.ToInt32(DSB.Tables[0].Rows[i]["MRP"].ToString());
                string Image = DSB.Tables[0].Rows[i]["MainImage"].ToString();
                string path = "/Content/Images/Product/";
                ObjB.MainImage = string.Concat(path, Image);



                listBuy.Add(ObjB);


            }
            objU.Buyers = listBuy;
            return View(objU);



        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //----------------Prathamesh End----------------//
        public ActionResult SearchProducts(string prosearch)
        {
            DataSet ds = new DataSet();
            ds = obj.SearchData(prosearch);
            Buyer objU = new Buyer();
            List<Buyer> productlist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                productlist.Add(new Buyer
                {

                    ProductName = dr["ProductName"].ToString(),
                    ProductCode = dr["ProductCode"].ToString(),
                    MRP = Convert.ToInt32(dr["MRP"].ToString()),
                    MainImage = dr["MainImage"].ToString(),
                    StatusId = Convert.ToInt32(dr["StatusId"].ToString()),
                    Quantity = Convert.ToInt32(dr["Quantity"].ToString()),

                });

                objU.products = productlist;
            }
            //string byuercode = Session["BuyerCode"].ToString();
            //DataSet ds1 = new DataSet();
            //ds1 = obj.WishList(byuercode);
            //Buyer prdDetails = new Buyer();
            //List<Buyer> LstProducts = new List<Buyer>();
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    Buyer obj1 = new Buyer();

            //   // obj1.MainImage = (ds.Tables[0].Rows[i]["MainImage"].ToString());
            //    obj1.ProductCode = (ds1.Tables[0].Rows[i]["ProductCode"].ToString());
            //    //obj1.ProductName = (ds.Tables[0].Rows[i]["ProductName"].ToString());
            //    //obj1.MRP = Convert.ToInt32(ds.Tables[0].Rows[i]["MRP"].ToString());
            //    //obj1.StatusId = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusId"].ToString());
            //    //obj1.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
            //    LstProducts.Add(obj1);

            //}
            //// prdDetails.CatProd = LstProducts;
            //objU.CatProd = LstProducts;

            return View(objU);
        }
        public ActionResult ShowProdDetails(string productcode)/////Show Product Details
        {
            Buyer obj1PD = new Buyer();
            obj1PD.ProductCode = productcode;
            DataSet ds = new DataSet();
            ds = obj.ViewProductDetails(obj1PD.ProductCode);
            List<Buyer> prodDetails = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Buyer objU = new Buyer();
                objU.ProductCode = dr["ProductCode"].ToString();
                objU.ProductName = dr["ProductName"].ToString();
                objU.MainImage = dr["MainImage"].ToString();
                objU.Image1 = dr["Image1"].ToString();
                objU.MRP = Convert.ToInt32(dr["MRP"].ToString());
                objU.Description = dr["Description"].ToString();
                objU.ProductWeight = dr["ProductWeight"].ToString();
                objU.IsproductExpirable = dr["IsproductExpirable"].ToString() == "True";
                objU.IsProductReturnable = dr["IsProductReturnable"].ToString() == "True";
                objU.StatusId = Convert.ToInt32(dr["StatusId"].ToString());
                objU.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                prodDetails.Add(objU);
            }
            obj1PD.products = prodDetails;

            Session["url"] = HttpContext.Request.Url.AbsoluteUri;      ///// Get page url
            //Session["ProductCodeForWishList"] = obj1PD.ProductCode;
            return View(obj1PD);
        }
        public ActionResult AddToWishList(string jsonproductcode, int qty)
        {
            try
            {
                if (Session["BuyerCode"] != null)
                {
                    //var serializer = new JavaScriptSerializer();
                    //dynamic productcode = serializer.Deserialize(jsonproductcode, typeof(object));
                    //Get your variables here from AJAX call
                    // var productcode = Convert.ToInt32(jsondata["id"]);

                    Buyer obj1 = new Buyer();
                    string buyercode = Session["BuyerCode"].ToString();
                    int orderstatusid = 18;
                    SqlDataReader dr;
                    dr = obj.CheckProductInwishList(buyercode, jsonproductcode);
                    if (dr.Read())
                    {
                        obj1.BuyerCode = dr["BuyerCode"].ToString();
                        obj1.ProductCode = dr["ProductCode"].ToString();
                        obj1.OrderStatusId = Convert.ToInt32(dr["OrderStatusId"].ToString());
                        obj1.OrderCode = dr["OrderCode"].ToString();
                    }
                   // Session["OrderCodeShowWishlistProduct"] = obj1.OrderCode;

                    dr.Close();
                    if (obj1.OrderCode != null)
                    {
                        string ordercode = obj1.OrderCode;

                        obj.RemoveFromWishList(ordercode, buyercode, jsonproductcode, orderstatusid);

                        return Json(new { status = "false", msg = "Remove From WishList" });
                    }
                    if (obj1.OrderCode == null)
                    {
                        SqlDataReader dr1;
                        dr1 = obj.GenerateOrderCode();
                        while (dr1.Read())
                        {
                            int generatecode = Convert.ToInt32(dr1["Id"].ToString());
                            generatecode = generatecode + 1;
                            string Id = "OD0";
                            Code = Id + generatecode;
                        }
                        dr1.Close();

                        obj1.OrderCode = Code;
                        string addordercode = obj1.OrderCode;
                        bool isnotify;
                        if (qty == 0)        ////////////////////////Update IsNotify when Product OutOf Stock
                        {
                            isnotify = true;
                            obj.AddToWishList(addordercode, buyercode, jsonproductcode, orderstatusid, isnotify);
                            ////Add Product In WishList with Notify
                        }
                        else
                        {
                            isnotify = false;
                            obj.AddToWishList(addordercode, buyercode, jsonproductcode, orderstatusid, isnotify);
                            ////Add Product In WishList with Notify
                        }

                        return Json(new { status = "true", msg = "Add To WishList" });
                    }
                    //Session["OrderCodeForWishList"] = obj1.OrderCode;

                    //Session["OrderStatusForWishList"] = 18;


                }
                else
                {
                    //var result = new { data = "error" };
                    //return Json (result,JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View();
        }
        //public ActionResult BuyerWishListProd(string productcode)
        //{
        //    if (Session["BuyerCode"] != null)
        //    {
        //        Buyer obj1 = new Buyer();
        //        string buyercode = Session["BuyerCode"].ToString();


        //        SqlDataReader dr = obj.CheckProductInwishList(buyercode, productcode);
        //        if (dr.Read())
        //        {
        //            obj1.BuyerCode = dr["BuyerCode"].ToString();
        //            obj1.ProductCode = dr["ProductCode"].ToString();
        //            obj1.OrderCode = dr["OrderCode"].ToString();
        //            obj1.OrderStatusId = Convert.ToInt32(dr["OrderStatusId"].ToString());

        //        }
        //        if (obj1.OrderCode != null)
        //        {
        //            return Json(new { status = "true" });
        //        }
        //        else
        //        {
        //            return Json(new { status = "false" });
        //        }


        //    }


        //    return View();
        //}
        public ActionResult WishListGrid(string productcode)
        {
            if (Session["BuyerCode"] != null)
            {
                string byuercode = Session["BuyerCode"].ToString();



                DataSet ds = new DataSet();
                ds = obj.WishList(byuercode);
                Buyer prdDetails = new Buyer();
                List<Buyer> LstProducts = new List<Buyer>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Buyer obj1 = new Buyer();

                    obj1.MainImage = (ds.Tables[0].Rows[i]["MainImage"].ToString());
                    obj1.ProductCode = (ds.Tables[0].Rows[i]["ProductCode"].ToString());
                    obj1.ProductName = (ds.Tables[0].Rows[i]["ProductName"].ToString());
                    obj1.MRP = Convert.ToInt32(ds.Tables[0].Rows[i]["MRP"].ToString());
                    obj1.StatusId = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusId"].ToString());
                    obj1.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
                    LstProducts.Add(obj1);

                }
                prdDetails.products = LstProducts;

               
                return View(prdDetails);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult DeleteFromWishList(string productcode)
        {
            if (Session["BuyerCode"] != null)
            {
                Buyer obj1 = new Buyer();
                string buyercode = Session["BuyerCode"].ToString();

                obj.DeleteWishlist(buyercode, productcode);
            }
            var result = new { data = "Delete" };
           // return RedirectToAction("WishListGrid");
           return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToCart(string productcode)
        {
            if (Session["BuyerCode"] != null)
            {
                Buyer obj1 = new Buyer();
                string buyercode = Session["BuyerCode"].ToString();
                SqlDataReader dr = obj.CheckProductInOrder(buyercode, productcode);

                if (dr.Read())
                {
                    obj1.BuyerCode = dr["BuyerCode"].ToString();
                    obj1.OrderCode = dr["OrderCode"].ToString();
                    obj1.ProductCode = dr["ProductCode"].ToString();
                    obj1.OrderStatusId = Convert.ToInt32(dr["OrderStatusId"].ToString());
                }
                dr.Close();
                if (obj1.OrderStatusId == 18)
                {
                    SqlDataReader dr2 = obj.CheckProductInCart(buyercode, productcode);
                    if (dr2.Read())
                    {
                        obj1.BuyerCode = dr2["BuyerCode"].ToString();
                        obj1.OrderCode = dr2["OrderCode"].ToString();
                        obj1.ProductCode = dr2["ProductCode"].ToString();
                        obj1.OrderStatusId = Convert.ToInt32(dr2["OrderStatusId"].ToString());
                    }
                    dr2.Close();
                    if (obj1.OrderStatusId == 19)
                    {
                        return Json(new { status = "true", msg = "Already In Cart" });

                    }
                    if (obj1.OrderStatusId == 18)
                    {
                      
                        //int orderstatusid = 19;
                        obj.UpdateStatusAndAddToCart(buyercode, productcode);
                        return Json(new { status = "New", msg = "Add To Cart" });
                    }

                }
                if (obj1.OrderStatusId == 19)
                {
                    return Json(new { status = "true", msg = "Already In Cart" });
                }
                if (obj1.OrderCode == null)
                {
                    SqlDataReader dr1;
                    dr1 = obj.GenerateOrderCode();
                    while (dr1.Read())
                    {
                        int generatecode = Convert.ToInt32(dr1["Id"].ToString());
                        generatecode = generatecode + 1;
                        string Id = "OD0";
                        Code = Id + generatecode;
                    }
                    dr1.Close();

                    obj1.OrderCode = Code;
                    string addordercode = obj1.OrderCode;
                    int orderstatusid = 19;
                    obj.AddToCart(addordercode, buyercode, productcode, orderstatusid);

                    return Json(new { status = "New", msg = "Add To Cart" });
                }


            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }




        public ActionResult SideBarShop(int? id, string prosearch)
        {
            DataSet ds = new DataSet();
            ds = obj.CategoeryShow();
            Buyer objU = new Buyer();
            List<Buyer> categorylist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                categorylist.Add(new Buyer
                {
                    CategoryName = dr["CategoryName"].ToString(),
                    CategoryId = Convert.ToInt32(dr["CategoryId"].ToString())

                });
                objU.category = categorylist;

            }
            if (id == null)
            {
                DataSet ds1 = new DataSet();
                ds1 = obj.ShowAllProducts();
                // Buyer objU = new Buyer();

                List<Buyer> prodlist = new List<Buyer>();
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    prodlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),
                        StatusId = Convert.ToInt32(dr["StatusId"].ToString()),
                        Quantity = Convert.ToInt32(dr["Quantity"].ToString()),
                    });
                }

                objU.products = prodlist;
            }
            else
            {
                DataSet ds2 = new DataSet();
                ds2 = obj.ShowCatProducts((int)id);
                // Buyer objU = new Buyer();
                List<Buyer> prodlist = new List<Buyer>();
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    prodlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),
                        StatusId = Convert.ToInt32(dr["StatusId"].ToString()),
                        Quantity = Convert.ToInt32(dr["Quantity"].ToString()),
                    });
                }

                objU.products = prodlist;
            }
            if (prosearch != null)
            {
                DataSet ds3 = new DataSet();
                ds3 = obj.SearchData(prosearch);

                List<Buyer> productlist = new List<Buyer>();
                foreach (DataRow dr in ds3.Tables[0].Rows)
                {
                    productlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),
                        StatusId = Convert.ToInt32(dr["StatusId"].ToString()),
                        Quantity = Convert.ToInt32(dr["Quantity"].ToString()),

                    });
                }
                objU.products = productlist;
            }

            return View(objU);
        }

        public ActionResult ShowCategoeryProducts(int id)
        {

            DataSet ds = new DataSet();
            ds = obj.ShowCatProducts(id);
            Buyer objU = new Buyer();
            List<Buyer> prodlist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Buyer
                {
                    ProductName = dr["ProductName"].ToString(),
                    //ProductCode = dr["ProductCode"].ToString(),
                    MRP = Convert.ToInt32(dr["MRP"].ToString()),
                    MainImage = dr["MainImage"].ToString(),
                    StatusId = Convert.ToInt32(dr["StatusId"].ToString()),
                    Quantity = Convert.ToInt32(dr["Quantity"].ToString()),
                });
            }

            objU.CatProd = prodlist;
            return PartialView("_ShowCategoeryProducts", objU);
        }
        public ActionResult Checkout()
        {
            if (Session["BuyerCode"] != null)
            {
                string buyercode = Session["BuyerCode"].ToString();
                // string buyercode = "B004";
                BALBuyer objbal = new BALBuyer();
                DataSet ds = new DataSet();
                ds = objbal.checkout(buyercode);

                List<Buyer> chrckoutlist = new List<Buyer>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    chrckoutlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        //ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        BuyerFullName = dr["BuyerFullName"].ToString(),
                        Home = dr["Home"].ToString(),
                        Office = dr["Office/Business"].ToString(),
                        Other = dr["Other"].ToString(),
                        ProductQuantity = Convert.ToInt32(dr["ProductQuantity"].ToString()),

                    });
                }
                return View(chrckoutlist);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddAddress()
        {
            GetCountry();

            return View();
        }
        ///.......... Address binding.................//

        public void GetCountry()
        {
            BALBuyer objB = new BALBuyer();
            DataSet ds = objB.GetCountry();
            List<SelectListItem> countrylist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                countrylist.Add(new SelectListItem
                {
                    Text = item["CountryName"].ToString(),
                    Value = item["CountryId"].ToString()

                });

                ViewBag.country = countrylist;
            }


        }
        [HttpGet]
        public JsonResult GetState(int countryid)
        {
            BALBuyer objB = new BALBuyer();
            //User objA = new User();
            DataSet ds = objB.GetState(countryid);
            List<SelectListItem> Statelist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Statelist.Add(new SelectListItem
                {
                    Text = item["StateName"].ToString(),
                    Value = item["StateId"].ToString()

                });


            }
            return Json(Statelist, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Get_City(int stateid)
        {
            BALBuyer objB = new BALBuyer();
            //User objA = new User();
            DataSet ds = objB.GetCity(stateid);
            List<SelectListItem> Citylist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Citylist.Add(new SelectListItem
                {
                    Text = item["CityName"].ToString(),
                    Value = item["CityId"].ToString()

                });


            }
            return Json(Citylist, JsonRequestBehavior.AllowGet);
        }


    }
}