using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        // Maps a Comment model to a Comment DTO for data transfer
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn.Date, // Only store the date portion of CreatedOn
                StockId = commentModel.StockId
            };
        }

        // Converts a CreateComment DTO to a Comment model for creation, associating it with a specific stock
        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId // Associate the comment with the given stock ID
            };
        }

        // Converts an UpdateCommentRequest DTO to a Comment model for updates
        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                // StockId is not set since it should not be changed during an update
            };
        }
    }
}