using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Coupons.Queries
{
    public class GetCoupons
    {
        public class Query : IRequest<IEnumerable<CouponResponse>>
        {

        }
        public class Handler : IRequestHandler<Query, IEnumerable<CouponResponse>>
        {
            private readonly ItemDbContext _context;
            public Handler(ItemDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<CouponResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<CouponResponse> coupons = await _context.Coupons
                    .Join(
                        _context.Companies,
                        coupon => coupon.CompanyId,
                        company => company.Id,
                        (coupon, company) => new CouponResponse
                        {
                            CompanyName = company.Name,
                            Website = company.Website,
                            LogoUrl = company.LogoUrl,
                            Description = coupon.Description,
                            Price = coupon.Price,
                            CouponName = coupon.Name,
                            CouponId = coupon.Id
                        }
                    ).ToListAsync();

                return coupons;
            }

            
        }
    }
}
