using StackF5.Repository.Incidence;

namespace StackF5.Repository.Comment;

public interface ICommentRepository
{
    Task<List<Entity.Comment>> GetAllComments(int incidenceId, IIncidenceRepository incidenceRepository);
    Task<Entity.Comment?> GetCommentById(int id);
    Task<int> CreateComment(Entity.Comment comment);
    Task UpdateComment(Entity.Comment comment);
    Task DeleteComment(int id);
}