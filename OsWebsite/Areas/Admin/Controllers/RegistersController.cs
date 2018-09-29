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
            var register = db.Register.OrderByDescending(x => x.Id).ToList();
            foreach (var item in register)
            {
                if (item.Job == "1")
                {
                    item.Job = "Giáo sư";
                }
                if (item.Job == "2")
                {
                    item.Job = "Phó giáo sư";
                }
                if (item.Job == "3")
                {
                    item.Job = "Thạc sĩ";
                }
                if (item.Job == "4")
                {
                    item.Job = "Tiến sĩ";
                }
                if (item.Job == "5")
                {
                    item.Job = "Bác sĩ";
                }
                if (item.Job == "6")
                {
                    item.Job = "Nghiên cứu sinh";
                }
                if (item.Job == "7")
                {
                    item.Job = "Khác";
                }
            }
            return View(register.ToPagedList(page, pagesize));
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string registerId in ids)
            {
                int registerId1 = int.Parse(registerId);
                var register = db.Register.Find(registerId1);
                db.Register.Remove(register);
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
            Register register = db.Register.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(new List<SelectListItem>
            {
                    new SelectListItem { Text = "Giáo sư", Value = "1"},
                    new SelectListItem { Text = "Phó giáo sư", Value = "2"},
                    new SelectListItem { Text = "Thạc sĩ", Value = "3"},
                    new SelectListItem { Text = "Tiến sĩ", Value = "4"},
                    new SelectListItem { Text = "Bác sĩ", Value = "5"},
                    new SelectListItem { Text = "Nghiên cứu sinh", Value = "6"},
                    new SelectListItem { Text = "Khác", Value = "7"}
            }, "Value", "Text", register.Job);
            register.IsActive = true;
            db.SaveChanges();
            return View(register);
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
            Register register = db.Register.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Admin/Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register register = db.Register.Find(id);
            db.Register.Remove(register);
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
