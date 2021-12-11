using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migraine_Analyzer.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }
    }

}
