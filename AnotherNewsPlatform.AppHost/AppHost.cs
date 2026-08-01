var builder = DistributedApplication.CreateBuilder(args);


var frontend = builder.AddViteApp(name:"frontend", appDirectory:"anp-client"); // дописать подключение ангуляра к аспайру
builder.AddProject<Projects.AnotherNewsPlatform_MVC>("anothernewsplatform-mvc");
builder.AddProject<Projects.AnotherNewsPlatform_WebApi>("web-api");

builder.Build().Run();

