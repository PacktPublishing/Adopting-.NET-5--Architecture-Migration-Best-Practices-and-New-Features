using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroducingNet5.Chapter3.CS9Performance
{
    [MemoryDiagnoser]
    [ShortRunJob]

    public class SubstringVsSlice
    {
        string _sampleText;
        IEnumerable<string> _wordsToSearch;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _sampleText = "Some of the words will appear many many times in this text. While some other words will not appear that many times.";
            _wordsToSearch = _sampleText.Split(" ").Distinct();
        }


        [Benchmark]
        public int Substring()
        {
            int count = 0;
            foreach (var wordToSearch in _wordsToSearch)
            {
                count += CountOccurencesBySubstring(_sampleText, wordToSearch);
            }
            return count;
        }

        [Benchmark]
        public int Slice()
        {
            int count = 0;
            foreach (var wordToSearch in _wordsToSearch)
            {
                count += CountOccurencesBySlice(_sampleText, wordToSearch);
            }
            return count;
        }

        public int CountOccurencesBySubstring(string text, string searchString)
        {
            int count = 0;

            for (int i = 0; i <= text.Length - searchString.Length; i++)
            {
                if (text.Substring(i, searchString.Length) == searchString)
                    count++; 
            }

            return count;
        }

        public int CountOccurencesBySlice(ReadOnlySpan<char> text, ReadOnlySpan<char> searchString) 
        {
            int count = 0;

            for (int i = 0; i <= text.Length - searchString.Length; i++)
            {
                if (text.Slice(i, searchString.Length).SequenceEqual(searchString))
                count++; 
            }

            return count;
 
        }
}
}
