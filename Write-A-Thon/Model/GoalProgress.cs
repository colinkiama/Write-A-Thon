using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Model
{
    public class GoalProgress
    {
        public uint TotalWordsToWrite { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public uint WordsToWritePerDay { get; set; }

        public GoalProgress(Goal createdGoal)
        {
            TotalWordsToWrite = createdGoal.TotalWordsToWrite;
            DueDate = createdGoal.DueDate;
            WordsToWritePerDay = createdGoal.CalculateNumOfWordsToWritePerDay();
        }
    }
}
