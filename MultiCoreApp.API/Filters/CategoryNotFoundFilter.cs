using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;

namespace MultiCoreApp.API.Filters
{
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        private readonly ICategoryService _category;

        public CategoryNotFoundFilter(ICategoryService category)
        {
            _category = category;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guid id = (Guid) context.ActionArguments.Values.FirstOrDefault()!;

            var cat = await _category.GetByIdAsync(id);
            if (cat != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;//NotFound Hata kodu.
                errorDto.Errors.Add($"Id'si {id} olan kategori bulunamadı");
                context.Result = new NotFoundObjectResult(errorDto);
            }

        }
    }
}
