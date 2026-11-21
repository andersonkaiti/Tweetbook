namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class DotEnvExtension
{
  public static IConfigurationManager LoadDotEnv(
    this IConfigurationManager configuration
  )
  {
    DotNetEnv.Env.Load();

    configuration.AddEnvironmentVariables();

    return configuration;
  }
}

public static class Env
{
  public static string ConnectionString =>
    DotNetEnv.Env.GetString("ConnectionStrings__Default");
  public static string JwtSecret =>
    DotNetEnv.Env.GetString("JwtSecret");
}
