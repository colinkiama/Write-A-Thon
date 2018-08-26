using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
   public class InfoBarService
    {
        public WordCounterService WordCounter { get; set; }
        public Goal GoalInfo { get; set; }
        

        public InfoBarService()
        {

        }

        public uint GetWordsRequiredPerDay()
        {
            return GoalInfo.CalculateNumOfWordsToWritePerDay();
        }

        public uint GetGoalTarget()
        {
            return GoalInfo.TotalWordsPerDay;
        }
    }
}
