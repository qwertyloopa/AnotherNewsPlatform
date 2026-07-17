var builder = DistributedApplication.CreateBuilder(args);


builder.AddViteApp(name:"frontend", appDirectory:"../"); // дописать подключение ангуляра к аспайру
builder.AddProject<Projects.AnotherNewsPlatform_MVC>("anothernewsplatform-mvc");
builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api");

builder.Build().Run();

