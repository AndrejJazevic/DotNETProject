using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string User { get; set; }
        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        public short PriceRating { get; set; }
        public short QualityRating { get; set; }
    }
}
