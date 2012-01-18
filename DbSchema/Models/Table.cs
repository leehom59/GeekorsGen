using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geekors.Generator.DbSchema.Models
{
    /// <summary>
    /// Tables u define
    /// </summary>
    public class Tables : List<Table>
    {
    }

    /// <summary>
    /// The name of table you define
    /// </summary>
    public class Table
    {
        public string 表格名稱 { get; set; }
        public List<Schema> Schemas { get; set; }
    }
}
