using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;

namespace Geekors.Generator
{
    /// <summary>
    /// Controllter 程式碼的產生者, (基本上只要傳入 Model Name 就可以了)
    /// </summary>
    public class ControllerMaker : BaseMaker
    {
        /// <summary>
        /// Controller 的名稱,同等於 ModelName
        /// </summary>
        protected string ControllerName;

        /// <summary>
        /// Service 的名稱, 同等於 ModelName
        /// </summary>
        protected string ServiceName;

        /// <summary>
        /// ListModel 的名稱, 同等於 ModelName
        /// </summary>
        protected string ListModelName;


        public ControllerMaker(
                Table _modelSchema,
                string _template
            ) : base(_modelSchema, _template)
        {
            base.Var_Folder = "Controllers";
            base.Var_FileName = base.Var_ModelName + "Controller.cs";

            this.ControllerName = base.Var_ModelName;
            this.ServiceName = base.Var_ModelName;
            this.ListModelName = base.Var_ModelName;
        }

        protected override void OnStart()
        {
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, base.Var_ModelName);
        }

    }
}
