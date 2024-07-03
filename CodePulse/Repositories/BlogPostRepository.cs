using CodePulse.API.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BlogPostRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _applicationDbContext.AddAsync(blogPost);
            await _applicationDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var blogPost = await _applicationDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (blogPost == null) 
            {
                return null;
            }
            _applicationDbContext.BlogPosts.Remove(blogPost);
            await _applicationDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _applicationDbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            var blogPost =  await _applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            if (blogPost == null)
            {
                return null;
            }
            return blogPost;
        }

        public async Task<BlogPost> GetByUrlAsync(string url)
        {
            var blogPost = await _applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == url);
            if (blogPost == null)
            {
                return null;
            }
            return blogPost;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var blogPostFromDb = await _applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (blogPostFromDb == null)
            {
                return null;
            }
            _applicationDbContext.Entry(blogPostFromDb).CurrentValues.SetValues(blogPost);
            blogPostFromDb.Categories = blogPost.Categories;    

            await _applicationDbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
