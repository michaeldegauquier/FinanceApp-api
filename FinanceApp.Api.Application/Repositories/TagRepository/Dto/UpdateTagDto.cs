﻿namespace FinanceApp.Api.Application.Repositories.TagRepository.Dto
{
    public class UpdateTagDto
    {
        public Guid UserId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; } = "";
    }
}
