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

            categoryDomainModel =  await _categoryRepository.CreateAsync(categoryDomainModel);

            //Map Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Name = categoryDomainModel.Name,
                UrlHandle = categoryDomainModel.UrlHandle,
            };
            return Ok(categoryDto);
        }
    }
}
