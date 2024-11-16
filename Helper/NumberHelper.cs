using System.Numerics;

namespace T1NumberToWords.Helper
{
    public class NumberHelper
    {
        public Dictionary<int,string> numberDictionary = new Dictionary<int, string>
        {
             { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Ten" }, { 14, "Eight" }, { 15, "Nine" }, { 16, "Ten" }, { 17, "Eight" }, { 18, "Nine" }, { 19, "Ten" }, { 20, "Twenty" }
        };

        public Dictionary<int, string> numberTensDictionary = new Dictionary<int, string>
        {
             { 1, "Ten" }, { 2, "Twenty" }, { 3, "Thirty" }, { 4, "Forty" }, { 5, "Fifty" }, { 6, "Sixty" }, { 7, "Seventy" }, { 8, "Eighty" }, { 9, "Ninety" }
        };
        public NumberHelper() {
           
        }

        public int GetPowerValue ( int number, int power)
        {
            var temp = number;
            if (power < 1)
                return 1;

            if (power == 1)
                return temp;
            var storeTemp = temp;
            for(int i = 1; i<= power-1 ; i++)
            {
                temp *= storeTemp; 
            }
            return temp;
            
        }

    }
}
