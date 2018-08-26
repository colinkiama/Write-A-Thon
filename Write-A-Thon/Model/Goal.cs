using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Model
{
    public class Goal
    {
        public uint TotalWordsPerDay { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
