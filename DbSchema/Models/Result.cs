using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geekors.Generator.DbSchema.Models
{
    /// <summary>
    /// 取得所有需要的結果
    /// </summary>
    public class Result
    {
        public Tables Tables { get; set; }

        public Schemas Schems { get; set; }
    }
}
