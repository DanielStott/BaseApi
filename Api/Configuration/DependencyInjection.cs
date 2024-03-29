﻿using Api.Configuration.Mediator;
using Data;
using Data.Employees;
using Data.Users;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using Domain.Shared.Interfaces;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

namespace Api.Configuration;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddMongoDbDependencies(services, configuration);

        services.TryAddScoped<IContext<User>, UserContext>();
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<IEmployeeRepository, EmployeeRepository>();
        services.TryAddScoped<LinkGenerator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    }

    private static void AddMongoDbDependencies(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.TryAddSingleton<IMongoDatabase>(_ => new MongoDatabaseFactory(configuration.GetConnectionString("MongoDb"), "BaseApi").CreateDatabase());
        services.TryAddTransient<IMongoStore<Employee>, MongoStore<Employee>>();
    }
}