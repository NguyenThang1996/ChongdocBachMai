using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using OsWebsite.Models;
using OsWebsite.Helper;

namespace OsWebsite.Controllers
{
    public class NewsController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        public ActionResult Social(int page = 1, int pagesize = 6)
        {
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            //kiểm tra tag xem có kèm gì khác ngoài tag
            string Tagcheck = Tag.Substring(Tag.LastIndexOf("?") + 1);
            //Replace hết kí tự lạ 
            Tag = Tag.Replace("?" + Tagcheck, "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == Tag && x.IsActive == true).ToList();
            ViewBag.NameGroupSocial = Newscheck[0].Name;
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdSocial = db.Menu.Where(x => x.IDCha == 317 && x.IDLang == LangWeb && x.IsActive == true).ToList();
            ViewBag.Tag = Newscheck[0].Tag;
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            foreach(var item in News)
            {
                helper.cut_String(item.Decription, 300);
            }
            return View(News.ToPagedList(page, pagesize));
        }
        public ActionResult Staff(int page = 1, int pagesize = 6)
        {
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            //kiểm tra tag xem có kèm gì khác ngoài tag
            string Tagcheck = Tag.Substring(Tag.LastIndexOf("?") + 1);
            //Replace hết kí tự lạ 
            Tag = Tag.Replace("?" + Tagcheck, "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == Tag && x.IsActive == true).ToList();
            ViewBag.NameGroupStaff = Newscheck[0].Name;
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == 318 && x.IDLang == LangWeb && x.IsActive == true).ToList();
            ViewBag.Tag = Newscheck[0].Tag;
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            foreach (var item in News)
            {
                helper.cut_String(item.Decription, 300);
            }
            return View(News.ToPagedList(page, pagesize));
        }
        public ActionResult Service(int page = 1, int pagesize = 6)
        {
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            string Tagcheck = Tag.Substring(Tag.LastIndexOf("?") + 1);
            Tag = Tag.Replace("?" + Tagcheck, "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var Newscheck = db.Menu.Where(x => x.Tag == Tag && x.IsActive == true).ToList();
            ViewBag.NameGroupService = Newscheck[0].Name;
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdService = db.Menu.Where(x => x.IDCha == 319 && x.IDLang == LangWeb && x.IsActive == true).ToList();
            ViewBag.Tag = Newscheck[0].Tag;
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            foreach (var item in News)
            {
                helper.cut_String(item.Decription, 300);
            }
            return View(News.ToPagedList(page, pagesize));
        }
        public ActionResult NewsDetail()
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            var News = db.News.Where(x => x.Tag == Tag && x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return View(News);
        }
        public PartialViewResult NewsOthers(int MenuID, int IdNews)
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            var News = db.News_Others(MenuID).Where(x => x.ID != IdNews && x.IsActive == true && x.IDLang == LangWeb).OrderByDescending(x => x.ID).Take(3).ToList();
            foreach (var item in News)
            {
                helper.cut_String(item.Decription, 300);
            }
            return PartialView(News);
        }
    }
}