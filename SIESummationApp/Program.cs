using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace SIESummationApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Write your SIE file with pathname:\n");
			string SearchFile = Console.ReadLine();
			StreamReader file = new StreamReader(SearchFile);

			Dictionary<string, double> Account = new Dictionary<string, double>();

			while (true)
			{
				var line = file.ReadLine();
				if (line == null)
					break;
				string Pattern = @"#TRANS (\d{4}) {.*} (-?\d*)";
				var match = Regex.Match(line, Pattern);
				if (match.Success)
				{
					var accountID = match.Groups[1].Value;
					var amount = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);

					if (Account.ContainsKey(accountID))
						Account[accountID] += amount;
					else
						Account[accountID] = amount;
				}

			}

			foreach (var entry in Account.OrderBy(e => e.Key))
			{
				Console.WriteLine($"Account: {entry.Key} Total: {entry.Value.ToString()}\n");
			}
					Console.ReadLine();
			
		}
	}
}
