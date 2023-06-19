using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Context;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public EventParticipantController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/EventParticipant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventParticipant>>> GetEventParticipants()
        {
          if (_context.EventParticipants == null)
          {
              return NotFound();
          }
            return await _context.EventParticipants.ToListAsync();
        }

        // GET: api/EventParticipant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventParticipant>> GetEventParticipant(Guid id)
        {
          if (_context.EventParticipants == null)
          {
              return NotFound();
          }
            var eventParticipant = await _context.EventParticipants.FindAsync(id);

            if (eventParticipant == null)
            {
                return NotFound();
            }

            return eventParticipant;
        }

        // PUT: api/EventParticipant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventParticipant(Guid id, EventParticipant eventParticipant)
        {
            if (id != eventParticipant.EventId)
            {
                return BadRequest();
            }

            _context.Entry(eventParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EventParticipant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventParticipant>> PostEventParticipant(EventParticipant eventParticipant)
        {
          if (_context.EventParticipants == null)
          {
              return Problem("Entity set 'ES2DbContext.EventParticipants'  is null.");
          }
            _context.EventParticipants.Add(eventParticipant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventParticipantExists(eventParticipant.EventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEventParticipant", new { id = eventParticipant.EventId }, eventParticipant);
        }

        // DELETE: api/EventParticipant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventParticipant(Guid id)
        {
            if (_context.EventParticipants == null)
            {
                return NotFound();
            }
            var eventParticipant = await _context.EventParticipants.FindAsync(id);
            if (eventParticipant == null)
            {
                return NotFound();
            }

            _context.EventParticipants.Remove(eventParticipant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventParticipantExists(Guid id)
        {
            return (_context.EventParticipants?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
