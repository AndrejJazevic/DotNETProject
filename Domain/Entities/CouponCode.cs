using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CouponCode
    {
        [Key]
        public string ActivationCode { get; set; }
        [ForeignKey("Coupon")]
        public Guid CouponId { get; set; }
    }
}
