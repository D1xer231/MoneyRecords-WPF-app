using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyRecords.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; } = "Active";
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User() { }
    }
}
