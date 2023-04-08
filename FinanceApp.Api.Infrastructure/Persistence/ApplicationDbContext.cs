using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        public DbSet<IncomeExpense> IncomesExpenses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildModel(builder);
        }

        private static void BuildModel(ModelBuilder builder)
        {
            builder.Entity<IncomeExpense>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.IncomesExpenses)
                .UsingEntity<IncomeExpenseTag>(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.IncomeExpenseTags)
                        .HasForeignKey(pt => pt.TagId)
                        .OnDelete(DeleteBehavior.NoAction),
                    j => j
                        .HasOne(pt => pt.IncomeExpense)
                        .WithMany(p => p.IncomeExpenseTags)
                        .HasForeignKey(pt => pt.IncomeExpenseId)
                        .OnDelete(DeleteBehavior.NoAction),
                    j =>
                    {
                        j.HasKey(t => new { t.IncomeExpenseId, t.TagId });
                    });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
