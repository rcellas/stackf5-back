using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackF5.DTOs.Tag;
using StackF5.Repository.Tag;

namespace StackF5Test.Controllers;

public class TagController
{
    private readonly Mock<ITagRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TagController _controller;
    public TagController()
    {
        _mockMapper= new Mock<IMapper>();
        _mockRepository= new Mock<ITagRepository>();
        _controller= new TagController(null,_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task TagController_GetAllTags_ShouldReturnOk_WhenTagsExist()
    {
        // Arrange
        var expectedTags = new List<TagDto>() { new TagDto { Id = 1, Name = "Test Tag" } };
        _mockRepository.Setup(repo => repo.GetAllTags()).ReturnsAsync(expectedTags);
        
        // Act
        var result = await _controller.GetAllTags();
        
        // Assert
        Assert.IsType<OkObjectResult>(result.Result); // Verify response type
        var okResult = result.Result as OkObjectResult; // Cast to OkObjectResult
        Assert.NotNull(okResult); // Check if OkObjectResult is not null
        Assert.Equal(expectedTags, okResult.Value); // Verify tag list in response
    }
}