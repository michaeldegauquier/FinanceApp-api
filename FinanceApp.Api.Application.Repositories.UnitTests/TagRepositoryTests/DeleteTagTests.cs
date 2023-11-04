using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Application.Repositories.TagRepo;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.TagRepositoryTests
{
    [TestFixture]
    public class DeleteTagTests
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
        public async Task DeleteTag_ShouldDeleteTag()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var amountDeleted = await repository.DeleteTag(_userId, 1, cancellationToken);

                // Assert
                var deletedRecord = await context.Tags.FindAsync((long)1);
                Assert.That(amountDeleted, Is.EqualTo(1));
                Assert.That(deletedRecord, Is.Null);
            }
        }

        [Test]
        public async Task DeleteTag_ShouldNotDeleteTag_WhenNotExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            using (var context = _context)
            {
                var repository = new TagRepository(context);

                // Act
                var deleted = await repository.DeleteTag(_userId, 3, cancellationToken);

                // Assert
                Assert.That(deleted, Is.EqualTo(-1));
            }
        }
    }
}
