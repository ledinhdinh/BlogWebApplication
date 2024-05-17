using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebApplication.ViewModel
{
	public class BlogViewModel
	{
		public string BlogName { get; set; }
		public string BlogDescription { get; set; }
		public string ReadingTime { get; set; }
		public string Author { get; set; }
		public string Link { get; set; }
		public IFormFile? Image { get; set; }
		public string CategoryID { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastDateModified { get; set; }

		public List<SelectListItem> listCategory { get; set; }
	}
}
