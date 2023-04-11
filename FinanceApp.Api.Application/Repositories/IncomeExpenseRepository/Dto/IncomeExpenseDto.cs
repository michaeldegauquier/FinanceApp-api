using FinanceApp.Api.Application.Repositories.TagRepository.Dto;

namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto
{
    public class IncomeExpenseDto
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IList<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
