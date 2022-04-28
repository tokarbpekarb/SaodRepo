using System;

namespace CountWords
{
	public class Loader
	{
		public Loader()
		{
		}
		public List<string> Load(string filename)
		{
			var words = new List<string>();
			string text = System.IO.File.ReadAllText(filename);
			string word = "";
			foreach (var ch in text)
			{
				if (ch >= 'a' && ch <= 'z' ||
							ch >= 'A' && ch <= 'Z' || ch == '\'')
					word += ch;
				else if (word.Length > 0)
				{
					words.Add(word);
					word = "";
				}
			}
			return words;
		}
	}
}
