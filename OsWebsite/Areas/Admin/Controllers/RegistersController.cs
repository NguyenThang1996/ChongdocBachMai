using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;
using PagedList.Mvc;
using PagedList;
namespace OsWebsite.Areas.Admin.Controllers
{
    public class RegistersController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        // GET: Admin/Register
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var Register = db.Register.OrderByDescending(x => x.Id).ToList();
            return View(Register.ToPagedList(page, pagesize));
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string RegisterId in ids)
            {
                int RegisterId1 = int.Parse(RegisterId);
                var Register = db.Register.Find(RegisterId1);
                db.Register.Remove(Register);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register Register = db.Register.Find(id);
            if (Register == null)
            {
                return HttpNotFound();
            }
            Register.IsActive = true;
            db.SaveChanges();
            return View(Register);
        }

        // GET: Admin/Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Register/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Title,Content,Phone,Email,Address,DateCreate")] Register Register)
        {
            if (ModelState.IsValid)
            {
                db.Register.Add(Register);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Register);
        }

        // GET: Admin/Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register Register = db.Register.Find(id);
            if (Register == null)
            {
                return HttpNotFound();
            }
            return View(Register);
        }

        // POST: Admin/Register/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Title,Content,Phone,Email,Address,DateCreate")] Register Register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Register);
        }

        // GET: Admin/Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register Register = db.Register.Find(id);
            if (Register == null)
            {
                return HttpNotFound();
            }
            return View(Register);
        }

        // POST: Admin/Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register Register = db.Register.Find(id);
            db.Register.Remove(Register);
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
