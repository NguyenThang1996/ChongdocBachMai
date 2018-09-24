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
namespace OsWebsite.Areas.Admin.Controllers
{
    public class RegionalsController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Regionals
        public ActionResult Index(int page = 1 , int pagesize = 20)
        {
            int IdLang = int.Parse(Session["LangWeb"].ToString());
            var regional = db.Regional.Where(x => x.IDLang == IdLang).OrderBy(x => x.IsOrder).ToList();
            return View(regional.ToPagedList(page,pagesize));
        }      

        // GET: Admin/Regionals/Create
        public ActionResult Create()
        {
            int countmax = db.Regional.Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Regional.Max(e => e.IsOrder.Value) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,IDLang,IsOrder,IsActive")] Regional regional)
        {
            if (ModelState.IsValid)
            {
                int IdLang = int.Parse(Session["LangWeb"].ToString());
                regional.IDLang = IdLang; 
                db.Regional.Add(regional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int countmax = db.Regional.Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Regional.Max(e => e.IsOrder.Value) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(regional);
        }

        // GET: Admin/Regionals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regional regional = db.Regional.Find(id);
            if (regional == null)
            {
                return HttpNotFound();
            }
            return View(regional);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,IDLang,IsOrder,IsActive")] Regional regional)
        {
            if (ModelState.IsValid)
            {
                int IdLang = int.Parse(Session["LangWeb"].ToString());
                regional.IDLang = IdLang;
                db.Entry(regional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regional);
        }

        public ActionResult Delete(int id)
        {
            Regional regional = db.Regional.Find(id);
            db.Regional.Remove(regional);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int ID)
        {
            var list = db.Regional.Find(ID);
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
                var regional = db.Regional.Find(int.Parse(ID));
                db.Regional.Remove(regional);
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
