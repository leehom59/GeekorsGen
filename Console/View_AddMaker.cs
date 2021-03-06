﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geekors.Generator.DbSchema.Models;

namespace Geekors.Generator
{
    public class View_AddMaker : ViewMaker
    {
        /// <summary>
        /// DAO 的key 名稱
        /// </summary>
        protected string KeyDaoFieldName;

        public View_AddMaker(
                Table _modelSchema,
                string _template
            ) : base(_modelSchema, _template)
        {
            base.Var_FileName = "Add.cshtml";

            Schema KeyFieldSchema = _modelSchema.Schemas.FirstOrDefault();

            KeyDaoFieldName = Library.ConvertTitleCase(KeyFieldSchema.欄位名稱);
        }

        protected override void OnStart()
        {
            FileContent = GlobalRegex.Rgx_ModelName.Replace(FileContent, base.Var_ModelName);
            FileContent = GlobalRegex.Rgx_KeyFieldName.Replace(FileContent, KeyDaoFieldName);
        }
    }
}
