using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Mvc.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha seu comentário")]
        [MaxLength(1024)]
        public required string Content { get; set; }

        [Required]
        public required Guid PostId { get; set; }
    }
}