var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres").WithDbGate().WithDataVolume();
var database = postgres.AddDatabase("AnotherNewsPlatformDb");

var migrations = builder.AddProject<Projects.AnotherNewsPlatform_MigrationService>("migrations").WithReference(database).WaitFor(database);

var api = builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api").WithReference(database).WaitFor(database);

builder.AddViteApp(name:"frontend", appDirectory:"../anp-client", runScriptName: "start")
    .WaitFor(api)
    .WithExternalHttpEndpoints(); // дописать подключение ангуляра к аспайру


builder.Build().Run();

