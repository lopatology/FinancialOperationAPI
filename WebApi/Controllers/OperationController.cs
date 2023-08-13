using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _service;

        public OperationController(IOperationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllOperations()
        {
            var result = _service.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOperationByID(int id)
        {
             var returnOperation = _service.Get(id);

            if (returnOperation == null)
            {
                return NotFound();
            }
            return Ok(returnOperation);
        }

        [HttpPost]
        public IActionResult PostOperation(Operation operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            var result = _service.Post(operation);
            return Ok(result);
        }
    }
}
