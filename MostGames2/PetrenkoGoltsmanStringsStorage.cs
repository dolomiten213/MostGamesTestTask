using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostGames2
{
    class PetrenkoGoltsmanStringsStorage
    {
        // key - Petrenko-Goltsman index
        // value - List of string with this index
        SortedDictionary<double, List<string>> Strings = new SortedDictionary<double, List<string>>();

        public PetrenkoGoltsmanStringsStorage(IEnumerable<string> strs)
        {
            this.Add(strs);
        }
        public void Add(string str)
        {
            var pgString = new PetrenkoGoltsmanString(str);

            List<string> listRef = null;
            if (Strings.TryGetValue(pgString.Index, out listRef))
            {
                listRef.Add(pgString.String);
            }
            else
            {
                Strings.Add(pgString.Index, new List<string>() { pgString.String });
            }
        }
        public void Add(IEnumerable<string> strs)
        {
            foreach (var str in strs)
            {
                var pgString = new PetrenkoGoltsmanString(str);
                List<string> listRef = null;
                if (Strings.TryGetValue(pgString.Index, out listRef))
                {
                    listRef.Add(pgString.String);
                }
                else
                {
                    Strings.Add(pgString.Index, new List<string>() { pgString.String });
                }
            }
        }


        public override string ToString()
        {
            string res = String.Empty;
            foreach (var a in Strings)
            {
                res += a.Key.ToString() + '\n';
                foreach (var b in a.Value)
                {
                    res += b + '\n';
                }
            }
            return res;
        }
        public IEnumerable<string> TryGetEqualStrings(string str)
        {
            var pgString = new PetrenkoGoltsmanString(str);

            List<string> outStrings = new List<string>();
            if (Strings.TryGetValue(pgString.Index, out outStrings))
            {
                return outStrings;
            }
            else
            {
                return new List<string>();
            }

        }
    }
}
