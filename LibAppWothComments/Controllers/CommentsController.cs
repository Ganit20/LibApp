using LibApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository repository;

        public CommentsController(ICommentRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult GetBookComments(int id)
        {
            var customer = repository.GetCommentsForBook(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult New()
        {
            var viewModel = new CommentViewModel();

            return View("CommentForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Edit(string id)
        {
            var comment = repository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            var viewModel = new CommentViewModel(comment);

            return View("CommentForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CommentViewModel(comment);

                return View("CommentForm", viewModel);
            }
            if (comment.Id == null)
            {
                comment.Id = System.Guid.NewGuid().ToString();
                repository.AddComment(comment);
            }
            else
            {
                var commentInDb = repository.GetCommentById(comment.Id);
                commentInDb.Content = comment.Content;
                repository.UpdateComment(commentInDb);
            }

            try
            {
                repository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}
