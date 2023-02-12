using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CouponResponse
    {
        public string CompanyName { get; set; }
        public string CouponName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public int Price { get; set; }
        public string LogoUrl { get; set; }
        public Guid CouponId { get; set; }

    }
}
