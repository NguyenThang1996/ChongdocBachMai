using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;
using OsWebsite.Areas.Admin.Models;
using OsWebsite.Common;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/OrderDetails
        public ActionResult Index(int OrderId)
        {
            Session["OrderId"] = OrderId;
            var orderCus = db.Order.Where(x => x.CustomerId == x.Customers.Id && x.Id == OrderId).Select(c => new
            {
                IdOrders = c.Id,
                CustomerName = c.Customers.Name,
                Email = c.Customers.Email,
                Phone = c.Customers.Phone,
                Address = c.Customers.Address,
                DateCreate = c.DateCreate,
                Note = c.Note,
                PaymentMethods = c.Customers.PaymentMethods,
                ModeTransport = c.Customers.ModeTransport,
                TotalPrice = c.TotalPrice,
                IsActive = c.IsActive
            }).ToList();

            ViewBag.CustomerName = orderCus[0].CustomerName;
            ViewBag.Email = orderCus[0].Email;
            ViewBag.Phone = orderCus[0].Phone;
            ViewBag.Address = orderCus[0].Address;
            ViewBag.DateCreate = DateTimeClass.ConvertDateTime_ddmmyyyy(orderCus[0].DateCreate.ToString());
            ViewBag.Note = orderCus[0].Note;
            ViewBag.IdOrders = orderCus[0].IdOrders;
            string PaymentMethods = "";

            if (orderCus[0].PaymentMethods == 0)
            {
                PaymentMethods = "Thanh toán trực tiếp";
            } else if (orderCus[0].PaymentMethods == 1)
            {
                PaymentMethods = "Thanh toán chuyển khoản";
            }
            else if (orderCus[0].PaymentMethods == 2)
            {
                PaymentMethods = "Thanh toán qua ngân lượng";
            }
            ViewBag.PaymentMethods = PaymentMethods;

            string ModeTransport = "";
            if (orderCus[0].ModeTransport == 0)
            {
                ModeTransport = "Nhận hàng tại cửa hàng";
            }
            else if (orderCus[0].ModeTransport == 1)
            {
                ModeTransport = "Nhận hàng tại nhà";
            }
            else if (orderCus[0].ModeTransport == 2)
            {
                ModeTransport = "Nhận hàng gửi xe";
            }
            else if (orderCus[0].ModeTransport == 3)
            {
                ModeTransport = "Nhận hàng ship cod";
            }

            ViewBag.ModeTransport = ModeTransport;
            string Moneytotal = "";
            if (orderCus[0].TotalPrice != 0)
            {
                Moneytotal = String.Format("{0:0,0 VNĐ}", orderCus[0].TotalPrice).ToString();
            } else {
                Moneytotal = "0 VNĐ";
            }
            ViewBag.TotalPrice = Moneytotal;
            //var orderDetail = from o in db.Order join odetail in db.OrderDetail on o.Id equals odetail.OrderId join p in db.Product on odetail.ProductId equals p.Id where o.Id == OrderId select
            var orderDetail = db.OrderDetail.Where(x => x.OrderId == OrderId).ToList();
            ViewBag.IsActive = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Đơn hàng chưa xác nhận", Value = "0"},
                new SelectListItem { Text = "Đơn hàng đã duyệt", Value = "1"},
                new SelectListItem { Text = "Đơn hàng đã hoàn thành", Value = "2"},
                new SelectListItem { Text = "Đơn hàng bị hủy", Value = "3"}
            }, "Value", "Text",orderCus[0].IsActive);
            return View(orderDetail.ToList());
        }
        public string UpStatus(int Id,int IsActive)
        {
            var list = db.Order.Find(Id);
            list.Id = Id;
            list.IsActive = IsActive;
            db.SaveChanges();
            return "";
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Note");
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Quantity,TotalAmount,IsActive")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Order, "Id", "Note", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Note", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Quantity,TotalAmount,IsActive")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Note", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetail.Find(id);
            db.OrderDetail.Remove(orderDetail);
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
