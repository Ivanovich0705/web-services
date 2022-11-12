using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Domain.Services;
using TakeMeHome.API.TakeMeHome.Domain.Services.Communication;

namespace TakeMeHome.API.TakeMeHome.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }

    public async Task<IEnumerable<Comment>> ListByUserIdAsync(int userId)
    {
        return await _commentRepository.FindByUserId(userId);
    }

    public async Task<CommentResponse> SaveAsync(Comment comment)
    {
        {
            try
            {
                await _commentRepository.AddAsync(comment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(comment);
            }
            catch (Exception e)
            {
                return new CommentResponse($"An error occurred while saving the comment: {e.Message}");
            }
        }
    }

    public async Task<CommentResponse> UpdateAsync(int id, Comment comment)
    {
        var existingComment = await _commentRepository.FindByIdAsync(id);

        if (existingComment == null)
        {
            return new CommentResponse("Comment not found.");
        }
        
        existingComment.Content = comment.Content;
        existingComment.Stars= comment.Stars;
        
        try
        {
            _commentRepository.Update(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while updating the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> DeleteAsync(int id)
    {
        var existingComment = await _commentRepository.FindByIdAsync(id);
        
        if(existingComment == null)
            return new CommentResponse("Order not found.");
        try
        {
            _commentRepository.Remove(existingComment);
            await _unitOfWork.CompleteAsync();
            
            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while deleting the comment: {e.Message}");
        }
    }
}