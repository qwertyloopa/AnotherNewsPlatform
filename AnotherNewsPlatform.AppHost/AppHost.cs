var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AnotherNewsPlatform_Mvc>("anothernewsplatform-mvc");

builder.Build().Run();
