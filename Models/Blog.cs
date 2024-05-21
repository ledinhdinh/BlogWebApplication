using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string? ReadingTime { get; set; }
		[MaxLength(100)]
		public string? Author { get; set; }
		public string? Link { get; set; }
		public string? Image { get; set; }
		public string CategoryID { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? LastDateModified { get; set; }
	}
}
