using Microsoft.EntityFrameworkCore;
using StackF5.Data;
using StackF5.Entity;
using StackF5.Repository.Tag;

namespace StackF5Test.Repository;

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
    public async Task TagRepository_GetAllTags_ShouldReturnIncludeIncidenceTags()
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
        Assert.All(tags, tag => Assert.Equal(expectedName, tag.Name));
        Assert.All(tags, tag => Assert.NotNull(tag.IncidenceTags));
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

    [Fact]
    public async Task TagRepository_CreateTag_ShouldReturnThrowException_WhenTagExists()
    {
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        var newTag = new Tag { Name = "Testing Tag" };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await tagRepository.CreateTag(newTag);
        });
    }
    
    [Fact]
    public async Task TagRepository_UpdateTag_ShouldReturnNoContent()
    {
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        var newTag = new Tag { Name = "New Testing Tag" };
        var tagId = await tagRepository.CreateTag(newTag);
        
        await dbContext.SaveChangesAsync();
        var createdTag = await dbContext.Tags.FindAsync(tagId);
        createdTag.Name = "Updated Testing Tag";
        
        await tagRepository.UpdateTag(createdTag);
        await dbContext.SaveChangesAsync();
        
        var updatedTag = await dbContext.Tags.FindAsync(tagId);
        Assert.NotNull(updatedTag);
        Assert.Equal(createdTag.Name, updatedTag.Name);
    }
    
    [Fact]
    public async Task TagRepository_UpdateTag_ShouldReturnThrowException_WhenTagNotExists()
    {
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        var newTag = new Tag { Name = "New Testing Tag" };
        
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await tagRepository.UpdateTag(newTag);
        });
    }

    [Fact]
    public async Task TagRepository_UpdateTag_ShouldReturnThrowException_WhenTagExists()
    {
        var dbContext = await GetApplicationDbContext();
        var tagRepository = new TagRepository(dbContext);
        var newTag = new Tag { Name = "Testing Tag" };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await tagRepository.CreateTag(newTag);
        });
    }

    
}