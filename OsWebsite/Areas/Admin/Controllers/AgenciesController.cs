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
    public class AgenciesController : Controller
    {
        private OsWebEntities db = new OsWebEntities();
        
        public ActionResult Index(int page = 1, int pagesize = 20)
        {
            var agency = db.Agency.OrderByDescending(x=>x.ID).ToList();
            return View(agency.ToPagedList(page,pagesize));
        }

        // GET: Admin/Agencies/Create
        public ActionResult Create()
        {
            ViewBag.IDRegional = new SelectList(db.Regional, "ID", "Name");
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            agency.IsActive = true;
            db.SaveChanges();
            return View(agency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Title,Content,Phone,Email,Address,IsActive,IDRegional")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Agency.Add(agency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDRegional = new SelectList(db.Regional, "ID", "Name", agency.IDRegional);
            return View(agency);
        }

        // GET: Admin/Agencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDRegional = new SelectList(db.Regional, "ID", "Name", agency.IDRegional);
            return View(agency);
        }

        // POST: Admin/Agencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Title,Content,Phone,Email,Address,IsActive,IDRegional")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDRegional = new SelectList(db.Regional, "ID", "Name", agency.IDRegional);
            return View(agency);
        }

        public ActionResult Delete(int id)
        {
            Agency agency = db.Agency.Find(id);
            db.Agency.Remove(agency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var agency = db.Agency.Find(int.Parse(ID));
                db.Agency.Remove(agency);
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
