using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Book
{
    [Column("BookID")] // Match the column name in the database
    public int BookID { get; set; }

    [Column("Title")]
    public string Title { get; set; } = string.Empty;

    [Column("Author")]
    public string Author { get; set; } = string.Empty;

    [Column("Publisher")]
    public string Publisher { get; set; } = string.Empty;

    [Column("ISBN")]
    public string ISBN { get; set; } = string.Empty;

    [Column("Classification")]
    public string Classification { get; set; } = string.Empty;

    [Column("Category")]
    public string Category { get; set; } = string.Empty;

    [Column("PageCount")]
    public int PageCount { get; set; }

    [Column("Price")]
    public decimal Price { get; set; }
}