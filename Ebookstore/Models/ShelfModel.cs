namespace Ebookstore.Models
{
    public class ShelfModel
    {
        public int id { get; set; }
        public int ebook { get; set; }
        public int userid { get; set; }
        public DateTime created_date { get; set; }

        public string? authors { get; }
        public string? title { get; }
        public string? path { get; }
        public string? filename { get; }
    }
}
