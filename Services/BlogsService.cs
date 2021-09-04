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

    internal Blog Get(int id)
    {
      Blog blog = _repo.Get(id);
      if (blog == null)
      {
          throw new Exception("Invalid Id");
      }
      return blog;
    }

    internal Blog Create(Blog newBlog)
    {
      return _repo.Create(newBlog);
    }

    internal Blog Edit(Blog updatedBlog)
    {
        Blog original = Get(updatedBlog.Id);
        updatedBlog.Title = updatedBlog.Title != null ? updatedBlog.Title : original.Title;
        updatedBlog.Body = updatedBlog.Body != null ? updatedBlog.Body : original.Body;
        updatedBlog.ImgUrl = updatedBlog.ImgUrl != null ? updatedBlog.ImgUrl : original.ImgUrl;
        updatedBlog.Published = updatedBlog.PublishedWasSet ? updatedBlog.Published : original.Published;
      return _repo.Edit(updatedBlog);
    }

    internal void Delete(int blogId, string userId)
    {
      Blog blogToDelete = Get(blogId);
      if (blogToDelete.CreatorId != userId)
      {
          throw new Exception("You do Not have permission to delete this blog");
      }
      _repo.Delete(blogId);
    }
  }
}