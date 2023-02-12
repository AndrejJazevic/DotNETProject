using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Items;
using Domain.Entities;
using Application.Items.Commands;
using Application.Items.Queries;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class ItemsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await Mediator.Send(new List.Query());

            if (!items.Any())
            {
                return NoContent();
            }

            return Ok(items);
        }

        [Authorize]
        [HttpGet("contributors/{id}")]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetContributors([FromRoute] Guid id)
        {
            var user = await GetUser();
            var contributors = await Mediator.Send(new GetApplicants.Query { ItemId = id , User = user});
            return Ok(contributors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new Details.Query { Id = id }));
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<List<Item>>> GetItems([FromQuery] string name, [FromQuery] string city)
        {
            var items = await Mediator.Send(new FilteredList.Query {Name = name, City = city});

            if (!items.Any())
            {
                return NoContent();
            }
            
            return Ok(items);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromForm] Item item)
        {
            var user = await GetUser();
            
            await Mediator.Send(new Create.Command { Item = item, User = user });

            // check how to implement Created()
            return NoContent();
        }

        [Authorize]
        [HttpPost("apply/{id}")]
        public async Task<IActionResult> ApplyForItem([FromRoute] Guid id, [FromForm] Applicant applicant)
        {
            var user = await GetUser();

            await Mediator.Send(new ApplyForItem.Command { ItemId = id, Applicant = applicant, User = user });

            return NoContent();
        }

        [Authorize]
        [HttpPost("rate/{id}")]
        public async Task<IActionResult> RateItem([FromRoute] Guid id ,[FromForm] Rating Rating)
        {
            var user = await GetUser();

            await Mediator.Send(new RateItem.Command {Id = id , Rating = Rating , User = user });

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem([FromRoute] Guid id, [FromForm] Item item)
        {
            var user = await GetUser();
            
            await Mediator.Send(new Edit.Command { Id = id, Item = item, User = user });
            
            return NoContent();
        }

        [Authorize]
        [HttpPut("unsuspend/{id}")]
        public async Task<IActionResult> UnsuspendItem([FromRoute] Guid id)
        {
            var user = await GetUser();

            await Mediator.Send(new UnsuspendItem.Command { Id = id , User = user});

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var user = await GetUser();
            
            await Mediator.Send(new Delete.Command { Id = id, User = user });
            
            return NoContent();
        }

        [Authorize]
        [HttpPost("winner/{id}")]
        public async Task<IActionResult> ChooseWinner([FromRoute] Guid id, [FromBody] Guid WinnerId)
        {
            var user = await GetUser();
            await Mediator.Send(new ChooseWinner.Command { User = user, WinnerId = WinnerId, ItemId = id });
            return NoContent();
        }
    }
}
