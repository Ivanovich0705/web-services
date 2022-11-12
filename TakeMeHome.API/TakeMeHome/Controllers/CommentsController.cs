using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;

[Route("/api/v1/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
        var result = await _commentService.SaveAsync(comment);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpGet]
    [Route("{user_id}")]
    public async Task<IEnumerable<CommentResource>> GetByUserId(int user_id)
    {
        var comments = await _commentService.ListByUserIdAsync(user_id);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpDelete]
    [Route("{comment_id}")]
    public async Task<IActionResult> DeleteAsync(int comment_id)
    {
        var result = await _commentService.DeleteAsync(comment_id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpPut]
    [Route("{comment_id}")]
    public async Task<IActionResult> PutAsync(int comment_id, [FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
        var result = await _commentService.UpdateAsync(comment_id, comment);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
}