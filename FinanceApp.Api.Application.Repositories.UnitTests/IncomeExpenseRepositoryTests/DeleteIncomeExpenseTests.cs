using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.IncomeExpenseRepositoryTests
{
    [TestFixture]
    public class DeleteIncomeExpenseTests
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

            var incomesExpenses = new List<IncomeExpense>
            {
                new IncomeExpense { UserId = _userId, Id = 1, Amount = 100, Notes = "This is an income", DateCreated = new DateTime(2023, 1, 10) },
                new IncomeExpense { UserId = _userId, Id = 2, Amount = -200, Notes = "This is an expense", DateCreated = new DateTime(2023, 1, 12) }
            };

            var tags = new List<Tag>
            {
                new Tag { UserId = _userId, Id = 1, Name = "Tag1" },
                new Tag { UserId = _userId, Id = 2, Name = "Tag2" }
            };

            _context = new ApplicationDbContext(contextOptions);

            await _context.IncomesExpenses.AddRangeAsync(incomesExpenses);
            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task DeleteIncomeExpense_ShouldDeleteIncomeExpense()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            using (var context = _context)
            {
                var repository = new IncomeExpenseRepository(context);

                // Act
                var amountDeleted = await repository.DeleteIncomeExpense(_userId, 1, cancellationToken);

                // Assert
                var deletedRecord = await context.IncomesExpenses.FindAsync((long)1);
                Assert.That(amountDeleted, Is.EqualTo(1));
                Assert.That(deletedRecord, Is.Null);
            }
        }

        [Test]
        public async Task DeleteIncomeExpense_ShouldNotDeleteIncomeExpense_WhenNotExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            using (var context = _context)
            {
                var repository = new IncomeExpenseRepository(context);

                // Act
                var deleted = await repository.DeleteIncomeExpense(_userId, 3, cancellationToken);

                // Assert
                Assert.That(deleted, Is.EqualTo(-1));
            }
        }
    }
}
