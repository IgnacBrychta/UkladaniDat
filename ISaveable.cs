using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkladaniDat;

public interface ISaveable
{
	public void SaveToTextFile(string filePath, char delimeter = '|');
}
