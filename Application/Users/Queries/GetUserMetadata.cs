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
    public class GetUserMetadata
    {
        public class Query : IRequest<User>
        {
            public string User { get; set; }
        }
        public class Handler : IRequestHandler<Query, User>
        {
            private readonly ItemDbContext _context;
            public Handler(ItemDbContext context)
            {
                _context = context;
            }
            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.User);

                return user;
            }
        }
    }
}
