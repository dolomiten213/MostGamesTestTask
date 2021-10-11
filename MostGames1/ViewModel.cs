
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MostGames1.lib;

namespace MostGames1
{
    class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Main Data for binding
        /// </summary>
        public ObservableCollection<ParsedString> MainData { get; set; } = new ObservableCollection<ParsedString>();
        
        /// <summary>
        /// Input string with IDs
        /// </summary>
        public String StringID { get; set; }
        
        /// <summary>
        /// Parsed Set of Ids
        /// </summary>
        private HashSet<Int32> IDs = new HashSet<Int32>();

        /// <summary>
        /// String count 
        /// </summary>
        private int _stringCount = 1;
        public int StringCount
        {
            get => _stringCount;
            set
            {
                _stringCount = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Count of currently processed strings
        /// </summary>
        private int _counter = 0;
        public int Counter
        {
            get => _counter;
            set
            {
                _counter = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Method takes ids, parse it, get strings from server and add into main data.
        /// </summary>
        /// 
        /// <returns>
        /// List of tuples with indexes to highlight errors. Item1 - index, Item2 - length
        /// </returns>
        public async Task<List<(int, int)>> Calculate()
        {
            Counter = 0;
            StringCount = 1;
            MainData.Clear();
            IDs.Clear();
            var a = ParseStringID();
            if (a.Count == 0)
            {
                StringCount = IDs.Count;
                
                foreach (var id in IDs)
                {
                    var str= await Server.GetStringsByIdAsync(id);
                    Counter++;
                    MainData.Add(new ParsedString(str));
                }
            }
            return a;
        }

        /// <summary>
        /// Parses StringID to IDs
        /// </summary>
        /// 
        /// <returns>
        /// List of tuples with indexes to highlight errors. Item1 - index, Item2 - length
        /// </returns>
        private List<(int, int)> ParseStringID()
        {
            if (StringID == null || StringID == String.Empty)
            {
                throw new Exception("Пустой список идентификаторов");
            }
            List<(int, int)> invalidSubstrings = new List<(int, int)>();
            var subStrings = StringID.Split(';', ',');
            foreach (var id in subStrings)
            {
                if (!String.IsNullOrWhiteSpace(id))
                {
                    var subStringWithoutSpaces = id.Trim();
                    if (subStringWithoutSpaces.All(char.IsDigit))
                    {
                        Int32 intId = Int32.Parse(subStringWithoutSpaces);

                        if (intId < 20 && intId > 0)
                        {
                            IDs.Add(intId);
                            continue;
                        }
                    }
                    invalidSubstrings.Add((StringID.IndexOf(subStringWithoutSpaces), subStringWithoutSpaces.Length));
                }
            }
            return invalidSubstrings;
        }


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
