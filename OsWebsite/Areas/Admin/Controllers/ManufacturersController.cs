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
    public class ManufacturersController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Manufacturers
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var manufacturer = db.Manufacturer.OrderByDescending(x => x.IsOrder).ToList();
            return View(manufacturer.ToPagedList(page, pagesize));
        }

        public ActionResult Create()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int countmax = db.Manufacturer.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Manufacturer.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Summary,Image,IDLang,IsActive,IsOrder")] Manufacturer manufacturer)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {                
                manufacturer.IDLang = IDLang;
                db.Manufacturer.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int countmax = db.Manufacturer.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Manufacturer.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(manufacturer);
        }

        // GET: Admin/Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            return View(manufacturer);
        }

        // POST: Admin/Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Summary,Image,IDLang,IsActive,IsOrder")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                int IDLang = int.Parse(Session["LangWeb"].ToString());
                manufacturer.IDLang = IDLang;
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ManufacturerId in ids)
            {
                int Manufacturer1 = int.Parse(ManufacturerId);
                var manufacturer = db.Manufacturer.Find(Manufacturer1);
                db.Manufacturer.Remove(manufacturer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            db.Manufacturer.Remove(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Manufacturer.Find(ID);
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
