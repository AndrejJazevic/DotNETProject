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

namespace Application.Coupons.Commands
{
    public class AddCompany
    {
        public class Command : IRequest
        {
            public string User { get; set; }
            public Company Company { get; set; }
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
                var user = await _context.Users.FindAsync(request.User);

                if (!user.IsAdmin)
                {
                    throw new UnauthorizedAccessException($"User {user.Username} is not an Admin");
                }

                _context.Companies.Add(request.Company);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
