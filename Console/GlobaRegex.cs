using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Geekors.Generator
{
    public class GlobalRegex
    {
        public static Regex Rgx_ModelName = new Regex("\\{ModelName\\}");
        public static Regex Rgx_DbModelName = new Regex("\\{DbModelName\\}");
        public static Regex Rgx_Contructor = new Regex("\\{Contructor\\}");
        public static Regex Rgx_Fields = new Regex("\\{Fields\\}");
        public static Regex Rgx_DataContextName = new Regex("\\{DataContextName\\}");

        public static Regex Rgx_KeyFieldName = new Regex("\\{KeyFieldName\\}");
        public static Regex Rgx_KeyEexpression = new Regex("\\{KeyEexpression\\}");
        public static Regex Rgx_DaoKeyFieldName = new Regex("\\{DaoKeyFieldName\\}");
        public static Regex Rgx_KeyFieldType = new Regex("\\{KeyFieldType\\}");
        public static Regex Rgx_UpdateFields = new Regex("\\{UpdateFields\\}");

        public static Regex Rgx_Mapping_ToDBModel = new Regex("\\{Mapping_Fields_A\\}");
        public static Regex Rgx_Mapping_ToDAOModel = new Regex("\\{Mapping_Fields_B\\}");
        //public static Regex Rgx_UpdateFields = new Regex("\\{UpdateFields\\}");

        public static Regex Rgx_List_Headers = new Regex("\\{Headers\\}");
        public static Regex Rgx_Grid_Columns = new Regex("\\{Columns\\}");
        public static Regex Rgx_EditArea_Rows = new Regex("\\{Rows\\}");
    }
}
