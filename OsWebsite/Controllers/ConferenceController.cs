using OsWebsite.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsWebsite.Controllers
{
    public class ConferenceController : Controller
    {
        readonly OsWebEntities db = new OsWebEntities();
        // GET: Conference
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            //int LangWeb = int.Parse(Session["LangWeb"].ToString());
            //var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-nhan-vien-y-duoc" && x.IsActive == true).ToList();
            //ViewBag.NameGroupStaff1 = Newscheck[0].Name.ToUpper();
            //var parentid = Newscheck[0].ID;
            //ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true).ToList();
            //return View();
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            //kiểm tra tag xem có kèm gì khác ngoài tag
            string Tagcheck = Tag.Substring(Tag.LastIndexOf("?") + 1);
            //Replace hết kí tự lạ 
            Tag = Tag.Replace("?" + Tagcheck, "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == Tag && x.IsActive == true).ToList();
            ViewBag.NameGroupService = Newscheck[0].Name;
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdService = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true).ToList();
            ViewBag.Tag = Newscheck[0].Tag;
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return View(News.ToPagedList(page, pagesize));
        }
    }
}