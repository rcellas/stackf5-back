using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackF5.Data;
using StackF5.DTOs.Comment;
using StackF5.Repository.Incidence;

namespace StackF5.Repository.Comment;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        this._context = context;
    }
    
    public async Task<List<Entity.Comment>> GetAllComments(int incidenceId, IIncidenceRepository incidenceRepository)
    {
        var incidenceExists = await incidenceRepository.ExistIncidence(incidenceId);
        if (!incidenceExists)
        {
            throw new Exception("Incidencia no encontrada");
        }
        return await _context.Comments.Where(c => c.IncidenceId == incidenceId).ToListAsync();
    }

    public async Task<Entity.Comment?> GetCommentById(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public async Task<int> CreateComment(Entity.Comment comment)
    {
        comment.CreatedAt = DateTime.Now;
        comment.UpdatedAt = DateTime.Now;
        _context.Add(comment);
        await _context.SaveChangesAsync();
        return comment.Id;
    }
    
    public async Task UpdateComment(Entity.Comment comment)
    {
        comment.UpdatedAt = DateTime.Now;
        _context.Update(comment);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteComment(int id)
    {
        await _context.Comments.Where(c=>c.Id == id).ExecuteDeleteAsync();
    }
}