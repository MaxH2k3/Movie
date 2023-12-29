﻿namespace Movies.Business.seasons;

public class SeasonDTO
{
    public Guid? SeasonId { get; set; }
    public int? SeasonNumber { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<EpisodeDTO>? Episodes { get; set; }
}