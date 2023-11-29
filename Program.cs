namespace UkladaniDat;

internal class Program
{
	public const int daysInYear = 365;

	static void Main(string[] args)
	{
		int hash = "string".GetHashCode();
        Console.WriteLine("Hello, World!");
		User userHonza = new User { Nickname = "přezdívka", Name = "Honza", Surname = "Novák", Email = "hu", Password = "heslo", DateOfBirth = new DateTime(1970, 11, 12) };
		User userMatej = new User { Name = "Matej", Surname = "idk", DateOfBirth = new DateTime(1980, 12, 31), Email = "nemam@nemam.hu" };
		string filePath = "obcane.txt";

		List<User> users = new List<User>() { userHonza, userMatej};
		User.SaveAllToTable("obcane.csv", users);

		List<User> loadedUsers = User.LoadAllUsersFromTable(filePath);

		userHonza.SaveToTextFile(filePath);
		userMatej.SaveToTextFile(filePath);

		foreach (var uzivatel in User.LoadUsersFromTextFile("obcane.txt"))
		{

		}
	}
}