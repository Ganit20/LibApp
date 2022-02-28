using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repository
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsForBook(int bookId);
        Comment GetCommentById(string CommentId);
        void AddComment(Comment Comment);
        void UpdateComment(Comment Comment);
        void DeleteComment(string CommentId);
        void Save();
    }
}
