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


    internal Comment Get(int id)
    {
      string sql = @"
      SELECT
        a.*,
        c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE c.id = @id
      ";
      return _db.Query<Profile, Comment, Comment>(sql, (Profile, Comment) =>
      {
          Comment.Creator = Profile;
          return Comment;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }


    internal Comment Create(Comment newComment)
    {
      string sql = @"
      INSERT INTO comments
      (body, blogId, creatorId)
      VALUES
      (@Body, @BlogId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
    int id = _db.ExecuteScalar<int>(sql, newComment);
    return Get(id);

  }


    internal Comment Update(Comment updatedComment)
    {
      string sql = @"
        UPDATE comments
        SET
            body = @Body
        WHERE id = @Id;
        ";
        _db.Execute(sql, updatedComment);
        return updatedComment;
    }
    internal void Delete(int id)
    {
      string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
}
}