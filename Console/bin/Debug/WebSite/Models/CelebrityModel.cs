﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace JamZooMng.Models
{
	using Library;

    public class CelebrityListModel : BaseListModel<CelebrityModel>
    {
        public CelebrityListModel() : base()
        { }
    }

    public class CelebrityModel 
    {
		[Display(Name = "編號")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public Guid Id { get; set; }

		[Display(Name = "標題")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public string Title { get; set; }

		[Display(Name = "內文")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public string Context { get; set; }

		[Display(Name = "排序")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		[RegularExpression("\\d+", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName="必須為數字")]
		public int Orders { get; set; }

		[Display(Name = "建立日期")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public DateTime Create_Date { get; set; }

		[Display(Name = "上線日期")]
		public DateTime S_Date { get; set; }

		[Display(Name = "下線日期")]
		public DateTime E_Date { get; set; }

		[Display(Name = "是否啟用")]
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName="必填")]
		public bool Online { get; set; }



        public CelebrityModel()
        {
			Id = Guid.NewGuid();
			Create_Date = DateTime.Now;
			S_Date = DateTime.Now;
			E_Date = DateTime.Now;
			Online = true;

        }
    }
}