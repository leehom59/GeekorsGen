using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;
using System.IO;

namespace Geekors.Generator
{
    public class ServiceMaker : BaseMaker
    {
        protected string DataContextName;

        protected string KeyDbFieldName;

        protected string KeyDaoFieldName;

        protected string KeyFieldType;

        public ServiceMaker(
            Table _modelSchema,
            string _template,
            String _dataContextName)
            : base(_modelSchema, _template)
        {
            if (_modelSchema.Schemas.Count < 1)
            {
                throw new Exception("Schema 錯誤 at ServiceMaker");
            }
            base.Var_Folder = "Services";
            base.Var_FileName = base.Var_ModelName + "Service.cs";

            this.DataContextName = _dataContextName;

            // 主鍵 只能放在第一欄
            Schema KeySchema = _modelSchema.Schemas.FirstOrDefault();
            this.KeyDbFieldName = KeySchema.欄位名稱;
            this.KeyDaoFieldName = Library.ConvertTitleCase(KeySchema.欄位名稱);
            this.KeyFieldType = Library.DbDataTypeConvert(KeySchema.資料型別);
            
        }

        protected override void OnStart()
        {
            //replace model name
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, Var_ModelName);
            FileContent = GlobalRegex.Rgx_DbModelName.Replace(FileContent, Var_DbModelName);
            FileContent = GlobalRegex.Rgx_DataContextName.Replace(FileContent, DataContextName);

            // Db Model 主鍵名稱
            FileContent = GlobalRegex.Rgx_KeyFieldName.Replace(FileContent, this.KeyDbFieldName);
            // Dao Model 主鍵名稱
            FileContent = GlobalRegex.Rgx_DaoKeyFieldName.Replace(FileContent, this.KeyDaoFieldName);
            // 主鍵欄位類別
            FileContent = GlobalRegex.Rgx_KeyFieldType.Replace(FileContent, this.KeyFieldType);

            //Mapping_Fields_A : 傳入 DAO Model , 丟出資料庫 Model
            //result.ID = p.ID;

            //Mapping_Fields_B : 傳入 Model , 丟出 DAO Model
            //result.Icon = p.ICON;

            StringBuilder sbToDbModel = new StringBuilder();
            StringBuilder sbToDaoModel = new StringBuilder();

            StringBuilder sbUpdatingFields = new StringBuilder();

            foreach (var field in ModelSchema.Schemas)
            {
                string dbField = field.欄位名稱;
                string daoField = Library.ConvertTitleCase(field.欄位名稱);
                string daoDataType = Library.DbDataTypeConvert(field.資料型別);
                bool isNullable = (field.允許空值 == "YES") ? true : false;
                
                #region 把 db Model 轉成 dao Model 需要一些慣例
                //如果 DateTime 或 date 是可 null 型別, 需要一些例外.
                if (isNullable && (daoDataType.Equals("DateTime") || daoDataType.Equals("Date")))
                {
                    //result.S_Date = (p.S_DATE.HasValue)? p.S_DATE.Value:DateTime.MinValue;
                    sbToDaoModel.AppendFormat("\t\t\tresult.{0} = (p.{1}.HasValue)? p.{1}.Value:DateTime.MinValue;\r\n",
                        daoField,
                        dbField
                    );
                }
                else
                {
                    sbToDaoModel.AppendFormat("\t\t\tresult.{0} = p.{1};\r\n",
                        daoField,
                        dbField
                    );
                }
                #endregion
                
                sbToDbModel.AppendFormat("\t\t\tresult.{0} = p.{1};\r\n",
                    dbField,
                    daoField
                );

                if (dbField != KeyDbFieldName)
                {
                    sbUpdatingFields.AppendFormat("\t\t\t\tdbEntity.{0} = entity.{1};\r\n",
                        dbField,
                        daoField
                        );
                }
            }
            //mapping function
            FileContent = GlobalRegex.Rgx_Mapping_ToDBModel.Replace(FileContent, sbToDbModel.ToString());
            FileContent = GlobalRegex.Rgx_Mapping_ToDAOModel.Replace(FileContent, sbToDaoModel.ToString());
            FileContent = GlobalRegex.Rgx_UpdateFields.Replace(FileContent, sbUpdatingFields.ToString());
        }
    }
}