using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

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
			$"{Nickname}{delimeter}{Name}{delimeter}{Surname}{delimeter}{Email}{delimeter}{Password}{delimeter}{DateOfBirth}{delimeter}{GetHashCode()}"
			);
	}

	public static IEnumerable<User> LoadUsersFromTextFile(string filePath, char delimeter = '|')
	{
		string[] data = LoadRawUserData(filePath, delimeter);
		foreach (var user in data)
		{
			string[] userData = user.Split(delimeter);
			if (WasDataModified(userData)) yield break;

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
		int textHash = int.Parse(userData[6]);
		int hash = HashCode.Combine(userData[0], userData[1], userData[2], userData[3], userData[4], userData[5]);
		return hash != textHash;

	}

	private static string[] LoadRawUserData(string filePath, char delimeter)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		using StreamReader sr = new StreamReader(fs);
		string[] data = sr.ReadToEnd().Split(Environment.NewLine);
		return data;
	}

	public override int GetHashCode()
	{
		int hashCode = HashCode.Combine(Nickname, Name, Surname, Email, 
			Password, DateOfBirth);
		return hashCode;
	}
}
