﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.globals;
using Movies.Business.seasons;
using Movies.Interface;
using Movies.Models;
using System.Collections.Generic;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class SeasonController : Controller
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IMapper _mapper;

    public SeasonController(ISeasonRepository seasonRepository,
                          IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _mapper = mapper;
    }

    [HttpGet("Seasons")]
    [ProducesResponseType(typeof(IEnumerable<SeasonDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetSeasons(Guid movieId, int? seasonNumber)
    {
        IEnumerable<SeasonDTO> seasons = null;

        if (seasonNumber != null)
        {
            seasons = _mapper.Map<IEnumerable<SeasonDTO>>(_seasonRepository.GetSeasonsByMovieAndNumber(movieId, seasonNumber));
        } else if (seasonNumber == null) {
            seasons = _mapper.Map<IEnumerable<SeasonDTO>>(_seasonRepository.GetSeasonsByMovie(movieId));
        } else
        {
            BadRequest("Error!");
        }

        
        return Ok(seasons);
    }

    [HttpPost("Seasons")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSeason([FromBody] NewSeason newSeason)
    {
        var response = await _seasonRepository.CreateSeason(newSeason);
        if (response.Status == HttpStatusCode.Created)
        {
            return Ok(response);
        }
        return BadRequest(response.Message);
    }
}
