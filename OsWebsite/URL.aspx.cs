using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using OsWebsite.Models;

namespace MyWeb
{
    public partial class URL : System.Web.UI.Page
    {
        string Tag = "";
        OsWebEntities db = new OsWebEntities();
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (Page.RouteData.Values["Tag"] != null && Regex.IsMatch(Page.RouteData.Values["Tag"].ToString(), @"^[0-9\p{L} '-.,\/\&]{0,300}$"))
            {
                Tag = Page.RouteData.Values["Tag"].ToString();
                Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=30", "");
                Tag = Tag.Replace("?utm_source=zalo&utm_medium=zalo&utm_campaign=zalo&zarsrc=31", "");
            }
            //cat chuoi url lay tag cuoi
            string LastTag = Tag.Substring(Tag.LastIndexOf("-") + 1);
            //kiem tra xem doan cat chuoi co thuoc dang int hay ko 
            int value;
            if (int.TryParse(LastTag, out value) && LastTag.Length == 5)
            {
                ////neu la content
                var list = db.News.Where(x => x.Tag == Tag).Select(x => x.Module.Mod_Url).ToList();
                if (list.Count > 0)
                {
                    Server.TransferRequest(list[0]);
                }
                else
                {
                    Response.Redirect("/Home/Not404");
                }

            }
            else if (int.TryParse(LastTag, out value) && LastTag.Length == 6)
            {
                ////neu la product
                var list = db.Product.Where(x => x.Tag == Tag).Select(x => x.Module.Mod_Url).ToList();
                if (list.Count > 0)
                {
                    Server.TransferRequest(list[0]);
                }
                else
                {
                    Response.Redirect("/Home/Not404");
                }
            }
            else
            {
                //neu la module
                //List<Data.Modtype> list = Business.ModtypeService.Modtype_GetByModUrl(Tag);
                var list = db.Menu.Where(x => x.Tag == Tag).Select(x => db.Module.Where(m => m.ID == x.ModID).Select(m => m.Mod_Url).FirstOrDefault()).ToList();
                if (list.Count > 0)
                {
                    Server.TransferRequest(list[0]);
                }
                else
                {
                    Response.Redirect("/Home/Not404");
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}