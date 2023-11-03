using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputFile = "input.txt";
        string[] lines = File.ReadAllLines(inputFile);

        Dictionary<string, WordInfo> wordInfoDictionary = new Dictionary<string, WordInfo>();

        int currentPage = 1;

        foreach (string line in lines)
        {
            string[] words = line.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                string normalizedWord = word.ToLower();

                if (!wordInfoDictionary.ContainsKey(normalizedWord))
                {
                    wordInfoDictionary[normalizedWord] = new WordInfo();
                }

                wordInfoDictionary[normalizedWord].Count++;

                if (!wordInfoDictionary[normalizedWord].Pages.Contains(currentPage))
                {
                    wordInfoDictionary[normalizedWord].Pages.Add(currentPage);
                }
            }

            currentPage++;
        }

        var sortedWords = wordInfoDictionary.Keys.OrderBy(word => word);

        string outputFile = "index.txt";
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (string word in sortedWords)
            {
                WordInfo info = wordInfoDictionary[word];
                writer.WriteLine($"{word}: Count = {info.Count}, Pages = {string.Join(", ", info.Pages)}");
            }
        }

        Console.WriteLine("Предметный указатель записан в файл index.txt");
    }
}

class WordInfo
{
    public int Count { get; set; }
    public List<int> Pages { get; set; } = new List<int>();
}