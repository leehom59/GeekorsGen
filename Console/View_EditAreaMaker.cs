using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geekors.Generator.DbSchema.Models;

namespace Geekors.Generator
{
    public class View_EditAreaMaker : ViewMaker
    {

        public View_EditAreaMaker(
                Table _modelSchema,
                string _template
            ) : base(_modelSchema, _template)
        {
            base.Var_FileName = "_EditArea.cshtml";
        }

        protected override void OnStart()
        {
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, base.Var_ModelName);

            StringBuilder sbRowColumns = new StringBuilder();
            foreach (var rowSchema in base.ModelSchema.Schemas)
            {
                sbRowColumns.AppendLine(Library.EditAreaColumn(rowSchema));
            }
            FileContent = GlobalRegex.Rgx_EditArea_Rows.Replace(FileContent, sbRowColumns.ToString());
        }
    }
}
