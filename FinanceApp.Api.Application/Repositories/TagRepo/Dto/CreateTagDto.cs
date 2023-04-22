namespace FinanceApp.Api.Application.Repositories.TagRepo.Dto
{
    public class CreateTagDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
    }
}
