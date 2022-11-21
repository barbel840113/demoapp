using DemoApp.Application.Features.Employees.Queries.GetEmployees;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EnrolmentController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSubjectQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
    }
}