namespace StackF5.Repository.Tag;

public interface ITagRepository
{
    Task<List<Entity.Tag>> GetAllTags();
    Task<int> CreateTag(Entity.Tag tag);
    Task UpdateTag(Entity.Tag tag);
    Task DeleteTag(int id);
}