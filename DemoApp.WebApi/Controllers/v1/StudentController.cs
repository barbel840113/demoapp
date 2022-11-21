using DemoApp.Application.Features.Positions.Commands.CreatePosition;
using DemoApp.Application.Features.Positions.Commands.DeletePositionById;
using DemoApp.Application.Features.Positions.Commands.UpdatePosition;
using DemoApp.Application.Features.Positions.Queries.GetPositionById;
using DemoApp.Application.Features.Positions.Queries.GetPositions;
using DemoApp.WebApi.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StudentController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStudentsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// GET api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetStudentByIdQuery { Id = id }));
        }

        /// <summary>
        /// POST api/controller
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateStudentCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        /// Bulk insert fake data by specifying number of rows
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMock")]
        public async Task<IActionResult> AddMock(InsertMockStudentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Support Ngx-DataTables https://medium.com/scrum-and-coke/angular-11-pagination-of-zillion-rows-45d8533538c0
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Paged")]
        public async Task<IActionResult> Paged(PagedSubjectQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// PUT api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
   
    }
}