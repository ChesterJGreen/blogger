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
//FIXME either fix this in the conroller or service
    //   if (original.CreatorId != userId)
    //   {
    //       throw new Exception("You do Not have permission to edit this comment");
    //   }
      updatedComment.Body = updatedComment.Body != null ? updatedComment.Body : original.Body;
      return _repo.Update(updatedComment);
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
  }
}