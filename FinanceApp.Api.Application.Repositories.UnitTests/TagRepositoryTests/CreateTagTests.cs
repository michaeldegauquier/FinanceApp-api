using FinanceApp.Api.Application.Repositories.TagRepo;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.TagRepositoryTests
{
    [TestFixture]
    public class CreateTagTests
    {
        private ApplicationDbContext _context;
        private Guid _userId;

        [SetUp]
        public async Task SetupAsync()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _userId = Guid.NewGuid();

            var tags = new List<Tag>
            {
                new Tag { UserId = _userId, Id = 1, Name = "Tag1" },
                new Tag { UserId = _userId, Id = 2, Name = "Tag2" }
            };

            _context = new ApplicationDbContext(contextOptions);

            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CreateTag_ShouldCreateTag()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var createTagDto = new CreateTagDto
            {
                UserId = _userId,
                Name = "Test",
            };

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var createdId = await repository.CreateTag(createTagDto, cancellationToken);

                // Assert
                var createdRecord = await context.Tags.FindAsync(createdId);
                Assert.That(createdRecord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(createdRecord.UserId, Is.EqualTo(createTagDto.UserId));
                    Assert.That(createTagDto.Name, Is.EqualTo(createdRecord.Name));
                });
            }
        }

        [Test]
        public async Task CreateTag_ShouldNotCreateTag_WhenExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var createTagDto = new CreateTagDto
            {
                UserId = _userId,
                Name = "Tag1",
            };

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var createdId = await repository.CreateTag(createTagDto, cancellationToken);

                // Assert
                Assert.That(createdId, Is.EqualTo(-1));
            }
        }
    }
}