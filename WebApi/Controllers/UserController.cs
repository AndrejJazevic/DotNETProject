using Application.Items.Queries;
using Application.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> onRegister()
        {
            var user = await GetUser();
            await Mediator.Send(new OnRegister.Command { User = user });
            return NoContent();
        }

        [Authorize]
        [HttpGet("applications")]
        public async Task<ActionResult<List<Applicant>>> GetUserApplications()
        {
            var user = await GetUser();
            var applications = await Mediator.Send(new GetUserApplications.Query { User = user });
            return Ok(applications);
        }

        [Authorize]
        [HttpGet("metadata")]
        public async Task<ActionResult<User>> GetUserMetaData()
        {
            var user = await GetUser();
            var metadata = await Mediator.Send(new GetUserMetadata.Query { User = user });
            return Ok(metadata);
        }

    }

}
