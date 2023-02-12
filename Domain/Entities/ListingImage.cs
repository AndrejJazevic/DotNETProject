using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ListingImage
    {
        [Key]
        public string ImageUrl { get; set; }
        [ForeignKey("Item")]
        public Guid ListingId { get; set; }
    }
}
