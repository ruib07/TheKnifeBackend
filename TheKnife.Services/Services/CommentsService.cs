using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface ICommentsService
    {
        Task<List<CommentsEfo>> GetAllCommentsAsync();
        Task<CommentsEfo> GetCommentByIdAsync(int id);
        Task<CommentsEfo> SendCommentAsync(CommentsEfo comment);
        Task<CommentsEfo> UpdateCommentAsync(int id, CommentsEfo updateComment);
        Task DeleteCommentAsync(int id);
    }

    public class CommentsService : ICommentsService
    {
        private readonly TheKnifeDbContext _context;

        public CommentsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentsEfo>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentsEfo> GetCommentByIdAsync(int id)
        {
            CommentsEfo? comment = await _context.Comments.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            return comment;
        }

        public async Task<CommentsEfo> SendCommentAsync(CommentsEfo comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<CommentsEfo> UpdateCommentAsync(int id, CommentsEfo updateComment)
        {
            try
            {
                CommentsEfo? comment = await _context.Comments.FindAsync(id);

                if (comment == null)
                {
                    throw new Exception("Entity doesn´t exist in the database!");
                }

                comment.Review = updateComment.Review;
                comment.Commentdate = updateComment.Commentdate;
                comment.Comment = updateComment.Comment;

                await _context.SaveChangesAsync();

                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating comment: {ex.Message}");
            }
        }

        public async Task DeleteCommentAsync(int id)
        {
            CommentsEfo? comment = await _context.Comments.FirstOrDefaultAsync(
                c => c.Id == id);

            if (comment == null)
            {
                throw new Exception("Entity doesn´t exist in the database!");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
