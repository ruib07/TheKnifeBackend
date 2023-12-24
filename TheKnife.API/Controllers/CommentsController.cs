using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET api/comments
        [HttpGet]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<CommentsEfo>>> GetAllCommentsAsync()
        {
            List<CommentsEfo> comments = await _commentsService.GetAllCommentsAsync();

            if (comments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, comments);
        }

        // GET api/comments/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCommentByIdAsync(int id)
        {
            CommentsEfo comment = await _commentsService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, comment);
        }

        // POST api/comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CommentsEfo>> SendCommentAsync([FromBody, Required] CommentsEfo comment)
        {
            if (ModelState.IsValid)
            {
                CommentsEfo newComment = await _commentsService.SendCommentAsync(comment);
                return StatusCode(StatusCodes.Status201Created, newComment);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // PUT api/comments/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateCommentAsync(int id, [FromBody] CommentsEfo updateComment)
        {
            try
            {
                CommentsEfo comment = await _commentsService.UpdateCommentAsync(id, updateComment);

                if (comment == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/comments/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            try
            {
                await _commentsService.DeleteCommentAsync(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Internal server error: {ex.Message}");
            }
        }
    }
}
