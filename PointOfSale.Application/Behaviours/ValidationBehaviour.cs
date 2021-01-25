using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = PointOfSale.Application.Exceptions.ValidationException;

namespace PointOfSale.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = GetRequestValidationErrors(context);

            if (failures.Count > 0)
            {
                var stringFailures = failures.Select(failure => failure.ToString()).ToArray();

                throw new ValidationException(string.Join(" ", stringFailures));
            }

            return await next();
        }

        private List<ValidationFailure> GetRequestValidationErrors(IValidationContext context)
        {
            var failures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            return failures;
        }
    }
}