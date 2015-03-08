using SQLite;

namespace SaveToSql
{
	public class Photo
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string ImageDesc { get; set; }
		public string ImageFileName { get; set;}
		public string ImageData { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public int IsProcesed { get; set; }
	}
}

