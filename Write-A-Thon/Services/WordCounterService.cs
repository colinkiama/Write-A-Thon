using System;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
    public class WordCounterService : NotifyingClass
    {
        static int WordCount = 0;

        public static event EventHandler WordCountChanged;

        public static void CalculateWordCount(string words)
        {
            WordCount = GetWordCountFromString(words);
            RaiseWordCountChanged();

        }

        public static void RaiseWordCountChanged()
        {
            WordCountChanged?.Invoke(null, EventArgs.Empty);
        }

        private static int GetWordCountFromString(string words)
        {
            throw new NotImplementedException();
        }
    }
}