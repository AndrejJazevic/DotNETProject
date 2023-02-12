using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users
{
    public class OnRegister
    {
        public class Command : IRequest
        {
            public string User { get; set; }
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
                /*
                var check = _context.Users.FindAsync(request.User);
                if(check != null)
                {
                    ///throw an exception
                    return Unit.Value;
                }
                await _context.AddAsync(new User { Username = request.User, KarmaPoints = 100, isVerified = false });
                await _context.SaveChangesAsync();
                */
                return Unit.Value;
            }
        }
    }
}
