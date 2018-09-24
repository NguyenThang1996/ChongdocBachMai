using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OsWebsite.Models;

namespace OsWebsite.Common
{
    public class ProductCart
    {
        OsWebEntities db = new OsWebEntities();
        public int Id { get; set; }
        public decimal Price { get; set; }        
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public ProductCart(int id,string note)
        {
            var list = db.Product.Where(x => x.Id == id).ToList();
            if (list.Count > 0)
            {
                this.Id = id;
                if (list[0].PriceOld != 0 && list[0].PriceOld != null)
                {
                    this.Price = Convert.ToDecimal(list[0].PriceOld);
                }
                else
                {
                    this.Price = Convert.ToDecimal(list[0].Price);
                }
               
                this.Name = list[0].Name;
                this.Date = DateTime.Parse(list[0].Date.ToString());
                this.Image = list[0].Image;
                this.Note = note;             
            }

        }

    }
}
