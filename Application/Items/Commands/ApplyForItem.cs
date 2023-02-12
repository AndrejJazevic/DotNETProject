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
    public class ApplyForItem
    {
        public class Command : IRequest
        {
            public string User { get; set; }
            public Guid ItemId { get; set; }
            public Applicant Applicant { get; set; }
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
                var user = await _context.Users.FindAsync(request.User);
                
                if(item == null)
                {
                    throw new NotFoundException(nameof(Item), request.ItemId); 
                }
                
                if(user == null)
                {
                    throw new NotFoundException(nameof(User), request.User); 
                }

                request.Applicant.User = user;
                request.Applicant.Item = item;

                item.Applicants.Add(request.Applicant);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
