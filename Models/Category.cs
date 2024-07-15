using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.Models
{
	public class Category
	{
		[MaxLength(100)]
		public string CategoryID { get; set; }
		[MaxLength(100)]
		public string CategoryName { get; set; } = "";
		public int Type { get; set; }
	}
}
