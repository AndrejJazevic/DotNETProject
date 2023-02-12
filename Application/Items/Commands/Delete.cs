using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.Items.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var item = await _context.Items.FindAsync(request.Id);
                var user = await _context.Users.FindAsync(request.User);

                if (item == null)
                {
                    throw new NotFoundException($"Item {request.Id} does not exist");
                }

                if (item.Uploader != user.Username)
                {
                    throw new ConflictException($"Item {request.Id} does not belong to the client");
                }
                
                _context.Remove(item);
                
                await _context.SaveChangesAsync();
                
                return Unit.Value;
            }
        }
    }
}
