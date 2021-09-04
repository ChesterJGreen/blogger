using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using blogger.Models;
using Dapper;

namespace blogger.Repositories
{
  public class CommentsRepository
  {
      private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Comment> Get()
    {
      string sql = @"
      SELECT 
        a.*,
        c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      ";
      return _db.Query<Profile, Comment, Comment>(sql, (profile, comment) =>
      {
          comment.Creator = profile;
          return comment;
      }, splitOn: "id").ToList();
    }
  }
}