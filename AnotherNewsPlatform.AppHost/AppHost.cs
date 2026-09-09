var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose");

var postgres = builder.AddPostgres("postgres")
    .WithDbGate()
    .WithDataVolume()
    .WithHostPort(5432)
    .PublishAsDockerComposeService((resource, service) => service.Name = "postgres"); // надо рассмотреть возможность подключения и работы через builder.AddNpsqlDbContext
var ollama = builder.AddOllama("ollama", 11343).WithGPUSupport();

var database = postgres.AddDatabase("AnotherNewsPlatformDb");
var gemma3 = ollama.AddModel("gemma3:270m"); // модель подключена, осталось дописать логику работы с ней и всё

//когда надо будет ставить всю эту байду заново (или для применения миграций), то строку ниже надо расскомментировать
var migrations = builder.AddProject<Projects.AnotherNewsPlatform_MigrationService>("migrations").WithReference(database).WaitFor(database);

var api = builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api")
    .WithReference(database)
    .WithReference(gemma3)
    .WaitFor(database)
    .WaitFor(gemma3)
    .PublishAsDockerComposeService((resource, service)=>
    {
        service.Image = "anothernewsplatform/web-api:latest";
        service.Name = "api";
    });

builder.AddViteApp(name:"client", appDirectory:"../anp-client", runScriptName: "start")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints()
    .PublishAsDockerComposeService((resource, service)=>
    {
        service.Image = "anothernewsplatform/frontend:latest";
        service.Name = "frontend";
    });


builder.Build().Run();