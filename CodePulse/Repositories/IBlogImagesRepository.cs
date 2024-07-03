using CodePulse.Models.Domain;

namespace CodePulse.Repositories
{
    public interface IBlogImagesRepository
    {
        public Task<BlogImage> Upload(IFormFile file,BlogImage blogImage);
        public Task<List<BlogImage>> GetAllAsync(); 
    }
}
