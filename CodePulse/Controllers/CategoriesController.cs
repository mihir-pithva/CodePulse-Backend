using CodePulse.API.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTOs;
using CodePulse.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            //Map DTO to domain Model
            var categoryDomainModel = new Category
            {
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            categoryDomainModel = await _categoryRepository.CreateAsync(categoryDomainModel);

            //Map Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Name = categoryDomainModel.Name,
                UrlHandle = categoryDomainModel.UrlHandle,
            };
            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoris()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();

            foreach (var category in categories) {
                response.Add(new CategoryDto {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            //Map model to DTO
            var categoryDto = new CategoryDto { 
                Id = category.Id, 
                Name = category.Name, 
                UrlHandle = category.UrlHandle 
            };
            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category == null)
            {
                return BadRequest();
            }
                return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategory) 
        {
            //Map DTO to Model
            var category = new Category
            {
                Name = updateCategory.Name,
                UrlHandle = updateCategory.UrlHandle,
            };

            category = await _categoryRepository.UpdateAsync(id, category);

            //Map Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle, 
            };

            return Ok(categoryDto);
        }
    }
}
