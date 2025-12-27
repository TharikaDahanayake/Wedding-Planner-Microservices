using Microsoft.AspNetCore.Mvc;
using TaskService.Data;
using Microsoft.EntityFrameworkCore;

using TaskService.Models;
using TaskService.Services;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController_D_BIT_23_0030 : ControllerBase
    {
        private readonly TaskDbContext_D_BIT_23_0030 _db;
        private readonly IEventClient_D_BIT_23_0030 _eventClient;
        public TasksController_D_BIT_23_0030(TaskDbContext_D_BIT_23_0030 db, IEventClient_D_BIT_23_0030 eventClient)
        {
            _db = db; _eventClient = eventClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask12345([FromBody] Task_D_BIT_23_0030 t)
        {
            if (!await _eventClient.EventExistsAsync(t.EventId))
                return BadRequest(new { error = "Event does not exist" });

            _db.Task_D_BIT_23_0030.Add(t);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask12345), new { id = t.TaskId }, t);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByEvent12345([FromQuery] int eventId)
        {
            var list = await _db.Task_D_BIT_23_0030.Where(x => x.EventId == eventId).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask12345(int id)
        {
            var t = await _db.Task_D_BIT_23_0030.FindAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask12345(int id, [FromBody] Task_D_BIT_23_0030 t)
        {
            var existing = await _db.Task_D_BIT_23_0030.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Description = t.Description;
            existing.Status = t.Status;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask12345(int id)
        {
            var existing = await _db.Task_D_BIT_23_0030.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Task_D_BIT_23_0030.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
