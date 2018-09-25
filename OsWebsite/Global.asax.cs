using OsWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OsWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string LangDefault = "";

        readonly OsWebEntities _db = new OsWebEntities();


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                Application["HomNay"] = 0;
                Application["HomQua"] = 0;
                Application["TuanNay"] = 0;
                Application["TuanTruoc"] = 0;
                Application["ThangNay"] = 0;
                Application["ThangTruoc"] = 0;
                Application["TatCa"] = 0;
                Application["visitors_online"] = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            int i = (int)Application["Online"];
            if (i > 1)
            {
                Application["Online"] = i - 1;
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //Response.Redirect("/error.aspx");
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["Userid"] = null;
            Session["Username"] = null;
            Session["Nameadmin"] = null;
            LangDefault = "3";
            int lang = Int32.Parse(LangDefault);
            if (Session["LangWeb"] == null)
            {
                Session["LangWeb"] = LangDefault;
            }
            if (Session["LangWeb"] != null)
            {
                try
                {
                    lang = int.Parse(Session["LangWeb"].ToString());
                }
                catch
                {

                }
            }
            switch (lang)
            {
                case 1:
                    //Session["OrientSoft"] = "Thực hiện bởi";
                    //Session["product"] = _db.Setting.Find("product").EN;
                    //Session["News_event"] = _db.Setting.Find("News_event").EN;
                    //Session["production"] = _db.Setting.Find("production").EN;
                    //Session["partner"] = _db.Setting.Find("partner").EN;
                    //Session["Service"] = _db.Setting.Find("Service").EN;
                    //Session["Cost"] = _db.Setting.Find("Cost").EN;
                    //Session["ContactUs"] = _db.Setting.Find("ContactUs").EN;
                    //Session["Status"] = _db.Setting.Find("Status").EN;
                    //Session["Stocking"] = _db.Setting.Find("Stocking").EN;
                    //Session["Outofstock"] = _db.Setting.Find("Outofstock").EN;
                    //Session["Detail"] = _db.Setting.Find("Detail").EN;
                    //Session["SameType"] = _db.Setting.Find("SameType").EN;
                    //Session["NhomKinhTonLop"] = _db.Setting.Find("NhomKinhTonLop").EN;
                    //Session["project"] = _db.Setting.Find("project").EN;
                    //Session["news"] = _db.Setting.Find("news").EN;
                    //Session["keyProduct"] = _db.Setting.Find("keyProduct").EN;
                    //Session["ChildPages"] = _db.Setting.Find("ChildPages").EN;
                    //Session["otherNews"] = _db.Setting.Find("otherNews").EN;
                    //Session["sendTo"] = _db.Setting.Find("sendTo").EN;
                    //Session["Amount"] = _db.Setting.Find("Amount").EN;
                    //Session["ordered"] = _db.Setting.Find("ordered").EN;
                    //Session["Specification"] = _db.Setting.Find("Specification").EN;
                    //Session["Updating"] = _db.Setting.Find("Updating").EN;
                    //Session["search"] = _db.Setting.Find("search").EN;
                    //Session["notResult"] = _db.Setting.Find("notResult").EN;
                    //Session["cart"] = _db.Setting.Find("cart").EN;
                    //Session["nameProduct"] = _db.Setting.Find("nameProduct").EN;
                    //Session["imageProduct"] = _db.Setting.Find("imageProduct").EN;
                    //Session["totalAmount"] = _db.Setting.Find("totalAmount").EN;
                    //Session["del"] = _db.Setting.Find("del").EN;
                    //Session["noProductCart"] = _db.Setting.Find("noProductCart").EN;
                    //Session["continueShop"] = _db.Setting.Find("continueShop").EN;
                    //Session["Priceincreases"] = _db.Setting.Find("Priceincreases").EN;
                    //Session["Pricedecreases"] = _db.Setting.Find("Pricedecreases").EN;
                    //Session["SortedfromA-Z"] = _db.Setting.Find("SortedfromA-Z").EN;
                    //Session["Latestproduct"] = _db.Setting.Find("Latestproduct").EN;
                    //Session["Filterby"] = _db.Setting.Find("Filterby").EN;
                    break;
                case 3:
                    //Session["OrientSoft"] = "Powered by";
                    //Session["product"] = _db.Setting.Find("product").VN;
                    //Session["News_event"] = _db.Setting.Find("News_event").VN;
                    //Session["production"] = _db.Setting.Find("production").VN;
                    //Session["partner"] = _db.Setting.Find("partner").VN;
                    //Session["Service"] = _db.Setting.Find("Service").VN;
                    //Session["Cost"] = _db.Setting.Find("Cost").VN;
                    //Session["ContactUs"] = _db.Setting.Find("ContactUs").VN;
                    //Session["Status"] = _db.Setting.Find("Status").VN;
                    //Session["Stocking"] = _db.Setting.Find("Stocking").VN;
                    //Session["Outofstock"] = _db.Setting.Find("Outofstock").VN;
                    //Session["Detail"] = _db.Setting.Find("Detail").VN;
                    //Session["SameType"] = _db.Setting.Find("SameType").VN;
                    //Session["NhomKinhTonLop"] = _db.Setting.Find("NhomKinhTonLop").VN;
                    //Session["project"] = _db.Setting.Find("project").VN;
                    //Session["news"] = _db.Setting.Find("news").VN;
                    //Session["keyProduct"] = _db.Setting.Find("keyProduct").VN;
                    //Session["ChildPages"] = _db.Setting.Find("ChildPages").VN;
                    //Session["otherNews"] = _db.Setting.Find("otherNews").VN;
                    //Session["sendTo"] = _db.Setting.Find("sendTo").VN;
                    //Session["Amount"] = _db.Setting.Find("Amount").VN;
                    //Session["ordered"] = _db.Setting.Find("ordered").VN;
                    //Session["Specification"] = _db.Setting.Find("Specification").VN;
                    //Session["Updating"] = _db.Setting.Find("Updating").VN;
                    //Session["search"] = _db.Setting.Find("search").VN;
                    //Session["notResult"] = _db.Setting.Find("notResult").VN;
                    //Session["cart"] = _db.Setting.Find("cart").VN;
                    //Session["nameProduct"] = _db.Setting.Find("nameProduct").VN;
                    //Session["imageProduct"] = _db.Setting.Find("imageProduct").VN;
                    //Session["totalAmount"] = _db.Setting.Find("totalAmount").VN;
                    //Session["del"] = _db.Setting.Find("del").VN;
                    //Session["noProductCart"] = _db.Setting.Find("noProductCart").VN;
                    //Session["continueShop"] = _db.Setting.Find("continueShop").VN;
                    //Session["Priceincreases"] = _db.Setting.Find("Priceincreases").VN;
                    //Session["Pricedecreases"] = _db.Setting.Find("Pricedecreases").VN;
                    //Session["SortedfromA-Z"] = _db.Setting.Find("SortedfromA-Z").VN;
                    //Session["Latestproduct"] = _db.Setting.Find("Latestproduct").VN;
                    //Session["Filterby"] = _db.Setting.Find("Filterby").VN;
                    break;
            }
            //if (Request.Cookies["LangWeb"] == null || Request.Cookies["LangWeb"].ToString() == "")
            //{
            //    HttpCookie cookie_Lang = new HttpCookie("LangWeb", LangDefault);
            //    cookie_Lang.Expires = DateTime.Now.AddDays(1);
            //    Response.Cookies.Add(cookie_Lang);
            //}

            if (Application["Online"] == null)
            {
                Application["Online"] = 1;
            }
            else
            {
                Application["Online"] = (int)Application["Online"] + 1;
            }
            try
            {
                var listonline = _db.sp_Visitor_GetByAll().ToList();
                if (listonline.Count > 0)
                {
                    Application["HomNay"] = Int32.Parse("0" + listonline[0].HomNay);
                    Application["HomQua"] = Int32.Parse("0" + listonline[0].HomQua);
                    Application["TuanNay"] = Int32.Parse("0" + listonline[0].TuanNay);
                    Application["TuanTruoc"] = Int32.Parse("0" + listonline[0].TuanTruoc);
                    Application["ThangNay"] = Int32.Parse("0" + listonline[0].ThangNay);
                    Application["ThangTruoc"] = Int32.Parse("0" + listonline[0].ThangTruoc);
                    Application["TatCa"] = Int32.Parse("0" + listonline[0].TatCa);
                }
                _db.SaveChanges();
            }
            catch { }
        }
        void Session_End(object sender, EventArgs e)
        {
            LangDefault = "3";
            if (Session["LangWeb"] == null)
            {
                Session["LangWeb"] = LangDefault;
            }
            //if (Request.Cookies["LangWeb"] == null || Request.Cookies["LangWeb"].ToString() == "")
            //{
            //    HttpCookie cookie_Lang = new HttpCookie("LangWeb", LangDefault);
            //    cookie_Lang.Expires = DateTime.Now.AddDays(1);
            //    Response.Cookies.Add(cookie_Lang);
            //}

            int i = (int)Application["Online"];
            if (i > 1)
            {
                Application["Online"] = i - 1;
            }
        }
    }
}
