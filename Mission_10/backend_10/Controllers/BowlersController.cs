using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_10.Data;
using backend_10.Models;

namespace backend_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlersController : ControllerBase
    {
        private readonly BowlingContext _context;

        public BowlersController(BowlingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBowlers()
        {
            return await _context.Bowlers
                .Include(b => b.Team) // Include Team data
                .Where(b => b.Team != null && (b.Team.TeamName == "Marlins" || b.Team.TeamName == "Sharks"))
                .Select(b => new
                {
                    BowlerID = b.BowlerID,
                    Name = $"{b.BowlerFirstName} {b.BowlerMiddleInit} {b.BowlerLastName}".Replace("  ", " "),
                    TeamName = b.Team!.TeamName,
                    Address = b.BowlerAddress,
                    City = b.BowlerCity,
                    State = b.BowlerState,
                    Zip = b.BowlerZip,
                    Phone = b.BowlerPhoneNumber
                })
                .ToListAsync();
        }
    }
}