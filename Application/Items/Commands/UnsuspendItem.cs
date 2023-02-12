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
    public class UnsuspendItem
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
              
                if (item == null)
                {
                    throw new NotFoundException(nameof(Item), request.Id);
                }
                if (item.Uploader != request.User)
                {
                    throw new ConflictException($"User {request.User} is not items uploader");
                }
                if (!item.IsReceived)
                {
                    throw new ConflictException($"Item {item.Id} is already received");
                }

                item.IsSuspended = false;
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
