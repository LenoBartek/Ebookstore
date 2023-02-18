namespace Ebookstore.Models
{
	public class UserModel
	{
		public int id { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string email { get; set; }
		public string login { get; set; }
		public string password { get; set; }
		public DateTime created_date { get; set; }
		public int max_number_ebooks { get; set; }
	}
}
