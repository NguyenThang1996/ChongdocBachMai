using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using OsWebsite.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class LanguageController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        // GET: Admin/Language
        public ActionResult Index(int page = 1, int pagesize = 10, string error = "")
        {
            if (error == "error")
            {
                ViewBag.TontaiLang = "Bạn không thể xóa ngôn ngữ vì hệ thống còn tồn tại dữ liệu của ngôn ngữ !";
            }
            else if (error == "dangdung")
            {
                ViewBag.TontaiLang = "Bạn không thể xóa ngôn ngữ đang dùng";
            }
            return View(db.Language.OrderByDescending(x => x.IDLang).ToPagedList(page, pagesize));
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["IDLang"].Split(new char[] { ',' });
            foreach (string IDLang in ids)
            {
                int IDLang1 = int.Parse(IDLang);
                int checkconfig = db.Config.Where(x => x.IDLang == IDLang1).Count();
                if (checkconfig > 0)
                {
                    Config config = db.Config.SingleOrDefault(x => x.IDLang == IDLang1);
                    db.Config.Remove(config);
                    db.SaveChanges();
                }
                var lang = db.Language.Find(IDLang1);
                db.Language.Remove(lang);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            int countmax = db.Language.Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Language.Max(e => e.IsOrder.Value) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View();
        }

        // POST: Admin/Language/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLang,NameLang,IconLang,CodeLang,IsOrder,IsActive")] Language language)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                db.Language.Add(language);
                db.SaveChanges();
                Config config = new Config();
                config.Logo = "Logo";
                config.PlaceHead = "Content PlaceHead";
                config.PlaceBody = "Content PlaceBody";
                config.GoogleId = "Google Maps";
                config.Contact = "Contact";
                config.Copyright = "Copyright";
                config.Title = "Title";
                config.Description = "Description";
                config.Keyword = "Keyword";
                config.HotLine = "HotLine";
                config.FacebookId = "Fanpage Facebook";
                config.IDLang  = db.Language.Max(x => x.IDLang);
                db.Config.Add(config);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int countmax = db.Advertise.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Advertise.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(language);
        }

        // GET: Admin/Language/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Language.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Admin/Language/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLang,NameLang,IconLang,CodeLang,IsOrder,IsActive")] Language language)
        {
            if (ModelState.IsValid)
            {
                db.Entry(language).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: Admin/Language/Delete/5
        public ActionResult Delete(int id)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int checkmenu = db.Menu.Where(x => x.IDLang == id).Count();
            int checknews = db.News.Where(x => x.IDLang == id).Count();            
            if (checkmenu > 0 || checknews > 0)
            {
                return RedirectToAction("Index", new { page = 1, pagesize = 10, error = "error" });
            }
            else if (id == IDLang)
            {
                return RedirectToAction("Index", new { page = 1, pagesize = 10, error = "dangdung" });
            }
            int checkconfig = db.Config.Where(x => x.IDLang == id).Count();
            if(checkconfig > 0)
            {
                Config config = db.Config.SingleOrDefault(x =>x.IDLang == id);
                db.Config.Remove(config);
                db.SaveChanges();
            }
            Language language = db.Language.Find(id);
            db.Language.Remove(language);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Language.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index");
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
