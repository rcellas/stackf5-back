using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StackF5.Controllers;
using StackF5.DTOs.Tag;
using StackF5.Entity;
using StackF5.Repository.Tag;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackF5Test.Controllers;
    public class TagControllerTest
    {
        private readonly Mock<ITagRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly tagController _controller;

        public TagControllerTest()
        {
         
            _mockRepository = new Mock<ITagRepository>();
            _mockMapper = new Mock<IMapper>();
            
            _controller = new tagController(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task TagController_GetAllTags_ShouldReturnOk_WhenTagsExist()
        {
            
            var tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } };
            var expectedTagDtos = new List<TagDto> { new TagDto { Id = 1, Name = "Test Tag" } };
            
            _mockRepository.Setup(repo => repo.GetAllTags()).ReturnsAsync(tags);
            _mockMapper.Setup(m => m.Map<List<TagDto>>(It.IsAny<List<Tag>>())).Returns(expectedTagDtos);
            
            var result = await _controller.GetAllTags();
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);

            var actualTagDtos = Assert.IsType<List<TagDto>>(okResult.Value);
            Assert.Equal(expectedTagDtos, actualTagDtos);
        }
        
        [Fact]
        public async Task TagController_CreateTag_ShouldReturnOk_WhenTagIsCreated()
        {
            var tag = new Tag { Id = 1, Name = "Test Tag" };
            var tagDto = new TagDto { Id = 1, Name = "Test Tag" };
            
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<TagDto>())).Returns(tag);
            _mockRepository.Setup(repo => repo.CreateTag(tag)).ReturnsAsync(tag.Id);
            
            var result = await _controller.CreateTag(tagDto);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            
            var actualTagId = Assert.IsType<int>(okResult.Value);
            Assert.Equal(tag.Id, actualTagId);
        }
        
        [Fact]
        public async Task TagController_UpdateTag_ShouldReturnOk_WhenTagIsUpdated()
        {
            var tag = new Tag { Id = 1, Name = "Test Tag" };
            var tagDto = new TagDto { Id = 1, Name = "Test Tag" };
            
            _mockMapper.Setup(m => m.Map<Tag>(It.IsAny<TagDto>())).Returns(tag);
            _mockRepository.Setup(repo => repo.UpdateTag(tag)).ReturnsAsync(tag.Id);
            
            var result = await _controller.UpdateTag(tag.Id, tagDto);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            
            var actualTagId = Assert.IsType<int>(okResult.Value);
            Assert.Equal(tag.Id, actualTagId);
        }
    }