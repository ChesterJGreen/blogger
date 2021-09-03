using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using blogger.Models;
using Dapper;

namespace blogger.Repositories
{
  public class BlogsRepository
  {
      private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Blog> Get()
    {
      string sql = @"
      SELECT
      a.*,
      b.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id";
      return _db.Query<Profile, Blog, Blog>(sql, (Profile, blogs) => 
      {
          blogs.Creator = Profile;
          return blogs;
      }, splitOn: "id").ToList();
    }
  }
}