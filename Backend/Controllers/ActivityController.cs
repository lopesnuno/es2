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
    public class ActivityController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public ActivityController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            if (_context.Activities == null)
            {
                return NotFound();
            }

            return await _context.Activities.Include(a => a.Participants).Include(a => a.Event).ToListAsync();
        }
        
        
        [HttpGet("event/{id}")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetEventActivities(Guid id)
        {
            if (_context.Activities == null)
            {
                return NotFound();
            }

            return await _context.Activities.Where(a => a.EventId == id).Include(a => a.Participants)
                .ToListAsync();
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            if (_context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.Include(a => a.Participants).FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // PUT: api/Activity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(Guid id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            if (_context.Activities == null)
            {
                return Problem("Entity set 'ES2DbContext.Activities'  is null.");
            }

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction("GetActivity", new { id = activity.Id }, activity));
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            if (_context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(Guid id)
        {
            return (_context.Activities?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("add-participant/{id}")]
        public async Task<ActionResult<Activity>> BookParticipant(Guid id, User participant)
        {
            if (_context.Activities == null)
            {
                return Problem("Entity set 'ES2DbContext.Activities'  is null.");
            }

            Activity? activity = await _context.Activities.FindAsync(id);

            if (activity is null)
                return BadRequest("Activity does not exist");

            activity.Participants.Add(participant);
            
            _context.Activities.Update(activity);
            
            await _context.SaveChangesAsync();

            return Ok("Success");
        }
    }
}