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
    public class NewsImagesController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/NewsImages
        public ActionResult Index(int NewsID = 0, int page = 1, int pagesize = 5)
        {
            ViewBag.NewsID = NewsID;
            if (NewsID == 0)
            {
                return RedirectToAction("Index", "News");
            }
            Session["NewsID"] = NewsID;
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int IDNews = int.Parse(Session["NewsID"].ToString());
            var newsImages = db.NewsImages.Where(x => x.NewsID == IDNews).OrderByDescending(x => x.IsOrder).ToList();
            return View(newsImages.ToPagedList(page, pagesize));
        }

        // GET: Admin/NewsImages/Create
        public ActionResult Create()
        {
            int IDNews = int.Parse(Session["NewsID"].ToString());
            int countmax = db.NewsImages.Where(x => x.NewsID == IDNews).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.NewsImages.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;

            return View();
        }

        // POST: Admin/NewsImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NewsID,Name,Images,Decription,IsOrder,IsActive")] NewsImages newsImages)
        {
            int IDNews = int.Parse(Session["NewsID"].ToString());
            if (ModelState.IsValid)
            {
                newsImages.NewsID = IDNews;
                db.NewsImages.Add(newsImages);
                db.SaveChanges();
                return RedirectToAction("Index", new { NewsID = IDNews });
            }
            int countmax = db.NewsImages.Where(x => x.NewsID == IDNews).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.NewsImages.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View(newsImages);
        }

        // GET: Admin/NewsImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsImages newsImages = db.NewsImages.Find(id);
            if (newsImages == null)
            {
                return HttpNotFound();
            }           
            return View(newsImages);
        }

        // POST: Admin/NewsImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NewsID,Name,Images,Decription,IsOrder,IsActive")] NewsImages newsImages)
        {
            if (ModelState.IsValid)
            {   
                db.Entry(newsImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { NewsID = newsImages.NewsID });
            }
            return View(newsImages);
        }

        // GET: Admin/NewsImages/Delete/5
        public ActionResult Delete(int id)
        {
            NewsImages newsImages = db.NewsImages.Find(id);
            db.NewsImages.Remove(newsImages);
            db.SaveChanges();
            int IDNews = int.Parse(Session["NewsID"].ToString());
            return RedirectToAction("Index", new { NewsID = IDNews });
        }
        public ActionResult Status(int ID)
        {
            var list = db.NewsImages.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            int IDNews = int.Parse(Session["NewsID"].ToString());
            return RedirectToAction("Index", new { NewsID = IDNews });
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var newsImages = db.NewsImages.Find(int.Parse(ID));
                db.NewsImages.Remove(newsImages);
                db.SaveChanges();
            }
            int IDNews = int.Parse(Session["NewsID"].ToString());
            return RedirectToAction("Index", new { NewsID = IDNews });
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
