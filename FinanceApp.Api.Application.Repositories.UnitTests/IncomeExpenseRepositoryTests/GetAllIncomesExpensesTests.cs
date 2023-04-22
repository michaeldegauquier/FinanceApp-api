using FinanceApp.Api.Application.Interfaces;
using Moq;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo;
using FinanceApp.Api.Domain.Models;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.UnitTests.IncomeExpenseRepositoryTests
{
    [TestFixture]
    public class GetAllIncomesExpensesTests
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
        public async Task GetAllIncomesExpenses_ShouldReturnCorrectData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomesExpenses = new List<IncomeExpense>
            {
                new IncomeExpense { UserId = userId, Id = 1, Amount = 100, Notes = "This is an income" },
                new IncomeExpense { UserId = userId, Id = 2, Amount = -200, Notes = "This is an expense" }
            };

            _dbContextMock.SetupGet(x => x.IncomesExpenses).ReturnsDbSet(incomesExpenses);

            // Act
            var result = await _repository.GetAllIncomesExpenses(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Amount.Should().Be(100);
            result[0].IsIncome.Should().BeTrue();
            result[0].Notes.Should().Be("This is an income");
            result[1].Amount.Should().Be(-200);
            result[1].IsIncome.Should().BeFalse();
            result[1].Notes.Should().Be("This is an expense");
        }

        [Test]
        public async Task GetAllIncomesExpenses_ShouldReturnInCorrectOrder()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var incomesExpenses = new List<IncomeExpense>
            {
                new IncomeExpense { UserId = userId, Id = 1, Amount = 100, DateCreated = new DateTime(2020, 1, 12) },
                new IncomeExpense { UserId = userId, Id = 2, Amount = 20, DateCreated = new DateTime(2022, 1, 12) },
                new IncomeExpense { UserId = userId, Id = 3, Amount = 50, DateCreated = new DateTime(2021, 1, 12) }
            };

            _dbContextMock.SetupGet(x => x.IncomesExpenses).ReturnsDbSet(incomesExpenses);

            // Act
            var result = await _repository.GetAllIncomesExpenses(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result[0].Id.Should().Be(2);
            result[0].Amount.Should().Be(20);
            result[0].DateCreated.Should().Be(new DateTime(2022, 1, 12));
            result[1].Id.Should().Be(3);
            result[1].Amount.Should().Be(50);
            result[1].DateCreated.Should().Be(new DateTime(2021, 1, 12));
            result[2].Id.Should().Be(1);
            result[2].Amount.Should().Be(100);
            result[2].DateCreated.Should().Be(new DateTime(2020, 1, 12));
        }
    }
}
