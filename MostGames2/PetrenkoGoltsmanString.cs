using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostGames2
{
    class PetrenkoGoltsmanString : IComparable<PetrenkoGoltsmanString>//, IComparer<PetrenkoGoltsmanString>
    {

        public string String { get; private set; }
        public double Index { get; private set; }

        private bool _isEnglish = true;


        public PetrenkoGoltsmanString(string str)
        {
            //
            foreach (char c in str.ToLower())
            {
                if (c >= 'а' && c <= 'я')
                {
                    _isEnglish = false;
                }
            }
            this.String = str;
            this.Index = CalculateIndex();

        }

        public static implicit operator PetrenkoGoltsmanString(string str)
        {
            return new PetrenkoGoltsmanString(str);
        }
        public double CalculateIndex()
        {
            if (String == null)
            {
                return 0;
            }

            if (String.Length == 0)
            {
                return 0;
            }

            HashSet<char> ignorableChars = new HashSet<char> { ' ', ',', ';', '!', '?', '.', '(', ')', '{', '}', '\'', '-' };

            // calsulate as sum of Arithmetic progression
            int d = 1;
            double a1 = 0.5;
            int length = 0;

            foreach (var a in String)
            {   
                if (!ignorableChars.Contains(a))
                {                   
                    if (a == '|' && _isEnglish)
                    {
                        break;
                    }
                    length++;
                }
            }

            // sum of Arithmetic progression
            return length > 0 ? ((2 * a1 + d * (length - 1)) / 2) * length * length : 0;
        }
        public override string ToString()
        {
            return $"{Index}    {String}";
        }
        public int CompareTo(PetrenkoGoltsmanString str)
        {
            return this.Index.CompareTo(str.Index);
        }
    }
}
