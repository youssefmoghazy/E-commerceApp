using Azure;
using Microsoft.AspNetCore.Mvc;
using Shared.SharedTransferObjects.ErrorModels;

namespace E_commerce.Factories
{
    public class APIResponseFactory
    {
        public static IActionResult GenerateAPIValidationResponse(ActionContext Context)
        {
            var errors = Context.ModelState.Where(m => m.Value.Errors.Any())
            .Select(m => new ValidationErorrs
            {
                Field = m.Key,
                Errors = m.Value.Errors.Select(e => e.ErrorMessage)
            }
            );
            var response = new ValidationErrorResponce { Erorrs = errors };
            return new BadRequestObjectResult(response);
        }
    }
}
