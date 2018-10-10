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
using OsWebsite.Models;
using OsWebsite.Areas.Admin.Models;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        // GET: Admin/Menu
        public ActionResult Index(int page = 1, int pagesize = 20, string error = "")
        {
            if (error == "error")
            {
                ViewBag.TontaiNews = "Bạn không thể xóa danh mục vì bài viết thuộc danh mục còn tồn tại";
            }
            int Lang = int.Parse(Session["LangWeb"].ToString());
            //var Menu = db.DacapMenu(Lang).ToList();
            //return View(Menu.ToPagedList(page,pagesize));
            ViewBag.Module = GetMod(0);
            return View();
        }

        private string GetMod(int id)
        {

            //List<Data.Mod> list = Business.ModService.Mod_GetByTop("", "Mod.Mod_Parent=" + id + " and Lang='" + Lang + "'", "Mod_Pos asc");
            int Lang = int.Parse(Session["LangWeb"].ToString());
            var list = db.Menu.Where(x => x.IDLang == Lang && x.IDCha == id).OrderBy(x => x.IsOrder).ToList();
            string a = "";
            if (list.Count > 0)
            {
                a += "<ul>";
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].IDCha == 0)
                    {
                        a += "<li class='branch " + activemod(list[i].IsActive.ToString()) + "' >" + list[i].Name + " (" + list[i].IsOrder + ") ";
                        a += "<span class='functionitem'> ";
                        a += "<a href='/Admin/Menu/Delete/" + list[i].ID + "' onclick=\"javascript:return confirm('Bạn có muốn xóa?');\"><i class='fa fa-trash'></i></a>";
                        a += "<a href='/Admin/Menu/Edit/" + list[i].ID + "'><i class='fa fa-edit'></i></a>";
                        a += "<a href='/Admin/Menu/Create/" + list[i].ID + "'><i class='fa fa-plus'></i></a>";
                        a += "<a href='/Admin/Menu/Status/" + list[i].ID + "'><i class='fa fa-eye'></i></a>";
                        a += "</span>";
                        a += GetMod(list[i].ID);
                        a += "</li>";
                    }
                    else
                    {
                        // 
                        a += "<li style=\"display:none\"  class='branch " + activemod(list[i].IsActive.ToString()) + "'>" + list[i].Name + " (" + list[i].IsOrder + ") ";
                        a += "<span class='functionitem'> ";
                        a += "<a href='/Admin/Menu/Delete/" + list[i].ID + "' onclick=\"javascript:return confirm('Bạn có muốn xóa?');\"><i class='fa fa-trash'></i></a>";
                        a += "<a href='/Admin/Menu/Edit/" + list[i].ID + "'><i class='fa fa-edit'></i></a>";
                        a += "<a href='/Admin/Menu/Create/" + list[i].ID + "'><i class='fa fa-plus'></i></a>";
                        a += "<a href='/Admin/Menu/Status/" + list[i].ID + "'><i class='fa fa-eye'></i></a>";
                        a += "</span>";
                        a += GetMod(list[i].ID);
                        a += "</li>";
                    }
                }
                a += "</ul>";

            }
            return a;
        }
        private string activemod(string active)
        {
            // check module không active thì gạch ngang
            string s = "";
            if (active == "False")
            {
                s += "line-through";
            }
            return s;
        }

        [HttpPost]
        public ActionResult DeleteMuti(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string ID in ids)
            {
                int MenusID = int.Parse(ID);
                var menu = db.Menu.Find(MenusID);
                db.Menu.Remove(menu);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // GET: Admin/Menu/Create
        public ActionResult Create(int? ID, string error = "")
        {
            if (error == "tontai")
            {
                ViewBag.Tontai = "Nhóm tin đã tồn tại !";
            }
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            Menu mm = new Menu();
            if (ID != null)
            {
                var menu = db.Menu.Where(x => x.ID == ID).FirstOrDefault();
                ViewBag.IDCha = new SelectList(db.DacapMenu(IDLang), "ID", "Name", menu.ID);
                mm.IDCha = ID;
                mm.IsActive = true;
                var items = new SelectList(db.Module.Where(x => x.IsActive == true).OrderBy(x => x.Position), "ID", "Name").ToList();
                items.Insert(0, (new SelectListItem { Text = "Tùy chỉnh liên kết", Value = "0" }));
                ViewBag.ModID = new SelectList(items, "Value", "Text", menu.ModID);

                ViewBag.Position = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Text = "Menu", Value = "1"},
                    new SelectListItem { Text = "Box bên phải", Value = "2"},
                }, "Value", "Text", menu.Position);

                int countmax = db.Menu.Where(x => x.IDCha == ID).Count();
                int ordermax = 0;
                if (countmax == 0)
                {
                    ordermax = 1;
                }
                else
                {
                    ordermax = db.Menu.Where(x => x.IDCha == ID).Max(e => e.IsOrder) + 1;
                }
                ViewBag.OrderMax = ordermax;
                ViewBag.enable = "enable";
            }
            else
            {
                ViewBag.IDCha = new SelectList(db.DacapMenu(IDLang), "ID", "Name");

                List<SelectListItem> items = new SelectList(db.Module.Where(x => x.IsActive == true).OrderBy(x => x.Position), "ID", "Name").ToList();
                items.Insert(0, (new SelectListItem { Text = "Tùy chỉnh liên kết", Value = "0" }));
                ViewBag.ModID = items;

                ViewBag.Position = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Text = "Menu", Value = "1"},
                    new SelectListItem { Text = "Box bên phải", Value = "2"},
                }, "Value", "Text");


                int countmax = db.Menu.Where(x => x.IsActive == true).Count();
                int ordermax = 0;
                if (countmax == 0)
                {
                    ordermax = 1;
                }
                else
                {
                    ordermax = db.Menu.Max(e => e.IsOrder) + 1;
                }
                ViewBag.OrderMax = ordermax;
                mm.IsActive = true;
            }
            return View(mm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Tag,IDCha,Link,Target,ModID,Position,Level,Images,Summary,Content,Title,Keyword,Description,Type,Style,Hot,Priority,Views,IDLang,IsOrder,IsActive")] Menu menu)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                int check = db.Menu.Where(x => x.Name == menu.Name && x.IDLang == IDLang).Count();
                if (check != 0)
                {
                    return Create(menu.IDCha, "tontai");
                }
                if (menu.IDCha == null)
                {
                    menu.IDCha = 0;
                }
                if (menu.ModID == 0)
                {
                    menu.Link = menu.Link;
                }
                else
                {
                    menu.Link = "/" + StringClass.NameToTag(menu.Name);
                }
                if (menu.Position == null)
                {
                    menu.Position = 0;
                }
                menu.IDLang = IDLang;
                menu.Tag = StringClass.NameToTag(menu.Name);
                menu.Style = menu.Style;
                menu.Summary = "";
                menu.Content = "";
                menu.Target = "";
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCha = new SelectList(db.DacapMenu(IDLang), "ID", "Name");

            var items = new SelectList(db.Module.Where(x => x.IsActive == true).OrderBy(x => x.Position), "ID", "Name").ToList();
            items.Insert(0, (new SelectListItem { Text = "Tùy chỉnh liên kết", Value = "0" }));
            ViewBag.ModID = new SelectList(items, "Value", "Text", menu.ModID);

            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                    new SelectListItem { Text = "Menu", Value = "1"},
                    new SelectListItem { Text = "Box bên phải", Value = "2"},
            }, "Value", "Text");
            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id, string error = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            if (error == "error")
            {
                ViewBag.Tontai = "Danh mục đã tồn tại !";
            }
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            ViewBag.IDCha = new SelectList(db.DacapMenu(IDLang), "ID", "Name", menu.IDCha);

            var items = new SelectList(db.Module.Where(x => x.IsActive == true).OrderBy(x => x.Position), "ID", "Name").ToList();
            items.Insert(0, (new SelectListItem { Text = "Tùy chỉnh liên kết", Value = "0" }));
            ViewBag.ModID = new SelectList(items, "Value", "Text", menu.ModID);

            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                    new SelectListItem { Text = "Menu", Value = "1"},
                    new SelectListItem { Text = "Box bên phải", Value = "2"},
            }, "Value", "Text", menu.Position);

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Tag,IDCha,Link,Target,ModID,Position,Level,Images,Summary,Content,Title,Keyword,Description,Type,Style,Hot,Priority,Views,IDLang,IsOrder,IsActive")] Menu menu)
        {
            int IDLang = int.Parse(Session["LangWeb"].ToString());
            if (ModelState.IsValid)
            {
                int check = db.Menu.Where(x => x.ID != menu.ID && x.Name == menu.Name && x.IDLang == IDLang).Count();
                if (check != 0)
                {
                    return Edit(menu.ID, "error");
                }
                if (menu.IDCha == null)
                {
                    menu.IDCha = 0;
                }

                if (menu.ModID == 0)
                {
                    menu.Link = menu.Link;
                }
                else
                {
                    menu.Link = "/" + StringClass.NameToTag(menu.Name);
                }
                if (menu.Position == null)
                {
                    menu.Position = 0;
                }
                menu.Summary = "";
                menu.Content = "";
                menu.Tag = StringClass.NameToTag(menu.Name);
                menu.Target = "";
                menu.IDLang = IDLang;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Style = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Menu Đầu Trang", Value = "0"},
                new SelectListItem { Text = "Menu Cuối Trang", Value = "1"},
            }, "Value", "Text", menu.Style);
            ViewBag.IDCha = new SelectList(db.DacapMenu(IDLang), "ID", "Name", menu.IDCha);

            var items = new SelectList(db.Module.Where(x => x.IsActive == true), "ID", "Name").ToList();
            items.Insert(0, (new SelectListItem { Text = "Tùy chỉnh liên kết", Value = "0" }));
            ViewBag.ModID = new SelectList(items, "Value", "Text", menu.ModID);

            ViewBag.Position = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Menu", Value = "1"},
                new SelectListItem { Text = "Box bên phải", Value = "2"},
            }, "Value", "Text", menu.Position);
            return View(menu);
        }

        public ActionResult Delete(int ID)
        {
            var check = db.News.Where(x => x.MenuID == ID).Count();
            if (check != 0)
            {
                return RedirectToAction("Index", new { page = 1, pagesize = 20, error = "error" });
            }
            var checkproduct = db.Product.Where(x => x.GroupProductId == ID).Count();
            if (check != 0)
            {
                return RedirectToAction("Index", new { page = 1, pagesize = 20, error = "error" });
            }

            var arrdetete = db.MenuType.Where(m => m.MenuID == ID);
            db.MenuType.RemoveRange(arrdetete);
            db.SaveChanges();

            Menu menu = db.Menu.Find(ID);
            db.Menu.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int ID)
        {
            var list = db.Menu.Find(ID);
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
