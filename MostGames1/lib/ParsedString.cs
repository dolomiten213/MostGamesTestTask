using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostGames1.lib
{
    class ParsedString
    {
        public String String { get; } = null;
        public int WordCount { get; } = 0;
        public int VowelCount { get; } = 0;


        public ParsedString(string str)
        {
            if (str == null)
            {
                String = null;
                WordCount = 0;
                VowelCount = 0;
            }
            else
            {
                String = str;
                WordCount = str.Trim().Split(' ').Length;
                VowelCount = str.ToLower().Replace('й', ' ').Normalize(System.Text.NormalizationForm.FormKD).Count(x => vowels.Contains(x));
            }
        }
        /// <summary>
        ///  Vowels letters
        /// </summary>
        private readonly char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'y', 'а', 'о', 'у', 'ы', 'и', 'е', 'ю', 'я', 'і', 'э', 'æ', 'œ', 'ꜳ', 'ꜷ', '⌀' };
    }
}
