using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;
using PagedList;
using PagedList.Mvc;
using OsWebsite.Areas.Admin.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Gallery
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            var gallery = db.Gallery.Where(x => x.IDLang == IDLang).OrderByDescending(x => x.IsOrder).ToList();
            return View(gallery.ToPagedList(page,pagesize));
        }

        // GET: Admin/Gallery/Create
        public ActionResult Create()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int countmax = db.Gallery.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Gallery.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Gallery ảnh", Value = "0"},
                new SelectListItem { Text = "Video Clip", Value = "1"},
            }, "Value", "Text");
            return View();
        }

        // POST: Admin/Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Tag,Images,Link,Position,IsOrder,IsActive,IDLang")] Gallery gallery)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {                
                gallery.Tag = StringClass.NameToTag(gallery.Name);
                gallery.IDLang = IDLang;
                db.Gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int countmax = db.Gallery.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Gallery.Max(e => e.IsOrder) + 1;
            }

            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Gallery ảnh", Value = "0"},
                new SelectListItem { Text = "Video Clip", Value = "1"},
            }, "Value", "Text");
            return View(gallery);
        }

        // GET: Admin/Gallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Gallery ảnh", Value = "0"},
                new SelectListItem { Text = "Video Clip", Value = "1"},
            }, "Value", "Text", gallery.Position);
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            return View(gallery);
        }

        // POST: Admin/Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Tag,Images,Link,Position,IsOrder,IsActive,IDLang")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                int IDLang = int.Parse(Session["LangWeb"].ToString());
                gallery.Tag = StringClass.NameToTag(gallery.Name);
                gallery.IDLang = IDLang;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Gallery ảnh", Value = "0"},
                new SelectListItem { Text = "Video Clip", Value = "1"},
            }, "Value", "Text",gallery.Position);            
            return View(gallery);
        }

        public ActionResult Delete(int id)
        {
            Gallery gallery = db.Gallery.Find(id);
            db.Gallery.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Gallery.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var gallery = db.Gallery.Find(int.Parse(ID));
                db.Gallery.Remove(gallery);
                db.SaveChanges();
            }
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
