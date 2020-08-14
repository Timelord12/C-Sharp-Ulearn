using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        // nice!
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var parsed = SentencesParserTask.ParseSentences(phraseBeginning);
            var lastline = parsed[parsed.Count - 1];
            var result = new StringBuilder();

            for (int i = 0; i < wordsCount; i++)
            {
                if (nextWords.ContainsKey(TakeLastWords(lastline)))
                {
                    // в строке ниже сложение строк. Стоило просто дважды сделать append
                    result.Append(" " + nextWords[TakeLastWords(lastline)]);
                    lastline.Add(nextWords[TakeLastWords(lastline)]);
                }
                else
                {
                    // heh
                    if (nextWords.ContainsKey(lastline[lastline.Count - 1]))
                    {
                        result.Append(" " + nextWords[lastline[lastline.Count - 1]]);
                        lastline.Add(nextWords[lastline[lastline.Count - 1]]);
                    }
                    else break;
				}
			}

            return phraseBeginning + result.ToString();
            
        }

        public static string TakeLastWords(List<string> line)
        {
            var linelength = line.ToArray().Length;
            if (linelength > 1)
            {
                return line[linelength - 2] + " " + line[linelength - 1];
			}
            else
            {
                return line[0];
			}
		}
    }
}