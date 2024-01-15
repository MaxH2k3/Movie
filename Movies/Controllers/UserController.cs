
using Microsoft.AspNetCore.Mvc;
using Movies.Business.anothers;
using Movies.Business.globals;
using Movies.Business.users;
using Movies.Interface;
using Movies.Security;
using System.Net;

namespace Movies.Controllers;

[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly JWTGenerator _tokenGenerator;

    public UserController(IUserRepository userRepository, JWTGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = jwtGenerator;
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

}
