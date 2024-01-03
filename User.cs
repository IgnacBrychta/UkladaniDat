using CsvHelper;
using System.Runtime.Serialization;
using System.Xml;

namespace UkladaniDat;

[DataContract]
public class User : Person, IPerson, ISaveable, ISaveableXml
{
	[DataMember]
	public string? Name { get; set; }
	[DataMember]
	public string? Surname { get; set; }

	public int Age { get => (int)(DateTime.Now - DateOfBirth).TotalDays / Program.daysInYear; }
	[DataMember]
	public string? Email { get; set; }
	[DataMember]
	public string? Password { get; set; }
	[DataMember]
	public DateTime DateOfBirth { get; set; }

	public User(string name, string surname, string email, string password, DateTime dateOfBirth)
	{
		Name = name;
		Surname = surname;
		Email = email;
		Password = password;
		DateOfBirth = dateOfBirth;

	}

	public User() { }

	public override string ToString()
	{
		return $"{Name} {Surname}: {Age}";
	}

	public void SaveToTextFile(string filePath, char delimeter = '|')
	{
		using FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
		using StreamWriter sw = new StreamWriter(fs);
		sw.WriteLine(
			$"{Nickname}{delimeter}{Name}{delimeter}{Surname}{delimeter}{Email}{delimeter}{Password}{delimeter}{DateOfBirth}{delimeter}{GetHash()}"
			);
	}

	public static IEnumerable<User> LoadUsersFromTextFile(string filePath, char delimeter = '|')
	{
		string[] data = LoadRawUserData(filePath);
		foreach (var user in data)
		{
			string[] userData = user.Split(delimeter);
			if (string.IsNullOrEmpty(user)) yield break;
			if (WasDataModified(userData)) throw new FileLoadException("V souboru existují neočekávané změny.");
			yield return new User
			{
				Nickname = userData[0],
				Name = userData[1],
				Surname = userData[2],
				Email = userData[3],
				Password = userData[4],
				DateOfBirth = DateTime.Parse(userData[5])
			};
		}
	}

	private static bool WasDataModified(string[] userData)
	{
		string textHash = userData[6];
		string hash = Extensions.GetHashString(userData[0], userData[1], userData[2], userData[3], userData[4], userData[5]);
		return hash != textHash;

	}

	private static string[] LoadRawUserData(string filePath)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		using StreamReader sr = new StreamReader(fs);
		string[] data = sr.ReadToEnd().Split(Environment.NewLine);
		return data;
	}

	public string GetHash()
	{
		string hash;
#if UseHashCodeCombine
			hash = HashCode.Combine(Nickname, Name, Surname, Email, 
			Password, DateOfBirth);
#else
		hash = Extensions.GetHashString(Nickname, Name, Surname, Email, Password, DateOfBirth.ToString());
#endif
		return hash;
	}

	public static void SaveAllToTable(string filePath, IEnumerable<User> users)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
		using StreamWriter sw = new StreamWriter(fs);
		using CsvWriter csvWriter = new CsvWriter(sw, ISaveable.CsvConfiguration);

		csvWriter.WriteHeader<User>();
		csvWriter.NextRecord();
		foreach (User user in users)
		{
			csvWriter.WriteRecord(user);
			csvWriter.NextRecord();
		}
	}

	public static List<User> LoadAllUsersFromTable(string filePath)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		using StreamReader sr = new StreamReader(fs);
		using CsvReader csvReader = new CsvReader(sr, ISaveable.CsvConfiguration);

		csvReader.Read();
		csvReader.ReadHeader();
		IEnumerable<User> users = csvReader.GetRecords<User>();
		return new List<User>(users);		
	}

	public static void SaveAllToXml(string filePath, List<User> users)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
		using XmlWriter writer = XmlWriter.Create(fs, ISaveableXml.settings);
		
		DataContractSerializer serializer = new DataContractSerializer(typeof(User));
		writer.WriteStartDocument();
		writer.WriteWhitespace(Environment.NewLine);
		writer.WriteStartElement("users");
		writer.WriteWhitespace(Environment.NewLine);
		foreach (User user in users)
		{
			serializer.WriteObject(writer, user);
			writer.WriteWhitespace(Environment.NewLine);
		}
		writer.WriteEndElement();
		writer.WriteWhitespace(Environment.NewLine);
		writer.WriteEndDocument();
	}
	public static List<User> LoadAllFromXml(string filePath)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		using XmlReader reader = XmlReader.Create(fs);
		List<User> users = new List<User>();
			
		DataContractSerializer serializer = new DataContractSerializer(typeof(User));

		while (reader.Read())
		{
#warning ukázat nameof()
			if (reader.NodeType == XmlNodeType.Element && reader.Name == nameof(User))
			{
				User? user = (User?)serializer.ReadObject(reader);
				if (user is null) continue;
				users.Add(user);
			}
		}
			
		

		return users;
	}
}