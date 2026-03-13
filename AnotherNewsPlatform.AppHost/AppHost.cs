var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AnotherNewsPlatform_App>("anothernewsplatform-app");

builder.Build().Run();
