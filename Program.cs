namespace UkladaniDat;

internal class Program
{
	public const int daysInYear = 365;

	static void Main(string[] args)
	{
		_ = new Database().TestConnection();
        Console.WriteLine("Hello, World!");
		User userHonza = new User { Nickname = "přezdívka", Name = "Honza", Surname = "Novák", Email = "hu", Password = "heslo", DateOfBirth = new DateTime(1970, 11, 12) };
		User userMatej = new User { Name = "Matej", Surname = "idk", DateOfBirth = new DateTime(1980, 12, 31), Email = "nemam@nemam.hu" };
		string textFilePath = "obcane.txt";
		string tableFilePath = "obcane.csv";
		List<User> users = new List<User>() { userHonza, userMatej};
		User.SaveAllToTable(tableFilePath, users);

		List<User> loadedUsers = new List<User>(User.LoadAllUsersFromTable(tableFilePath));

		userHonza.SaveToTextFile(textFilePath);
		userMatej.SaveToTextFile(textFilePath);

		foreach (var uzivatel in User.LoadUsersFromTextFile("obcane.txt"))
		{

		}
	}
}