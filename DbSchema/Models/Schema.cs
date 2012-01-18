using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geekors.Generator.DbSchema.Models
{
    /// <summary>
    /// Schema Table
    /// </summary>
    public class Schemas : List<Schema>
    {
    }


    /// <summary>
    /// The schema of database you create
    /// </summary>
    public class Schema
    {
        public string 表格類型 { get; set; }

        public string 表格名稱 { get; set; }

        public string 欄位名稱 { get; set; }

        public string 欄位備註 { get; set; }

        public string 資料型別 { get; set; }

        public int 最大長度 { get; set; }

        public string 允許空值 { get; set; }

        public int 數字長度 { get; set; }

        public int 小數位數 { get; set; }

        public string 預設值 { get; set; }

        public int 欄位順序 { get; set; }

        public string 主鍵資料表 { get; set; }

        private Dictionary<string, string> result;
        public Dictionary<string, string> 自訂規則
        {
            get
            {
                result = new Dictionary<string, string>();

                //xxx=xx;xxx=xxx
                if (!string.IsNullOrEmpty(欄位備註))
                {
                    string[] settings = 欄位備註.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var setting in settings)
                    {
                        string[] keyvalue = setting.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (keyvalue.Length == 2)
                        {
                            if (result.ContainsKey(keyvalue[0]))
                                result[keyvalue[0]] = keyvalue[1];
                            else
                                result.Add(keyvalue[0], keyvalue[1]);
                        }

                    }
                }
                return result;
            }
        }
        
    }
}
