using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geekors.Generator.DbSchema.Models;

namespace Geekors.Generator
{
    public class ViewListMaker : ViewMaker
    {
        public ViewListMaker(
                Table _modelschema,
                string _template
            ) : 
            base(_modelschema, _template)
        {
            base.Var_FileName = "List.cshtml";
        }

        protected override void OnStart()
        {
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, base.Var_ModelName);

            //headers
            StringBuilder sbHeaders = new StringBuilder();
            foreach (var rowSchema in base.ModelSchema.Schemas)
            {
                string DaoFieldName = Library.ConvertTitleCase(rowSchema.欄位名稱);

                string DisplayName = (rowSchema.自訂規則.ContainsKey("displayName")) ?
                    rowSchema.自訂規則["displayName"] : DaoFieldName;

                if (rowSchema.自訂規則.ContainsKey("list"))
                {
                    if (rowSchema.自訂規則["list"] == "true")
                    {
                        sbHeaders.Append("\t\t\t\t\t" + Library.ListHeader(DisplayName,DaoFieldName) + "\r\n");
                    }
                }
            }

            FileContent = GlobalRegex.Rgx_List_Headers.Replace(FileContent, sbHeaders.ToString());
        }
    }
}
