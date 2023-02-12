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
using Hangfire;

namespace Application.Items.Commands
{
    public class ChooseWinner
    {
        public class Command : IRequest
        {
            public String User { get; set; }
            public Guid ItemId { get; set; }
            public Guid WinnerId { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly ItemDbContext _context;

            public Handler(ItemDbContext context)
            { 
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Items.FindAsync(request.ItemId);
                var winner = item.Applicants.Where(applicant => applicant.ApplicantId == request.WinnerId).First();

                if (item == null)
                {
                    throw new NotFoundException($"Item {request.ItemId} does not exist");
                }
                if (item.IsSuspended)
                {
                    throw new ConflictException($"Item {request.ItemId} is suspended");
                }
                if (item.IsReceived)
                {
                    throw new ConflictException($"Item {request.ItemId} has already been recieved");
                }
                if (item.Redeemer != null)
                {
                    throw new ConflictException($"Item {request.ItemId} is suspended");
                }
                if (winner == null)
                {
                    throw new NotFoundException($"Contributor {request.WinnerId} does not exist");
                }
                if (winner.User.AuthId == request.User)
                {
                    throw new ConflictException($"You can not win your own item");
                }
                if (item.Uploader != request.User)
                {
                    throw new ConflictException($"User is not the uploader of item");
                }
                if(item.WinnerChosenRandomly)
                {
                    throw new ConflictException($"Items {request.ItemId} winner will be chosen randomly");
                }
                
                item.Redeemer = winner.User.AuthId;
                item.IsSuspended = true;
                
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
