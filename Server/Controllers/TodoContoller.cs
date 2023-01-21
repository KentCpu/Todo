using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;


namespace Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo")]
    public class TodoContoller : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TodoContoller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
        [HttpPost]
        public async Task<ActionResult> createTodo([FromBody] Todo todo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (TokenController.validateToken(token) != true)
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                    Todo newTodo = new Todo(
                        todo.id,
                        todo.title,
                        todo.description,
                        todo.dateCompletion,
                        todo.userId,
                        todo.isCompleted
                    );

                    await _dataContext.Todos.AddAsync(newTodo);
                    await _dataContext.SaveChangesAsync();
                }
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при создании дела", error);
            }

            return Ok();
        }

        
        [HttpGet("{userId}/{dateCompletion}")]
        public IActionResult getByDateTodos([FromRoute] Guid userId, [FromRoute] string dateCompletion)
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (TokenController.validateToken(token) != true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var todos = _dataContext.Todos.Where(todo => todo.dateCompletion == dateCompletion 
                && todo.userId == userId);
                if (todos == null)
                {
                    return NotFound();
                }

                return Ok(todos);
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при получении списка задач по названию", error);
            }
        }

        [HttpGet("{title}/{userId:Guid}")]
        public IActionResult getByTitleTodos([FromRoute] Guid userId, [FromRoute] string title)
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (TokenController.validateToken(token) != true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var todos = _dataContext.Todos.Where(todo => todo.userId == userId && todo.title == title);
                if (todos == null)
                {
                    return NotFound();
                }

                return Ok(todos);
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при получении списка задач по названию", error);
            }
        }

        [HttpGet("completed/{userId:Guid}/{dateCompletion}")]
        public IActionResult getByCompletedTodos([FromRoute] Guid userId, [FromRoute] string dateCompletion)
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (TokenController.validateToken(token) != true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var todos = _dataContext.Todos.Where(todo => todo.userId == userId
                    && todo.dateCompletion == dateCompletion
                    && todo.isCompleted == true) ;
                if (todos == null)
                {
                    return NotFound();
                }

                return Ok(todos);
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при получении списка выполненных задач", error);
            }
        }

        [HttpGet("incompleted/{userId:Guid}/{dateCompletion}")]
        public IActionResult getByIncompletedTodos([FromRoute] Guid userId, [FromRoute] string dateCompletion)
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (TokenController.validateToken(token) != true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var todos = _dataContext.Todos.Where(todo => todo.userId == userId
                    && todo.dateCompletion == dateCompletion
                    && todo.isCompleted == false);
                if (todos == null)
                {
                    return NotFound();
                }

                return Ok(todos);
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при получении списка невыполненных задач", error);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> deleteTodo([FromRoute] Guid id)
        {
            try
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (TokenController.validateToken(token) != true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var todo = await _dataContext.Todos.FindAsync(id);
                if (todo == null)
                {
                    return NotFound();
                }

                _dataContext.Todos.Remove(todo);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception error)
            {
                throw new Exception("Ошибка при удалении дела", error);
            }

            return Ok();
        }



        [HttpPatch]
        public async Task<ActionResult> EditTodo([FromBody] Todo newTodo)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (TokenController.validateToken(token) != true)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            var todo = await _dataContext.Todos.FindAsync(newTodo.id);
            if (todo != null)
            {
                todo.title = newTodo.title;
                todo.description = newTodo.description;
                todo.dateCompletion = newTodo.dateCompletion;
                todo.isCompleted = newTodo.isCompleted;
                await _dataContext.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}