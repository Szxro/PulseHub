using FluentValidation.Results;
using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.SharedKernel;

namespace PulseHub.Application.Common.Utilities;

public static class ErrorHelpers
{
    public static Dictionary<string, List<ErrorResponse>> GroupErrorsByPropertyName(Error[] errors)
    {
        // Grouping the errors by property name 
        return errors
               .GroupBy(x => x.PropertyName)
               .ToDictionary(
                   property => property.Key,
                   property => property
                       .Select(failure => new ErrorResponse
                       {
                           Code = failure.ErrorCode,
                           Message = failure.Description
                       }).ToList());
    }

    public static Error CreateErrorFromValidationFailure(ValidationFailure validationFailure)
        => Error.Validation(validationFailure.PropertyName,validationFailure.ErrorCode,validationFailure.ErrorMessage);
}