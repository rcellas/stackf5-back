namespace StackF5.Repository.Comment;

public interface ICommentRepository
{
    Task<List<Entity.Comment>> GetAllComments(int incidenceId);
    Task<Entity.Comment?> GetCommentById(int id);
    Task<int> CreateComment(Entity.Comment comment);
    Task<bool> ExistComment(int id);
    Task UpdateComment(Entity.Comment comment);
    Task DeleteComment(int id);
}