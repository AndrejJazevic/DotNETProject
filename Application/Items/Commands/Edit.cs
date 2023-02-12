using AutoMapper;
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
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Item Item { get; set; }
            public string User { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly ItemDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ItemDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Items.FindAsync(request.Id);
                var user = await _context.Users.FindAsync(request.User);

                if (item == null)
                {
                    throw new NotFoundException(nameof(Item), request.Id);
                }
                if(item.IsReceived)
                {
                    throw new ConflictException($"Item {request.Id} has already been received");
                }
                if(item.IsSuspended)
                {
                    throw new ConflictException($"Item {request.Id} is suspended");
                }
                if (item.Uploader != user.Username)
                {
                    throw new ConflictException($"Item {request.Id} does not belong to the client");
                }

                request.Item.Id = request.Id;
                request.Item.Uploader = user.Username;

                _mapper.Map(request.Item, item);
                
                await _context.SaveChangesAsync();
                
                return Unit.Value;
            }
        }
    }
}
