using System;
using System.Linq;

namespace TestTasks.VowelCounting
{
    public class StringProcessor
    {
        public (char symbol, int count)[] GetCharCount(string veryLongString, char[] countedChars)
        {
            int[] counts = new int[128];
            foreach(char c in veryLongString){
                if (c < 128)
                    counts[c]++;
            }

            return countedChars.Select(c => (c, c < 128 ? counts[c] : 0)).ToArray();
        }
    }
}