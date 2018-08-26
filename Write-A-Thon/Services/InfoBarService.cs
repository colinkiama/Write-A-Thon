using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Events;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
   public class InfoBarService
    {
        public GoalProgress GoalProgress { get; set; }

        public InfoBarService(GoalProgress goalProgress)
        {
            GoalProgress = goalProgress;
        }

        public uint GetWordsRequiredPerDay()
        {
            return GoalProgress.WordsToWritePerDay;
        }

        public uint GetGoalTarget()
        {
            return GoalProgress.TotalWordsToWrite;
        }
    }
}
