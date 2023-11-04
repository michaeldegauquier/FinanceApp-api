using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Repositories.TagRepo;
using FinanceApp.Api.Domain.Models;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.TagRepositoryTests
{
    [TestFixture]
    public class GetTagByIdTests
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
        public async Task GetTagById_ShouldReturnCorrectTag()
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
            var result = await _repository.GetTagById(userId, 1);

            // Assert
            result.Should().NotBeNull();
            result?.Name.Should().Be("A tag");
        }

        [Test]
        public async Task GetTagById_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tags = new List<Tag>();

            _dbContextMock.SetupGet(x => x.Tags).ReturnsDbSet(tags);

            // Act
            var result = await _repository.GetTagById(userId, 1);

            // Assert
            result.Should().BeNull();
        }
    }
}
