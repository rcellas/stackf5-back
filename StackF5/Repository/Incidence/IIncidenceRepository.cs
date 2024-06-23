using StackF5.Entity;
namespace StackF5.Repository.Incidence;


public interface IIncindenceRepository
{
    Task<List<Entity.Incidence>> GetAllIncidence();
    Task<Entity.Incidence?> GetIncidenceById(int id);
    Task<int> CreateIncidence(Entity.Incidence incidence);
    Task UpdateIncidence(Entity.Incidence incidence);
    Task DeleteIncidence(int id);
}