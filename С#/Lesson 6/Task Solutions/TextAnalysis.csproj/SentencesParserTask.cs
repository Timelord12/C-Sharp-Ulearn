using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var answer = new List<List<string>>();

            foreach (var line in Sentences(@text))
            {
                answer.Add(Words(@line));
            }

            return answer;
        }

        public static List<string> Sentences(string text)
        {
            var separators = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            var separated = text.Split(separators).ToList();
            var answer = new List<string>();

            foreach (var line in separated)
            {
                if (Words(line).ToArray().Length > 0)
                {
                    answer.Add(line.Trim());
                }
            }

            return answer;
        }

        public static List<string> Words(string sentence)
        {
            var chararray = sentence.ToCharArray();
            // Вместо билдера можно было прост брать sentence.Substring(...) 
            // Дешево и сердито --J
            var local = new StringBuilder();
            var answer = new List<string>();
            foreach (var letter in chararray)
            {
                if (Char.IsLetter(letter) || (letter == '\''))
                {
                    local.Append(char.ToLower(letter));
                }
                else
                {
                    if (local.Length > 0)
                        answer.Add(local.ToString());
                    local = new StringBuilder();
                }
            }
            if (local.Length > 0)
                answer.Add(local.ToString());
            return answer;
        }
    }
}