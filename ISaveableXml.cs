using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UkladaniDat;

internal interface ISaveableXml
{
	public static XmlWriterSettings settings = new XmlWriterSettings()
	{
		ConformanceLevel = ConformanceLevel.Auto
	};
	public static abstract void SaveAllToXml(string filePath, List<User> users);
}
