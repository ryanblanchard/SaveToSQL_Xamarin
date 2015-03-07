using System;
using SQLite;

namespace SaveToSql
{
	public class BPitImage
	{

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string ImageDesc { get; set; }
		public string ImageFileName { get; set;}
		[MaxLength(15)]
		public string LatCoord { get; set;}
		[MaxLength(15)]
		public string LongCoord { get; set;}
		[MaxLength(1)]
		public string Processed { get; set;}
		public DateTime CreatedOn { get; set; }
		public DateTime CreatedBy { get; set; }
	}
}

