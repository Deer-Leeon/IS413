using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(BookContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string sortBy = "title",
        [FromQuery] string? searchTitle = null,
        [FromQuery] string? category = null)
    {
        try
        {
            _logger.LogInformation(
                "Fetching books with page={Page}, pageSize={PageSize}, sortBy={SortBy}, searchTitle={SearchTitle}, category={Category}",
                page, pageSize, sortBy, searchTitle, category);

            var query = _context.Books.AsQueryable();

            // Case-insensitive title search
            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(b => 
                    EF.Functions.Like(b.Title, $"%{searchTitle}%"));
            }

            // Category filter
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(b => b.Category == category);
            }

            // Sorting
            query = sortBy.ToLower() switch
            {
                "author" => query.OrderBy(b => b.Author),
                "price" => query.OrderBy(b => b.Price),
                "pages" => query.OrderBy(b => b.PageCount),
                "category" => query.OrderBy(b => b.Category),
                _ => query.OrderBy(b => b.Title)
            };

            var totalCount = await query.CountAsync();
            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            _logger.LogInformation("Successfully fetched {Count} books", results.Count);

            return Ok(new
            {
                TotalCount = totalCount,
                Results = results,
                Page = page,
                PageSize = pageSize
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching books");
            return StatusCode(500, new { 
                Error = "An error occurred while fetching books",
                Details = ex.Message
            });
        }
    }

    // GET: api/books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching book with ID {BookId}", id);
            return StatusCode(500, "Error fetching book");
        }
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book");
            return StatusCode(500, "Error creating book");
        }
    }

    // PUT: api/books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
    {
        try
        {
            if (id != book.BookID)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book with ID {BookId}", id);
            return StatusCode(500, "Error updating book");
        }
    }

    // DELETE: api/books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book with ID {BookId}", id);
            return StatusCode(500, "Error deleting book");
        }
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookID == id);
    }
}