using System;
using SQLite;

namespace SaveToSql
{
	public class BPitSQLiteHelper
	{

		public string databaseName = "BPitData.db";
		public string dataFolder;

		public BPitSQLiteHelper ()
		{

			string folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var conn = new SQLiteConnection (System.IO.Path.Combine (folder, "stocks.db"));
			//conn.CreateTable<Stock>();
			//conn.CreateTable<Valuation>();
		}
	}
}

