using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OsWebsite.Helper
{
    public static class helper
    {
        public static String cut_String(String s, int sokt)
        {
            if (!String.IsNullOrEmpty(s) && s.Length > sokt)
            {
                while (s.Substring(sokt - 1, 1) != " " && sokt < s.Length - 1)
                {
                    sokt += 1;
                }
                s = s.Substring(0, sokt);
                s += "...";
            }
            return s;
        }
    }
}