var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AnotherNewsPlatform_MVC>("anothernewsplatform-mvc");

builder.Build().Run();

