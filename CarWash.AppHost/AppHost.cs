var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var carwashDb = postgres.AddDatabase("carwash");

builder.AddProject<Projects.CarWash_Api>("api")
    .WithReference(carwashDb)
    .WaitFor(carwashDb)
    .WithHttpHealthCheck("/health");

builder.Build().Run();