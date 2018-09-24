using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OsWebsite.Models;

namespace OsWebsite.Controllers
{
    public class ContactController : Controller
    {
        OsWebEntities db = new OsWebEntities();
        public ActionResult Index()
        {
            return View();
        }
        public string Contact(string Name, string Email, string Phone, string Address, string Title, string Content)
        {
            var obj = new Contact();
            obj.Name = Name;
            obj.Email = Email;
            obj.Phone = Phone;
            obj.Address = Address;
            obj.Title = Title;
            obj.Content = Content;
            obj.DateCreate = DateTime.Now;
            obj.IsActive = false;
            db.Contact.Add(obj);
            db.SaveChanges();
            string str = "";
            str += "<h3>Khách hàng " + Name + " đã gửi liên hệ </h3>";
            str += "<h4>Tên khách hàng : " + Name + "</h4>";
            str += "<h4>Email : " + Email + "</h4>";
            str += "<h4>Số điện thoại : " + Phone + "</h4>";
            str += "<h4>Tiêu đề : " + Title + "</h4>";
            str += "<h4>Nội dung : " + Content + "</h4>";
            //gửi mail cho admin
            var Mail = db.Config.Select(x => x.Email).FirstOrDefault();
            Task.Factory.StartNew(() =>
            {
                Sendmail_Gmail(Mail, Content, "Khách hàng " + Name + " đã gửi thông tin liên hệ", str);
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