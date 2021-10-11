using System;
using System.Collections.Generic;
using System.Linq;

namespace MostGames2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] englishStrings = new string[]
            {
                "qwe",
                "aa",
                "q------q",
                "ваы",
                "Sorts a range of elements in a pair of one-dimensional Array objects (one contains the keys and the other contains the corresponding items) based on the keys in the first Array using the specified IComparer.",
                "Sorts the elements in a range of elements in a one-dimensional Array using the specified IComparer.",
                "Sorts a range of elements in a pair of one-dimensional Array objects (one contains the keys and the other contains the corresponding items) based on the keys in the first Array using the IComparable implementation of each key",
                "Provides methods for creating, manipulating, searching, and sorting arrays, thereby serving as the base class for all arrays in the common language runtime.",
                "Creates a shallow copy of the Array.",
                "Не выходи из комнаты, не совершай ошибку."
            };
            string[] russianStrings = new string[]
            {
                "аыв",
                "Сортирует диапазон элементов в паре одномерных объектов Array (один содержит ключи, а другой — соответствующие элементы) по ключам в первом массиве Array, используя указанный интерфейс IComparer.",
                "Сортирует элементы в диапазоне элементов одномерного массива Array с помощью реализации интерфейса IComparable каждого элемента массива Array.",
                "Реализация интерфейса IComparer, которая используется при сравнении элементов.",
                "В следующем примере кода показано, как сортировать значения в Array с помощью компаратора по умолчанию и пользовательского компаратора, который изменяет порядок сортировки на обратный. Обратите внимание, что результат может отличаться в зависимости от текущего CultureInfo .",
                "В следующем примере кода показано, как сортировать два связанных массива, где первый массив содержит ключи, а второй массив содержит значения.",
                "опа",
            };

            PetrenkoGoltsmanStringsStorage english = new PetrenkoGoltsmanStringsStorage(englishStrings);

            // multidimensial array
            // every array item is a list of string whose petrenko goltsman index equals russian petrenko goltsman index
            List<string>[] equalsStrings = new List<string>[russianStrings.Length];

            for (int i = 0; i < russianStrings.Length; i++)
            {
                equalsStrings[i] = (List<string>)english.TryGetEqualStrings(russianStrings[i]);
            }

        }
    }
}
