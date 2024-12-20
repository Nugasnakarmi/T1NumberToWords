﻿using System.Numerics;

namespace T1NumberToWords.Helper
{
    public class NumberHelper
    {
        public Dictionary<int,string> numberDictionary = new Dictionary<int, string>
        {
             { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }
        };

        public Dictionary<int, string> numberTensDictionary = new Dictionary<int, string>
        {
             { 1, "Ten" }, { 2, "Twenty" }, { 3, "Thirty" }, { 4, "Forty" }, { 5, "Fifty" }, { 6, "Sixty" }, { 7, "Seventy" }, { 8, "Eighty" }, { 9, "Ninety" }
        };
        public NumberHelper() {}
    }
}
