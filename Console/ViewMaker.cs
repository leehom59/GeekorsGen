using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;
using System.IO;

namespace Geekors.Generator
{
    /// <summary>
    /// Views 的產生者
    /// </summary>
    public abstract class ViewMaker : BaseMaker
    {

        public ViewMaker(
            Table _modelSchema,
            string _template)
            : base(_modelSchema, _template)
        {
            base.Var_Folder = string.Format("Views/{0}/", base.Var_ModelName);
        }


        public override void Save()
        {
            base.SaveFile(Path.Combine(Var_Folder, Var_FileName), FileContent);
        }
    }
}