using Backend.Data;
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
}