using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;
//Method Controller for Users
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "List of all the users in the database",
        OperationId = "GetAllUsers"
        )]
    [SwaggerResponse(200, "The operation was successful", typeof(IEnumerable<CommentResource>))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(404, "Users were not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user by id",
        Description = "Get a user by id",
        OperationId = "GetUserById"
        )]
    
    [SwaggerResponse(200, "The operation was successful", typeof(UserResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(404, "User was not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> FindByIdAsync(int id)
    {
        var result = await _userService.FindByIdAsync(id);
        if (result == null)
            return BadRequest("User not found");
        var resource = _mapper.Map<User, UserResource>(result);
        return Ok(resource);
    }
    
 
    [HttpGet]
    [Route("{email}/{password}")]
    [SwaggerOperation(
        Summary = "Get user by email and password",
        Description = "Get a specific user by email and password",
        OperationId = "FindByEmailAndPasswordAsync"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(CommentResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(404, "User was not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> FindByEmailAndPasswordAsync(string email, string password)
    {
        var result = await _userService.FindByEmailAndPasswordAsync(email, password);
        if (result == null)
            return BadRequest("Email or password is incorrect");
        var resource = _mapper.Map<User, UserResource>(result);
        return Ok(resource);
    }
    
    [HttpGet]
    [Route("username={username}")]
    [SwaggerOperation(
        Summary = "Get user by username",
        Description = "Get a specific user by username",
        OperationId = "FindByUsernameAsync"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(CommentResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(404, "User was not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> FindByUsernameAsync(string username)
    {
        var result = await _userService.FindByUserNameAsync(username);
        if (result == null)
            return BadRequest("User not found");
        var resource = _mapper.Map<User, UserResource>(result);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new user",
        Description = "Create a new user",
        OperationId = "CreateUser"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(UserResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.SaveAsync(user);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a user",
        Description = "Delete a user",
        OperationId = "DeleteUser"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(UserResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a user",
        Description = "Update a user",
        OperationId = "UpdateUser"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(UserResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.UpdateAsync(id, user);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
    
    [HttpPatch("{id}")]
    [SwaggerOperation(
        Summary = "Update specific user attribute",
        Description = "Update specific user attribute",
        OperationId = "UpdateUser"
    )]
    [SwaggerResponse(200, "The operation was successful", typeof(UserResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<User> resource)
    {
        var user = await _userService.FindByIdAsync(id);
        
        if (user == null)
            return BadRequest("User not found");
        
        resource.ApplyTo(user);

        var result = await _userService.UpdateAsync(id, user);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(user);
    }
    
}