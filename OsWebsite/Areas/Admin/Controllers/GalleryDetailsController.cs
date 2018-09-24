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
    public class GalleryDetailsController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/GalleryDetails
        public ActionResult Index(int GalleryID = 0, int page = 1, int pagesize = 10)
        {
            if (GalleryID == 0)
            {
                return RedirectToAction("Index", "Gallery");
            }
            Session["GalleryID"] = GalleryID;            
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            var galleryDetail = db.GalleryDetail.Where(x=>x.GalleryID == IDGallery).OrderByDescending(x => x.IsOrder).ToList();
            return View(galleryDetail.ToPagedList(page,pagesize));
        }
    
        // GET: Admin/GalleryDetails/Create
        public ActionResult Create()
        {
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            int countmax = db.GalleryDetail.Where(x => x.GalleryID == IDGallery).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.GalleryDetail.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View();
        }

        // POST: Admin/GalleryDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GalleryID,Name,Images,Link,Position,IsOrder,IsActive")] GalleryDetail galleryDetail)
        {
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            if (ModelState.IsValid)
            {                
                galleryDetail.GalleryID = IDGallery;
                galleryDetail.Position = db.Gallery.FirstOrDefault(x => x.ID == IDGallery).Position.Value;
                if (galleryDetail.Images == null)
                {
                    galleryDetail.Images = "";
                }
                db.GalleryDetail.Add(galleryDetail);
                db.SaveChanges();
                return RedirectToAction("Index",new { GalleryID = IDGallery });
            }
            int countmax = db.GalleryDetail.Where(x => x.GalleryID == IDGallery).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.GalleryDetail.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(galleryDetail);
        }

        // GET: Admin/GalleryDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryDetail galleryDetail = db.GalleryDetail.Find(id);
            if (galleryDetail == null)
            {
                return HttpNotFound();
            }            
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            galleryDetail.GalleryID = IDGallery;
            galleryDetail.Position = db.Gallery.FirstOrDefault(x => x.ID == IDGallery).Position.Value;
            return View(galleryDetail);
        }

        // POST: Admin/GalleryDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GalleryID,Name,Images,Link,Position,IsOrder,IsActive")] GalleryDetail galleryDetail)
        {
            if (ModelState.IsValid)
            {
                int IDGallery = int.Parse(Session["GalleryID"].ToString());
                galleryDetail.Position = db.Gallery.FirstOrDefault(x => x.ID == IDGallery).Position.Value;
                galleryDetail.GalleryID = IDGallery;
                if (galleryDetail.Images == null)
                {
                    galleryDetail.Images = "";
                }
                db.Entry(galleryDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { GalleryID = IDGallery });
            }           
            return View(galleryDetail);
        }

        public ActionResult Delete(int id)
        {
            GalleryDetail gallerydetail = db.GalleryDetail.Find(id);
            db.GalleryDetail.Remove(gallerydetail);
            db.SaveChanges();
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            return RedirectToAction("Index", new { GalleryID = IDGallery });
        }
        public ActionResult Status(int ID)
        {
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            var list = db.GalleryDetail.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index", new { GalleryID = IDGallery });
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var gallerydetail = db.GalleryDetail.Find(int.Parse(ID));
                db.GalleryDetail.Remove(gallerydetail);
                db.SaveChanges();
            }
            int IDGallery = int.Parse(Session["GalleryID"].ToString());
            return RedirectToAction("Index", new { GalleryID = IDGallery });
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
