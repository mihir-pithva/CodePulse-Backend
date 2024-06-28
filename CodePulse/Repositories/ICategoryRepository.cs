using CodePulse.Models.Domain;

namespace CodePulse.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(Guid id);
        public Task<Category> DeleteAsync(Guid id);
        public Task<Category> UpdateAsync(Guid id, Category category);
    }
}
