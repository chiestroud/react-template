using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Models
{
    public class Migraines
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LogDate { get; set; }
        public int DayId { get; set; }
        public int MonthId { get; set; }

        public int CurrentYear { get; set; }

        public int TimeId { get; set; }

        public IntensityType Intensity { get; set; }

        public int DurationId { get; set; }
        public bool Vomit { get; set; }
        public WeatherType Weather { get; set; }

        public EmotionType Emotion { get; set; }

        public int TemperatureId { get; set; }

        public string Comment { get; set; }

        public int FoodId { get; set; }
        public int DrinkId { get; set; }

        public int MedicineId { get; set; }
    }


    public enum IntensityType
    {
        Foggy,
        Gloggy,
        Mild,
        Moderate,
        Severe
    }

    public enum WeatherType
    {
        Sunny,
        Cloudy,
        Rainy,
        Snowy
    }

    public enum EmotionType
    {
        Angry,
        Sad,
        Happy,
        Scared,
        Stressed,
        Frustrated,
        Anxious,
        Nothing
    }
}
