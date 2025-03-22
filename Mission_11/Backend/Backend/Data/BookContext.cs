using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options) { }
    
    public DbSet<Book> Books => Set<Book>();
}