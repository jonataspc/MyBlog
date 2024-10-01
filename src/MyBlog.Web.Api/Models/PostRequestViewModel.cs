using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBlog.Web.Api.Models
{
    public class PostRequestViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(512)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(1024)]
        public required string Summary { get; set; }

        [Required]
        [MaxLength(1024 * 5)]
        public required string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime PublishDate { get; set; }
    }
}