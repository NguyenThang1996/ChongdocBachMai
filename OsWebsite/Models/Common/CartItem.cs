using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsWebsite.Common
{
    /**
 * CartItem Class
 * 
 * Tạo cấu trúc của giỏ hàng
 */
    public class CartItem : IEquatable<CartItem>
    {
        #region Properties
        // Tạo thuộc tính getter và setter.
        public int Quantity { get; set; }
        public string Note
        {
            get;set;
        }
        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set
            {
                _product = null;
                _productId = value;
            }
        }

        private ProductCart _product = null;
        public ProductCart Prod
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductCart(ProductId,Note);
                }
                return _product;
            }
        }


        public string Name
        {
            get { return Prod.Name; }
        }
        public string Image
        {
            get { return Prod.Image; }
        }
        public decimal UnitPrice
        {
            get { return Prod.Price; }
        }

        public decimal TotalPrice
        {
            get { return UnitPrice * Quantity; }
        }         

        public DateTime Date
        {
            get { return Prod.Date; }
        }
        #endregion

        public CartItem(int productId,string note)
        {
            this.ProductId = productId;
            this.Note = note;
        }

        public bool Equals(CartItem item)
        {
            return item.ProductId == this.ProductId;
        }
    }
}
