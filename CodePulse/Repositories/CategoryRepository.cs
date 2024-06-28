using CodePulse.API.Data;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _applicationDbContext.Categories.AddAsync(category);
            await _applicationDbContext.SaveChangesAsync();
            return category;
        }
    }
}
