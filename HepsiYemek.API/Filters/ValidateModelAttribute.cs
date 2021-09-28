using HepsiYemek.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Filters
{


    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                  .Where(a => a.Value.Errors.Count > 0)
                  .SelectMany(x => x.Value.Errors)
                  .ToList();
                var result = new ServiceResponse(new ErrorDetail { ErrorCode = 400, Message = string.Join(" , ", errors.Select(x=>x.ErrorMessage).ToList()) });
                context.Result = new BadRequestObjectResult(result);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }

}
