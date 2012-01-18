using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;
using System.IO;

namespace Geekors.Generator
{
    public abstract class BaseMaker : Base
    {
        /// <summary>
        /// 檔案名稱 (要同 Class 名稱)
        /// </summary>
        protected string Var_FileName;

        /// <summary>
        /// DAO (C#) Model 的名稱
        /// </summary>
        protected string Var_ModelName;

        /// <summary>
        /// 資料庫表格的名稱 
        /// </summary>
        protected string Var_DbModelName;

        /// <summary>
        /// 樣版
        /// </summary>
        protected string Var_Template;

        /// <summary>
        /// 資料夾名稱
        /// </summary>
        protected string Var_Folder;

        /// <summary>
        /// 要產生的 cs 檔
        /// </summary>
        public string FileContent { get; protected set; }

        public BaseMaker(Table _modelSchema, string _template) 
            : base(_modelSchema, _template)
        {
            Var_DbModelName = _modelSchema.表格名稱;
            Var_ModelName = Library.ConvertTitleCase(_modelSchema.表格名稱);
            Var_Template = _template;
            //start to replace
            FileContent = Var_Template;
        }

        public virtual void Start()
        {
            OnStart();
        }

        public virtual void Save()
        {
            base.SaveFile(Path.Combine(Var_Folder, Var_FileName), FileContent);
        }

        protected abstract void OnStart();
    }
}
