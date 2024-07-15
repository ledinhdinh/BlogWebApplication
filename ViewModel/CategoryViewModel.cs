using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebApplication.ViewModel
{
	public class CategoryViewModel
	{
		public string CategoryID { get; set; }
		public string CategoryName { get; set; }
		public int Type { get; set; }
		public List<SelectListItem> listCategory { get; set; }
	}
}
