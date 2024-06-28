using CodePulse.Models.Domain;

namespace CodePulse.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
    }
}
