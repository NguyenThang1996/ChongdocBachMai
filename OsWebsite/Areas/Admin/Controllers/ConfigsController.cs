using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class ConfigsController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        // GET: Admin/Configs
        public ActionResult Index()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            return View(db.Config.FirstOrDefault(x => x.IDLang == IDLang));
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Logo,PlaceHead,PlaceBody,GoogleId,Contact,Copyright,Title,Description,Keyword,HotLine,Email,Password,FacebookId,IDLang,MailFooter,TitleEvent,AboutEvent,TimelineEvent,GuestEvent, TimeEvent, TimeEventCount, ImageEvent")] Config config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(config).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(config);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
