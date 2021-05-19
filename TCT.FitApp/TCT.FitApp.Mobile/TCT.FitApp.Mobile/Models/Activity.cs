using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.Mobile.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EasyCaloriesPerHour { get; set; }
        public int MediumCaloriesPerHour { get; set; }
        public int HardCaloriesPerHour { get; set; }

        public Guid DayActivityId { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }

        public double DurationHr { get { return (double)Duration / 60; } }


        public double CaloriesBurned
        {
            get
            {
                if (DifficultyLevel == 1)
                    return DurationHr * EasyCaloriesPerHour;
                else if (DifficultyLevel == 2)
                    return DurationHr * MediumCaloriesPerHour;
                else
                    return DurationHr * HardCaloriesPerHour;
            }
        }

    }
}
