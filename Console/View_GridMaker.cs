using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geekors.Generator.DbSchema.Models;

namespace Geekors.Generator
{
    public class View_GridMaker : ViewMaker
    {
        /// <summary>
        /// DAO 的key 名稱
        /// </summary>
        protected string KeyDaoFieldName;

        public View_GridMaker(
                Table _modelschema,
                string _template
            ) : 
            base(_modelschema, _template)
        {
            base.Var_FileName = "_Grid.cshtml";

            Schema KeyFieldSchema = _modelschema.Schemas.FirstOrDefault();

            KeyDaoFieldName = Library.ConvertTitleCase(KeyFieldSchema.欄位名稱);
        }

        protected override void OnStart()
        {
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, base.Var_ModelName);
            FileContent = GlobalRegex.Rgx_DaoKeyFieldName.Replace(FileContent, KeyDaoFieldName);

            //headers
            StringBuilder sb_Row_Coluns = new StringBuilder();
            foreach (var rowSchema in base.ModelSchema.Schemas)
            {
                string DaoFieldName = Library.ConvertTitleCase(rowSchema.欄位名稱);
                string DaoFieldType = Library.DbDataTypeConvert(rowSchema.資料型別);

                //從自訂規則來產生每個 columns 的程式碼
                if (rowSchema.自訂規則.ContainsKey("list"))
                {
                    if (rowSchema.自訂規則["list"] == "true")
                    {
                        sb_Row_Coluns.Append("\t\t\t\t\t" + Library.List_Grid_Columns(DaoFieldName,DaoFieldType) + "\r\n");
                    }
                }
            }

            FileContent = GlobalRegex.Rgx_Grid_Columns.Replace(FileContent, sb_Row_Coluns.ToString());
        }
    }
}
