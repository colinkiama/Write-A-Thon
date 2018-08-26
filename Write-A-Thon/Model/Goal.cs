using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Model
{
    public class Goal
    {
        public uint TotalWordsToWrite { get; set; }
        public DateTimeOffset DueDate { get; set; }



        public uint CalculateNumOfWordsToWritePerDay()
        {
            uint numOfWordsPerDay = 0;
            if (TotalWordsToWrite > 0)
            {
                var currentDate = DateTimeOffset.UtcNow;
                TimeSpan timeBetweenNowAndDeadline = DueDate - currentDate;
                double totalDaysTillDeadline = timeBetweenNowAndDeadline.TotalDays;
                if (totalDaysTillDeadline > 0)
                {
                    uint roundedUpDaysTillDeadline = 1;
                    if (totalDaysTillDeadline > 1)
                    {
                        roundedUpDaysTillDeadline = (uint)Math.Ceiling(totalDaysTillDeadline);
                    }
                    numOfWordsPerDay = TotalWordsToWrite / roundedUpDaysTillDeadline;
                }

            }
            return numOfWordsPerDay;
        }
    }
}
