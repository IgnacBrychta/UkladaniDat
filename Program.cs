namespace UkladaniDat;

internal class Program
{
	public const int daysInYear = 365;

	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		User userHonza = new User { Name = "Honza", Surname = "Novák", DateOfBirth = new DateTime(1970, 1, 1) };
		User userMatej = new User { Name = "Matej", Surname = "idk", DateOfBirth = new DateTime(1980, 12, 31), Email = "nemam@nemam.hu" };
		string filePath = "obcane.txt";
		userHonza.SaveToTextFile(filePath);
		userMatej.SaveToTextFile(filePath, delimeter: '%');
	}
}