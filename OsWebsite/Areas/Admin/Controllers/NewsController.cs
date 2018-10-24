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
using OsWebsite.Common;
using OsWebsite.Models;
using OsWebsite.Areas.Admin.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        // GET: Admin/News
        public ActionResult Index(int? MenuId, string keyword, int IsActive = 0, int page = 1,int pagesize=10)
        {
            bool active = false;
            ViewBag.MenuId = MenuId;
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
            int Lang = int.Parse(Session["LangWeb"].ToString());
            var news = new List<News>();
            news = db.News.Where(x => x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            if (keyword == null && MenuId != null && IsActive == 0)
            {
                news = db.News.Where(x => x.MenuID == MenuId && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (keyword != null && MenuId == null && IsActive == 0)
            {
                news = db.News.Where(x => x.Tag.Contains(keyword) && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (keyword == null && MenuId == null && IsActive != 0)
            {
                news = db.News.Where(x => x.IsActive == active && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (keyword == null && MenuId != null && IsActive != 0)
            {
                news = db.News.Where(x => x.IsActive == active && x.MenuID == MenuId && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (keyword != null && MenuId == null && IsActive != 0)
            {
                news = db.News.Where(x => x.IsActive == active && x.IDLang == Lang && x.Tag.Contains(keyword)).OrderBy(e => e.IsOder).ToList();
            }
            else if (MenuId != null && keyword != null && IsActive == 0)
            {
                news = db.News.Where(x => x.Tag.Contains(keyword) && x.MenuID == MenuId && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (MenuId != null && keyword != null && IsActive != 0)
            {
                news = db.News.Where(x => x.MenuID == MenuId && x.Tag.Contains(keyword) && x.IsActive == active && x.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            else if (MenuId == null && keyword == "" || keyword == null && IsActive == 0)
            {
                news = db.News.Where(e => e.IDLang == Lang).OrderBy(e => e.IsOder).ToList();
            }
            var listtopics = db.DacapMenu(Lang).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--- Chọn nhóm tin ---", Value = null });
            foreach (var item in listtopics)
            {
                li.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
            }
            ViewData["ddltopics"] = li;
            return View(news.OrderByDescending(x=>x.ID).ToPagedList(page,pagesize));
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                int IDNews = int.Parse(ID);
                var arrdetete = db.NewsImages.Where(m => m.NewsID == IDNews);
                db.NewsImages.RemoveRange(arrdetete);
                db.SaveChanges();

                var news = db.News.Find(IDNews);
                db.News.Remove(news);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // GET: Admin/News/Create
        public ActionResult Create(string error = "")
        {
            if (error == "thieumota")
            {
                ViewBag.Thieumota = "Chưa nhập mô tả!";
            }

            int Lang = int.Parse(Session["LangWeb"].ToString());
            int countmax = db.News.Where(x => x.IDLang == Lang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.News.Max(e => e.IsOder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.MenuID = new SelectList(db.DacapMenu(Lang), "ID", "Name");
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 1).ToList(), "ID", "Name");
            return View();
        }
  
        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Tag,MenuID,Decription,DecriptionTag,ContentNew,DateCreate,DateUpdate,Img,Title,Keyword,Description,View,Position,IDLang,IsOder,IsActive,ActiveTop")] News news)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            
            if (ModelState.IsValid)
            {
                news.Tag = StringClass.NameToTag(news.Name) + "-" + StringClass.RandomNum(5);
                int check = db.News.Where(x => x.Tag == news.Tag && x.IDLang == IDLang).Count();
                if(check != 0)
                {
                    news.Tag = news.Tag + "_2";
                }
                if (news.Decription == null)
                {
                    return Create("thieumota");
                }

                else
                {
                    news.DecriptionTag = StringClass.NameToTag(news.Decription);
                }

                news.DateCreate = DateTime.Now;
                news.IDLang = IDLang;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int Lang = int.Parse(Session["LangWeb"].ToString());
            ViewBag.MenuID = new SelectList(db.DacapMenu(Lang), "ID", "Name");
            int countmax = db.News.Where(x => x.IDLang == Lang).Count();
            int ordermax = 0;
            if (countmax == 0)
            {
                ordermax = 1;
            }
            else
            {
                ordermax = db.News.Max(e => e.IsOder) + 1;
            }
            ViewBag.OrderMax = ordermax;
            ViewBag.DateCreate = DateTimeClass.ConvertDateTime(DateTime.Now);
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 1).ToList(), "ID", "Name");
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id, string error = "")
        {
            if (error == "thieumota")
            {
                ViewBag.Thieumota = "Chưa nhập mô tả!";
            }
            int ordermax = db.News.Max(e => e.IsOder) + 1;
            ViewBag.OrderMax = ordermax;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            int Lang = int.Parse(Session["LangWeb"].ToString());
            //news.DateUp = DateTime.Now;            
            ViewBag.MenuID = new SelectList(db.DacapMenu(Lang), "ID", "Name", news.MenuID);
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 1).ToList(), "ID", "Name",news.Position);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Tag,MenuID,Name,Decription,DecriptionTag,ContentNew,DateCreate,DateUpdate,Img,Title,Keyword,Description,View,Position,IDLang,IsOder,IsActive,ActiveTop")] News news)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            int idnew = int.Parse(Request["ID"]);
            if (ModelState.IsValid)
            {
                if (news.Decription == null)
                {
                    return Create("thieumota");
                }
                news.DateUpdate = DateTime.Now;
                string Tag = "";
                string Tagcheck = news.Tag.Substring(news.Tag.LastIndexOf("-") + 1);
                //Replace hết kí tự lạ
                Tag = news.Tag.Replace("-" + Tagcheck, "");
                //Check nếu tên trung với tên cũ giữ nguyên Tag
                if (Tag == StringClass.NameToTag(news.Name))
                {
                    Tag = news.Tag;
                }
                else
                {
                    Tag = StringClass.NameToTag(news.Name) + "-" + StringClass.RandomNum(5);
                }
                news.Tag = Tag;
                if(news.Decription != null)
                {
                    news.DecriptionTag = StringClass.NameToTag(news.Decription);
                }
              
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int Lang = IDLang;
            ViewBag.MenuID = new SelectList(db.DacapMenu(Lang), "ID", "Name", news.MenuID);
            ViewBag.Position = new SelectList(db.Module.Where(x => x.IsActive == true && x.Priority == 1).ToList(), "ID", "Name", news.Position);
            return View(news);
        }


        public ActionResult Delete(int id)
        {
            var arrdetete = db.NewsImages.Where(m => m.NewsID == id);
            db.NewsImages.RemoveRange(arrdetete);
            db.SaveChanges();

            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int ID)
        {
               var list= db.News.Find(ID);
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
