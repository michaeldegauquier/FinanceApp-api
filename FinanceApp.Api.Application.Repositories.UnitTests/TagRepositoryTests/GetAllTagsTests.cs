using FinanceApp.Api.Application.Interfaces;
using Moq;
using FinanceApp.Api.Application.Repositories.TagRepo;
using FinanceApp.Api.Domain.Models;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.TagRepositoryTests
{
    [TestFixture]
    public class GetAllTagsTests
    {
        private Mock<IApplicationDbContext> _dbContextMock;
        private TagRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _repository = new TagRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetAllTags_ShouldReturnCorrectData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tags = new List<Tag>
            {
                new Tag { UserId = userId, Id = 1, Name = "A tag" },
                new Tag { UserId = userId, Id = 2, Name = "Tag" }
            };

            _dbContextMock.SetupGet(x => x.Tags).ReturnsDbSet(tags);

            // Act
            var result = await _repository.GetAllTags(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("A tag");
            result[1].Name.Should().Be("Tag");
        }
    }
}
