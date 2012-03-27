using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace JamZooMng.Models
{
	using Library;

    public class ImagesListModel : BaseListModel<ImagesModel>
    {
        public ImagesListModel() : base()
        { }
    }

    public class ImagesModel 
    {
		[Display(Name = "Id")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Id { get; set; }

		[Display(Name = "Fid")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public Guid Fid { get; set; }

		[Display(Name = "Cname")]
		public string Cname { get; set; }

		[Display(Name = "Orders")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Orders { get; set; }

		[Display(Name = "Create_Date")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public DateTime Create_Date { get; set; }



        public ImagesModel()
        {
			Fid = Guid.NewGuid();
			Create_Date = DateTime.Now;

        }
    }
}