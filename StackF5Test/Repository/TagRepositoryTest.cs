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
    public async Task TagRepository_GetAllTags_ShouldReturnListOfTags()
    {
        // Arrange
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        string expectedName = "Testing Tag";

        // Act
        var tags = await tagRepository.GetAllTags();

        // Assert
       Assert.NotNull(tags);
       Assert.IsType<List<Tag>>(tags);
       Assert.NotEmpty(tags);
       Assert.All(tags,tag => Assert.Equal(expectedName, tag.Name));
    }
    
    [Fact]
    public async Task TagRepository_CreateTag_ShouldReturnTagId()
    {
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        var newTag = new Tag { Name = "New Testing Tag" };
        
        var tagId = await tagRepository.CreateTag(newTag);
        await dbContext.SaveChangesAsync();
        var createdTag = await dbContext.Tags.FindAsync(tagId);
        
        Assert.IsType<int>(tagId); 
        Assert.NotNull(createdTag); 
        Assert.Equal(newTag.Name, createdTag.Name);

    }
    
}