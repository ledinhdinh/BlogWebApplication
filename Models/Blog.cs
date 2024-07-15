using System.ComponentModel.DataAnnotations;

namespace BlogWebApplication.Models
{
	public class Blog
	{
		[Key]
		public int BlogID { get; set; }
		[MaxLength(100)]
		public string BlogName { get; set; }
		public string BlogDescription { get; set; }
		[MaxLength(100)]
		public string? Author { get; set; }
		public string? Link { get; set; }
		public string? CoverImage { get; set; }
		public string CategoryID { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? LastDateModified { get; set; }
	}
}
