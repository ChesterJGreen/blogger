using System;
using System.Collections.Generic;
using blogger.Models;
using blogger.Repositories;

namespace blogger.Services
{
  public class CommentsService
  {
      private readonly CommentsRepository _repo;
      
    public CommentsService(CommentsRepository repo)
    {
      _repo = repo;
    }

    internal List<Comment> Get()
    {
        return _repo.Get();
    }

    internal Comment Get(int id)
    {
      Comment comment = _repo.Get(id);
      if ( comment == null)
      {
          throw new Exception("Invalid Id");
      }
      return comment;
    }

    internal Comment Create(Comment newComment)
    {
      return _repo.Create(newComment);
    }
    internal Comment Edit(Comment updatedComment)
    {
      Comment original = Get(updatedComment.Id);
      return Edit(original, updatedComment);
    }
    internal Comment Edit(Comment original, Comment updatedComment)
    {
      updatedComment.Body = updatedComment.Body != null ? updatedComment.Body : original.Body;
      return _repo.Edit(updatedComment);
    }

    internal void Delete(int commentId, string userId)
    {
      Comment commentToDelete = Get(commentId);
      if (commentToDelete.CreatorId != userId)
      {
          throw new Exception("You do Not have permission to delete this event");
      }
      _repo.Delete(commentId);
    }

    internal List<Comment> GetAllByProfileId(string id)
    
      {
      List<Comment> comments = _repo.GetByProfileId(id);
      if(comments == null)
      {
        throw new Exception("No comments in this Profile");
      }
      return comments;
    
    }

    internal List<Comment> GetCommentsByBlogId(int id)
    {
    List<Comment> comments = _repo.GetCommentsByBlogId(id);
      if ( comments == null)
      {
          throw new Exception("Invalid Id");
      }
      return comments;
    }
  }
}