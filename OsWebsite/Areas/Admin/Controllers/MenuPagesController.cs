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
    public class MenuPagesController : Controller
    {
        private OsWebEntities db = new OsWebEntities();

        // GET: Admin/MenuPages
        public ActionResult Index(int page = 1 , int pagesize = 20)
        {
            var menupage = db.MenuPage.OrderByDescending(x => x.Id).ToList();
            return View(menupage.ToPagedList(page,pagesize));
        }

        // GET: Admin/MenuPages/Create
        public ActionResult Create()
        {
            int IdLang = int.Parse(Session["LangWeb"].ToString());
            var menupage = db.DacapMenu(IdLang).ToList();
            var MenupageModel = new List<MenuPagesViewModel>();
            for (int i = 0; i < menupage.Count; i++)
            {
                    MenupageModel.Add(new MenuPagesViewModel {
                    IDMenu = menupage[i].ID.Value,
                    NameMenu = menupage[i].Name
                });
            }
            ViewBag.MenuPage = MenupageModel;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuPagesViewModel menuPageViewModel, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var menupages = new MenuPage();
                menupages.Name = menuPageViewModel.Name;
                menupages.CodeMenu = menuPageViewModel.CodeMenu;
                menupages.IsActive = menuPageViewModel.IsActive;
                db.MenuPage.Add(menupages);
                db.SaveChanges();

                int IdMenuPages = db.MenuPage.Select(x => x.Id).Max();
                string[] ids = formCollection["IDMenu"].Split(new char[] { ',' });
                foreach (string IDMenu in ids)
                {
                    int MenuID = int.Parse(IDMenu);
                    var Menutype = new MenuType();
                    Menutype.MenuPageID = IdMenuPages;
                    Menutype.MenuID = MenuID;
                    db.MenuType.Add(Menutype);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuPage = db.MenuPage.Find(id);
            MenuPagesViewModel menuPageModel = new MenuPagesViewModel();
            menuPageModel.MenuPageID = menuPage.Id;
            menuPageModel.Name = menuPage.Name;
            menuPageModel.CodeMenu = menuPage.CodeMenu;
            menuPageModel.IsActive = menuPage.IsActive;

            int IdLang = int.Parse(Session["LangWeb"].ToString());
            var menupages = db.DacapMenu(IdLang).ToList();
            var MenupageModel = new List<MenuPagesViewModel>();            
            for (int i = 0; i < menupages.Count; i++)
            {
                MenupageModel.Add(new MenuPagesViewModel
                {
                    IDMenu = menupages[i].ID.Value,
                    NameMenu = menupages[i].Name
                });
            }
            ViewBag.MenuPage = MenupageModel;
            var Menucheck = db.MenuType.Where(x => x.MenuPageID == id).ToList();
            var Menuchecked = new List<int>();
            for (int j = 0; j < Menucheck.Count; j++)
            {
                Menuchecked.Add(Menucheck[j].MenuID);
            }
            ViewBag.MenuPageChecked = Menuchecked;
            if (menuPage == null)
            {
                return HttpNotFound();
            }
            return View(menuPageModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuPagesViewModel menuPageViewModel, FormCollection formCollection)
        {
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                var menupages = new MenuPage();
                menupages.Id = menuPageViewModel.MenuPageID;
                menupages.Name = menuPageViewModel.Name;
                menupages.CodeMenu = menuPageViewModel.CodeMenu;
                menupages.IsActive = menuPageViewModel.IsActive;
                db.Entry(menupages).State = EntityState.Modified;
                db.SaveChanges();

                var arrdetete = db.MenuType.Where(m => m.MenuPageID == menuPageViewModel.MenuPageID && m.IDLang == LangWeb);
                db.MenuType.RemoveRange(arrdetete);
                db.SaveChanges();

                string[] ids = formCollection["IDMenu"].Split(new char[] { ',' });
                foreach (string IDMenu in ids)
                {
                    int MenuID = int.Parse(IDMenu);
                    var Menutype = new MenuType();
                    Menutype.MenuPageID = menuPageViewModel.MenuPageID;
                    Menutype.MenuID = MenuID;
                    Menutype.IDLang = LangWeb;
                    db.MenuType.Add(Menutype);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(menuPageViewModel);
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string MenupageID in ids)
            {
                int MenupageID1 = int.Parse(MenupageID);
                var Menupage = db.MenuPage.Find(MenupageID1);
                db.MenuPage.Remove(Menupage);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var arrdetete = db.MenuType.Where(m => m.MenuPageID == id);
            db.MenuType.RemoveRange(arrdetete);
            db.SaveChanges();

            MenuPage menupage = db.MenuPage.Find(id);
            db.MenuPage.Remove(menupage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.MenuPage.Find(ID);
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
