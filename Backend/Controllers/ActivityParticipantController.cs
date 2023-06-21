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
    public class ActivityParticipantController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public ActivityParticipantController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityParticipant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityParticipant>>> GetEventParticipants()
        {
            if (_context.ActivityParticipants == null)
            {
                return NotFound();
            }

            return await _context.ActivityParticipants.Include(ap => ap.Participant).Include(ap => ap.Activity)
                .ToListAsync();
        }

        // GET: api/ActivityParticipant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityParticipant>> GetActivityParticipant(Guid id)
        {
            if (_context.ActivityParticipants == null)
            {
                return NotFound();
            }

            var activityParticipant = await _context.ActivityParticipants.FindAsync(id);

            if (activityParticipant == null)
            {
                return NotFound();
            }

            return activityParticipant;
        }

        // PUT: api/ActivityParticipant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityParticipant(Guid id, ActivityParticipant activityParticipant)
        {
            if (id != activityParticipant.ActivityId)
            {
                return BadRequest();
            }

            _context.Entry(activityParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityParticipantExists(id))
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

        // POST: api/ActivityParticipant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityParticipant>> PostActivityParticipant(
            ActivityParticipant activityParticipant)
        {
            if (_context.ActivityParticipants == null)
            {
                return Problem("Entity set 'ES2DbContext.EventParticipants'  is null.");
            }

            _context.ActivityParticipants.Add(activityParticipant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActivityParticipantExists(activityParticipant.ActivityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActivityParticipant", new { id = activityParticipant.ActivityId },
                activityParticipant);
        }

        // DELETE: api/ActivityParticipant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityParticipant(Guid id)
        {
            if (_context.ActivityParticipants == null)
            {
                return NotFound();
            }

            var activityParticipant = await _context.ActivityParticipants.FindAsync(id);
            if (activityParticipant == null)
            {
                return NotFound();
            }

            _context.ActivityParticipants.Remove(activityParticipant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityParticipantExists(Guid id)
        {
            return (_context.ActivityParticipants?.Any(e => e.ActivityId == id)).GetValueOrDefault();
        }
    }
}