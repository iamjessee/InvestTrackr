using api.Dtos;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager,
        IFMPService fmpService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        // Retrieves all comments based on query parameters
        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepository.GetAllAsync(queryObject);
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }

        // Retrieves a comment by ID
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        // Creates a new comment for a stock
        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("this stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        // Updates an existing comment by ID
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        // Deletes a comment by ID
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel = await _commentRepository.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(commentModel);
        }
    }
}