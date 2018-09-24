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
    public class ProductImagesController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/ProductImages
        public ActionResult Index(int ProductID = 0, int page = 1, int pagesize = 10)
        {
            if (ProductID == 0) {
                return RedirectToAction("Index", "Products");
            }
            Session["ProductID"] = ProductID;
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            var productImages = db.ProductImages.Where(x => x.Product_Id == IdProduct).OrderByDescending(x => x.IsOrder).ToList();
            return View(productImages.ToPagedList(page,pagesize));
        }

       
        public ActionResult Create()
        {
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            int countmax = db.ProductImages.Where(x => x.Product_Id == IdProduct).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.ProductImages.Max(e => e.IsOrder.Value) + 1;
            }
            ViewBag.OrderMax = ordermax;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_Id,Name,Images,Decription,Content,IsOrder,IsActive")] ProductImages productImages)
        {
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            if (ModelState.IsValid)
            {                
                productImages.Product_Id = IdProduct;
                productImages.Decription = "";
                productImages.Content = "";
                db.ProductImages.Add(productImages);
                db.SaveChanges();
                return RedirectToAction("Index", new { ProductID = IdProduct });
            }

            int countmax = db.ProductImages.Where(x => x.Product_Id == IdProduct).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.ProductImages.Max(e => e.IsOrder.Value) + 1;
            }
            ViewBag.OrderMax = ordermax;

            return View(productImages);
        }

        // GET: Admin/ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = db.ProductImages.Find(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }            
            return View(productImages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_Id,Name,Images,Decription,Content,IsOrder,IsActive")] ProductImages productImages)
        {
            if (ModelState.IsValid)
            {
                productImages.Decription = "";
                productImages.Content = "";
                db.Entry(productImages).State = EntityState.Modified;
                db.SaveChanges();                
                return RedirectToAction("Index", new { ProductID = productImages.Product_Id });
            }            
            return View(productImages);
        }

        public ActionResult Delete(int id)
        {
            ProductImages productimages = db.ProductImages.Find(id);
            db.ProductImages.Remove(productimages);
            db.SaveChanges();
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            return RedirectToAction("Index", new { ProductID = IdProduct });
        }
        public ActionResult Status(int ID)
        {
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            var list = db.ProductImages.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index", new { ProductID = IdProduct });
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                var productImages = db.ProductImages.Find(int.Parse(ID));
                db.ProductImages.Remove(productImages);
                db.SaveChanges();
            }
            int IdProduct = int.Parse(Session["ProductID"].ToString());
            return RedirectToAction("Index", new { ProductID = IdProduct });
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
