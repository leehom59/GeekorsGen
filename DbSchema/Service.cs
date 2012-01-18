using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Geekors.Generator.DbSchema
{
    using Models;
    using System.Reflection;

    public class Service : IService
    {
        public string ConnectionString { get; private set; }



        public Service(string _connectionString)
        {
            this.ConnectionString = _connectionString;
            Init();
        }

        protected SqlConnection Connection;
        protected SqlCommand Command;

        protected void Init()
        {
            string strSqlFilepath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/GetSchema.sql");
            string strSqlCommand = File.ReadAllText(strSqlFilepath);

            Connection = new SqlConnection(this.ConnectionString);
            Command = new SqlCommand(strSqlCommand,Connection);
        }

        protected Schemas GetSchemas()
        {
            Schemas ListSchema = new Schemas();
            using (Connection)
            {
                Connection.Open();
                #region reader

                using (SqlDataReader _reader = Command.ExecuteReader())
                {
                    while (_reader.Read())
                    {
                        Schema schema = new Schema();

                        #region reflection 寫法 不要用.
                        PropertyInfo[] props = schema.GetType().GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            try
                            {
                                int idx = _reader.GetOrdinal(prop.Name);
                                if (idx != -1)
                                {

                                    if (prop.PropertyType.Name.ToLower() == "string")
                                    {
                                        string value = _reader.GetString(idx);
                                        prop.SetValue(schema, value, null);
                                    }
                                    else if (prop.PropertyType.Name.ToLower() == "int32")
                                    {
                                        int value = (_reader.IsDBNull(idx))? 0:_reader.GetInt32(idx);
                                        prop.SetValue(schema, value, null);
                                    }

                                }
                            }
                            catch { }
                        }
                        #endregion
                        
                        ListSchema.Add(schema);
                    }
                }

                #endregion

                Connection.Close();
            }

            return ListSchema;
        }


        public Result GetResult()
        {
            Schemas schems = GetSchemas();
            Tables tables = new Tables();

            var oTables = schems.Select(p => new { p.表格名稱 }).GroupBy(p => p);

            foreach (var oTable in oTables)
            {
                tables.Add(new Table() { 
                    表格名稱 = oTable.Key.表格名稱,
                    Schemas = schems.Where(p => p.表格名稱== oTable.Key.表格名稱).ToList<Schema>()
                });
            }

            return new Result() { 
                Schems = schems,
                Tables = tables
            };
            
        }
    }
}
