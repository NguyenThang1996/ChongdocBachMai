using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace OsWebsite.Common
{

    /**
     * ShoppingCart class
     * 
     * Lưu trữ sản phẩm đã mua và thực hiện một số các phương thức khác
     */
    public class ShoppingCart
    {
        #region Properties

        #endregion

        #region Singleton Implementation


        public static ShoppingCart Cart
        {
            get
            {
                var cart = HttpContext.Current.Session["Cart"] as ShoppingCart;
                // Nếu chưa có giỏ hàng trong session -> tạo mới và lưu vào session
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    HttpContext.Current.Session["Cart"] = cart;
                }
                return cart;
            }
        }

        // Chứa các mặt hàng đã chọn
        public List<CartItem> Items = new List<CartItem>();
         

        #endregion

        //phương thức thay đổi item
        #region Item Modification Methods
        /**
	 * AddItem() - Add một item vào trong giỏ hàng
	 */
        //,int quantity
        public void AddItem(int productId,  int quantity,string note)
        {
            // Tạo mới một Cartitem
            CartItem newItem = new CartItem(productId,note);
            //nếu đã có sp trong giỏ hàng rồi
            if (Items.Contains(newItem))
            {
                List<CartItem> listTemp = new List<CartItem>();
                //
                foreach (CartItem item in Items)
                {
                    if (item.ProductId.Equals(newItem.ProductId))
                    {
                        //thay 1 = qiantity
                        item.Quantity +=quantity;
                        bool check = item.Note.Contains(note);
                         if(check == false && item.Note !="")
                         {
                            item.Note += "," + note;
                         }
                        return;
                    }
                    else
                    {
                        //thay 1 = qiantity
                        newItem.Quantity = quantity;                         
                        listTemp.Add(newItem);
                    }
                }

                if (listTemp.Count > 0)
                {
                    Items.AddRange(listTemp);
                }
            }
            else // nếu chưa có sp trong giỏ hàng thì thê sp vào giỏ hàng và mặc định số lượng = 1
            {
                //thay 1 = qiantity
                newItem.Quantity = quantity;                
                Items.Add(newItem);
            }
        }

        /**
         * SetItemQuantity() - Thay đổi số lượng của sản phẩm trong giỏ hàng
         */
        public void SetItemQuantity(int productId, int quantity,string note)
        {
            // Nếu số lượng bằng 0 thì xóa item
            if (quantity == 0)
            {
                RemoveItem(productId);
                return;
            }

            // Tìm và update số lượng cho item trong giỏ hàng
            CartItem updatedItem = new CartItem(productId,note);
            foreach (CartItem item in Items)
            {
                if (item.ProductId.Equals(updatedItem.ProductId))
                {
                    item.Quantity = quantity;
                    item.Note = note;
                    return;
                }
            }
        }

        /**
         * RemoveItem() - Xóa item trong giỏ hàng
         */
        public void RemoveItem(int productId)
        {
            CartItem removedItem = new CartItem(productId,"");

            foreach (CartItem item in Items)
            {
                if (item.ProductId.Equals(removedItem.ProductId))
                {
                    Items.Remove(item);
                    return;
                }
            }
        }

        #endregion



        #region Reporting Methods
        /**
	 * GetSubTotal() - Tính tổng tiền
	 */
        public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (CartItem item in Items)
            {
                subTotal += item.TotalPrice;
            }
            return subTotal;
        }
        
        #endregion
    }

}
