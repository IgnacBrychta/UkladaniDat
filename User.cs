using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkladaniDat;

public class User : Person, IPerson, ISaveable
{
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Age { get => (int)(DateTime.Now - DateOfBirth).TotalDays / Program.daysInYear; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public DateTime DateOfBirth { get; set; }

	public override string ToString()
	{
		return $"{Name} {Surname}: {Age}";
	}

	public void SaveToTextFile(string filePath, char delimeter = '|')
	{
		using FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
		using StreamWriter sw = new StreamWriter(fs);
		sw.WriteLine(
			$"{Nickname}{delimeter}{Name}{delimeter}{Surname}{delimeter}{Email}{delimeter}{Password}{delimeter}{DateOfBirth}"
			);
	}
}
