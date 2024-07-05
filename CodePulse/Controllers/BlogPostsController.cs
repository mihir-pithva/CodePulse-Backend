using CodePulse.Models.Domain;
using CodePulse.Models.DTOs;
using CodePulse.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this._blogPostRepository = blogPostRepository;
            this._categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostDto blogPost)
        {
            //Map DTO to Model
            var blogPostDomain = new BlogPost
            {
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                UrlHandle = blogPost.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryId in blogPost.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    blogPostDomain.Categories.Add(category);
                }
            }

            blogPostDomain = await _blogPostRepository.CreateAsync(blogPostDomain);

            //Map Model to DTO
            var blogPostDto = new BlogPostDto
            {
                Id = blogPostDomain.Id,
                Title = blogPostDomain.Title,
                ShortDescription = blogPostDomain.ShortDescription,
                Author = blogPostDomain.Author,
                FeaturedImageUrl = blogPostDomain.FeaturedImageUrl,
                PublishedDate = blogPostDomain.PublishedDate,
                Content = blogPostDomain.Content,
                IsVisible = blogPostDomain.IsVisible,
                UrlHandle = blogPostDomain.UrlHandle,
                Categories = blogPostDomain.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
            };

            return Ok(blogPostDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            if (blogPosts == null)
            {
                return NotFound();
            }
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    IsVisible = blogPost.IsVisible,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPost([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var blogPostDto = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(blogPostDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostDto updateBlogPost)
        {
            //DTO to domain model
            var blogPost = new BlogPost
            {
                Id = id,
                Title = updateBlogPost.Title,
                ShortDescription = updateBlogPost.ShortDescription,
                Author = updateBlogPost.Author,
                FeaturedImageUrl = updateBlogPost.FeaturedImageUrl,
                Content = updateBlogPost.Content,
                IsVisible = updateBlogPost.IsVisible,
                PublishedDate = updateBlogPost.PublishedDate,
                UrlHandle = updateBlogPost.UrlHandle,
                Categories = new List<Category>()
            };
            foreach (var categoryGuid in updateBlogPost.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryGuid);
                if (category != null)
                {
                    blogPost.Categories.Add(category);
                }
            }
            var UpdatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);
            if (UpdatedBlogPost == null)
            {
                return null;
            }

            //Model to DTO
            var response = new BlogPostDto
            {
                Id = UpdatedBlogPost.Id,
                Title = UpdatedBlogPost.Title,
                ShortDescription = UpdatedBlogPost.ShortDescription,
                Author = UpdatedBlogPost.Author,
                Content = UpdatedBlogPost.Content,
                IsVisible = UpdatedBlogPost.IsVisible,
                UrlHandle = UpdatedBlogPost.UrlHandle,
                PublishedDate = UpdatedBlogPost.PublishedDate,
                FeaturedImageUrl = UpdatedBlogPost.FeaturedImageUrl,
                Categories = UpdatedBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.DeleteAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var blogPostDto = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl
            };
            return Ok(blogPostDto);
        }

        [HttpGet]
        [Route("blog/{url}")]
        public async Task<IActionResult> GetBlogPostByUrl([FromRoute] string url)
        {
            var blogPost = await _blogPostRepository.GetByUrlAsync(url);
            if (blogPost == null)
            {
                return NotFound();
            }
            var blogPostDto = new BlogPostDto 
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(blogPostDto);
        }
    }
}
