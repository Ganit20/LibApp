using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.ViewModels
{
    public class CommentViewModel : Controller
    {

        public Comment Comment { get; set; }
        public Customer Customer { get; set; }
        public CommentViewModel(Comment Comment)
        {
            this.Comment = Comment;
        }
        public CommentViewModel()
        {
           
        }
        public string Title
        {
            get
            {
                if (Comment != null)
                {
                    return "Edit Comment";
                }

                return "New Comment";
            }
        }
    }
}
