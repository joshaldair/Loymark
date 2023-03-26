using Application.Features.Activities.Commands;
using Application.Features.Activities.Queries;
using Application.Features.Activities.Queries.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivity()
        {

            var query = new GetActivityQuery();
            var listado = await _mediator.Send(query);
            return Ok(listado);

        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateActivity([FromBody] CreateActivityCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateActivity([FromBody] UpdateActivityCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            var command = new DeleteActivityCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
