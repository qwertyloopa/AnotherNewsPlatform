var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres").WithDbGate().WithDataVolume();
var ollama = builder.AddOllama("ollama");

var database = postgres.AddDatabase("AnotherNewsPlatformDb");
var gemma3 = ollama.AddModel("gemma3:270m").WithGp;

//когда надо будет ставить всю эту байду заново, то строку ниже надо расскомментировать
//var migrations = builder.AddProject<Projects.AnotherNewsPlatform_MigrationService>("migrations").WithReference(database).WaitFor(database);

var api = builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api").WithReference(database).WithReference(gemma3).WaitFor(database);

builder.AddViteApp(name:"frontend", appDirectory:"../anp-client", runScriptName: "start")
    .WaitFor(api)
    .WithExternalHttpEndpoints(); // дописать подключение ангуляра к аспайру


builder.Build().Run();