using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interfejs łączenia dwóch sekwencji w jedną według jakiejś reguły
    /// </summary>
    public interface IMerger
    {
        /// <summary>
        /// Nazwa metody łączenia sekwencji
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda łącząca sekwencji
        /// </summary>
        /// <param name="IEnumerable1">Pierwsza sekwencji</param>
        /// <param name="IEnumerable2">Druga sekwencji</param>
        /// <returns>Sekwencja będąca wynikiem połączenia pierwszej i drugiej sekwencji</returns>
        IEnumerable Merge(IEnumerable sequence1, IEnumerable sequence2);
    }

    class Add : IMerger
    {
        private string name;
        public Add() { this.name = "Added values";  }
        string IMerger.Name
        {
            get
            {
                return name;
            }
        }

 

        public IEnumerable Merge(IEnumerable sequance1, IEnumerable sequence2)
        {
            IEnumerator it = sequence2.GetEnumerator();
            // set position to first element.
            // If this line isn't present then it.Current will be null :(
            it.MoveNext();
            foreach(int num1 in sequance1)
            {
                int num2 = (int)it.Current;
                int result = num1 + num2;
                yield return result;
            }
        }
    }

}
