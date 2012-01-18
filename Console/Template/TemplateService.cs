using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Geekors.Generator.Template
{
    public enum ViewsTemplateCategory { 
        _EditArea,
        _Grid,
        Add,
        Edit,
        List
    }
    /// <summary>
    /// Load Template
    /// </summary>
    public class TemplateService
    {
        /// <summary>
        /// 
        /// </summary>
        public string ModelTemplate { get; private set; }

        public string ControllerTemplate { get; private set; }

        public string ServiceTempate { get; private set; }

        public string _EditAreaTemplate { get; private set; }

        public string _GridTemplate { get; private set; }

        public string AddTemplate { get; private set; }

        public string EditTemplate { get; private set; }

        public string ListTemplate { get; private set; }

        public TemplateService()
        {
            ModelTemplate = GetModelTemplate();
            ControllerTemplate = GetControllerTemplate();
            ServiceTempate = GetServiceTemplate();

            var DictViewsTemplates = GetViewsTempate();

            _EditAreaTemplate = DictViewsTemplates[ViewsTemplateCategory._EditArea];
            _GridTemplate = DictViewsTemplates[ViewsTemplateCategory._Grid];
            AddTemplate = DictViewsTemplates[ViewsTemplateCategory.Add];
            EditTemplate = DictViewsTemplates[ViewsTemplateCategory.Edit];
            ListTemplate = DictViewsTemplates[ViewsTemplateCategory.List];
        }

        protected static string Load(string RelativePath)
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RelativePath);
            return File.ReadAllText(strPath, System.Text.Encoding.UTF8);
        }
        #region Model
        public static string GetModelTemplate()
        {
            return Load("Template/Models/TempModels.cs.txt");
        }
        #endregion

        #region Controller
        public static string GetControllerTemplate()
        {
            return Load("Template/Controllers/TempController.cs.txt");
        }
        #endregion

        #region Services
        public static string GetServiceTemplate()
        {
            return Load("Template/Services/TempService.cs.txt");
        }
        #endregion

        public static Dictionary<ViewsTemplateCategory, string> GetViewsTempate()
        {
            Dictionary<ViewsTemplateCategory, string> result = new Dictionary<ViewsTemplateCategory, string>();

            result[ViewsTemplateCategory._EditArea] = Load("Template/Views/Temp/_EditArea.cshtml.txt");
            result[ViewsTemplateCategory._Grid] = Load("Template/Views/Temp/_Grid.cshtml.txt");
            result[ViewsTemplateCategory.Add] = Load("Template/Views/Temp/Add.cshtml.txt");
            result[ViewsTemplateCategory.Edit] = Load("Template/Views/Temp/Edit.cshtml.txt");
            result[ViewsTemplateCategory.List] = Load("Template/Views/Temp/List.cshtml.txt");

            return result;
        }
    }
}
