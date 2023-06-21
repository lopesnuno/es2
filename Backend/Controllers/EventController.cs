using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public EventController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            if (_context.Events == null)
            {
                return NotFound();
            }

            return await _context.Events.Include(e => e.Organizer)
                .Include(e => e.Activities)
                .Include(e => e.Tickets)
                .ToListAsync();
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var sEvent = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Activities)
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(c => c.Id == id);

            return Ok(sEvent);
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            // TODO: change this to get and user current auth user
            var organizer = new Organizer
            {
                Id = new Guid(),
                Email = "a@gmail.com",
                Name = "a",
                // TODO: add bcript
                Password = "a",
                Username = "a",
                PhoneNumber = 123456789,
                EventsCreated = new List<Event>()
            };

            @event.Organizer = organizer;
            @event.OrganizerId = organizer.Id;
            @event.Tickets = new List<EventTicket>();
            @event.Activities = new List<Activity>();

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return Ok(await _context.Events.Include(e => e.Tickets)
                .Include(e => e.Activities).ToListAsync());
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}