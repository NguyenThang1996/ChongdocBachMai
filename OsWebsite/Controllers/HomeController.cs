using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;
using OsWebsite.Common;
using OsWebsite.Helper;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace OsWebsite.Controllers
{
    public class HomeController : Controller
    {
        readonly OsWebEntities db = new OsWebEntities();
        public ActionResult Index()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Contact = db.Config.FirstOrDefault(x => x.IDLang == LangWeb);
            ViewBag.Contact = Contact.Contact;
            ViewBag.Hotline = Contact.HotLine;
            return View();
        }
        public PartialViewResult Menu()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            ViewBag.logo = db.Advertise.OrderBy(x => x.IsOrder).FirstOrDefault(x => x.Position == 0 && x.IsActive == true && x.IDLang == LangWeb).Image;
            ViewBag.linkLogo = db.Advertise.OrderBy(x => x.IsOrder).FirstOrDefault(x => x.Position == 0 && x.IsActive == true && x.IDLang == LangWeb).Link;
            ViewBag.hotline = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).HotLine;
            ViewBag.hotline2 = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Copyright;
            ViewBag.hotline3 = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Title;
            ViewBag.Mail = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Email;
            ViewBag.FacebookId = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).FacebookId;
            ViewBag.Countcart = ShoppingCart.Cart.Items.Count();
            var menu = db.Menu.Where(x => x.IDLang == LangWeb && x.IsActive == true && x.MenuType.Count(m => m.MenuPage.CodeMenu == "Main" && m.IDLang == LangWeb) > 0 && (x.Position == 1 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            var IconShare = db.Advertise.Where(x => x.Position == 3 && x.IDLang == LangWeb).OrderBy(x => x.IsOrder).Take(2).ToList();
            var Language = db.Language.Where(x => x.IsActive == true).OrderBy(x => x.IsOrder).Take(2).ToList();
            ViewBag.Language = Language;
            ViewBag.IconShare = IconShare;
            return PartialView(menu);
        }
        public PartialViewResult MenuChildMobile(int id)
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var menu = db.Menu.Where(x => x.IDLang == LangWeb && x.IsActive == true && x.MenuType.Count(m => m.MenuPage.CodeMenu == "Main" && m.IDLang == LangWeb) > 0 && (x.Position == 1 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            return PartialView(menu);
        }
        public PartialViewResult Slider()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var slider = db.Advertise.Where(x => x.Position == 1 && x.IDLang == LangWeb && x.IsActive == true).OrderBy(x => x.IsOrder).ToList();

            return PartialView(slider);
        }
        public PartialViewResult Footer()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            ViewBag.Contact = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Contact;
            ViewBag.Maps = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).GoogleId;
            ViewBag.IconShare = db.Advertise.Where(x => x.Position == 3 && x.IDLang == LangWeb && x.IsActive == true).OrderBy(x => x.IsOrder).ToList();          
            return PartialView();
        }
        public PartialViewResult Social()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-cong-dong" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupSocial = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdSocial = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && (x.Position == 2 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult Staff()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-nhan-vien-y-duoc" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupStaff = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && (x.Position == 2 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult Social_Right()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-cong-dong" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupSocial = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdSocial = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && (x.Position == 2 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult Staff_Right()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-nhan-vien-y-duoc" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupStaff = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && (x.Position == 2 || x.Position == 3)).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult PublicEducation()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "public-education" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupPublicEducation = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdPublicEducation = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult TrainingAndEducation()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "training-and-education" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupTrainingAndEducation = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdTrainingAndEducation = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult PublicEducation_Right()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "public-education" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupPublicEducation = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdPublicEducation = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult TrainingAndEducation_Right()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "training-and-education" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true && x.IDLang == LangWeb && (x.Position == 2 || x.Position == 3)).ToList();
            ViewBag.NameGroupTrainingAndEducation = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdTrainingAndEducation = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult Social_Banner()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var socialbanner = db.Advertise.Where(x => x.Position == 5 && x.IDLang == LangWeb && x.IsActive == true).OrderBy(x => x.IsOrder).ToList();
            return PartialView(socialbanner);
        }
        public PartialViewResult Header()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());         
            var Language = db.Language.Where(x => x.IsActive == true).OrderBy(x => x.IsOrder).Take(2).ToList();
            ViewBag.Language = Language;
            return PartialView();
        }

        public ActionResult SetLang()
        {
            string LangDefault = "3";
            HttpCookie cookie_Lang = new HttpCookie("LangWeb", LangDefault);
            cookie_Lang.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_Lang);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeLang(int IDLang)
        {
            var listLang = db.Language.SingleOrDefault(n => n.IDLang == IDLang);
            Session["LangWeb"] = listLang.IDLang;
            Init();
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