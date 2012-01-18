using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Geekors.Generator
{
    using DbSchema.Models;
    using Template;

    public abstract class Base
    {
        protected string TargetFolderName = "WebSite";

        protected Table ModelSchema;

        protected string Template;

        public Base(Table _modelSchema, string _template)
        {
            ModelSchema = _modelSchema;
            Template = _template;
        }

        protected void SaveFile(string relativePath,string content)
        {
            string absolutePath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    TargetFolderName,
                    relativePath
                );
            DirectoryInfo di = System.IO.Directory.GetParent(absolutePath);

            if (!di.Exists)
            {
                di.Create();
            }

            File.WriteAllText(absolutePath, content, System.Text.Encoding.UTF8);
        }
    }
}
