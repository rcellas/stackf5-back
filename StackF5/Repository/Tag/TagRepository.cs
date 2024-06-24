using Microsoft.EntityFrameworkCore;
using StackF5.Data;

namespace StackF5.Repository.Tag;

public class TagRepository
{
    private readonly ApplicationDbContext _context;
    public TagRepository(ApplicationDbContext context)
    {
        _context= context;
    }
    
    public async Task<List<Entity.Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }
    
    public async Task<int> CreateTag(Entity.Tag tag)
    {
        _context.Add(tag);
        await _context.SaveChangesAsync();
        return tag.Id;
    }
}