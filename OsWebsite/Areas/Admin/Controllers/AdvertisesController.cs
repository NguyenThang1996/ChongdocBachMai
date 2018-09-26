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
    public class AdvertiseController : Controller
    {
        OsWebEntities db = new OsWebEntities();        

        // GET: Admin/Advertise
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            int lang = int.Parse(Session["LangWeb"].ToString());
            var Advertise = db.Advertise.Where(n=>n.IDLang == lang).ToList();
            return View(Advertise.OrderByDescending(x =>x.Id).ToPagedList(page,pagesize));
        }

        // GET: Admin/Advertise/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advertise advertise = db.Advertise.Find(id);
        //    if (advertise == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advertise);
        //}

        // GET: Admin/Advertise/Create
        public ActionResult Create()
        {
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Logo", Value = "0"},
                new SelectListItem { Text = "Slider", Value = "1"},
                new SelectListItem { Text = "Hình nền trang", Value = "2"},
                new SelectListItem { Text = "Icon Share", Value = "3"},
                new SelectListItem { Text = "Logo đối tác", Value = "4"},
                new SelectListItem { Text = "Banner Social", Value = "5"},
            }, "Value", "Text");
            int IDLang = int.Parse(Session["LangWeb"].ToString());

            int countmax = db.Advertise.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Advertise.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.MenuID = new SelectList(db.DacapMenu(IDLang), "ID", "Name");

            return View();
        }

        // POST: Admin/Advertise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Advertise advertise)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                advertise.IDLang = IDLang;
                db.Advertise.Add(advertise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Logo", Value = "0"},
                new SelectListItem { Text = "Slider", Value = "1"},
                new SelectListItem { Text = "Hình nền trang", Value = "2"},
                new SelectListItem { Text = "Icon Share", Value = "3"},
                new SelectListItem { Text = "Logo đối tác", Value = "4"},
                new SelectListItem { Text = "Banner Lafco", Value = "5"},
            }, "Value", "Text");
            int countmax = db.Advertise.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Advertise.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(advertise);
        }

        // GET: Admin/Advertise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertise.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Logo", Value = "0"},
                new SelectListItem { Text = "Slider", Value = "1"},
                new SelectListItem { Text = "Hình nền trang", Value = "2"},
                new SelectListItem { Text = "Icon Share", Value = "3"},
                new SelectListItem { Text = "Logo đối tác", Value = "4"},
                new SelectListItem { Text = "Banner Lafco", Value = "5"},
            }, "Value", "Text",advertise.Position);
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            ViewBag.MenuID = new SelectList(db.DacapMenu(IDLang), "ID", "Name", advertise.MenuID);
            return View(advertise);
        }

        // POST: Admin/Advertise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Advertise advertise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Logo", Value = "0"},
                new SelectListItem { Text = "Slider", Value = "1"},
                new SelectListItem { Text = "Hình nền trang", Value = "2"},
                new SelectListItem { Text = "Icon Share", Value = "3"},
                new SelectListItem { Text = "Logo đối tác", Value = "4"},
                new SelectListItem { Text = "Banner Lafco", Value = "5"},
            }, "Value", "Text", advertise.Position);
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            return View(advertise);
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string AdvertiseID in ids)
            {
                int AdvertiseID1 = int.Parse(AdvertiseID);
                var Advertise = db.Advertise.Find(AdvertiseID1);
                db.Advertise.Remove(Advertise);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Advertise advertise = db.Advertise.Find(id);
            db.Advertise.Remove(advertise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Advertise.Find(ID);
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
