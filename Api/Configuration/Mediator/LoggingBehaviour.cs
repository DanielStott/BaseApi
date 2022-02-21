﻿using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Shared.Attributes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Configuration.Mediator;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Handling {NameOfRequest}");
        var myType = request.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

        foreach (var prop in props)
        {
            if (prop.GetCustomAttribute<DontLogAttribute>() != null)
            {
                _logger.LogInformation("{Property} : {@Value}", prop.Name, "[NotLogged]");
                continue;
            }

            _logger.LogInformation("{Property} : {@Value}", prop.Name, prop.GetValue(request, null));
        }

        return await next();
    }
}