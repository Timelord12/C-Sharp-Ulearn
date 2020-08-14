using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            return GetNGram(text);
        }

        public static Dictionary<string, string> GetNGram(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var bigbase = new Dictionary<string, Dictionary<string, int>>();

            foreach (var line in text)
            {
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

        public static void SortBigBase(ref Dictionary<string, string> result, Dictionary<string, Dictionary<string, int>> bigbase)
        {
            foreach (var wordbase in bigbase)
            {
                foreach (var word in wordbase.Value)
                {
                    var subbaseWord = word.Key;
                    var baseWord = wordbase.Key;
                    var meetCount = word.Value;
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
    }
}