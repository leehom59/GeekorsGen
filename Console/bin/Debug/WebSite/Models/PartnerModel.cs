using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JamZooMng.Models
{
    public class PartnerListModel : BaseListModel<PartnerModel>
    {
        public PartnerListModel() : base()
        { }
    }

    public class PartnerModel 
    {
		[Display(Name = "編號")]
		[Required]
		public Guid Id { get; set; }
		[Display(Name = "廠商名稱")]
		[Required]
		public string Cname { get; set; }
		[Display(Name = "連結")]
		public string Link { get; set; }
		[Display(Name = "電話")]
		public string Phone { get; set; }
		[Display(Name = "區碼")]
		[Required]
		[RegularExpression("\\d+", ErrorMessage="必須為數字")]
		public int Areaid { get; set; }
		[Display(Name = "地址")]
		public string Address { get; set; }
		[Display(Name = "經度")]
		[Required]
		public double Gmap_Lng { get; set; }
		[Display(Name = "緯度")]
		[Required]
		public double Gmap_Lat { get; set; }
		[Display(Name = "優惠憑證標題")]
		public string Discount_Title { get; set; }
		[Display(Name = "優惠憑證圖")]
		public string Discount_Image { get; set; }
		[Display(Name = "優惠憑證描述")]
		public string Discount_Desc { get; set; }
		[Display(Name = "分店地址")]
		public string Location_Address { get; set; }
		[Display(Name = "分店經度")]
		[Required]
		public double Location_Gmap_Lng { get; set; }
		[Display(Name = "分店緯度")]
		[Required]
		public double Location_Gmap_Lat { get; set; }
		[Display(Name = "排序(由大到小)")]
		[Required]
		[RegularExpression("\\d+", ErrorMessage="必須為數字")]
		public int Orders { get; set; }
		[Display(Name = "建立日期")]
		[Required]
		public DateTime Create_Date { get; set; }
		[Display(Name = "上線日期")]
		public DateTime S_Date { get; set; }
		[Display(Name = "下線日期")]
		public DateTime E_Date { get; set; }
		[Display(Name = "是否上線")]
		[Required]
		public bool Online { get; set; }


        public PartnerModel()
        {
			Id = Guid.NewGuid();
			Create_Date = DateTime.Now;
			S_Date = DateTime.Now;
			E_Date = DateTime.Now;
			Online = true;

        }
    }
}