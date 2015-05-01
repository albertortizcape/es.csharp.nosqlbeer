using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLBeerCore
{
    public class Utils
    {
        public static string CleanString(string pDirtyString)
        {
            return pDirtyString.Replace(@"\", @"\\").Replace("'", "\\'");
        }
    }
}
