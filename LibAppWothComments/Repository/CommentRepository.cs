using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
        }

        public void DeleteComment(string CommentId)
        {
            context.Comments.Remove(GetCommentById(CommentId));
        }

        public Comment GetCommentById(string CommentId)
        {
            return context.Comments.Where(c => c.Id == CommentId).SingleOrDefault();
        }

        public IEnumerable<Comment> GetCommentsForBook(int bookId)
        {
            return context.Comments.Where(x=>x.BookId==bookId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            this.context.Entry(comment).State = EntityState.Modified;
        }

    }
}
