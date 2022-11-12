using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]
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
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return resources;
    }
    
    [HttpGet]
    [Route("{id}")]
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
    public async Task<IActionResult> FindByUsernameAsync(string username)
    {
        var result = await _userService.FindByUserNameAsync(username);
        if (result == null)
            return BadRequest("User not found");
        var resource = _mapper.Map<User, UserResource>(result);
        return Ok(resource);
    }

    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
    
    [HttpPut("{id}")]
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