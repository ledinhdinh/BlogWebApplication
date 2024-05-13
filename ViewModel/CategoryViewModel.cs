using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.ViewModel
{
	public class CategoryViewModel
	{
		public int CategoryID { get; set; }
		public string CategoryName { get; set; } = "";
		public int Type { get; set; }


	}
}
