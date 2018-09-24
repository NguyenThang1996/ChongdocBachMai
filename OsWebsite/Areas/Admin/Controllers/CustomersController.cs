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
    public class CustomersController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Customers
        public ActionResult Index(int page = 1,int pagesize = 20)
        {
            var customer = db.Customers.OrderByDescending(x => x.Id).ToList();
            return View(customer.ToPagedList(page,pagesize));
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            ViewBag.PaymentMethods = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Thanh toán trực tiếp", Value = "0"},
                new SelectListItem { Text = "Thanh toán chuyển khoản", Value = "1"},
                new SelectListItem { Text = "Thanh toán qua ngân lượng", Value = "2"}
            }, "Value", "Text");

            ViewBag.ModeTransport = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nhận hàng tại của hàng", Value = "0"},
                new SelectListItem { Text = "Nhận hàng tại nhà", Value = "1"},
                new SelectListItem { Text = "Nhận hàng gửi xe", Value = "2"},
                new SelectListItem { Text = "Nhận hàng ship cod", Value = "3"}
            }, "Value", "Text");

            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,Address,PaymentMethods,ModeTransport,IsActive")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentMethods = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Thanh toán trực tiếp", Value = "0"},
                new SelectListItem { Text = "Thanh toán chuyển khoản", Value = "1"},
                new SelectListItem { Text = "Thanh toán qua ngân lượng", Value = "2"}
            }, "Value", "Text");

            ViewBag.ModeTransport = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nhận hàng tại của hàng", Value = "0"},
                new SelectListItem { Text = "Nhận hàng tại nhà", Value = "1"},
                new SelectListItem { Text = "Nhận hàng gửi xe", Value = "2"},
                new SelectListItem { Text = "Nhận hàng ship cod", Value = "3"}
            }, "Value", "Text");
            return View(customers);
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentMethods = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Thanh toán trực tiếp", Value = "0"},
                new SelectListItem { Text = "Thanh toán chuyển khoản", Value = "1"},
                new SelectListItem { Text = "Thanh toán qua ngân lượng", Value = "2"}
            }, "Value", "Text",customers.PaymentMethods);

            ViewBag.ModeTransport = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nhận hàng tại của hàng", Value = "0"},
                new SelectListItem { Text = "Nhận hàng tại nhà", Value = "1"},
                new SelectListItem { Text = "Nhận hàng gửi xe", Value = "2"},
                new SelectListItem { Text = "Nhận hàng ship cod", Value = "3"}
            }, "Value", "Text",customers.ModeTransport);
            return View(customers);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Address,PaymentMethods,ModeTransport,IsActive")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentMethods = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Thanh toán trực tiếp", Value = "0"},
                new SelectListItem { Text = "Thanh toán chuyển khoản", Value = "1"},
                new SelectListItem { Text = "Thanh toán qua ngân lượng", Value = "2"}
            }, "Value", "Text", customers.PaymentMethods);

            ViewBag.ModeTransport = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nhận hàng tại của hàng", Value = "0"},
                new SelectListItem { Text = "Nhận hàng tại nhà", Value = "1"},
                new SelectListItem { Text = "Nhận hàng gửi xe", Value = "2"},
                new SelectListItem { Text = "Nhận hàng ship cod", Value = "3"}
            }, "Value", "Text", customers.ModeTransport);
            return View(customers);
        }


        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string CustomerId in ids)
            {
                int CustomerId1 = int.Parse(CustomerId);
                var Customer = db.Customers.Find(CustomerId1);
                db.Customers.Remove(Customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Customers.Find(ID);
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
