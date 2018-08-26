using System;
using Write_A_Thon.Events;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
    public class WordCounterService
    {
        private uint _wordCount;

        public uint WordCount
        {
            get { return _wordCount; }
            set
            {
                _wordCount = value;
                RaiseWordCountChanged();
            }
        }


        public static event WordCountEventHandler WordCountCalculated;
        public event EventHandler WordCountChanged;

        public WordCounterService()
        {
            WordCountCalculated += WordCounterService_WordCountCalculated;

        }

        private void WordCounterService_WordCountCalculated(object sender, WordCountEventArgs args)
        {
            WordCount = args.WordCount;
        }

        public static void CalculateWordCount(string words)
        {
            uint numOfWords = GetWordCountFromString(words);
            RaiseWordCountCalculated(numOfWords);
        }

        public void RaiseWordCountChanged()
        {
            WordCountChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void RaiseWordCountCalculated(uint wordCount)
        {
            WordCountCalculated?.Invoke(null, new WordCountEventArgs(wordCount));
        }

        public static uint GetWordCountFromString(string words)
        {
            string textToUse = words.Replace("\r", " ");
            string[] listOfWords = textToUse.Split(' ');
            uint wordCount = (uint)listOfWords.Length;
            uint emptyWords = 0;
            for (uint i = 0; i < wordCount; i++)
            {
                if (listOfWords[i] == "")
                {
                    emptyWords += 1;
                }
            }
            wordCount -= emptyWords;
            return wordCount;
        }
    }
}