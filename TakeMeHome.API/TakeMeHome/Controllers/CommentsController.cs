using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TakeMeHome.API.Shared.Extensions;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Resources;

namespace TakeMeHome.API.TakeMeHome.Controllers;
//Comment Method Controller
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
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
    [SwaggerOperation(
        Summary = "Get all comments",
        Description = "List of all the comments in the database",
        OperationId = "GetAllComments"
        /*Tags = new[] { "comments" }*/)]
    [SwaggerResponse(200, "The operation was successful", typeof(IEnumerable<CommentResource>))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(404, "Comments were not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Save a comment",
        Description = "Save a comment in the database",
        OperationId = "PostComment"
        )]
    [SwaggerResponse(200, "The operation was successful", typeof(CommentResource))]
    [SwaggerResponse(400, "The data is not valid")]
    [SwaggerResponse(500, "An unexpected error occurred")]
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
    [SwaggerOperation(
        Summary = "Get comments by user id",
        Description = "Get a list of comments by user id",
        OperationId = "GetCommentsByUserId"
        )]
    [SwaggerResponse(200, "The operation was successful", typeof(IEnumerable<CommentResource>))]
    [SwaggerResponse(404, "The comments of the user were not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
    
    public async Task<IEnumerable<CommentResource>> GetByUserId(int user_id)
    {
        var comments = await _commentService.ListByUserIdAsync(user_id);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpDelete]
    [Route("{comment_id}")]
    [SwaggerOperation(
        Summary = "Delete a comment",
        Description = "Delete a comment in the database",
        OperationId = "DeleteComment"
        )]
    [SwaggerResponse(200, "Comment deleted successfully", typeof(CommentResource))]
    [SwaggerResponse(400, "The entry data is not valid")]
    [SwaggerResponse(404, "The comment was not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
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
    [SwaggerOperation(
        Summary = "Update a comment",
        Description = "Update a comment in the database",
        OperationId = "UpdateComment"
        )]
    [SwaggerResponse(200, "Comment updated successfully", typeof(CommentResource))]
    [SwaggerResponse(400, "The entry data is not valid")]
    [SwaggerResponse(404, "The comment was not found")]
    [SwaggerResponse(500, "An unexpected error occurred")]
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