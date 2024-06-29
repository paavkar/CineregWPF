﻿using System.ComponentModel.DataAnnotations;

namespace CineregWPF.Models
{
    public class Movie
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ReleaseYear { get; set; } = DateTime.Now.Year;
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Director { get; set; }
        public string? UserId { get; set; }
        [Required]
        public int WatchedYear { get; set; } = DateTime.Now.Year;
        [Required]
        public string ViewingForm { get; set; }
        [Required]
        public string Review { get; set; }
    }
}
