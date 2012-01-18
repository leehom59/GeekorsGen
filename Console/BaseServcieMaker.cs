using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;

namespace Geekors.Generator
{
    public class BaseServiceMaker : Base
    {
        protected string ModelFolder = "Services";

        public BaseServiceMaker(
            Table _modelSchema,
            TemplateService _templateService)
            : base(_modelSchema, _templateService)
        {  }
    }
}
