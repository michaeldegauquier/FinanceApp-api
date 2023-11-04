using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.IncomeExpenseRepositoryTests
{
    [TestFixture]
    public class UpdateIncomeExpenseTests
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
        public async Task UpdateIncomeExpense_ShouldUpdateIncomeExpense()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var updateIncomeExpenseDto = new UpdateIncomeExpenseDto
            {
                Id = 1,
                UserId = _userId,
                DateCreated = new DateTime(2023, 5, 12),
                Amount = 200,
                Notes = "Test",
                Tags = new List<long> { 1, 2 }
            };

            using (var context = _context)
            {
                var repository = new IncomeExpenseRepository(context);

                // Act
                await repository.UpdateIncomeExpense(updateIncomeExpenseDto, cancellationToken);

                // Assert
                var updatedRecord = await context.IncomesExpenses.FindAsync(updateIncomeExpenseDto.Id);
                Assert.That(updatedRecord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(updatedRecord.UserId, Is.EqualTo(updateIncomeExpenseDto.UserId));
                    Assert.That(updateIncomeExpenseDto.DateCreated, Is.EqualTo(updatedRecord.DateCreated));
                    Assert.That(updateIncomeExpenseDto.Amount, Is.EqualTo(updatedRecord.Amount));
                    Assert.That(updateIncomeExpenseDto.Notes, Is.EqualTo(updatedRecord.Notes));
                    Assert.That(updateIncomeExpenseDto.Tags, Has.Count.EqualTo(updatedRecord.Tags.Count));
                });
            }
        }

        [Test]
        public async Task UpdateIncomeExpense_ShouldNotUpdateIncomeExpense_WhenNotExisting()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var updateIncomeExpenseDto = new UpdateIncomeExpenseDto
            {
                Id = 3,
                UserId = _userId,
                DateCreated = new DateTime(2023, 5, 12),
                Amount = 200,
                Notes = "Test",
                Tags = new List<long> { 1, 2 }
            };

            using (var context = _context)
            {
                var repository = new IncomeExpenseRepository(context);

                // Act
                var updated = await repository.UpdateIncomeExpense(updateIncomeExpenseDto, cancellationToken);

                // Assert
                Assert.That(updated, Is.EqualTo(-1));
            }
        }
    }
}
