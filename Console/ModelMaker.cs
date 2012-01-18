using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;
using System.IO;

namespace Geekors.Generator
{
    public class ModelMaker : BaseMaker
    {
        public ModelMaker(
            Table _modelSchema,
            string _template)
            : base(_modelSchema, _template)
        {
            base.Var_Folder = "Models";
            base.Var_FileName = base.Var_ModelName + "Model.cs";
        }

        protected override void OnStart()
        {
            //replace model name
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, Var_ModelName);

            StringBuilder sbFieds = new StringBuilder();
            StringBuilder sbContructor = new StringBuilder();

            foreach (var field in ModelSchema.Schemas)
            {
                string fieldName = Library.ConvertTitleCase(field.欄位名稱);
                string dataType = Library.DbDataTypeConvert(field.資料型別);
                string DisplayName = (field.自訂規則.ContainsKey("displayName")) ?
                    field.自訂規則["displayName"] : fieldName;

                bool isRequired = (field.允許空值.Equals("YES")) ? false : true;

                //建構函式
                sbContructor.Append(Library.ContructorInit(dataType, fieldName));

                sbFieds.Append(Library.FieldsFormat(DisplayName,dataType, fieldName, isRequired));
            }

            //replace {Fields}
            FileContent = GlobalRegex.Rgx_Fields.Replace(FileContent, sbFieds.ToString());
            //replace 
            FileContent = GlobalRegex.Rgx_Contructor.Replace(FileContent, sbContructor.ToString());
        }

    }
}