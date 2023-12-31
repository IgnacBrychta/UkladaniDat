﻿namespace UkladaniDat;

internal class Program
{
	public const int daysInYear = 365;

	static void Main(string[] args)
	{
		Database db = new Database();
        Console.WriteLine("Hello, World!");
		User userHonza = new User { Nickname = "přezdívka", Name = "Honza", Surname = "Novák", Email = "hu", Password = "heslo", DateOfBirth = new DateTime(1970, 11, 12) };
		User userMatej = new User { Name = "Matej", Surname = "idk", DateOfBirth = new DateTime(1980, 12, 31), Email = "nemam@nemam.hu" };
		//db.InsertUser(userMatej);
		string filePath = "obcane.txt";

		List<User> users = new List<User>() { userHonza, userMatej};
		User.SaveAllToTable("obcane.csv", users);
		User.SaveAllToXml("obcane.xml", users);
		User.LoadAllFromXml("obcane.xml");
		//List<User> loadedUsers = User.LoadAllUsersFromTable("obcane.csv");

		//userHonza.SaveToTextFile(textFilePath);
		//userMatej.SaveToTextFile(textFilePath);

		foreach (var uzivatel in User.LoadUsersFromTextFile("obcane.txt"))
		{

		}
	}
}