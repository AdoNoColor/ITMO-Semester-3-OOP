using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.DataAccessLayer.Reports;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;
using Reports.Models;
using Task = Reports.DataAccessLayer.Task.Task;

namespace Reports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
       private readonly TodoContext _context;

        public ReportsController(TodoContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Id = todoItemDTO.Id;
            todoItem.Task = todoItemDTO.Task;
            todoItem.Report = todoItemDTO.Report;
            todoItem.Worker = todoItemDTO.Worker;
            todoItem.WorkersStorage = todoItemDTO.WorkersStorage;
            todoItem.TasksStorage = todoItemDTO.TasksStorage;
            todoItem.ReportsStorage = todoItemDTO.ReportsStorage;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                Task = todoItemDTO.Task,
                Report = todoItemDTO.Report,
                Worker = todoItemDTO.Worker,
                WorkersStorage = todoItemDTO.WorkersStorage,
                TasksStorage = todoItemDTO.TasksStorage,
                ReportsStorage = todoItemDTO.ReportsStorage,
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Task = todoItem.Task,
                Report = todoItem.Report,
                Worker = todoItem.Worker,
                WorkersStorage = todoItem.WorkersStorage,
                TasksStorage = todoItem.TasksStorage,
                ReportsStorage = todoItem.ReportsStorage,
            };
    }
}
