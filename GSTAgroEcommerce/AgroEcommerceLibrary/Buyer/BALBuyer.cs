using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Buyer
{

    public class BALBuyer
    {
        //SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS;Initial Catalog=GSTAgroE-Commerce;Integrated Security=True");
        //Prathamesh//
        SqlConnection con = new SqlConnection("Data Source=PRATHAMESH\\SQLEXPRESS;Initial Catalog=GSTAgroE-Commerce;Integrated Security=True");

        public SqlDataReader LogIn(string email, string password)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "login");
            cmd.Parameters.AddWithValue("@emailid", email);
            cmd.Parameters.AddWithValue("@password", password);
            //cmd.Parameters.AddWithValue("@buyerfullname", buyername);
            //cmd.Parameters.AddWithValue("@buyercode", buyercode);

            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            con.Close();

        }
        public SqlDataReader ExtrnalLogIn(string email)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CheckExtranalLogin");
            cmd.Parameters.AddWithValue("@emailid", email);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        public void ResisterExtrnalLogIn(string email, string buyercode, string buyerfullname, DateTime registationdate)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "RegisterExtrLogin");
            cmd.Parameters.AddWithValue("@emailid", email);
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@buyerfullname", buyerfullname);
            cmd.Parameters.AddWithValue("@registrationdate", registationdate);
            cmd.ExecuteNonQuery();

        }

        public void AddToWishList(string addordercode, string buyercode, string productcode, int orderstatusid,bool isnotify)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "AddToCartAndWishList");
            cmd.Parameters.AddWithValue("@OrderCode", addordercode);
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);
            cmd.Parameters.AddWithValue("@OrderStatusId", orderstatusid);
            cmd.Parameters.AddWithValue("@IsNotify", isnotify);

            cmd.ExecuteNonQuery();

        }

        public void AddToCart(string addordercode, string buyercode, string productcode, int orderstatusid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "AddToCartAndWishList");
            cmd.Parameters.AddWithValue("@OrderCode", addordercode);
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);
            cmd.Parameters.AddWithValue("@OrderStatusId", orderstatusid);
            cmd.ExecuteNonQuery();

        }
        public void UpdateStatusAndAddToCart(string buyercode, string productcode) 
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "UpdateOrderAndToCart");
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);
            //cmd.Parameters.AddWithValue("@OrderStatusId", orderstatusid);
            cmd.ExecuteNonQuery();

        }

        public SqlDataReader CheckProductInwishList(string buyercode, string productcode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CheckProductInWishList");
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);

            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
            public SqlDataReader CheckProductInCart(string buyercode, string productcode) 
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "CheckProductInCart");
                cmd.Parameters.AddWithValue("@buyercode", buyercode);
                cmd.Parameters.AddWithValue("@productcode", productcode);

                SqlDataReader dr2 = cmd.ExecuteReader();
                return dr2;
                con.Close();
            }

            public void RemoveFromWishList(string ordercode, string buyercode, string productcode, int orderstatusid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RemoveFromWishList");
            cmd.Parameters.AddWithValue("@OrderCode", ordercode);
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);
            cmd.Parameters.AddWithValue("@OrderStatusId", orderstatusid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public SqlDataReader GenerateOrderCode()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GenerateOrderCode");
            SqlDataReader dr1;
            dr1 = cmd.ExecuteReader();
            return dr1;
            con.Close();


        }

        public SqlDataReader CheckProductInOrder(string buyercode,string productcode) 
        {
            if (con.State==System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CheckPoductInOrder");
            ////Check Product in Order Table And Update OrderStatusId And set product in Cart  
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.Parameters.AddWithValue("@productcode", productcode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }

        //public void UpdateOrderId(string buyercode,string productcode,int orderstatusid,string ordercode)///Update WishList Status And Add into Cart
        //{
        //    if (con.State==System.Data.ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
        //    cmd.Parameters.AddWithValue("@Flag", "AddToCartAndWishList");
        //    cmd.Parameters.AddWithValue("@OrderCode", ordercode);
        //    cmd.Parameters.AddWithValue("@buyercode", buyercode);
        //    cmd.Parameters.AddWithValue("@productcode", productcode);
        //    cmd.Parameters.AddWithValue("@OrderStatusId", orderstatusid);
        //    cmd.ExecuteNonQuery();


        //}
        public DataSet SearchData(string prosearch)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SerarchWithImage");
            cmd.Parameters.AddWithValue("@productname", prosearch);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet CategoeryShow()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {

                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetCategory");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
            con.Close();


        }
        ///All Product Show\\\
        public DataSet ShowAllProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ShowAllProducts");
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        //public SqlDataReader ShowCatProducts(int id) 
        //{
        //    if (con.State == System.Data.ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "ShowCatProd");
        //    cmd.Parameters.AddWithValue("@categoryid", id);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;
        //    con.Close();
        //}
        public DataSet ShowCatProducts(int id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ShowCatProd");
            cmd.Parameters.AddWithValue("@categoryid", id);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }

        public DataSet checkout(string buyercode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Checkout");
            cmd.Parameters.AddWithValue("@usercode", buyercode);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        //////View Product Details
        public DataSet ViewProductDetails(string productcode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ViewProductDetails");
            cmd.Parameters.AddWithValue("@productcode", productcode);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }


        //Address binding

        public DataSet GetCountry()
        {
            if (con.State==System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getCountry");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }
        public DataSet GetState(int countryid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getState");
            cmd.Parameters.AddWithValue("@countryid", countryid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }
        public DataSet GetCity(int stateid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getCity");
            cmd.Parameters.AddWithValue("@stateid", stateid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }

        /// //GET CATEGORY

        public DataSet GetCategory()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetCategory");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }

        public DataSet WishList(string BuyerCode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "WishList");
            cmd.Parameters.AddWithValue("@BuyerCode", BuyerCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }
        public void AddCart(string buyercode, string productcode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Cart");
            cmd.Parameters.AddWithValue("@BuyerCode", buyercode);
            cmd.Parameters.AddWithValue("@ProductCode", productcode);
            cmd.ExecuteNonQuery();
            con.Close();




        }
        public void DeleteWishlist( string buyercode,string productcode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RemoveFromWishList");
            cmd.Parameters.AddWithValue("@ProductCode", productcode);
            cmd.Parameters.AddWithValue("@buyercode", buyercode);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        //Prathamesh Start//
        public DataSet GetProductDetails()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetProductDetailPN");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return (ds);
            con.Close();
        }

        //SummerProducts
        public DataSet GetSUmmerProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetSummerPN");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return (ds);
            con.Close();
        }

        //WinterProducts
        public DataSet GetWinterProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetWinterProductPN");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return (ds);
            con.Close();
        }

        //RainyProducts
        public DataSet GetRainyProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetRainyProductPN");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return (ds);
            con.Close();
        }
        //Prathamesh End//
    }
}
