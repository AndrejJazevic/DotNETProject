using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;

namespace Application.Items.Queries
{
    public class FilteredList
    {
        public class Query : IRequest<List<Item>>
        {
            public string Name { get; set; }
            public string City { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, List<Item>>
        {
            private readonly ItemDbContext _context;
            
            public Handler(ItemDbContext context)
            {
                _context = context;
            }

            public async Task<List<Item>> Handle(Query request, CancellationToken cancellationToken)
            {
                bool queryParamsExist = false;
                
                var items = await _context.Items.ToListAsync(cancellationToken);
                var filteredItems = new List<Item>();

                if (request.Name != null)
                {
                    string[] splitNames = request.Name.ToLower().Split(",");

                    var filteredByName =
                        from item in items
                        where item.Name.ToLower().ContainsAll(splitNames)
                        select item;
                    
                    filteredItems.AddRange(filteredByName);

                    queryParamsExist = true;
                }

                if (request.City != null)
                {
                    string[] splitCities = request.City.ToLower().Split(",");

                    var filteredByCity =
                        from item in items
                        where item.City != null && splitCities.Contains(item.City.ToLower())
                        select item;

                    if (queryParamsExist)
                    {
                        filteredItems = filteredItems.Intersect(filteredByCity).ToList();
                    }
                    else
                    {
                        filteredItems.AddRange(filteredByCity);
                    }
                    
                    queryParamsExist = true;
                }

                if (queryParamsExist)
                {
                    return filteredItems;
                }
                
                return items;
            }
        }
    }
}