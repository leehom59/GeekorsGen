using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JamZooMng.Models
{
    public class ImagesListModel : BaseListModel<ImagesModel>
    {
        public ImagesListModel() : base()
        { }
    }

    public class ImagesModel 
    {
		[Display(Name = "Id")]
		[Required]
		[RegularExpression("\\d+", ErrorMessage="必須為數字")]
		public int Id { get; set; }
		[Display(Name = "Fid")]
		[Required]
		public Guid Fid { get; set; }
		[Display(Name = "Cname")]
		public string Cname { get; set; }
		[Display(Name = "Orders")]
		[Required]
		[RegularExpression("\\d+", ErrorMessage="必須為數字")]
		public int Orders { get; set; }
		[Display(Name = "Create_Date")]
		[Required]
		public DateTime Create_Date { get; set; }


        public ImagesModel()
        {
			Fid = Guid.NewGuid();
			Create_Date = DateTime.Now;

        }
    }
}