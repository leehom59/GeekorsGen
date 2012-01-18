using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;

namespace Geekors.Generator
{
    public class BaseModelMaker : Base
    {
        

        public BaseModelMaker(
            Table _modelSchema,
            TemplateService _templateService)
            : base(_modelSchema, _templateService)
        {  }
    }
}
