using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title Must be at least 5 Characters")]
        [MaxLength(25, ErrorMessage = "Title cannont be over 25 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Comment Must be at least 5 Characters")]
        [MaxLength(280, ErrorMessage = "Comment cannont be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}