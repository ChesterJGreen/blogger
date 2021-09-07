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
        List<Blog> blogs = _repo.GetAll();
        return blogs.FindAll(b => b.Published == true);
    }

    internal Blog Get(int id, bool checkPublished = true)
    {
      Blog blog = _repo.GetById(id);
      if (blog == null || (checkPublished && blog.Published == false))
      {
          throw new Exception("Invalid Id");
      }
      return blog;
    }

    internal List<Blog> GetAllByProfileId(string id)
    {
      List<Blog> blogs = _repo.GetByProfileId(id);
      if(blogs == null)
      {
        throw new Exception("No blogs in this Profile");
      }
      return blogs;
    }
    internal Blog Create(Blog newBlog)
    {
      return _repo.Create(newBlog);
    }

    internal Blog Edit(Blog updatedBlog)
    {
        Blog original = Get(updatedBlog.Id, false);
        if(original.CreatorId != updatedBlog.CreatorId){
          throw new Exception("Invalid Access");
        }
        original.Title = updatedBlog.Title.Length > 0 ? updatedBlog.Title : original.Title;
        original.Body = updatedBlog.Body.Length > 0 ? updatedBlog.Body : original.Body;
        original.ImgUrl = updatedBlog.ImgUrl != null && updatedBlog.ImgUrl.Length > 0 ? updatedBlog.ImgUrl : original.ImgUrl;
        original.Published = updatedBlog.Published != null ? updatedBlog.Published : original.Published;
        _repo.Edit(updatedBlog);
        return original;
    }

    // internal List<Blog> GetAllBlogsByCreator(string id, false)
    // {
    //   List<Blog> blogs = _repo.GetAll(creatorId);
    //   if (careIfPublished == false)
    // }

    internal void Delete(int blogId, string userId)
    {
      Blog blogToDelete = Get(blogId, false);
      if (blogToDelete.CreatorId != userId)
      {
          throw new Exception("You do Not have permission to delete this blog");
      }
      _repo.Delete(blogId);
    }
  }
}