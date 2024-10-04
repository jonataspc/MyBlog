using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBlog.Web.Api.Models
{
    public class CommentRequestViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(1024)]
        public required string Content { get; set; }
    }
}