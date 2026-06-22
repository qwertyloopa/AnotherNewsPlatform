var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.AnotherNewsPlatform_MVC>("anothernewsplatform-mvc");
builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api");
//builder.AddProject<Projects.AnotherNewsPlatform_BlazorApp>("Blazor");

builder.Build().Run();

