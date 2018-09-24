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
using OsWebsite.Areas.Admin.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/Products
        public ActionResult Index(int? MenuId, string keyword, int IsActive = 0, int page = 1, int pagesize = 10,string error="")
        {
            if (error == "error")
            {
                ViewBag.ErrorTontai = "Bạn không thể xóa sản phẩm vì còn tồn tại đơn hàng chứa sản phẩm";
            }
            bool active = false;
            if (keyword == "")
            {
                keyword = null;
            }
            else if (keyword != null && keyword != "")
            {
                keyword = StringClass.NameToTag(keyword);
            }
            if (IsActive == 1)
            {
                active = true;
            }
            else if (IsActive == 2)
            {
                active = false;
            }
            ViewBag.MenuId = null;
            ViewBag.keyword = "";            
            ViewBag.IsActive = 0;
            int Lang = int.Parse(Session["LangWeb"].ToString());
            var product = db.Product.Where(x => x.IDLang == Lang);
            if (!string.IsNullOrWhiteSpace(keyword)) {
                product = product.Where(x => x.Tag.Contains(keyword));
                ViewBag.keyword = keyword;
            }
            if (MenuId != null) {
                product = product.Where(x => x.GroupProductId == MenuId);
                ViewBag.MenuId = MenuId;
            }
            if (IsActive != 0)
            {
                product = product.Where(x => x.IsActive == active);
                ViewBag.IsActive = IsActive;
            }
            product = product.OrderByDescending(x => x.IsOrder);
            var listGroupProduct = db.DacapProduct(Lang,4).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--- Chọn nhóm sản phẩm ---", Value = null });
            foreach (var item in listGroupProduct)
            {
                li.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
            }
            ViewData["ddlGroupProduct"] = li;
            return View(product.ToList().ToPagedList(page, pagesize));
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            ViewBag.GroupProductId = new SelectList(db.DacapProduct(IDLang,4), "ID", "Name");
            int countmax = db.Product.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Product.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.Priority = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 2).ToList(), "ID", "Name");
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                product.ContentDetail2 = "";
                if (product.Summary == null)
                {
                    product.Summary = "";
                }
                if (product.Discount == null)
                {
                    product.Discount = "";
                }
                product.Tag = StringClass.NameToTag(product.Name) + "-" + StringClass.RandomNum(6);
                int check = db.Product.Where(x => x.Tag == product.Tag && x.IDLang == IDLang).Count();
                if (check != 0)
                {
                    product.Tag = product.Tag + "_2";
                }                
                product.IDLang = IDLang;
                product.Date = DateTime.Now;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupProductId = new SelectList(db.DacapProduct(IDLang,4), "ID", "Name",product.GroupProductId);
            int countmax = db.Product.Where(x => x.IDLang == IDLang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.Product.Max(e => e.IsOrder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.Priority = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 2).ToList(), "ID", "Name", product.Priority);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            ViewBag.GroupProductId = new SelectList(db.DacapProduct(IDLang,4), "ID", "Name", product.GroupProductId);
            ViewBag.Priority = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 2).ToList(), "ID", "Name",product.Priority);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                product.ContentDetail2 = "";
                if (product.Summary == null)
                {
                    product.Summary = "";
                }
                if (product.Discount == null)
                {
                    product.Discount = "";
                }                
                string Tag = "";
                string Tagcheck = product.Tag.Substring(product.Tag.LastIndexOf("-") + 1);
                //Replace hết kí tự lạ
                Tag = product.Tag.Replace("-" + Tagcheck, "");
                //Check nếu tên trung với tên cũ giữ nguyên Tag
                if (Tag == StringClass.NameToTag(product.Name))
                {
                    Tag = product.Tag;
                }
                else
                {
                    Tag = StringClass.NameToTag(product.Name) + "-" + StringClass.RandomNum(6);
                }
                product.Tag = Tag;
                product.IDLang = IDLang;
                product.Date = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupProductId = new SelectList(db.DacapProduct(IDLang,4), "ID", "Name", product.GroupProductId);
            ViewBag.Priority = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 2).ToList(), "ID", "Name", product.Priority);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var check = db.OrderDetail.Where(x => x.ProductId == id).Count();
            if (check != 0)
            {
                return RedirectToAction("Index", new { page = 1, pagesize = 10, error = "error" });
            }

            var arrdetete = db.ProductImages.Where(m => m.Product_Id == id);
            db.ProductImages.RemoveRange(arrdetete);
            db.SaveChanges();

            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Product.Find(ID);
            list.IsActive = !list.IsActive;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                int ProductID = int.Parse(ID);

                var check = db.OrderDetail.Where(x => x.ProductId == ProductID).Count();
                if (check != 0)
                {
                    return RedirectToAction("Index", new { page = 1, pagesize = 10, error = "error" });
                }
               
                var arrdetete = db.ProductImages.Where(m => m.Product_Id == ProductID);
                db.ProductImages.RemoveRange(arrdetete);

                db.SaveChanges();
                var Product = db.Product.Find(ProductID);
                db.Product.Remove(Product);
                db.SaveChanges();
            }
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
