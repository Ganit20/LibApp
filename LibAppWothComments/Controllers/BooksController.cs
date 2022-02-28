using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LibApp.Repository;

namespace LibApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICommentRepository commentRepository;

        public BooksController(IBookRepository repository, IGenreRepository genreRepository,ICustomerRepository customerRepository, ICommentRepository commentRepository)
        {
            this.repository = repository;
            this.genreRepository = genreRepository;
            this.customerRepository = customerRepository;
            this.commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = repository.GetBookById(id);

            if (book == null)
            {
                return Content("Book not found");
            }

            var comment = book.Comments.FirstOrDefault(x => x.Customer.Email == User.Identity.Name);
            if (comment == null)
            {
                comment = new Comment();
            }
            return View("Details",new BookDetailsViewModel() { Book = book, BlankComment = comment });
        }

        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult Edit(int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = genreRepository.GetGenres().ToList()
            };

            return View("BookForm", viewModel);
        }
        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult Delete(int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            repository.DeleteBook(id);
            repository.Save();

            return View();
        }
        [Authorize(Roles = "Owner,StoreManager,User")]
        public IActionResult AddComment(BookDetailsViewModel model,int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            var customer = customerRepository.GetCustomerByName(User.Identity.Name);
            if (customer == null)
            {
                return NotFound();
            }


                var comment = new Comment()
            {
                    Id = Guid.NewGuid().ToString(),
            Customer = customer,
                Book =book,
                Content = model.BlankComment.Content,
                Added= DateTime.Now,
                IsLike= model.BlankComment.IsLike
            };
            commentRepository.AddComment(comment);
            commentRepository.Save();


            return Details(id);
        }
        [Authorize(Roles = "Owner,StoreManager,User")]
        public IActionResult EditComment(BookDetailsViewModel model,int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            var comment = book.Comments.FirstOrDefault(x => x.Customer.Email == User.Identity.Name);
            comment.Content = model.BlankComment.Content;
            comment.IsLike = model.BlankComment.IsLike;
            commentRepository.UpdateComment(comment);
            commentRepository.Save();


            return Details(id);
        }
        [Authorize(Roles = "Owner,StoreManager,User")]
        public IActionResult DeleteComment(string id,int bookId)
        {
            commentRepository.DeleteComment(id);
            commentRepository.Save();


            return Details(bookId);
        }

        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = genreRepository.GetGenres().ToList()
            };

            return View("BookForm", viewModel);
        }
    }
}
