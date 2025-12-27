using EventService.Data;
using EventService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventService.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController_D_BIT_23_0030 : ControllerBase
    {
        private readonly EventDbContext_D_BIT_23_0030 _db;
        public EventsController_D_BIT_23_0030(EventDbContext_D_BIT_23_0030 db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> CreateEvent_D_BIT_23_0030([FromBody] Event_D_BIT_23_0030 evt)
        {
            _db.Events.Add(evt);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent_D_BIT_23_0030), new { id = evt.EventId }, evt);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent_D_BIT_23_0030(int id)
        {
            var e = await _db.Events.FindAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent_D_BIT_23_0030(int id, [FromBody] Event_D_BIT_23_0030 evt)
        {
            var existing = await _db.Events.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = evt.Title;
            existing.Date = evt.Date;
            existing.Venue = evt.Venue;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent_D_BIT_23_0030(int id)
        {
            var existing = await _db.Events.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Events.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
