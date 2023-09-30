namespace webapi.Models
{
	public class UserAddresses
	{
        public int id { get; set; }
        public int user_id { get; set; }
        public string address_line1 { get; set; }
        public string? address_line2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? telephone { get; set; }
        public string? mobile { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public int? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
