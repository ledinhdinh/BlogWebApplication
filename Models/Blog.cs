namespace BlogWebApplication.Models
{
    public class Blog
    {
        public string BlogID { get; set; }
        public string BlogName { get; set; } = "";
        public string BlogDescription { get; set; } = "";
        public string ReadingTime { get; set; } = "";
        public string Author { get; set; } = "";
        public string Link { get; set; } = "";
        public string Image { get; set; } = "";
        public string CategoryID { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public DateTime LastDateModified { get; set; }

    }
}
