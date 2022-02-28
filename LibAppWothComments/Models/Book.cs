using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Enter Name")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Enter Author Name")]
		public string AuthorName { get; set; }
		[Required]
		public Genre Genre { get; set; }
		[Required(ErrorMessage = "Enter Genre")]
		public byte GenreId { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }
		[Required(ErrorMessage = "Requeired")]
		[DataType(DataType.Date)]
		public DateTime ReleaseDate { get; set; }
		[Required(ErrorMessage = "Enter stock number")]
		[Range(1, 20, ErrorMessage = "Value must be beetwen 1-20")]
		public int NumberInStock { get; set; }
		public virtual List<Comment> Comments { get; set; }
	}
      
}
