using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Models
{
    public class UserMedicines
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MedicineName { get; set; }
    }
}
