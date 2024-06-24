using Microsoft.EntityFrameworkCore;
using StackF5.Data;
using StackF5.Entity;
using StackF5.Repository.Tag;

namespace StackF5Test;

public class TagRepositoryTest
{
    private async Task<ApplicationDbContext> GetApplicationDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dataContext = new ApplicationDbContext(options);
        dataContext.Database.EnsureCreated();
        if (await dataContext.Tags.CountAsync() <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                dataContext.Tags.Add(
                    new Tag()
                    {
                        Name = "Testing Tag"
                    }
                );
                await dataContext.SaveChangesAsync();
            }
        }
        return dataContext;
    }

    [Fact]
    public async Task GetAllTags_ShouldReturnListOfTags()
    {
        // Arrange
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        string expectedName = "Testing Tag";
        int tagId = 1;

        // Act
        var tags = await tagRepository.GetAllTags(tagId);

        // Assert
       Assert.NotNull(tags);
       Assert.IsType<Tag>(tags);
       Assert.Equal(expectedName, tags.Name);
    }
}