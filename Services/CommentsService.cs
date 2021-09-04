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
  }
}