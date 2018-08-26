using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Events
{
    public delegate void WordCountEventHandler(object sender, WordCountEventArgs args);
    public class WordCountEventArgs : EventArgs
    {
        public uint WordCount { get; set; }
        public WordCountEventArgs(uint wordCount)
        {
            WordCount = wordCount;
        }
    }
}
