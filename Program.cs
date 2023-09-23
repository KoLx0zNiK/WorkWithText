// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Открываем исходный файл для чтения
        string inputFile = "input.txt";
        string[] lines = File.ReadAllLines(inputFile);

        // Создаем словарь для хранения информации о словах
        Dictionary<string, WordInfo> wordInfoDictionary = new Dictionary<string, WordInfo>();

        // Номер текущей страницы (или строки)
        int currentPage = 1;

        foreach (string line in lines)
        {
            // Разбиваем строку на слова
            string[] words = line.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                string normalizedWord = word.ToLower(); // Приводим слово к нижнему регистру

                if (!wordInfoDictionary.ContainsKey(normalizedWord))
                {
                    // Создаем новую запись для слова
                    wordInfoDictionary[normalizedWord] = new WordInfo();
                }

                // Увеличиваем счетчик вхождений
                wordInfoDictionary[normalizedWord].Count++;

                // Добавляем текущую страницу (или строку)
                if (!wordInfoDictionary[normalizedWord].Pages.Contains(currentPage))
                {
                    wordInfoDictionary[normalizedWord].Pages.Add(currentPage);
                }
            }

            currentPage++; // Увеличиваем номер страницы (или строки)
        }

        // Сортируем слова в алфавитном порядке
        var sortedWords = wordInfoDictionary.Keys.OrderBy(word => word);

        // Создаем файл для записи предметного указателя
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
    public int Count { get; set; } // Количество вхождений
    public List<int> Pages { get; set; } = new List<int>(); // Список страниц (или строк)
}