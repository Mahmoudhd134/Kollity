﻿using API.Extensions;
using Domain.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers;

public class CustomValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context.ModelState.Keys
            .SelectMany(k => context.ModelState[k]!.Errors
                .Select(e => Error.Validation(k, e.ErrorMessage)))
            .ToList();

        var validationResult = Result.Failure(errors);

        context.Result = new ObjectResult(validationResult.ToFailureType());
    }
}