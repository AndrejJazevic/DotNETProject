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

namespace Application.Items.Queries
{
    public class Details
    {
        public class Query : IRequest<Item>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Item>
        {
            private readonly ItemDbContext _context;
            public Handler(ItemDbContext context)
            {
                _context = context;
            }
            public async Task<Item> Handle(Query request, CancellationToken cancellationToken)
            {
                var item = await _context.Items.FindAsync(request.Id);

                if (item == null)
                {
                    throw new NotFoundException($"Item {request.Id} does not exist");
                }

                return item;
            }
        }
    }
}
