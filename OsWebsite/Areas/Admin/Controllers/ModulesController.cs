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
    public class ModulesController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Modules
        public ActionResult Index(int page =1,int pagesize = 10)
        {
            var Module = db.Module.OrderBy(x => x.Position).ToList();
            return View(Module.ToPagedList(page,pagesize));
        }

        // GET: Admin/Modules/Create
        public ActionResult Create()
        {
            int countmax = db.Module.Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Module.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;

            ViewBag.Position = new SelectList(db.Module.Where(x=>x.IsActive == true).ToList(), "ID", "Name");
            ViewBag.Priority = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nội Dung Bài Viết", Value = "1"},
                new SelectListItem { Text = "Nội Dung Sản Phẩm", Value = "2"}
            }, "Value", "Text");
            return View();
        }

        // POST: Admin/Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CodeMod,Mod_Url,IsOrder,Position,Priority,IsActive")] Module module)
        {
            if (ModelState.IsValid)
            {
                if (module.Position == null)
                {
                    module.Position = 0;
                }
                if (module.Priority == null)
                {
                    module.Position = 0;
                }
                db.Module.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true).ToList(), "ID", "Name");
            ViewBag.Priority = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nội Dung Bài Viết", Value = "1"},
                new SelectListItem { Text = "Nội Dung Sản Phẩm", Value = "2"}
            }, "Value", "Text");
            int countmax = db.Module.Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Module.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(module);
        }

        // GET: Admin/Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true).ToList(), "ID", "Name",module.Position);
            ViewBag.Priority = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nội Dung Bài Viết", Value = "1"},
                new SelectListItem { Text = "Nội Dung Sản Phẩm", Value = "2"}
            }, "Value", "Text",module.Priority);
            return View(module);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CodeMod,Mod_Url,IsOrder,Position,Priority,IsActive")] Module module)
        {
            if (ModelState.IsValid)
            {
                if (module.Position == null)
                {
                    module.Position = 0;
                }
                if (module.Priority == null)
                {
                    module.Position = 0;
                }
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true).ToList(), "ID", "Name", module.Position);
            ViewBag.Priority = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nội Dung Bài Viết", Value = "1"},
                new SelectListItem { Text = "Nội Dung Sản Phẩm", Value = "2"}
            }, "Value", "Text", module.Priority);
            return View(module);
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ModuleID in ids)
            {
                int ModuleID1 = int.Parse(ModuleID);
                var Module = db.Module.Find(ModuleID1);
                db.Module.Remove(Module);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Module module = db.Module.Find(id);
            db.Module.Remove(module);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Module.Find(ID);
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
