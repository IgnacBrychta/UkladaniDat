using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace UkladaniDat;

internal static class Extensions
{
	private static byte[] GetHash(string inputString)
	{
		using HashAlgorithm algorithm = MD5.Create();
		return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
	}

	public static string GetHashString(this string inputString)
	{
		StringBuilder sb = new StringBuilder();
		foreach (byte b in GetHash(inputString))
			sb.Append(b.ToString("X2"));

		return sb.ToString();
	}

	public static string GetHashString<T>(params T[] values)
	{
		StringBuilder sb = new StringBuilder();
		foreach(T t in values)
		{
			sb.Append(t);
		}
		return string.Join("", sb).GetHashString();
	}

	public static string CapitalizeFirstLetter(this string str)
	{
		return char.ToUpper(str[0]) + str[1..];
	}
}
