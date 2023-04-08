namespace FinanceApp.Api.Application.Repositories.TagRepository.Dto
{
    public class CreateTagDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
    }
}
