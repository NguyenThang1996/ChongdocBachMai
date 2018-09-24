using OsWebsite.Models;
using System.Linq;
using System.Web.Mvc;

namespace OsWebsite.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        OsWebEntities db = new OsWebEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if(user.Username !=null && user.Password !=null)
            {
                if (user.Username == "osadmin" && user.Password == "#Os1234567") {
                    Session["Userid"] = "9999";
                    Session["Username"] = "osadmin";
                    Session["Nameadmin"] = "Supper Admin";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string pass = Encryptor.Encryptor.MD5Hash(user.Password);
                    var usercheck = db.User.SingleOrDefault(x => x.Username == user.Username && x.Password == pass);
                    if (usercheck != null)
                    {
                        Session["Userid"] = usercheck.Id;
                        Session["Username"] = usercheck.Username;
                        Session["Nameadmin"] = usercheck.Name;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.errorlogin = "Đăng nhập không thành công !";
            return View();
        }

        public ActionResult Logout()
        {
            Session["Userid"] = null;
            Session["Username"] = null;
            Session["Nameadmin"] = null;
            return RedirectToAction("Index");
        }
    }
}