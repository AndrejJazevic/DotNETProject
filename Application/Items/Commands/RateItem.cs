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
using Application.Core;
using Application.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Items.Commands
{
    public class RateItem
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string User { get; set; }
            public Rating Rating { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly ItemDbContext _context;
            private readonly PointGiver _pointGiver;

            public Handler(ItemDbContext context , PointGiver pointGiver)
            {
                _context = context;
                _pointGiver = pointGiver;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Items.FindAsync(request.Id);
                var user = await _context.Users.FindAsync(request.User);

                if (item == null)
                {
                    throw new NotFoundException($"Item {request.Id} does not exist");
                }
                if (user == null)
                {
                    throw new NotFoundException($"User {request.User} does not exist");
                }

                var ratings = await _context.Ratings.Where(s => s.ItemId == item.Id && s.User == user.Username).ToListAsync();
                if (ratings.Any())
                {
                    throw new ConflictException($"User {user.Username} already rated this item");
                }
                if (user.Username == item.Uploader)
                {
                    throw new ConflictException($"User {user.Username} trying to rate his own item");
                }

                request.Rating.ItemId = item.Id;
                request.Rating.User = user.Username;
                await _context.Ratings.AddAsync(request.Rating);
                await _context.SaveChangesAsync();

                await _pointGiver.GivePoints(user.Username, 1);

                return Unit.Value;
            }
        }
    }
}
