using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto;
using FinanceApp.Api.Domain.Models;
using FinanceApp.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.IncomeExpenseRepositoryTests
{
    [TestFixture]
    public class CreateIncomeExpenseTests
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
        public async Task CreateIncomeExpense_ShouldCreateIncomeExpense()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var createIncomeExpenseDto = new CreateIncomeExpenseDto
            {
                UserId = _userId,
                DateCreated = new DateTime(2023, 5, 12),
                Amount = 100,
                Notes = "Test",
                Tags = new List<long> { 1, 2 }
            };

            using (var context = _context)
            {
                var repository = new IncomeExpenseRepository(context);

                // Act
                var createdId = await repository.CreateIncomeExpense(createIncomeExpenseDto, cancellationToken);

                // Assert
                var createdRecord = await context.IncomesExpenses.FindAsync(createdId);
                Assert.That(createdRecord, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(createdRecord.UserId, Is.EqualTo(createIncomeExpenseDto.UserId));
                    Assert.That(createIncomeExpenseDto.DateCreated, Is.EqualTo(createdRecord.DateCreated));
                    Assert.That(createIncomeExpenseDto.Amount, Is.EqualTo(createdRecord.Amount));
                    Assert.That(createIncomeExpenseDto.Notes, Is.EqualTo(createdRecord.Notes));
                    Assert.That(createIncomeExpenseDto.Tags, Has.Count.EqualTo(createdRecord.Tags.Count));
                });
            }
        }
    }
}