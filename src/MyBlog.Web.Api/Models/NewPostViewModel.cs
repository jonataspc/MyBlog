using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Api.Models
{
    public record NewPostViewModel(

        [Required]
        [MaxLength(512)]
        string Title,

        [Required]
        [MaxLength(1024)]
        string Summary,

        [Required]
        [MaxLength(1024 * 5)]
        string Content,

        [Required]
        [DataType(DataType.DateTime)]
        DateTime PublishDate);
}