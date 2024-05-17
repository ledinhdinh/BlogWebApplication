using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.Models
{
	public class Menu
	{
		[Key]
		public int MenuID { get; set; }
		[MaxLength(100)]
		public string MenuParentName { get; set; }
		public int MenuParentIndex { get; set; }
		[MaxLength(50)]
		public string Controller { get; set; }
		public int MenuLevel { get; set; }
		public string MenuChildName { get; set; }
		public int MenuChildIndex { get; set; }
	}
}
