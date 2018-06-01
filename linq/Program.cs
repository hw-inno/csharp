using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace linq
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//			var vocabulary = GetSortedWords(
			//				"Hello, hello, hello, how low",
			//				"",
			//				"With the lights out, it's less dangerous",
			//				"Here we are now; entertain us",
			//				"I feel stupid and contagious",
			//				"Here we are now; entertain us",
			//				"A mulatto, an albino, a mosquito, my libido...",
			//				"Yeah, hey"
			//			);
			//			foreach (var word in vocabulary)
			//				Console.WriteLine(word);

			//			var result = GetMostFrequentWords("A box of biscuits, a box of mixed biscuits, and a biscuit mixer.", 2);
			//			foreach (var tuple in result)
			//			{
			//				Console.WriteLine(tuple.Item1 + " " + tuple.Item2);
			//			}


			Document[] documents =
			{
				new Document {Id = 1, Text = "Hello world!"},
				new Document {Id = 2, Text = "World, world, world... Just words..."},
				new Document {Id = 3, Text = "Words — power"},
				new Document {Id = 4, Text = ""}
			};
			var index = BuildInvertedIndex(documents);

			SearchQuery("world", index);
			SearchQuery("words", index);
			SearchQuery("power", index);
			SearchQuery("cthulhu", index);
			SearchQuery("", index);


			//			var result = GetSortedWords("A box of biscuits, a box of mixed biscuits, and a biscuit mixer.");
			//			foreach (var tuple in result)
			//			{
			//				Console.WriteLine(tuple);
			//			}

			//Console.WriteLine(GetLongest(new[] { "azaz", "as", "sdsd" }));

			Console.ReadKey();
		}

		private static void SearchQuery(string words, ILookup<string, int> index)
		{
		}

		public static string[] GetSortedWords(params string[] textLines)
		{
			return textLines
				.Where(x => !string.IsNullOrEmpty(x))
				.SelectMany(x => x.Split(new[] {" ", ",", "'", "...", ";"}, StringSplitOptions.RemoveEmptyEntries))
				.Select(x => x.ToLower())
				.OrderBy(x => x)
				.Distinct()
				.ToArray();
		}

		public static Tuple<string, int>[] GetMostFrequentWords(string text, int count)
		{
			return Regex.Split(text, @"\W+")
				.Where(word => word != "")
				// ваш код
				.Select(x => x.ToLower())
				.GroupBy(x => x)
				.Select(x => Tuple.Create(x.Key, x.Count()))
				.OrderByDescending(x => x.Item2)
				.ThenBy(x => x.Item1)
				.Take(count)
				.ToArray();
		}

		public static ILookup<string, int> BuildInvertedIndex(Document[] documents)
		{
			return documents
				.SelectMany(x => Regex.Split(x.Text, @"\W+"))
				.ToLookup(x => x, x => x.Length);
		}

		public static List<string> GetSortedWords(string text)
		{
			return text
				.Split(new string[]{" ", ".", ","}, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.ToLower())
				.Distinct()
				.Select(x => Tuple.Create(x.Length, x))
				.OrderBy(x => x)
				.Select(x => x.Item2)
				.ToList();
		}

		public static IEnumerable<Point> GetNeighbours(Point p)
		{
			int[] d = { -1, 0, 1 }; // используйте подсказку, если не понимаете зачем тут этот массив :)
			return d
				.SelectMany(x => 
					d.Select(xx => new Point(p.X + xx, p.Y + x)));
		}

		public static string GetLongest(IEnumerable<string> words)
		{
			return words
				.Select(x => x)
				.OrderByDescending(x => x.Length)
				.ThenBy(x => x)
				.ToList()[0];
		}

	}

	public class Document
	{
		public int Id;
		public string Text;
	}
}