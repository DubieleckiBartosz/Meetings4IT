using FluentValidation;
using MediatR;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Tools;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger _logger;

    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger loggerManager)
    {
        this._validators = validators ?? throw new ArgumentNullException(nameof(validators));
        this._logger = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandType = request.GetType().FullName;
        this._logger.Information($"----- Validating command {commandType} --------");

        var errorList = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errorList.Any())
        {
            var message = new
            {
                Message = "Validation errors",
                Command = commandType,
                Errors = errorList
            };

            this._logger.Warning(message.Serialize());

            throw new ValidationErrorListException(errorList.Select(_ => _.ErrorMessage));
        }


        return await next();
    }
}