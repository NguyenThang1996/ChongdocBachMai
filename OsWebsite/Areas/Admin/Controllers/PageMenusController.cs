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
    public class PageMenuController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        // GET: Admin/PageMenu
        public ActionResult Index(int page = 1,int pagesize = 10)
        {
            var pagelist = db.PageMenu.ToList();
            return View(pagelist.ToPagedList(page,pagesize));
        }

        // GET: Admin/PageMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageMenu PageMenu = db.PageMenu.Find(id);
            if (PageMenu == null)
            {
                return HttpNotFound();
            }
            return View(PageMenu);
        }
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var PageMenu = db.PageMenu.Find(int.Parse(ID));
                db.PageMenu.Remove(PageMenu);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/PageMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PageMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Link,Active")] PageMenu PageMenu)
        {
            if (ModelState.IsValid)
            {
                db.PageMenu.Add(PageMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(PageMenu);
        }

        // GET: Admin/PageMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageMenu PageMenu = db.PageMenu.Find(id);
            if (PageMenu == null)
            {
                return HttpNotFound();
            }
            return View(PageMenu);
        }

        // POST: Admin/PageMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Link,Active")] PageMenu PageMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(PageMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(PageMenu);
        }

        // GET: Admin/PageMenu/Delete/5
        
        public ActionResult Delete(int id)
        {
            PageMenu PageMenu = db.PageMenu.Find(id);
            db.PageMenu.Remove(PageMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.PageMenu.Find(ID);
            list.Active = !list.Active;
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
