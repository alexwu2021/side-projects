using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Todos.Api.Services;

namespace Todos.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        //private readonly ILogger<TodosController> _logger;
        private readonly TodosRepository _todosRepository;
        private readonly IConfiguration configuration;

        //public TodosController(ILogger<TodosController> logger, IConfiguration config)
        public TodosController(IConfiguration config)
        {
            //_logger = logger;
            configuration = config;
            _todosRepository = new TodosRepository(configuration);
        }

        // GET todos/getdefault
        [HttpGet()]
        [HttpGet("getdefault")]
        public ActionResult<IEnumerable<Models.Todo>> Getdefault()
        {
            return GetAll();
        }

        // GET todos/getall
        [HttpGet("getall")]
        public ActionResult<IEnumerable<Models.Todo>> GetAll()
        {
            return _todosRepository.GetAll().ToArray();
        }

        // GET todos/get/3
        [HttpGet("get/{id}")]
        public ActionResult<Models.Todo> Get(int id)
        {
            return _todosRepository.Get(id);
        }


        // POST todos/update
        //[ValidateAntiForgeryToken]
        [HttpPost("update")]
        public ActionResult<ProcessingResult> Update([FromBody] Models.Todo todo)
        {
            return _todosRepository.Update(todo);
        }

        // POST todos/add
        [HttpPost("add")]
        public ActionResult<ProcessingResult> Add([FromBody] Models.Todo todo)
        {
            return _todosRepository.Add(todo);
        }

        // GET todos/remove/4
        [HttpGet("del/{id}")]
        public ActionResult<ProcessingResult> Delete(int id)
        {
            return _todosRepository.Delete(id);
        }
    }
}
