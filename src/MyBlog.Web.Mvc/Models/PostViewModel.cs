using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Mvc.Models
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha o {0}")]
        [Display(Name = "Título")]
        [MaxLength(512)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Por favor preencha o {0}")]
        [Display(Name = "Resumo detalhado")]
        [MaxLength(1024)]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Por favor preencha o {0}")]
        [Display(Name = "Conteúdo do post")]
        [MaxLength(1024 * 5)]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Por favor preencha a {0}")]
        [Display(Name = "Data de publicação")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, DateTimeKind.Local);
    }
}