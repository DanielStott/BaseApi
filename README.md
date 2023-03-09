# BaseApi
BaseApi is a .NET Core 7.0 Web API project that uses Docker containers for development and deployment. It is a starting point for building a new API project. 
The project is built using the following technologies:
* MediatR for CQRS
* FluentValidation for validation
* AutoMapper for mapping
* Entity Framework Core for data access
* Serilog for logging
* Swagger for API documentation
* WebApplicationFactory and NUnit for integration testing
* DbUp for database migrations
* Docker for containerization


## Installation
### Requirements
* Docker Desktop for Windows or Mac.
* .NET Core 7.0 SDK for your platform.
### Setup Docker Containers
```bash
docker-compose up -d 
```
### Setup Database
Run the Database project inside your IDE or using the following command:
```bash
dotnet run --project Database
```
### Setup Development Certificate
```bash
dotnet dev-certs https --trust
```


## Running
### Api Project
Run the Api project inside your IDE or using the following command:
```bash
dotnet run --project Api 
```
### Postman Collection
A Postman collection is available in the `script` directory. This collection contains some useful requests for testing the API.

## Testing
Contains integration test framework using WebApplicationFactory and NUnit.
### Integration Tests
Run the IntegrationTests project inside your IDE or using the following command:
```bash 
dotnet test
```