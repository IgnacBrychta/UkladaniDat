using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UkladaniDat;

internal class Database
{
#warning veřejně přístupné heslo? who cares lol
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
		var cmd = new NpgsqlCommand("SELECT * FROM users;", conn);
		var result = await cmd.ExecuteReaderAsync();
		foreach (var item in result)
		{
			Console.WriteLine(item.ToString());
		}
	}

	public int? InsertUser(IPerson person)
	{
		var dataSource = NpgsqlDataSource.Create(connString);

		using NpgsqlConnection conn = dataSource.OpenConnection();

		// Insert some data
		NpgsqlCommand command = new NpgsqlCommand(
			@"
			INSERT INTO all_users(name, surname, email, password, date_of_birth, is_admin)
			VALUES(@name, @surname, @email, @password, @date_of_birth, @is_admin)
			RETURNING Id;
			", 
			conn);
		command.Parameters.AddWithValue("name", person.Name ?? (object)DBNull.Value);
		command.Parameters.AddWithValue("surname", person.Surname ?? (object)DBNull.Value);
		command.Parameters.AddWithValue("email", person.Email ?? (object)DBNull.Value);
		command.Parameters.AddWithValue("password", person.Password ?? (object)DBNull.Value);
		command.Parameters.AddWithValue("date_of_birth", person.DateOfBirth);
		command.Parameters.AddWithValue("is_admin", person is Admin);
		var result = command.ExecuteScalar();
		try
		{
			int? id = (int?)result;
			return id;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
