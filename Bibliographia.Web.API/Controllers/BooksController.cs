using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliographia.Web.API.Models.Domain;
using AutoMapper.QueryableExtensions;
using Bibliographia.Web.API.Models.DataTransfer.Book;
using Bibliographia.Web.API.Static;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Bibliographia.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BiblioContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BiblioContext context, ILogger<BooksController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                _logger.LogInformation($"making request call to {nameof(GetBooks)}");
                List<BookReadOnlyDto>? books = await _context.Books
                    .Include(y => y.Author)
                    .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }       

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    _logger.LogWarning($"No {nameof(Book)} record was returned for request for Id: {id} in {nameof(GetBook)}");
                    return NotFound();
                }
                BookDetailsDto? myBook = await _context.Books
                    .Include(y => y.Author)
                    .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (myBook == null)
                {
                    _logger.LogWarning($"No {nameof(Book)} record was returned for request for Id: {id} in {nameof(GetBook)}");
                    return NotFound();
                }

                _logger.LogInformation($"Request for {nameof(Book)} Id: {id} with GET operation call to {nameof(GetBook)}");
                return Ok(myBook);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation for Id: {id} in {nameof(GetBook)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }
        #region administrator only
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookUpdateDto)
        {
            if (id != bookUpdateDto.Id)
            {
                _logger.LogWarning($"Update ID invalid in {nameof(PutBook)} - Id {id}");
                return BadRequest();
            }
            Book? book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"{nameof(Book)} record not found in {nameof(PutBook)} - Id {id}");
                return BadRequest();
            }
            _ = _mapper.Map(bookUpdateDto, book);
            book.Updated = DateTime.UtcNow;
            _context.Entry(book).State = EntityState.Modified;
            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (!BookExists(id))
                {
                    _logger.LogError(exep, $"{nameof(Book)} record not found in {nameof(PutBook)} for ID - {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookCreateDto)
        {
            try
            {
                Book? book = _mapper.Map<Book>(bookCreateDto);

                if (book == null)
                {
                    _logger.LogWarning($"{nameof(Book)} record not found in {nameof(PostBook)}");
                    return BadRequest();
                }

                _ = await _context.Books.AddAsync(book);
                _ = await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error when creating {nameof(Book)} record in {nameof(PutBook)}");
                return BadRequest();
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    _logger.LogWarning($"{nameof(Book)} records were not found");
                    return NotFound();
                }

                Book? book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"{nameof(Book)} record for Id: {id} was not found");
                    return NotFound();
                }

                _ = _context.Books.Remove(book);
                _ = await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error in DELETE operation for Id: {id} in {nameof(DeleteBook)}");
                return BadRequest();
            }
        }

        #endregion administrator only

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
