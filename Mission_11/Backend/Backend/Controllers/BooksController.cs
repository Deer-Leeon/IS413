using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")] // This sets the route to /api/books
public class BooksController : ControllerBase
{
    private readonly BookContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(BookContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string sortBy = "title")
    {
        try
        {
            _logger.LogInformation("Fetching books with page={Page}, pageSize={PageSize}, sortBy={SortBy}", page, pageSize, sortBy);

            var query = _context.Books.AsQueryable();

            // Sorting
            switch (sortBy.ToLower())
            {
                case "author":
                    query = query.OrderBy(b => b.Author);
                    break;
                case "price":
                    query = query.OrderBy(b => b.Price);
                    break;
                case "pages":
                    query = query.OrderBy(b => b.PageCount);
                    break;
                case "category":
                    query = query.OrderBy(b => b.Category);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

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
            _logger.LogError(ex, "An error occurred while fetching books");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}