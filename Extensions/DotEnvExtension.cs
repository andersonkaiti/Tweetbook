namespace Tweetbook.Extensions;

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