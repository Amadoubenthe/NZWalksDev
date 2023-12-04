using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string[] studentNames = new string[] {"Amadou", "Brice", "Chabehou"};
            return Ok(studentNames);
        }
    }
}
