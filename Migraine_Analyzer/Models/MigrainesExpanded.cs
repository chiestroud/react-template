using Migraine_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Models
{
    public class MigrainesExpanded : Migraines
    {
        public Users UserInfo { get; set; }
        public Days DayInfo { get; set; }
        public Months MonthsInfo { get; set; }
        public Temperature TempInfo { get; set; }
        public TimeOfTheDay TimeInfo { get; set; }
        public Durations DurationInfo { get; set; }
    }

    public class MigraineWithFoodDrinkMedicine : Migraines
    {
        public Users UserInfoForFoodDrinkMedicine { get; set; }
        public UserFood FoodInfo { get; set; }
        public UserDrinks DrinkInfo { get; set; }
        public UserMedicines MedicineInfo { get; set; }
    }
}
