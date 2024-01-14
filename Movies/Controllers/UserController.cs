using Google.Apis.Auth.OAuth2.Responses;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Movies.Business.globals;
using Movies.Business.users;
using Movies.Interface;
using Movies.Models;
using Movies.Repository;
using Movies.Security;
using Movies.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Movies.Controllers;

[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly JWTGenerator _tokenGenerator;
    private readonly IMailRepository _mailService;

    public UserController(IUserRepository userRepository, JWTGenerator jwtGenerator, IMailRepository mailService)
    {
        _userRepository = userRepository;
        _tokenGenerator = jwtGenerator;
        _mailService = mailService;
    }

    [Route("Authenticate")]
    [HttpPost]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseDTO), (int) HttpStatusCode.BadRequest)]
    public IActionResult Authenticate([FromBody] UserDTO userDTO)
    {
        var userResponse = _userRepository.Login(userDTO);
        if (userResponse.Status != HttpStatusCode.OK)
        {
            return BadRequest(userResponse);
        }
        string? token = _tokenGenerator.GenerateToken(userDTO);

        //tokenResponse.JWTToken = finaltoken;
        //tokenResponse.RefreshToken = tokenGenerator.GenerateToken(user.username);

        return Ok(token);
    }

    [HttpGet]
    [Route("User")]
    public IActionResult GetUser()
    {
        var user = _userRepository.GetUsers();
        return Ok(user);
    }

    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseDTO), (int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        var response = await _userRepository.Register(registerUser);
        if (response.Status != HttpStatusCode.Created)
        {
            return BadRequest(response);
        }
        return Created(response.Message, response.Data);
    }

    /// <summary>
    /// Verify account by token by email
    /// </summary>
    /// <param name="token">token of user</param>
    /// <param name="userId">id of user</param>
    /// <returns></returns>
    [HttpGet]
    [Route("Verify/{userId}/{token}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseDTO), (int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> VerifyAccount(string? token, Guid userId)
    {
        var response = await _userRepository.VerifyAccount(token, (Guid) userId);
        if (response.Status != HttpStatusCode.OK)
        {
            return BadRequest(response);
        }

        try
        {
            var client = new HttpClient();
            var dispatch = await client.GetAsync("https://movie-nextjs-five.vercel.app/");
            dispatch.EnsureSuccessStatusCode();
        } catch (HttpRequestException) {
            return StatusCode(500, "Failed to access website");
        }
        
        return Ok(response.Message);
    }

    /// <summary>
    /// Resend token to user if token expired
    /// </summary>
    /// <param name="userId">id of user</param>
    /// <returns></returns>
    [HttpPost]
    [Route("Resend/{userId}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseDTO), (int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ResendToken(Guid? userId)
    {
        var response = await _userRepository.ResendToken((Guid) userId);
        if (response.Status != HttpStatusCode.OK)
        {
            return BadRequest(response);
        }
        return Ok(response.Message);
    }

    [HttpGet("Mail")]
    public async Task GetMail()
    {
        
    }

}
