using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliographia.Web.API.Models.Domain;
using Bibliographia.Web.API.Models.DataTransfer.Author;
using AutoMapper;
using Bibliographia.Web.API.Models.DataTransfer.Publisher;

namespace Bibliographia.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BiblioContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public PublishersController(BiblioContext context, ILogger<BooksController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
          if (_context.Publishers == null)
          {
              return NotFound();
          }
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherReadOnlyDto>> GetPublisher(int id)
        {
            try
            {
                Publisher? publisher = await _context.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    _logger.LogWarning($"{nameof(Publisher)} record not found in {nameof(GetPublisher)} - Id {id}");
                    return NotFound();
                }

                PublisherReadOnlyDto? publisherReadOnlyDto = _mapper.Map<PublisherReadOnlyDto>(publisher);

                return publisherReadOnlyDto;
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetPublisher)}");
                return BadRequest();
            }
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherUpdateDto publisherUpdateDto)
        {
            try
            {
                if (id != publisherUpdateDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid in {nameof(PutPublisher)} - Id {id}");
                    return BadRequest();
                }

                Publisher? publisher = await _context.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    _logger.LogWarning($"{nameof(Publisher)} record not found in {nameof(PutPublisher)} - Id {id}");
                    return BadRequest();
                }

                _ = _mapper.Map(publisherUpdateDto, publisher);

                _context.Entry(publisher).State = EntityState.Modified;
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (!await PublisherExistsAsync(id))
                {
                    _logger.LogError(exep, $"{nameof(Publisher)} record not found in {nameof(PutPublisher)} for ID - {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(PublisherCreateDto publisherCreateDto)
        {
            try
            {
                var publisher = _mapper.Map<Publisher>(publisherCreateDto);
                if (publisher == null)
                {
                    _logger.LogWarning($"{nameof(Publisher)} record not found in {nameof(PostPublisher)}");
                    return BadRequest();
                }
                if (_context.Publishers == null)
                {
                    return Problem("Entity set 'BiblioContext.Publishers' is null.");
                }
                _context.Publishers.Add(publisher);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing POST operation in {nameof(PostPublisher)}");
                return BadRequest();
            }
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PublisherExistsAsync(int id)
        {
            return (await _context.Publishers?.AnyAsync(e => e.Id == id));
        }
    }
}
