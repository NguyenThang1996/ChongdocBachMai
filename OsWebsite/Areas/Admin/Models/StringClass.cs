using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace OsWebsite.Areas.Admin.Models
{
    public class StringClass : System.Web.UI.Page
    {
        /// <summary>
        /// Create attribute and funtion for textbox
        /// </summary>
        /// <param name="textBox">Textbox to set client attribute</param>
        /// <param name="Event">Event set for textbox</param>
        /// <param name="Funtion">Value of event - funtion javascript</param>
        /// <param name="ScriptFuntion">String funtion script return</param>
        public static void CreateClientScriptAttributes(TextBox textBox, string Event, string Funtion, string ScriptFuntion)
        {
            textBox.Page.ClientScript.RegisterClientScriptBlock(textBox.GetType(), textBox.ID.ToString() + "_" + Event, ScriptFuntion);
            textBox.Attributes.Add(Event, Funtion);
        }

        /// <summary>
        /// Create attribute and funtion for textbox
        /// </summary>
        /// <param name="checkBox">CheckBox to set client attribute</param>
        /// <param name="Event">Event set for checkbox</param>
        /// <param name="Funtion">Value of event - funtion javascript</param>
        /// <param name="ScriptFuntion">String funtion script return</param>
        public static void CreateClientScriptAttributes(CheckBox checkBox, string Event, string Funtion, string ScriptFuntion)
        {
            checkBox.Page.ClientScript.RegisterClientScriptBlock(checkBox.GetType(), checkBox.ID.ToString() + "_" + Event, ScriptFuntion);
            checkBox.Attributes.Add(Event, Funtion);
        }

        /// <summary>
        /// Ma hoa chuoi ky tu (MD5)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = md5.ComputeHash(valueArray);
            var sb = new StringBuilder();
            for (int i = 0; i < valueArray.Length; i++)
                sb.Append(valueArray[i].ToString("x2").ToLower());
            return sb.ToString();
        }
        /// <summary>
        /// Tao mot chuoi ngau nghien
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        #region Random String
        public static string RandomString(int size)
        {
            Random rnd = new Random();
            string srds = "";
            string[] str = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < size; i++)
            {
                srds = srds + str[rnd.Next(0, 61)];
            }
            return srds;
        }
        public static string RandomNum(int size)
        {
            Random rnd = new Random();
            string srds = "";
            string[] str = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < size; i++)
            {
                srds = srds + str[rnd.Next(0, 9)];
            }
            return srds;
        }
        #endregion
        /// <summary>
        /// Tao chuoi dung cho rewrite url
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        /// 
        public static string NameToSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string StripTag(string inputString)
        {
            const string HTML_TAG_PATTERN = "<.*?>";
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }
        public static string Subtext(string text, int number)
        {
            string s = "";
            string removehtml = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
            if (removehtml.Length > number)
            {
                int index = removehtml.Substring(0, number).LastIndexOf(' ');
                s = removehtml.Substring(0, index) + " ...";
            }
            else
            {
                s = removehtml;
            }
            return s;
        }
        //public static string Subtext(string text, int number)
        //{
        //    string s = "";
        //    //return Regex.Replace(Txt, "<(.|\\n)*?>", string.Empty);
        //    if (Regex.Replace(text, "<(.|\\n)*?>", string.Empty).Length > number)
        //    {
        //        s = Regex.Replace(text, "<(.|\\n)*?>", string.Empty).Substring(0, number) + "...";
        //    }
        //    else
        //    {
        //        s = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
        //    }
        //    return s;
        //}
        #region Name To Tag
        public static string NameToTag(string strName)
        {
            string strReturn = "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(strName, "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion
        #region Name To Tag2
        public static string NameToTag2(string strName)
        {
            string strReturn = "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(strName, "[^\\w\\s]", string.Empty).Replace(" ", "_").ToLower();
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion

        // thêm các dấu chấm vào các level cấp con
        public static string ShowNameLevel(string Name, string Level)
        {
            // chỉ thêm dấu chấm vào cột level có length>5
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel; i++)
            {
                strReturn = strReturn + "-----";
            }
            strReturn += Name;
            return strReturn;
        }

        /// <summary>
        /// Xoa ky tu unicode tu chuoi
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        #region Remove Unicode
        public static string RemoveUnicode(string strString)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string strFormD = strString.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion
        /// <summary>
        /// Ma hoa mot chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region Encode
        public static string Encode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            string strtemp = Convert.ToBase64String(encbuff);
            string strtam = "";
            Int32 i = 0, len = strtemp.Length;
            for (i = 3; i <= len; i += 3)
            {
                strtam = strtam + strtemp.Substring(i - 3, 3) + RandomString(1);
            }
            strtam = strtam + strtemp.Substring(i - 3, len - (i - 3));
            return strtam;
        }
        #endregion
        /// <summary>
        /// Giai ma mot chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region Decode
        public static string Decode(string str)
        {
            string strtam = "";
            Int32 i = 0, len = str.Length;
            for (i = 4; i <= len; i += 4)
            {
                strtam = strtam + str.Substring(i - 4, 3);
            }
            strtam = strtam + str.Substring(i - 4, len - (i - 4));
            byte[] decbuff = Convert.FromBase64String(strtam);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        public static string Format_PriceVN(string values)
        {
            values = values.Replace(",", "");
            values = values.Replace(".", "");
            string s = "";
            if (values=="0")
            {
                s = "Liên hệ";
            }
            else
            {
                while (values.Length > 3)
                {
                    s = "." + values.Substring(values.Length - 3) + s;
                    values = values.Substring(0, values.Length - 3);
                }
                s = values + s + " đ";
            }
            
            return s;
        }
    }
}
