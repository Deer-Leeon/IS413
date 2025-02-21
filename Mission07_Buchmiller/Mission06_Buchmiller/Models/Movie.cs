using System.ComponentModel.DataAnnotations;

namespace Mission06_Buchmiller.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")]
        public int Year { get; set; }

        public string? Director { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        public string Rating { get; set; }

        [Required(ErrorMessage = "Edited field is required.")]
        public bool Edited { get; set; }

        public string? LentTo { get; set; } 
        
        [Required(ErrorMessage = "CopiedToPlex field is required.")]
        public bool CopiedToPlex { get; set; }

        [MaxLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
        public string? Notes { get; set; }
    }
}