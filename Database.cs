using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkladaniDat;

internal class Database
{
	static string connString = "Host=cornelius.db.elephantsql.com;Username=cgfykfrq;Password=Eet5Yl0xuH8gLDchwI-WuLEwse7o937z;Database=cgfykfrq";
	
	//NpgsqlConnection connection = new NpgsqlConnection(connString);
	public Database()
	{

	}

	public async Task TestConnection()
	{
		var dataSource = NpgsqlDataSource.Create(connString);

		using NpgsqlConnection conn = dataSource.OpenConnection();

		// Insert some data
		var cmd = new NpgsqlCommand("CREATE TABLE idk (Id SERIAL PRIMARY KEY, Name VARCHAR(50), Surname VARCHAR(50), Email VARCHAR(50))", conn);
		await cmd.ExecuteNonQueryAsync();
	}
}
