using System;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
    public class WordCounterService
    {
        private uint _wordCount;

        public uint WordCount
        {
            get { return _wordCount; }
            set { _wordCount = value;
                RaiseWordCountChanged();
            }
        }


        public static event EventHandler WordCountChanged;

        public WordCounterService()
        {
            
        }

        

        public static void CalculateWordCount(string words)
        {
            uint numOfWords = GetWordCountFromString(words);
            RaiseWordCountChanged();
        }

        public static void RaiseWordCountChanged()
        {
            WordCountChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void RaiseWordCountChanged(int wordCount)
        {

        }

        private static int GetWordCountFromString(string words)
        {
            throw new NotImplementedException();
        }
    }
}