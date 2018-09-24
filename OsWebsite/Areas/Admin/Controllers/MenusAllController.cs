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
    public class MenusAllController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/MenusAll
        public ActionResult Index()
        {
            var menu = db.Menu.Include(m => m.Language);
            return View(menu.ToList());
        }

        // GET: Admin/MenusAll/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/MenusAll/Create
        public ActionResult Create()
        {
            ViewBag.IDLang = new SelectList(db.Language, "IDLang", "NameLang");
            return View();
        }

        // POST: Admin/MenusAll/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Tag,IDCha,Link,Target,ModID,Position,Level,Images,Summary,Content,Title,Keyword,Description,Type,Style,Hot,Priority,Views,IDLang,IsOrder,IsActive")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLang = new SelectList(db.Language, "IDLang", "NameLang", menu.IDLang);
            return View(menu);
        }

        // GET: Admin/MenusAll/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLang = new SelectList(db.Language, "IDLang", "NameLang", menu.IDLang);
            return View(menu);
        }

        // POST: Admin/MenusAll/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Tag,IDCha,Link,Target,ModID,Position,Level,Images,Summary,Content,Title,Keyword,Description,Type,Style,Hot,Priority,Views,IDLang,IsOrder,IsActive")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLang = new SelectList(db.Language, "IDLang", "NameLang", menu.IDLang);
            return View(menu);
        }

        // GET: Admin/MenusAll/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/MenusAll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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
