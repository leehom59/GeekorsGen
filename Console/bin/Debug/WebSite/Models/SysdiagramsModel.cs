using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace JamZooMng.Models
{
	using Library;

    public class SysdiagramsListModel : BaseListModel<SysdiagramsModel>
    {
        public SysdiagramsListModel() : base()
        { }
    }

    public class SysdiagramsModel 
    {
		[Display(Name = "Name")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public string Name { get; set; }

		[Display(Name = "Principal_Id")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Principal_Id { get; set; }

		[Display(Name = "Diagram_Id")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Diagram_Id { get; set; }

		[Display(Name = "Version")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Version { get; set; }

		[Display(Name = "Definition")]
		public string Definition { get; set; }



        public SysdiagramsModel()
        {

        }
    }
}