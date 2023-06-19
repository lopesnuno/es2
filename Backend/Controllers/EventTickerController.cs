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
    public class EventTickerController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public EventTickerController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/EventTicker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTicket>>> GetTicketTypes()
        {
          if (_context.TicketTypes == null)
          {
              return NotFound();
          }
            return await _context.TicketTypes.ToListAsync();
        }

        // GET: api/EventTicker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTicket>> GetEventTicket(Guid id)
        {
          if (_context.TicketTypes == null)
          {
              return NotFound();
          }
            var eventTicket = await _context.TicketTypes.FindAsync(id);

            if (eventTicket == null)
            {
                return NotFound();
            }

            return eventTicket;
        }

        // PUT: api/EventTicker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventTicket(Guid id, EventTicket eventTicket)
        {
            if (id != eventTicket.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTicketExists(id))
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

        // POST: api/EventTicker
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventTicket>> PostEventTicket(EventTicket eventTicket)
        {
          if (_context.TicketTypes == null)
          {
              return Problem("Entity set 'ES2DbContext.TicketTypes'  is null.");
          }
            _context.TicketTypes.Add(eventTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventTicket", new { id = eventTicket.Id }, eventTicket);
        }

        // DELETE: api/EventTicker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventTicket(Guid id)
        {
            if (_context.TicketTypes == null)
            {
                return NotFound();
            }
            var eventTicket = await _context.TicketTypes.FindAsync(id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            _context.TicketTypes.Remove(eventTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventTicketExists(Guid id)
        {
            return (_context.TicketTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
