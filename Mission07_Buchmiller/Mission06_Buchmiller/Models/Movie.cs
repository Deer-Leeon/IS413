using System.ComponentModel.DataAnnotations;

namespace Mission06_Buchmiller.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        
        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }
        
        [Required]
        public string? Director { get; set; }

        [Required]
        public string Rating { get; set; }

        public bool Edited { get; set; } // Now non-nullable

        public string? LentTo { get; set; } // Nullable is fine
        
        [Required]
        public bool? CopiedToPlex { get; set; }

        [MaxLength(25)]
        public string? Notes { get; set; } // Nullable is fine
    }
}