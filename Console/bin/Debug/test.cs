using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JamZooMng.Models
{
    public class CelebrityListModel : BaseListModel<CelebrityModel>
    {
        public CelebrityListModel() : base()
        { }
    }

    public class CelebrityModel 
    {
        		[Required]
		public int Id { get; set; }

		[Required]
		public string Icon { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Context { get; set; }

		[Required]
		public string C_Name { get; set; }

		[Required]
		public int Orders { get; set; }

		[Required]
		public DateTime Create_Date { get; set; }

		public DateTime S_Date { get; set; }

		public DateTime E_Date { get; set; }

		[Required]
		public bool Online { get; set; }



        public CelebrityModel()
        {
            





		Create_Date = DateTime.Now;

		S_Date = DateTime.Now;

		E_Date = DateTime.Now;



        }
    }
}