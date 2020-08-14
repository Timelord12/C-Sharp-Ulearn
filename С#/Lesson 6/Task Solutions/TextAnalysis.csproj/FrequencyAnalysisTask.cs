using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            return GetNGram(text);
        }

        // когда возможно, лучше в названиях давать всего и вся как можно больше давать информации о том, что это
        // и какими свойствами обладает. Например, чтобы пользующийся методом ниже мог понять не читая кода, исходя из 
        // параметра:
        // text --> sentences
        public static Dictionary<string, string> GetNGram(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var bigbase = new Dictionary<string, Dictionary<string, int>>();

            // line --> sentence
            foreach (var line in text)
            {
                // lineLength, (lower camelCase) 
                // и вообще лучше wordsCount
                var linelength = line.ToArray().Length;

                if (linelength > 1)
                {
                    for (int i = 1; i < linelength; i++)
                    {
                        var nextword = line[i];
                        var word = line[i - 1];

                        BuildBigBase(ref bigbase, nextword, word);
                    }
                }

                if (linelength > 2)
                {
                    for (int i = 2; i < linelength; i++)
                    {
                        var nextword = line[i];
                        var word = line[i - 2] + " " + line[i - 1];

                        BuildBigBase(ref bigbase, nextword, word);
                    }
                }
            }

            SortBigBase(ref result, bigbase);

            return result;
        }

        // ref избыточен: dictionary и так, являясь объектом, передается по ссылке
        // c bigbase покричал, но название метода IncNGramFrequency все же понятнее  
        public static void BuildBigBase(ref Dictionary<string, Dictionary<string, int>> bigbase, string nextword, string word)
        {
            if (!bigbase.ContainsKey(word))
            {
                bigbase[word] = new Dictionary<string, int>();
            }

            if (!bigbase[word].ContainsKey(nextword))
            {
                bigbase[word][nextword] = 1;
            }
            else
            {
                bigbase[word][nextword] += 1;
            }
        }
        
        
        // ref избыточен;
        // словарь result можно создавать здесь же, возвращать его
        // GetMostFrequentNGrams() ?
        public static void SortBigBase(ref Dictionary<string, string> result, Dictionary<string, Dictionary<string, int>> bigbase)
        {
            //wordBase, subBaseWord не самые удачные названия
            foreach (var wordbase in bigbase)
            {
                foreach (var word in wordbase.Value)
                {
                    var subbaseWord = word.Key;
                    var baseWord = wordbase.Key;
                    var meetCount = word.Value;
                    // значение ниже стоит считать во внешнем цикле
                    var maxMeetCount = wordbase.Value.Values.Max();

                    if (meetCount == maxMeetCount)
                    {
                        if (!result.ContainsKey(baseWord))
                            result[baseWord] = subbaseWord;
                        else if (String.CompareOrdinal(subbaseWord, result[baseWord]) < 0)
                        {
                            result[baseWord] = subbaseWord;
                        }
                    }
                }
            }
        }

        // кстати, в .NET Framework 4.7 и выше есть фича: 
        public static void ShowFeature(Dictionary<int, float> dictionary)
        {
            foreach (var (key, value) in dictionary)
                Console.WriteLine($"{key} {value}");
        }
        
        // Версии ниже требуют в проекте расширение:
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }
        // о расширениях расскажут в курсе, а так же в документации
        //https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        
        // О фиче деконструкции кортежей подробнее, если интересно 
        // https://docs.microsoft.com/ru-ru/dotnet/csharp/deconstruct
    }
}