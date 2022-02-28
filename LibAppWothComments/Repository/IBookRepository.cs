using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks(bool withComments = true);
        Book GetBookById(int bookId);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
        void Save();
    }
}
