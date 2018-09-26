using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;

namespace OsWebsite.Controllers
{
    public class AboutController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        public ActionResult Index()
        {
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            int Aboutcheck = db.Menu.Where(x => x.Position == 1 && x.IDLang == LangWeb && x.IsActive == true).Count();
            if (Aboutcheck > 0 && Tag == "")
            {
                var About = db.News.FirstOrDefault(x => x.Menu.Position == 1 && x.IDLang == LangWeb && x.IsActive == true && x.ActiveTop == true);
                if (About != null)
                {
                    ViewBag.NameAbout = About.Name;
                    ViewBag.ContentNew = About.ContentNew;
                    ViewBag.Images = About.Img;
                    var GalleryImages = db.NewsImages.Where(x => x.NewsID == About.ID && x.IsActive == true).OrderBy(x => x.IsOrder).ToList();
                    ViewBag.GalleryImages = GalleryImages;
                    //Seo
                    ViewBag.MetaTitle = About.Name;
                    ViewBag.MetaDescription = About.Description;
                    ViewBag.MetaKeywords = About.Keyword;
                }
            }
            else if (Aboutcheck > 0 && Tag != "")
            {
                var About = db.News.FirstOrDefault(x => x.Menu.Position == 1 && x.Menu.Tag == Tag && x.IDLang == LangWeb && x.IsActive == true);
                if (About != null)
                {
                    ViewBag.NameAbout = About.Name;
                    ViewBag.ContentNew = About.ContentNew;
                    ViewBag.Images = About.Img;
                    var GalleryImages = db.NewsImages.Where(x => x.NewsID == About.ID && x.IsActive == true).OrderBy(x => x.IsOrder).ToList();
                    ViewBag.GalleryImages = GalleryImages;
                    //Seo
                    ViewBag.MetaTitle = About.Name;
                    ViewBag.MetaDescription = About.Description;
                    ViewBag.MetaKeywords = About.Keyword;
                }
            }
            return View();
        }
    }
}