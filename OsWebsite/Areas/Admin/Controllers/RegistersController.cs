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
                    item.Job = "GS.TS.BS";
                }
                if (item.Job == "2")
                {
                    item.Job = "PGS.TS.BS";
                }
                if (item.Job == "3")
                {
                    item.Job = "TS.BS";
                }
                if (item.Job == "4")
                {
                    item.Job = "ThS.BS";
                }
                if (item.Job == "5")
                {
                    item.Job = "BS";
                }
                if (item.Job == "6")
                {
                    item.Job = "BSCKII";
                }
                if (item.Job == "7")
                {
                    item.Job = "BSCKI";
                }
                if (item.Job == "8")
                {
                    item.Job = "BSNT";
                }
                if (item.Job == "9")
                {
                    item.Job = "Sinh viên";
                }
                if (item.Job == "10")
                {
                    item.Job = "ThS.Điều dưỡng";
                }
                if (item.Job == "11")
                {
                    item.Job = "ThS.Điều dưỡng";
                }
                if (item.Job == "12")
                {
                    item.Job = "Nhân viên y tế";
                }
                if (item.Job == "13")
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
                    new SelectListItem { Text = "GS.TS.BS", Value = "1"},
                    new SelectListItem { Text = "PGS.TS.BS", Value = "2"},
                    new SelectListItem { Text = "TS.BS", Value = "3"},
                    new SelectListItem { Text = "ThS.BS", Value = "4"},
                    new SelectListItem { Text = "BS", Value = "5"},
                    new SelectListItem { Text = "BSCKII", Value = "6"},
                    new SelectListItem { Text = "BSCKI", Value = "7"},
                    new SelectListItem { Text = "BSNT", Value = "8"},
                    new SelectListItem { Text = "Sinh viên", Value = "9"},
                    new SelectListItem { Text = "ThS.Điều dưỡng", Value = "10"},
                    new SelectListItem { Text = "Cử nhân điều dưỡng", Value = "11"},
                    new SelectListItem { Text = "Nhân viên y tế", Value = "12"},
                    new SelectListItem { Text = "Khác", Value = "13"}
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
