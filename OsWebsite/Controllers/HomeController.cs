using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;
using OsWebsite.Common;
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
            return View();
        }
        public PartialViewResult Menu()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            ViewBag.logo = db.Advertise.OrderBy(x => x.IsOrder).FirstOrDefault(x => x.Position == 0 && x.IsActive == true && x.IDLang == LangWeb).Image;
            ViewBag.linkLogo = db.Advertise.OrderBy(x => x.IsOrder).FirstOrDefault(x => x.Position == 0 && x.IsActive == true).Link;
            ViewBag.hotline = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).HotLine;
            ViewBag.hotline2 = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Copyright;
            ViewBag.hotline3 = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Title;
            ViewBag.Mail = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).Email;
            ViewBag.FacebookId = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).FacebookId;
            ViewBag.Countcart = ShoppingCart.Cart.Items.Count();
            var menu = db.Menu.Where(x => x.IDLang == LangWeb && x.IsActive == true && x.MenuType.Count(m => m.MenuPage.CodeMenu == "Main" && m.IDLang == LangWeb) > 0 && x.Position == 1).OrderBy(x => x.IsOrder).ToList();
            var IconShare = db.Advertise.Where(x => x.Position == 3 && x.IDLang == LangWeb).OrderBy(x => x.IsOrder).Take(2).ToList();
            var Language = db.Language.Where(x => x.IsActive == true).OrderBy(x => x.IsOrder).Take(2).ToList();
            ViewBag.Language = Language;
            ViewBag.IconShare = IconShare;
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
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true).ToList();
            ViewBag.NameGroupSocial = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdSocial = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).Skip(1).ToList();
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return PartialView(News);
        }
        public PartialViewResult Staff()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-nhan-vien-y-duoc" && x.IsActive == true).ToList();
            ViewBag.Group = db.Menu.Where(x => x.IsActive == true).ToList();
            ViewBag.NameGroupStaff = Newscheck[0].Name.ToUpper();
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true && x.Position == 1).OrderBy(x => x.IsOrder).Skip(1).ToList();
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
            return PartialView();
        }
    }
}