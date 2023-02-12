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
    public class ConfirmRecieving
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string User { get; set; } 
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
                if(item.IsReceived)
                {
                    throw new ConflictException($"Item {request.Id} has already been recieved");
                }
                if(user.Username != item.Redeemer)
                {
                    throw new ConflictException($"User {user.Username} is not the items redeemer");
                }

                item.IsReceived = true;
                await _pointGiver.GivePointsOnRedemption(item.Uploader, item.Id);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
