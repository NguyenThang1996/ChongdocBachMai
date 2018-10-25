using OsWebsite.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OsWebsite.Controllers
{
    public class ConferenceController : Controller
    {
        readonly OsWebEntities db = new OsWebEntities();
        // GET: Conference
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            //int LangWeb = int.Parse(Session["LangWeb"].ToString());
            //var Newscheck = db.Menu.Where(x => x.Tag == "danh-cho-nhan-vien-y-duoc" && x.IsActive == true).ToList();
            //ViewBag.NameGroupStaff1 = Newscheck[0].Name.ToUpper();
            //var parentid = Newscheck[0].ID;
            //ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == parentid && x.IDLang == LangWeb && x.IsActive == true).ToList();
            //return View();
            string Tag = System.IO.Path.GetFileNameWithoutExtension(Request.RawUrl);
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
            Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            //kiểm tra tag xem có kèm gì khác ngoài tag
            string Tagcheck = Tag.Substring(Tag.LastIndexOf("?") + 1);
            //Replace hết kí tự lạ 
            Tag = Tag.Replace("?" + Tagcheck, "");
            int LangWeb = int.Parse(Session["LangWeb"].ToString());
            ViewBag.TimeEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).TimeEvent;
            ViewBag.TitleEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).TitleEvent;
            ViewBag.AboutEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).AboutEvent;
            ViewBag.ImageEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).ImageEvent;
            ViewBag.TimelineEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).TimelineEvent;
            ViewBag.GuestEvent = db.Config.FirstOrDefault(x => x.IDLang == LangWeb).GuestEvent;
            var Newscheck = db.Menu.Where(x => x.Tag == Tag && x.IsActive == true).ToList();
            ViewBag.NameGroupService = Newscheck[0].Name;
            var parentid = Newscheck[0].ID;
            ViewBag.MenuIdStaff = db.Menu.Where(x => x.IDCha == 318 && x.IDLang == LangWeb && x.IsActive == true).ToList();
            ViewBag.Tag = Newscheck[0].Tag;
            var News = db.News_Get4Cap(Newscheck[0].ID).Where(x => x.IDLang == LangWeb && x.IsActive == true).OrderByDescending(x => x.ID).ToList();
            return View(News.ToPagedList(page, pagesize));
        }

        public string Conference(string Name, string Chucvu, string Tochuc, string Phong, string Address, string Phone, string Email, string Content)
        {
            var obj = new Register();
            obj.Name = Name;
            obj.Job = Chucvu;
            obj.Hospital = Tochuc;
            obj.Room = Phong;
            obj.Address = Address;
            obj.Phone = Phone;
            obj.Email = Email;
            obj.Content = Content;
            obj.DateCreate = DateTime.Now;
            obj.IsActive = false;
            db.Register.Add(obj);
            db.SaveChanges();
            string str = "";
            str += "<h3>Khách hàng " + Name + " đã gửi </h3>";
            str += "<h4>Tên khách hàng : " + Name + "</h4>";
            str += "<h4>Email : " + Email + "</h4>";
            str += "<h4>Số điện thoại : " + Phone + "</h4>";
            str += "<h4>Tổ chức : " + Tochuc + "</h4>";
            str += "<h4>Nội dung : " + Content + "</h4>";
            //gửi mail cho admin
            var Mail = db.Config.Select(x => x.Email).FirstOrDefault();
            Task.Factory.StartNew(() =>
            {
                Sendmail_Gmail(Mail, Content, "Khách hàng " + Name + " đã gửi thông tin đăng ký", str);
            });
            return "";
        }

        private string MailBody(string content, string mdh, string str)
        {
            string strHTML = "";
            strHTML += mdh + "<br>";
            strHTML += str + "<br>";
            return strHTML;
        }

        public bool Sendmail_Gmail(string to, string content, string mdh, string str)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = mdh;
            mail.IsBodyHtml = true;
            mail.Body = MailBody(content, mdh, str);
            int IdLang = int.Parse(Session["LangWeb"].ToString());
            string Mail = db.Config.Where(x => x.IDLang == IdLang).Select(x => x.Email).FirstOrDefault();
            string Pass = db.Config.Where(x => x.IDLang == IdLang).Select(x => x.Password).FirstOrDefault();
            mail.From = new MailAddress(Mail);
            try
            {
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                NetworkCredential credentials = new NetworkCredential("lienhenewweb2014@gmail.com", "thuongthoi1.");
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
    }
   
}