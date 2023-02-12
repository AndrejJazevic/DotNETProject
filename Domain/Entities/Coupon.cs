using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
