using System;
using System.Collections.Generic;
using blogger.Models;
using blogger.Repositories;

namespace blogger.Services
{
  public class BlogsService
  {
      private readonly BlogsRepository _repo;

    public BlogsService(BlogsRepository repo)
    {
      _repo = repo;
    }

    internal List<Blog> Get()
    {
        return _repo.Get();
    }
  }
}