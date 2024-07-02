using CodePulse.Models.Domain;
using CodePulse.Models.DTOs;

namespace CodePulse.Repositories
{
    public interface IBlogPostRepository
    {
        public Task<BlogPost> CreateAsync(BlogPost blogPost);
        public Task<List<BlogPost>> GetAllAsync();
        public Task<BlogPost> GetByIdAsync(Guid id);
        public Task<BlogPost> UpdateAsync(BlogPost blogPost);
        public Task<BlogPost> DeleteAsync(Guid id);
    }
}
