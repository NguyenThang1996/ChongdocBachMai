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
    public class UsersController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        // GET: Admin/Users
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            return View(db.User.OrderByDescending(x => x.Id).ToPagedList(page, pagesize));
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["Id"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var user = db.User.Find(int.Parse(id));
                db.User.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create(string error = "")
        {
            if (error == "error")
            {
                ViewBag.Tontai = "Tài khoản hoặc email đã tồn tại";
            }
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var check = db.User.Where(x => x.Username == user.Username || x.Email == user.Email).Count();
                if (check != 0)
                {
                    return Create("error");
                }
                user.DateCreate = DateTime.Now;
                user.Role = 0;
                user.Password = Encryptor.Encryptor.MD5Hash(user.Password);
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id, string error)
        {
            if (error == "error")
            {
                ViewBag.Tontai = "Tài khoản hoặc email đã tồn tại";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var check = db.User.Where(x => x.Id != user.Id && x.Username == user.Username || x.Id != user.Id && x.Email == user.Email).Count();
                if (check != 0)
                {
                    return Edit(user.Id, "error");
                }
                user.Password = Encryptor.Encryptor.MD5Hash(user.Password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
