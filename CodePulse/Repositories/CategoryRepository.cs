using CodePulse.API.Data;
using CodePulse.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category> DeleteAsync(Guid id)
        {
            var category = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _applicationDbContext.Categories.Remove(category);
                await _applicationDbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _applicationDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return null;
            }
            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var categoryFromDb = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryFromDb != null)
            {
                categoryFromDb.Name = category.Name;
                categoryFromDb.UrlHandle = category.UrlHandle;
                await _applicationDbContext.SaveChangesAsync();
                return categoryFromDb;
            }
            return null;
        }
    }
}
