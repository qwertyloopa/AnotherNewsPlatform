var builder = DistributedApplication.CreateBuilder(args);

var compose = builder.AddDockerComposeEnvironment("compose");

var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume().WithHostPort(5432); // надо рассмотреть возможность подключения и работы через builder.AddNpsqlDbContext
var ollama = builder.AddOllama("ollama").WithGPUSupport();

var database = postgres.AddDatabase("AnotherNewsPlatformDb");
var gemma3 = ollama.AddModel("gemma3:270m"); // модель подключена, осталось дописать логику работы с ней и всё

//когда надо будет ставить всю эту байду заново, то строку ниже надо расскомментировать
//var migrations = builder.AddProject<Projects.AnotherNewsPlatform_MigrationService>("migrations").WithReference(database).WaitFor(database);

var api = builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api")
    .WithReference(database)
    .WithReference(gemma3)
    .WaitFor(database)
    .WaitFor(gemma3);

builder.AddViteApp(name:"frontend", appDirectory:"../anp-client", runScriptName: "start")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();


builder.Build().Run();