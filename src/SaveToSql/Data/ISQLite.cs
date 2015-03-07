
using System;
using SQLite.Net;

namespace SaveToSQL
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}