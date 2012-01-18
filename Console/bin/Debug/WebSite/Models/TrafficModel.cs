﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JamZooMng.Models
{
    public class TrafficListModel : BaseListModel<TrafficModel>
    {
        public TrafficListModel() : base()
        { }
    }

    public class TrafficModel 
    {
		[Display(Name = "編號")]
		[Required]
		public Guid Id { get; set; }
		[Display(Name = "標題")]
		[Required]
		public string Title { get; set; }
		[Display(Name = "內文")]
		[Required]
		public string Context { get; set; }
		[Display(Name = "區域編號")]
		[Required]
		[RegularExpression("\\d+", ErrorMessage="必須為數字")]
		public int Areaid { get; set; }
		[Display(Name = "地址")]
		[Required]
		public string Address { get; set; }
		[Display(Name = "經度
")]
		[Required]
		public double Gmap_Lng { get; set; }
		[Display(Name = "緯度")]
		[Required]
		public double Gmap_Lat { get; set; }
		[Display(Name = "排序")]
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
		[Display(Name = "是否啟用")]
		[Required]
		public bool Online { get; set; }


        public TrafficModel()
        {
			Id = Guid.NewGuid();
			Create_Date = DateTime.Now;
			S_Date = DateTime.Now;
			E_Date = DateTime.Now;
			Online = true;

        }
    }
}