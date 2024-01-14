﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Business.movies;

public class NewMovie
{
    public Guid? MovieId { get; set; }
    public double? Mark { get; set; }
    public int? Time { get; set; }
    public int? Viewer { get; set; }
    [MinLength(20)]
    public string? Description { get; set; }
    [MaxLength(100)]
    [MinLength(2)]
    [Required]
    public string? EnglishName { get; set; }
    [MaxLength(100)]
    [MinLength(2)]
    [Required]
    public string? VietnamName { get; set; }
    public IFormFile? Thumbnail { get; set; }
    [Required]
    public string? Trailer { get; set; }
    [Required]
    public string? NationId { get; set; }
    [Required]
    public int? FeatureId { get; set; }
    [Required]
    public DateTime? ProducedDate { get; set; }
    public IEnumerable<int> Categories { get; set; }
}
