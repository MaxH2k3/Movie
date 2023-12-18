﻿

namespace Movies.Business;

public class SeasonDTO
{
    public int? SeasonId { get; set; }
    public int? SeasonNumber { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<EpisodeDTO>? Episodes { get; set; }
}
