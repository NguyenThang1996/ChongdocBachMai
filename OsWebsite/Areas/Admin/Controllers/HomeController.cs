using OsWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            List<News> list = db.News.Where(x => x.IsActive == true && x.IDLang == IDLang).OrderByDescending(x => x.ID).Take(10).ToList();
            ViewBag.Noactive = db.News.Where(x => x.IsActive == false && x.IDLang == IDLang).Count();
            ViewBag.Active = db.News.Where(x => x.IsActive == true && x.IDLang == IDLang).Count();
            ViewBag.CountNews = db.News.Where(x => x.IDLang == IDLang).Count();
            ViewBag.CountContact = db.Contact.Where(x => x.IsActive == false).Count();
            ViewBag.CountUser = db.User.Count();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            ViewBag.HomNay = HttpContext.Application["HomNay"].ToString();
            ViewBag.TuanNay = HttpContext.Application["TuanNay"].ToString();
            ViewBag.ThangNay = HttpContext.Application["ThangNay"].ToString();
            ViewBag.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            ViewBag.TatCa = HttpContext.Application["TatCa"].ToString();

            return View(list);
        }
        public PartialViewResult MenuTop()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            var list = db.Language.Where(x => x.IsActive == true).ToList();
            int count = list.Count();
            ViewBag.CountContact = db.Contact.Where(x => x.IsActive == false).Count();
            ViewBag.CountOrder = db.Order.Where(x => x.IsActive == 0).Count();
            ViewBag.Langs = db.Language.Find(IDLang).NameLang;
            return PartialView(list);
        }
        public ActionResult ChangeLang(int IDLang)
        {
            Session["LangWeb"] = IDLang;
            Init();
            Response.Cookies["LangWeb"].Expires = DateTime.Now.AddDays(-1);
            HttpCookie cookie_Lang = new HttpCookie("LangWeb", IDLang.ToString());
            cookie_Lang.Expires = DateTime.Now.AddHours(4);
            Response.Cookies.Add(cookie_Lang);
            return RedirectToAction("Index");
        }
        public int lang;
        public void Init()
        {
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
                    Session["OrientSoft"] = "Thực hiện bởi";
                    Session["product"] = db.Setting.Find("product").EN;
                    Session["News_event"] = db.Setting.Find("News_event").EN;
                    Session["production"] = db.Setting.Find("production").EN;
                    Session["partner"] = db.Setting.Find("partner").EN;
                    Session["Service"] = db.Setting.Find("Service").EN;
                    Session["Cost"] = db.Setting.Find("Cost").EN;
                    Session["Contact"] = db.Setting.Find("Contact").EN;
                    Session["Status"] = db.Setting.Find("Status").EN;
                    Session["Stocking"] = db.Setting.Find("Stocking").EN;
                    Session["Outofstock"] = db.Setting.Find("Outofstock").EN;
                    Session["Detail"] = db.Setting.Find("Detail").EN;
                    Session["SameType"] = db.Setting.Find("SameType").EN;
                    Session["NhomKinhTonLop"] = db.Setting.Find("NhomKinhTonLop").EN;
                    Session["project"] = db.Setting.Find("project").EN;
                    Session["news"] = db.Setting.Find("news").EN;
                    Session["keyProduct"] = db.Setting.Find("keyProduct").EN;
                    Session["ChildPages"] = db.Setting.Find("ChildPages").EN;
                    Session["otherNews"] = db.Setting.Find("otherNews").EN;
                    Session["sendTo"] = db.Setting.Find("sendTo").EN;
                    Session["Amount"] = db.Setting.Find("Amount").EN;
                    Session["ordered"] = db.Setting.Find("ordered").EN;
                    Session["Specification"] = db.Setting.Find("Specification").EN;
                    Session["Updating"] = db.Setting.Find("Updating").EN;
                    Session["search"] = db.Setting.Find("search").EN;
                    Session["notResult"] = db.Setting.Find("notResult").EN;
                    Session["cart"] = db.Setting.Find("cart").EN;
                    Session["nameProduct"] = db.Setting.Find("nameProduct").EN;
                    Session["imageProduct"] = db.Setting.Find("imageProduct").EN;
                    Session["totalAmount"] = db.Setting.Find("totalAmount").EN;
                    Session["del"] = db.Setting.Find("del").EN;
                    Session["noProductCart"] = db.Setting.Find("noProductCart").EN;
                    Session["continueShop"] = db.Setting.Find("continueShop").EN;
                    Session["Priceincreases"] = db.Setting.Find("Priceincreases").EN;
                    Session["Pricedecreases"] = db.Setting.Find("Pricedecreases").EN;
                    Session["SortedfromA-Z"] = db.Setting.Find("SortedfromA-Z").EN;
                    Session["Latestproduct"] = db.Setting.Find("Latestproduct").EN;
                    Session["Filterby"] = db.Setting.Find("Filterby").EN;
                    break;
                case 3:
                    Session["OrientSoft"] = "Powered by";
                    Session["product"] = db.Setting.Find("product").VN;
                    Session["News_event"] = db.Setting.Find("News_event").VN;
                    Session["production"] = db.Setting.Find("production").VN;
                    Session["partner"] = db.Setting.Find("partner").VN;
                    Session["Service"] = db.Setting.Find("Service").VN;
                    Session["Cost"] = db.Setting.Find("Cost").VN;
                    Session["Contact"] = db.Setting.Find("Contact").VN;
                    Session["Status"] = db.Setting.Find("Status").VN;
                    Session["Stocking"] = db.Setting.Find("Stocking").VN;
                    Session["Outofstock"] = db.Setting.Find("Outofstock").VN;
                    Session["Detail"] = db.Setting.Find("Detail").VN;
                    Session["SameType"] = db.Setting.Find("SameType").VN;
                    Session["NhomKinhTonLop"] = db.Setting.Find("NhomKinhTonLop").VN;
                    Session["project"] = db.Setting.Find("project").VN;
                    Session["news"] = db.Setting.Find("news").VN;
                    Session["keyProduct"] = db.Setting.Find("keyProduct").VN;
                    Session["ChildPages"] = db.Setting.Find("ChildPages").VN;
                    Session["otherNews"] = db.Setting.Find("otherNews").VN;
                    Session["sendTo"] = db.Setting.Find("sendTo").VN;
                    Session["Amount"] = db.Setting.Find("Amount").VN;
                    Session["ordered"] = db.Setting.Find("ordered").VN;
                    Session["Specification"] = db.Setting.Find("Specification").VN;
                    Session["Updating"] = db.Setting.Find("Updating").VN;
                    Session["search"] = db.Setting.Find("search").VN;
                    Session["notResult"] = db.Setting.Find("notResult").VN;
                    Session["cart"] = db.Setting.Find("cart").VN;
                    Session["nameProduct"] = db.Setting.Find("nameProduct").VN;
                    Session["imageProduct"] = db.Setting.Find("imageProduct").VN;
                    Session["totalAmount"] = db.Setting.Find("totalAmount").VN;
                    Session["del"] = db.Setting.Find("del").VN;
                    Session["noProductCart"] = db.Setting.Find("noProductCart").VN;
                    Session["continueShop"] = db.Setting.Find("continueShop").VN;
                    Session["Priceincreases"] = db.Setting.Find("Priceincreases").VN;
                    Session["Pricedecreases"] = db.Setting.Find("Pricedecreases").VN;
                    Session["SortedfromA-Z"] = db.Setting.Find("SortedfromA-Z").VN;
                    Session["Latestproduct"] = db.Setting.Find("Latestproduct").VN;
                    Session["Filterby"] = db.Setting.Find("Filterby").VN;
                    break;
            }
        }
    }
}