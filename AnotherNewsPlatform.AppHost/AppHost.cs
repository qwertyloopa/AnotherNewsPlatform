var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AnotherNewsPlatform_MVC>("anothernewsplatform-mvc");
builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api");

builder.Build().Run();

