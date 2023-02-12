using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public string AuthId { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public bool IsVerified { get; set; }
        public bool IsAdmin { get; set; }
        
        public ICollection<Item> Listings { get; set; }
        public ICollection<Applicant> Applications { get; set; }
    }
}
