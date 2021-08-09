namespace TodoList.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TodoList.WebApi.Models;

    [Authorize]
    [Route("api/[controller]")]
    [RequiredScope(scopeRequiredByAPI)]
    public class TodoListController : ControllerBase
    {
        const string scopeRequiredByAPI = "access_as_user";
        private const string userIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        // In-memory TodoList
        private static readonly Dictionary<int, Todo> TodoStore = new Dictionary<int, Todo>();
        private static int idCount = 0;

        private readonly IHttpContextAccessor _contextAccessor;

        public TodoListController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Todo> Get()
        {   
            string owner = User.Identity.Name;
            return TodoStore.Values.Where(x => x.Owner == User.Claims.First(c => c.Type == userIdentifier).Value);
        }

        // GET: api/values
        [HttpGet("{id}", Name = "Get")]
        public Todo Get(int id)
        {
            return TodoStore.Values.FirstOrDefault(t => t.Id == id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TodoStore.Remove(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Todo todo)
        {
            Todo todonew = new Todo() { Id = ++idCount, Owner = User.Claims.First(c => c.Type == userIdentifier).Value, Title = todo.Title, Description = todo.Description, IsChecked = todo.IsChecked };
            TodoStore.Add(idCount, todonew);

            return Ok(todo);
        }

        // PATCH api/values
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (TodoStore.Values.FirstOrDefault(x => x.Id == id) == null)
            {
                return NotFound();
            }
                        
            TodoStore.Remove(id);

            todo.Owner = User.Claims
                .First(c => c.Type == userIdentifier)
                .Value;

            TodoStore.Add(id, todo);

            return Ok(todo);
        }
    }
}