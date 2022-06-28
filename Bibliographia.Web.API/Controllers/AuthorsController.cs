using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliographia.Web.API.Models.Domain;
using AutoMapper;
using Bibliographia.Web.API.Models.DataTransfer.Author;
using Bibliographia.Web.API.Static;

namespace Bibliographia.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BiblioContext _context;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;


        public AuthorsController(BiblioContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            _logger.LogInformation($"making request call to {nameof(GetAuthors)}");
            try
            {

                if (_context.Authors == null)
                {
                    _logger.LogWarning($"Records not found for GET operation in {nameof(GetAuthors)}");
                    return NotFound();
                }
                IEnumerable<AuthorReadOnlyDto>? authors = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(await _context.Authors.ToListAsync());
                return Ok(authors);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                Author? author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(GetAuthor)} - Id {id}");
                    return NotFound();
                }

                AuthorReadOnlyDto? authorReadOnlyDto = _mapper.Map<AuthorReadOnlyDto>(author);

                return authorReadOnlyDto;
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetAuthor)}");
                return BadRequest();
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorUpdateDto)
        {
            try
            {
                if (id != authorUpdateDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                Author? author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                _ = _mapper.Map(authorUpdateDto, author);

                _context.Entry(author).State = EntityState.Modified;
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (!await AuthorExistsAsync(id))
                {
                    _logger.LogError(exep, $"{nameof(Author)} record not found in {nameof(PutAuthor)} for ID - {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorCreateDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorCreateDto);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PostAuthor)}");
                    return BadRequest();
                }
                if (_context.Authors == null)
                {
                    return Problem("Entity set 'BiblioContext.Authors' is null.");
                }
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing POST operation in {nameof(PostAuthor)}");

                return BadRequest();
            }
        }

       

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AuthorExistsAsync(int id)
        {
            return await _context.Authors?.AnyAsync(e => e.Id == id);
        }

    }
}