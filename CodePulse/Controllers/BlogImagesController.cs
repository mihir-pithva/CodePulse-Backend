using CodePulse.Models.Domain;
using CodePulse.Models.DTOs;
using CodePulse.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImagesController : ControllerBase
    {
        private readonly IBlogImagesRepository _blogImagesRepository;

        public BlogImagesController(IBlogImagesRepository blogImagesRepository)
        {
            this._blogImagesRepository = blogImagesRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadBlogImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string title) 
        {
            ValidateFileUpload(file);

            if(ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now, 
                };

                var blogImgDomainModel = await _blogImagesRepository.Upload(file, blogImage);

                var response = new BlogImageDto
                {
                    Id = blogImgDomainModel.Id,
                    FileName = blogImgDomainModel.FileName,
                    Title = blogImgDomainModel.Title,
                    FileExtension = blogImgDomainModel.FileExtension,
                    DateCreated = blogImgDomainModel.DateCreated,
                    Url = blogImgDomainModel.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogImages()
        {
            var blogImagesDomain = await _blogImagesRepository.GetAllAsync();
            var response = new List<BlogImageDto>();
            foreach (var blogImage in blogImagesDomain)
            {
                response.Add(new BlogImageDto
                {
                    Id=blogImage.Id,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url,
                    FileExtension =blogImage.FileExtension,
                });
            }
            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file","Unsupported File Formate");
            }
            if(file.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File must be less than 10MB");
            }
        }
    }
}
