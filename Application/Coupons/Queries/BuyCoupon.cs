using Application.Exceptions;
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
    public class BuyCoupon
    {
        public class Query : IRequest<CouponCode>
        {
            public Guid CouponId { get; set; }
            public string User { get; set; }
        }
        public class Handler : IRequestHandler<Query, CouponCode>
        {
            private readonly ItemDbContext _context;
            public Handler(ItemDbContext context)
            {
                _context = context;
            }

            public async Task<CouponCode> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.User);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.User);
                }

                var coupon = await _context.Coupons.FindAsync(request.CouponId);

                if (coupon == null)
                {
                    throw new NotFoundException(nameof(Coupon), request.CouponId);
                }

                if (user.Points < coupon.Price)
                {
                    throw new NotEnoughCreditsException($"User doesnt have enough credits for {request.CouponId} coupon");
                }

                var code = await _context.CouponCodes.Where(coupon => coupon.CouponId == request.CouponId).FirstAsync();

                if(code == null)
                {
                    throw new NotFoundException($"Sorry no more tickets available with id {request.CouponId}");
                }

                user.Points -= coupon.Price;
                _context.CouponCodes.Remove(code);

                await _context.SaveChangesAsync();

                return code;
            }
        }
    }
}
