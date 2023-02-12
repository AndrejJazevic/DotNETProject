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
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries
{
    public class GetUserApplications
    {
        public class Query : IRequest<List<Applicant>>
        {
            public string User { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<Applicant>>
        {
            private readonly ItemDbContext _context;
            public Handler(ItemDbContext context)
            {
                _context = context;
            }
            public async Task<List<Applicant>> Handle(Query request, CancellationToken cancellationToken)
            {
                return null;
            }
        }
    }
}
