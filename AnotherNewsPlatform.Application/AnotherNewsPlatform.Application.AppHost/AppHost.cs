var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AnotherNewsPlatform_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AnotherNewsPlatform_Application_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
