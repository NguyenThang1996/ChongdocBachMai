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
    public class OrdersController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Orders
        public ActionResult Index(string keyword,int? IsStatus,int page = 1, int pagesize = 20)
        {
            var order = db.Order.OrderByDescending(o => o.DateCreate).ToList();
            if (keyword == "")
            {
                keyword = null;
            }
            if (keyword != null && IsStatus == null)
            {
                order = db.Order.Where(x => x.Customers.Name.Contains(keyword) || x.Customers.Phone.Contains(keyword)).OrderByDescending(o => o.DateCreate).ToList();
            }
            else if (keyword == null && IsStatus != null)
            {
                order = db.Order.Where(x => x.IsActive == IsStatus).OrderByDescending(o => o.DateCreate).ToList();
            }
            else if (keyword != null && IsStatus != null)
            {
                order = db.Order.Where(x => x.IsActive == IsStatus && (x.Customers.Name.Contains(keyword) || x.Customers.Phone.Contains(keyword))).OrderByDescending(o => o.DateCreate).ToList();
            }
            return View(order.ToPagedList(page,pagesize));
        }

        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,TotalPrice,DateCreate,Note,IsActive")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,TotalPrice,DateCreate,Note,IsActive")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string OrderID in ids)
            {
                int OrderID1 = int.Parse(OrderID);

                var arrdetete = db.OrderDetail.Where(od => od.OrderId == OrderID1);
                db.OrderDetail.RemoveRange(arrdetete);
                db.SaveChanges();

                var Order = db.Order.Find(OrderID1);
                db.Order.Remove(Order);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var arrdetete = db.OrderDetail.Where(od => od.OrderId == id);
            db.OrderDetail.RemoveRange(arrdetete);
            db.SaveChanges();

            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Status(int ID)
        //{
        //    var list = db.Order.Find(ID);
        //    list.IsActive = !list.IsActive;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
