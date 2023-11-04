using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Domain.Models;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.IncomeExpenseRepositoryTests
{
    [TestFixture]
    public class GetIncomeExpenseByIdTests
    {
        private Mock<IApplicationDbContext> _dbContextMock;
        private IncomeExpenseRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _repository = new IncomeExpenseRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetIncomeExpenseById_ShouldReturnCorrectIncomeExpense()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomesExpenses = new List<IncomeExpense>
            {
                new IncomeExpense { UserId = userId, Id = 1, Amount = 100, Notes = "This is an income", DateCreated = new DateTime(2023, 1, 10) },
                new IncomeExpense { UserId = userId, Id = 2, Amount = -200, Notes = "This is an expense", DateCreated = new DateTime(2023, 1, 12) }
            };

            _dbContextMock.SetupGet(x => x.IncomesExpenses).ReturnsDbSet(incomesExpenses);

            // Act
            var result = await _repository.GetIncomeExpenseById(userId, 1);

            // Assert
            result.Should().NotBeNull();
            result?.Amount.Should().Be(100);
            result?.IsIncome.Should().BeTrue();
            result?.Notes.Should().Be("This is an income");
            result?.DateCreated.Should().Be(new DateTime(2023, 1, 10));
        }

        [Test]
        public async Task GetIncomeExpenseById_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomesExpenses = new List<IncomeExpense>();

            _dbContextMock.SetupGet(x => x.IncomesExpenses).ReturnsDbSet(incomesExpenses);

            // Act
            var result = await _repository.GetIncomeExpenseById(userId, 1);

            // Assert
            result.Should().BeNull();
        }
    }
}
