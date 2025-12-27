using GuestService.Data;
using GuestService.Models;
using GuestService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuestService.Controllers
{
    [ApiController]
    [Route("guests")]
    public class GuestsController_D_BIT_23_0030 : ControllerBase
    {
        private readonly GuestDbContext_D_BIT_23_0030 _db;
        private readonly IEventClient_D_BIT_23_0030 _eventClient;
        public GuestsController_D_BIT_23_0030(GuestDbContext_D_BIT_23_0030 db, IEventClient_D_BIT_23_0030 eventClient)
        {
            _db = db; _eventClient = eventClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddGuest12345([FromBody] Guest_D_BIT_23_0030 g)
        {
            // Validate event exists via Event Service
            if (!await _eventClient.EventExistsAsync(g.EventId))
                return BadRequest(new { error = "Event does not exist" });

            _db.Guest_D_BIT_23_0030.Add(g);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGuest12345), new { id = g.GuestId }, g);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestsByEvent12345([FromQuery] int eventId)
        {
            var list = await _db.Guest_D_BIT_23_0030.Where(x => x.EventId == eventId).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuest12345(int id)
        {
            var g = await _db.Guest_D_BIT_23_0030.FindAsync(id);
            if (g == null) return NotFound();
            return Ok(g);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest12345(int id, [FromBody] Guest_D_BIT_23_0030 g)
        {
            var existing = await _db.Guest_D_BIT_23_0030.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = g.Name;
            existing.Email = g.Email;
            existing.RSVP = g.RSVP;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest12345(int id)
        {
            var existing = await _db.Guest_D_BIT_23_0030.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Guest_D_BIT_23_0030.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
