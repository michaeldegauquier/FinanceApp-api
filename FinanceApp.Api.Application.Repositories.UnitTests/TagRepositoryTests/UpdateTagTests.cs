using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Api.Application.Repositories.TagRepo;

namespace FinanceApp.Api.Application.Repositories.UnitTests.TagRepositoryTests
{
    [TestFixture]
    public class UpdateTagTests
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
        public async Task UpdateTag_ShouldUpdateTag()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var updateTagDto = new UpdateTagDto
            {
                Id = 1,
                UserId = _userId,
                Name = "Test"
            };

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                await repository.UpdateTag(updateTagDto, cancellationToken);

                // Assert
                var updatedRecord = await context.Tags.FindAsync(updateTagDto.Id);
                Assert.That(updatedRecord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(updatedRecord.UserId, Is.EqualTo(updateTagDto.UserId));
                    Assert.That(updateTagDto.Name, Is.EqualTo(updatedRecord.Name));
                });
            }
        }

        [Test]
        public async Task UpdateTag_ShouldNotUpdateTag_WhenExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var updateTagDto = new UpdateTagDto
            {
                Id = 2,
                UserId = _userId,
                Name = "Tag1"
            };

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var updated = await repository.UpdateTag(updateTagDto, cancellationToken);

                // Assert
                Assert.That(updated, Is.EqualTo(-1));
            }
        }

        [Test]
        public async Task UpdateTag_ShouldNotUpdateTag_WhenNotExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var updateTagDto = new UpdateTagDto
            {
                Id = 3,
                UserId = _userId,
                Name = "Test"
            };

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var updated = await repository.UpdateTag(updateTagDto, cancellationToken);

                // Assert
                Assert.That(updated, Is.EqualTo(-2));
            }
        }
    }
}
