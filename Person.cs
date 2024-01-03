using System.Runtime.CompilerServices;

namespace UkladaniDat;

[Serializable]
public abstract class Person
{
	internal string? Nickname { get; set; }
}

public interface IPerson
{
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Age { get; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public DateTime DateOfBirth { get; set; }
}