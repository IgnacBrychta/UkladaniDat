using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkladaniDat;

public interface ISaveable
{
	private static CultureInfo CultureInfo = new CultureInfo("cs-cz");
	public static CsvConfiguration CsvConfiguration = new CsvConfiguration(CultureInfo)
	{
		Delimiter = ",",
		NewLine = Environment.NewLine,
		PrepareHeaderForMatch = args => args.Header.CapitalizeFirstLetter(),
	};
	public void SaveToTextFile(string filePath, char delimeter = '|');

	public static abstract void SaveAllToTable(string filePath, IEnumerable<User> users);

	public static abstract List<User> LoadAllUsersFromTable(string filePath);
	public static abstract IEnumerable<User> LoadUsersFromTextFile(string filePath, char delimeter = '|');
}
