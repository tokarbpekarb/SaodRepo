using System;

namespace CountWords
{
	interface ICountWords
	{
		int Count { get; }
		bool ContainsKey(string key);
		void Add(string key, int value);
		int this[string key] { get; set; }
	}
}
