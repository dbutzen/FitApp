using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.BL.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EasyCaloriesPerHour { get; set; }
        public int MediumCaloriesPerHour { get; set; }
        public int HardCaloriesPerHour { get; set; }

        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }

        public int CaloriesBurned
        {
            get
            {
                var durationHr = (double)Duration / 60.0;

                if (DifficultyLevel == 1)
                    return (int)(durationHr * EasyCaloriesPerHour);
                else if (DifficultyLevel == 2)
                    return (int)(durationHr * MediumCaloriesPerHour);
                else
                    return (int)(durationHr * HardCaloriesPerHour);
            }
        }

    }
}
